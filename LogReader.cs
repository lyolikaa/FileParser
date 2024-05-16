using System.Globalization;
using System.Runtime.InteropServices.JavaScript;
using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json.Linq;

namespace FileParser;

public class LogReader
{
    public static IEnumerable<string> GetColumns(string filename)
    {
        CsvConfiguration csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture);
        using TextReader textReader = new StreamReader(filename);
        using CsvReader csvReader = new CsvReader(textReader, csvConfig);
        csvReader.Read();
        csvReader.ReadHeader();
        return csvReader.HeaderRecord;
    }
    
    public static IEnumerable<JObject> GetData(string filename, IEnumerable<string> columns)
    {
        CsvConfiguration csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture);
        using TextReader textReader = new StreamReader(filename);
        using CsvReader csvReader = new CsvReader(textReader, csvConfig);
        csvReader.Read();
        csvReader.ReadHeader();
        while (csvReader.Read())
        {
            JObject row = new( );
            foreach (var col in columns)
            {
                row.Add(new JProperty(col, csvReader.GetField<string>(col)));
            }

            yield return row;
        }
    }

    public static IEnumerable<string> GetFileNames()
    {
        return Directory.GetFiles(@".\files", "*.csv");
    }

}