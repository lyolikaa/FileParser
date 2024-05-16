using FileParser;

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




Console.WriteLine($"OK");
    void WriteError(string message)
    {
        Console.WriteLine($"{message}");
        Environment.Exit(0);
    }
