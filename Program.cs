using FileParser;

var input = Console.ReadLine();
Console.WriteLine($"query: {input}");
//validate input, parse to conditions
var parser = new InputParser(input);
if (string.IsNullOrWhiteSpace(input) || !parser.ConditionsStrings.Any())
    WriteError("invalid input");


//read files columns
//validate columns



Console.WriteLine($"OK");
    void WriteError(string message)
    {
        Console.WriteLine($"{message}");
        Environment.Exit(0);
    }
