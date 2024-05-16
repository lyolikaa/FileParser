using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

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

    public static IEnumerable<string> GetFileNames()
    {
        return Directory.GetFiles(@".\files", "*.csv");
    }

}