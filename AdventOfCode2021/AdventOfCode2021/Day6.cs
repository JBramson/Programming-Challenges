/*
 * https://adventofcode.com/2021/day/6
 * Objective: Given an initial state of lanternfish reproduction timers, track their reproduction over time.
 * Part 1: 
 * Part 2: 
 * Part of me learning C#.
 */

namespace AdventOfCode2021;

public class Day6
{
	static void Main()
	{
		string initialLineStr =
			File.ReadAllText("/home/jack/Dev/Programming-Challenges/AdventOfCode2021/AdventOfCode2021/input.txt");
		List<Int16> timers = new List<short>();

		foreach (string initialNumberStr in initialLineStr.Split(","))
		{
			timers.Add(Convert.ToInt16(initialNumberStr));
		}
		
		Console.WriteLine(timers);
	}
}