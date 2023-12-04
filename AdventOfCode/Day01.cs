using System.ComponentModel;
using System.Runtime.ExceptionServices;
using System.Text.RegularExpressions;
namespace AdventOfCode;

public partial class Day01 : BaseDay
{
    private readonly string _input;
    List<string> numbers = new() { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

	public Day01()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1() => new($"Solution to {ClassPrefix} {solvePartOne(_input)}, part 1");

    public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {solvePartTwo(_input)}, part 2");

    private int solvePartOne(string input)
    {
        int result = 0;
        var parsedInput = parseLines(input);
		Regex regex = PartOneRegex();

		foreach (var line in parsedInput)
        {
			var matches = regex.Matches(line);

			var first = matches.First().Value.First().ToString();
            var last = matches.Last().Value.Last().ToString();
            result += int.Parse(first + last);
		}

		return result;
    }

    private int solvePartTwo(string input)
    {
		var parsedInput = parseLines(input);
        int sum = 0;

		foreach (var line in parsedInput)
        {
            var first = GetNumberFromMatch(PartTwoRegex().Matches(line).First()) * 10;
            var lastMatch = PartTwoRegex().Matches(line).Last();
			var last = GetNumberFromMatch(lastMatch, false);

            var tmp = PartTwoRegex().Match(line, lastMatch.Index + 1);
            if (tmp.Success)
            {
			    int x = numbers.IndexOf(tmp.Value);
                if (x != -1)
                {
                    last = x;
                }
		    }

            sum += first + last;
        }

        return sum;
	}

    private int GetNumberFromMatch(Match match, bool first = true)
    {

        int result = 0;
        var tmp = numbers.IndexOf(match.Value);
		if (tmp == -1)
        {
			if (first)
			{
				result = int.Parse(match.Value.First().ToString());
			}
			else
			{
				result = int.Parse(match.Value.Last().ToString());
			}
		} else
        {
            result = tmp;
        }

        return result;
    }

    private string[] parseLines(string input)
    {
        return input.Split("\r\n");
    }

	[GeneratedRegex(@"\d+")]

	private static partial Regex PartOneRegex();

	[GeneratedRegex(@"(\d+)|one|two|three|four|five|six|seven|eight|nine")]


	private static partial Regex PartTwoRegex();
}
