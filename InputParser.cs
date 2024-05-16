using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

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

    private IEnumerable<string> _conditions;
    public IEnumerable<string> ConditionsStrings => _conditions;
    public IEnumerable<string> ConditionsColumns => 
        _conditions.Select(c=>c.Split(_equalSign)[0]);

    public IEnumerable<Func<JObject, bool>> SearchConditions(IEnumerable<string> columns)
    {
        var conditions = _conditions
            .Select(c => (c.Split(_equalSign)[0], c.Split(_equalSign)[1]));
        return conditions.Where(kvp => columns.Any(c => c == kvp.Item1))
            .Select(c=> GetWhereCondition(c.Item1, c.Item2));
    }
    public Func<JObject, bool> GetWhereCondition(string key, string value) 
        => r => r.ContainsKey(key) && r.GetValue(key).Value<string>().Contains(value);
}