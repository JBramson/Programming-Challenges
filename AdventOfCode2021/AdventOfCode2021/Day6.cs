/*
 * https://adventofcode.com/2021/day/6
 * Objective: Given an initial state of lanternfish reproduction timers, track their reproduction over time.
 * Part 1: How many will there be after 80 days?
 * Part 2: How many will there be after 256 days?
 * Part of me learning C#.
 */

namespace AdventOfCode2021;

public class Day6
{
	static void Main()
	{
		string initialLineStr =
			File.ReadAllText("/home/jack/Dev/Programming-Challenges/AdventOfCode2021/AdventOfCode2021/input.txt");
		// Change numberOfDays to test each part.
		const int numberOfDays = 256;
		double[] counts = new double[9];
		double[] newCounts = new double[9];
		double totalFish = 0;


		foreach (string initialNumberStr in initialLineStr.Split(","))
		{
			counts[Convert.ToInt16(initialNumberStr)]++;
		}

		for (int i = 0; i < numberOfDays; i++)
		{
			Array.Clear(newCounts); // Reset all new values to 0
			for (int j = 0; j < 9; j++)
			{
				if (j == 0)
				{
					newCounts[8] = counts[0];
					newCounts[6] = counts[0];
				}
				else
				{
					newCounts[j - 1] += counts[j];
				}
			}
			counts = newCounts.Clone() as double[];
		}

		foreach (double count in counts)
		{
			totalFish += count;
		}
		Console.WriteLine(totalFish);
	}
}