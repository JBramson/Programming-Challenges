/*
 * https://adventofcode.com/2021/day/3
 * Objective: Given a series of binary numbers, count which bit is more popular in each place.
 * Part of me learning C#.
 */
namespace AdventOfCode2021;

public class Day3
{
	static void OldMain()
	{
		IEnumerable<string> numbers = System.IO.File.ReadLines("/home/jack/Dev/Programming-Challenges/AdventOfCode2021/AdventOfCode2021/input.txt");
		int numberLength = numbers.ElementAt(0).Length;
		int gamma, epsilon;
		int[] bitCounts = new int[numberLength]; // Tracks 1s minus 0s in each position (positive = more 1s, negative = more 0s)
		string gammaStr = "", epsilonStr = "";

		foreach (string number in numbers)
		{
			for (int i = 0; i < numberLength; i++)
			{
				if (number[i] == '1')
				{
					bitCounts[i] += 1;
				}
				else
				{
					bitCounts[i] -= 1;
				}
			}
		}
		// Rates to gamma and decimal
		foreach (int bitCount in bitCounts)
		{
			if (bitCount > 0)
			{
				gammaStr += '1';
				epsilonStr += '0';
			}
			else
			{
				gammaStr += '0';
				epsilonStr += '1';
			}
		}

		gamma = Convert.ToInt32(gammaStr, 2);
		epsilon = Convert.ToInt32(epsilonStr, 2);
		
		Console.WriteLine(gamma * epsilon);
	}

}