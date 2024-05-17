using System.Runtime.InteropServices.JavaScript;
using FileParser;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

Console.WriteLine($"start");

//---
var input = Console.ReadLine();
Console.WriteLine($"query: {input}");
//validate input, parse to conditions
var parser = new InputParser(input);
if (string.IsNullOrWhiteSpace(input) || !parser.ConditionsStrings.Any())
    WriteError("invalid input");

//read files columns
var filenames = LogReader.GetFileNames();
if (!filenames.Any())
    WriteError("no csv files");

//validate columns
//TODO suppose we have same file structure
var columns = LogReader.GetColumns(filenames.FirstOrDefault());
var existingColumns = parser.ConditionsColumns.Intersect(columns);
var absentColumns = parser.ConditionsColumns.Except(existingColumns);
Console.WriteLine($"column(s) not found: {string.Join(";", absentColumns)}");
Console.WriteLine($"column(s) to search: {string.Join(";", existingColumns)}");

//get data
var data = LogReader.GetData(filenames.FirstOrDefault(), columns);
var searchConditions = parser.SearchConditions(existingColumns);
foreach (var search in searchConditions)
{
    data = data.Where(search);
}

JObject logResults = new();
logResults.Add("datetime", DateTime.Now);
logResults.Add("searchQuery", input);
logResults.Add("logsCount", data.Count());
logResults.Add("result", new JArray(data));
var jsonString = JsonConvert.SerializeObject(
    logResults, Formatting.Indented,
    new JsonConverter[] {new StringEnumConverter()});
Console.WriteLine(jsonString);

//save to sqlite database
new DB().Save(logResults);

Console.WriteLine($"OK");
    void WriteError(string message)
    {
        Console.WriteLine($"{message}");
        Environment.Exit(0);
    }
