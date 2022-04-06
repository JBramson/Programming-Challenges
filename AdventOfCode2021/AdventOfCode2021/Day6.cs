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
	static List<Int16> timers = new List<short>();
	
	static void AddNewFish(int numToAdd)
	{
		for (int i = 0; i < numToAdd; i++)
		{
			timers.Add(8);
		}
	}
	// TODO: Change storage from raw number list to array where positions represent the current number of each number
	// TODO: e.g.: (0:1, 1:3, 2:2) could mean: {0, 1, 1, 1, 2, 2}
	static void Main()
	{
		string initialLineStr =
			File.ReadAllText("/home/jack/Dev/Programming-Challenges/AdventOfCode2021/AdventOfCode2021/input.txt");
		// Change numberOfDays to test each part.
		const int numberOfDays = 256;
		int currentReproductionCount = 0;

		foreach (string initialNumberStr in initialLineStr.Split(","))
		{
			timers.Add(Convert.ToInt16(initialNumberStr));
		}

		for (int i = 0; i < numberOfDays; i++)
		{
			for (int j = 0; j < timers.Count; j++)
			{
				if (timers[j] == 0)
				{
					currentReproductionCount++;
					timers[j] = 6;
				}
				else
				{
					timers[j]--;
				}
			}
			AddNewFish(currentReproductionCount);
			currentReproductionCount = 0;
			Console.WriteLine(i);
		}
		
		Console.WriteLine(timers.Count);
	}
}