using System.Text.RegularExpressions;

namespace FileParser;

public class InputParser
{
    private readonly string _colName = "col_name";
    private readonly string _equalSign = "=";
    private readonly string _andCondition = "&&";
    private readonly string _orCondition = "||";

    public InputParser(string input)
    {
        //TODO && or ||
        //TODO use both && and ||
        _conditions = input.Split(_andCondition)
            .Where(ValidateCondition);

    }

    public bool ContainsEqual(string condition)
        => Regex.Matches(condition, _equalSign).Count == 1;
    

    public bool ValidateCondition(string condition)
    {
        return condition.Split(_equalSign).Length == 2
            && condition.Split(_equalSign).All(p => p.Length > 0);
    }

    public Func<string, bool> GetWhereCondition(string condition)
    {
        return null;
    }

    private IEnumerable<string> _conditions;
    public IEnumerable<string> ConditionsStrings => _conditions;
    public IEnumerable<string> ConditionsColumns => 
        _conditions.Select(c=>c.Split(_equalSign)[0]);
}