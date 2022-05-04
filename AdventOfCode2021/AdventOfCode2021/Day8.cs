/*
 * https://adventofcode.com/2021/day/8
 * Objective: Given a series of 10 attempts at numbers and 4 final numbers,
 * Part 1: Find how many instances of (1, 4, 7, and 8) are in the final ones.
 * Part 2: Decode all the output values and sum them.
 * Part of me learning C#.
 */

namespace AdventOfCode2021;

public class Day8
{
	
	
	static void Main()
	{
		string inputString =
			File.ReadAllText("/home/jack/Dev/Programming-Challenges/AdventOfCode2021/AdventOfCode2021/input.txt");
		int matches = 0;
		// List<string> inputs = new List<string>();
		// List<string> outputs = new List<string>();
		string[] lineArray = new string[2];
		string[] inputArray = new string[10];
		string[] outputArray = new string[4];

		foreach (string line in inputString.Split("\n"))
		{
			lineArray = line.Split("|");
			foreach (string output in lineArray[1].Split(" "))
			{
				if (output.Length == 2 || output.Length == 3 || output.Length == 4 || output.Length == 7) {
					matches++;
				}
			}
		}

		
		
		Console.WriteLine(matches);
	}
}