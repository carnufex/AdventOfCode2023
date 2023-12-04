using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
	class Game
	{
		private List<int> red = new();
		private List<int> green = new();
		private List<int> blue = new();

		public int GameSum { get; private set; } = 0;

		public Game(string line)
        {
			parseLine(line);
		}

		public (int, int, int) FindMaxVals()
		{
			return (red.Max(), green.Max(), blue.Max());
		}

		public bool Validate()
		{
			bool isValid = true;
			isValid &= red.All(x => x <= 12);
			isValid &= green.All(x => x <= 13);
			isValid &= blue.All(x => x <= 14);

			return isValid;
		}

		private void parseLine(string input)
		{
			var gameScoreSplit = input.Split(": ");
			var gameSplit = gameScoreSplit[1].Split("; ");
			foreach (var game in gameSplit)
			{
				var splitLines = game.Split(", ");
				foreach (var line in splitLines)
				{
					var colorAmount = line.Split(" ");
					switch (colorAmount[1])
					{
						case "red":
							red.Add(int.Parse(colorAmount[0]));
							break;
						case "green":
							green.Add(int.Parse(colorAmount[0]));
							break;
						case "blue":
							blue.Add(int.Parse(colorAmount[0]));
							break;
						default: 
							break;
					}
				}
			}
		}
	}

	internal class Day02 : BaseDay
	{
		private readonly string _input;

		public Day02()
		{
			_input = File.ReadAllText(InputFilePath);
		}

		public override ValueTask<string> Solve_1() => new($"Solution to {ClassPrefix} {solvePartOne(_input)}, part 1");

		public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {solvePartTwo(_input)}, part 2");

		private object solvePartOne(string input)
		{
			int result = 0;
			var lines = input.Split("\r\n");

			for (int i = 0; i < lines.Length; i++)
			{
				result += new Game(lines[i]).Validate() ? i+1 : 0;
			}

			return result;
		}

		private object solvePartTwo(string input)
		{
			int result = 0;
			foreach (var line in input.Split("\r\n"))
			{
				var tmp = new Game(line).FindMaxVals();
				result += tmp.Item1 * tmp.Item2 * tmp.Item3;
			}

			return result;
		}
	}
}
