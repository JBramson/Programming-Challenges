/*
 * https://adventofcode.com/2021/day/1#part2
 * Objective: Given a list of ints, count the number of three successive values summed
 * that are greater than their predecessor.
 * Part of me learning C#.
 */
namespace AdventOfCode2021;

public class Day1Part2
{
	static void Old_Main(string[] args)
	{
		IEnumerable<string> numberStrings = System.IO.File.ReadLines("/home/jack/Dev/Programming-Challenges/AdventOfCode2021/AdventOfCode2021/input.txt");
		List<int> depths = new List<int>();
		List<int> depth_trips = new List<int>();
		int increases = 0;
    
		// Cast strings to ints
		foreach (var numberStr in numberStrings)
		{
			depths.Add(Convert.ToInt16(numberStr));
		}

		// Group by threes
		for (int i = 0; i < depths.Count - 2; i++)
		{
			depth_trips.Add(depths[i] + depths[i + 1] + depths[i + 2]);
		}
		
		// Check vals
		for (int i = 1; i < depth_trips.Count; i++)
		{
			if (depth_trips[i] > depth_trips[i - 1])
			{
				increases++;
			}
		}
		
		Console.WriteLine(increases);
	}
}