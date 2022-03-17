/*
 * https://adventofcode.com/2021/day/1
 * Objective: Given a list of ints, count the number of values greater than their predecessor.
 * Part of me learning C#.
 */
namespace AdventOfCode2021;
public class Day1
{
	static void Old_Main(string[] args)
	{
		
		IEnumerable<string> numberStrings = System.IO.File.ReadLines("/home/jack/Dev/Programming-Challenges/AdventOfCode2021/AdventOfCode2021/input.txt");
		List<int> depths = new List<int>();
		int increases = 0;
    
		// Cast strings to ints
		foreach (var numberStr in numberStrings)
		{
			depths.Add(Convert.ToInt16(numberStr));
		}
		// Check vals
		for (int i = 1; i < depths.Count; i++)
		{
			if (depths[i] > depths[i - 1])
			{
				increases++;
			}
		}
    
		Console.WriteLine(increases);
	}
}

