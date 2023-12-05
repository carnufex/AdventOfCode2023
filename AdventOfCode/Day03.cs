using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
	public record Schematic
	{
		public Point Position;
		public int Number;
	}

	class Puzzle
	{
		private char[][] _parsed;
		public int SolvePart1 { get; private set; }
		public Puzzle(string input)
		{
			_parsed = Parse(input);
			SolvePart1 = _solvePart1();
		}

		private int _solvePart1()
		{
			List<Schematic> schematics = new List<Schematic>();
			for (int y = 0; y < _parsed.Length; y++)
			{
				char[] array = _parsed[y];

				for (int x = 0; x < array.Length; x++)
				{
					char tjar = array[x];
					if (!char.IsDigit(tjar) && tjar != '.' )
					{
						schematics = schematics.Concat(FindNeighbours(y, x)).ToList();
					}
				}
			}


			return CalculateSum(schematics);
		}

		private int CalculateSum(List<Schematic> schematics)
		{
			var uniqueRecords = schematics.Distinct(new RecordComparer()).ToList();
			return uniqueRecords.Sum(scehmatic => scehmatic.Number);
		}

		private List<Schematic> FindNeighbours(int y, int x)
		{
			List<Schematic> schematics = new();
			for (int xOffset = -1; xOffset <= 1; xOffset++)
			{
				for (int yOffset = -1; yOffset <= 1; yOffset++)
				{
					try
					{
						if (char.IsDigit(_parsed[y+yOffset][x+xOffset]))
						{
							schematics.Add(FindWholeNumber(y+yOffset, x+xOffset));
						}
					} catch (IndexOutOfRangeException)
					{
						continue;
					}
				}
			}

			return schematics;
		}

		private Schematic FindWholeNumber(int y, int x)
		{
			string number = "0";

			var chr = _parsed[y][x];
			while (char.IsDigit(chr))
			{
				x -= 1;
				if (x >= 0 && x <= _parsed.Length)
				{
					chr = _parsed[y][x];
				}
				else
				{
					break;
				}
			}
			chr = _parsed[y][++x];
			Point firstDigitPos = new(x, y);
			while (char.IsDigit(chr))
			{
				if (x >= 0 && x <= _parsed.Length)
				{
					number += chr.ToString();
					chr = _parsed[y][++x];
				}
				else
				{
					break;
				}
			}

			return new Schematic() { Number=int.Parse(number), Position=firstDigitPos};
		}

		private static char[][] Parse(string input)
		{
			var lines = input.Split("\r\n");
			char[][] array = new char[lines.Length][];

			for (int i = 0; i < lines.Length; i++)
			{
				array[i] = lines[i].ToCharArray();
			}

			return array;
		}
	}

	internal class Day03 : BaseDay
	{
		private readonly string _input;

		public Day03()
		{
			_input = File.ReadAllText(InputFilePath);
		}

		public override ValueTask<string> Solve_1() => new($"Solution to {ClassPrefix} {solvePartOne(_input)}, part 1");


		public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {solvePartTwo(_input)}, part 2");


		private int solvePartOne(string input)
		{
//			input = @"467..114..
//...*......
//..35..633.
//......#...
//617*......
//.....+.58.
//..592.....
//......755.
//...$.*....
//.664.598..";

			int result = 0;

			result = new Puzzle(input).SolvePart1;

			return result;

		}
		private object solvePartTwo(string input)
		{
			input = @"467..114..
...*......
..35..633.
......#...
617*......
.....+.58.
..592.....
......755.
...$.*....
.664.598..";

			int result = 0;


			return result;
		}
	}

	// Custom comparer for the Schematic record
	public class RecordComparer : IEqualityComparer<Schematic>
	{
		public bool Equals(Schematic x, Schematic y) => x.Position.Equals(y.Position) && x.Number == y.Number;

		public int GetHashCode(Schematic obj) => obj.Position.GetHashCode() ^ obj.Number.GetHashCode();
	}
}
