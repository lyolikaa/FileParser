using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace FileParser;

public class DB
{
    public void Save(JObject res)
    {
        using var db = new MyDbContext();
        db.LogsModel.Add(new LogsModel
        {
            searchQuery = res.GetValue("searchQuery").Value<string>(),
            datetime = res.GetValue("datetime").Value<string>(),
            logsCount = res.GetValue("logsCount").Value<string>(),
            result = res.GetValue("result").Value<JArray>().ToString()
        });
        db.SaveChanges();
    }
}

public class LogsModel
{
    [Key]
    public string datetime { get; set; }
    public string searchQuery { get; set; }
    public string logsCount { get; set; }
    public string result { get; set; }
}

public class MyDbContext : DbContext
{
    public MyDbContext()
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    public DbSet<LogsModel> LogsModel { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=parser.db;");
    }
}