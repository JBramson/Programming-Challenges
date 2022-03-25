/*
 * https://adventofcode.com/2021/day/1
 * Objective: Given a list of ints, count the number of values greater than their predecessor.
 * Part of me learning C#.
 */
namespace AdventOfCode2021;
public class Day1
{
	static void Main(string[] args)
	{
		
		// IEnumerable<string> numberStrings = System.IO.File.ReadLines("/home/jack/Dev/Programming-Challenges/AdventOfCode2021/AdventOfCode2021/input.txt");
		// List<int> depths = new List<int>();
		List<int> depths = File
			.ReadAllLines("/home/jack/Dev/Programming-Challenges/AdventOfCode2021/AdventOfCode2021/input.txt")
			.Select(Int32.Parse).ToList();
		int increases = 0;
		
		for (int i = 1; i < depths.Count; i++)
		{
			if (depths[i] > depths[i - 1])
			{
				increases++;
			}
		}
    
		Console.WriteLine(increases);
	}
	
	// This is the old way of reading in values, done piece-meal. Above is more efficient & less error-prone,
	// as it filters and sets them directly, all at once.
	static void OldAncientMain(string[] args)
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

