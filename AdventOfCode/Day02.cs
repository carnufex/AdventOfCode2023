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
		private int red = 0;
		private int green = 0;
		private int blue = 0;

		public int GameSum { get; private set; } = 0;

        public Game(string line, int game)
        {
			GameSum = parseLine(line) ? game : 0;
		}


		// Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
		private bool parseLine(string input)
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
							var RedAmount = int.Parse(colorAmount[0]);
							if (RedAmount > 12)
							{
								return false;
							}
							break;
						case "green":
							var GreenAmount = int.Parse(colorAmount[0]);
							if (GreenAmount > 13)
							{
								return false;
							}
							break;
						case "blue":
							var BlueAmount = int.Parse(colorAmount[0]);
							if (BlueAmount > 14)
							{
								return false;
							}
							break;
						default: 
							break;
					}
				}
			}

			return true;
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
//			input = @"Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
//Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
//Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
//Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
//Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green";

			int result = 0;
			var lines = input.Split("\r\n");

			List<Game> games = new();


			for (int i = 0; i < lines.Length; i++)
			{
				var game = new Game(lines[i], i + 1);
				games.Add(game);
				result += game.GameSum;
			}



			return result;
		}

		private object solvePartTwo(string input)
		{
			throw new NotImplementedException();
		}

	}
}
