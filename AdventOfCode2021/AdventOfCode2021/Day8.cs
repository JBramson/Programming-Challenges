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

	static int Part1Solution(string inputString)
	{
		int matches = 0;
		
		string[] lineArray = new string[2];

		foreach (string line in inputString.Split("\n"))
		{
			lineArray = line.Split("|");
			foreach (string output in lineArray[1].Split(" "))
			{
				if (output.Length == 2 || output.Length == 3 || output.Length == 4 || output.Length == 7)
				{
					matches++;
				}
			}
		}
		return matches;
	}

	static int Part2Solution(string inputString)
	{
		int score = 0;
		
		string[] lineArray = new string[2];
		string[] inputArray = new string[10];
		string[] outputArray = new string[4];
		string[] numberCodes = new string[10];
		char[] locationCodes = new char[7]; // This is ordered as if reading a book- 0 is top, 1 is top-left, 3 is center, and 6 is bottom.

		foreach (string line in inputString.Split("\n"))
		{
			lineArray = line.Split("|");
			inputArray = lineArray[0].Split(" ");
			outputArray = lineArray[1].Split(" ");
		}
		// Set the strings we know first (1, 7, 4, and 8)
		foreach (string input in inputArray)
		{
			switch (input.Length)
			{
				case 2:
					numberCodes[1] = input;
					break;
				case 3:
					numberCodes[7] = input;
					break;
				case 4:
					numberCodes[4] = input;
					break;
				case 7:
					numberCodes[8] = input;
					break;
			}
		}
		/* Now, we find the wires. */
		// Top wire
		locationCodes[0] = FindMissingChar(numberCodes[1], numberCodes[7]);
		// Bottom wire (9 has this + the top)
		// locationCodes[3] = FindMissingChar(numberCodes[4] + locationCodes[0], numberCodes[9]);
		
		foreach (char locationCode in locationCodes)
		{
			Console.WriteLine(locationCode);
		}
		return score;
	}
	
	/*
	* Ideas for  part 2:
	* The 0 entry will be missing a wire- this is the center.
	* The 7 entry will have one wire the 1 entry does not- this is the top.
	* The 9 entry will have two wires the 4 does not- one is the bottom, and the other is the top (already found).
	*	* 9 will also lack a wire- this is the bottom-left.
	* The 6 entry will lack a wire- this is the top-right.
	* The 1 entry will contain a wire that 6 does not- this is the bottom-right.
	* The only wire we haven't found is the top-left.
	*/

	// Find the singular character that is missing from shortString, but not longString.
	// Flip if necessary.
	static char FindMissingChar(string shortString, string longString)
	{
		if (shortString.Length + 1 != longString.Length)
		{
			string tempStr = longString;
			longString = shortString;
			shortString = tempStr;

			if (shortString.Length + 1 != longString.Length)
			{
				throw new FormatException($"{shortString} is not 1 char shorter than {longString}.");
			}
		}
		foreach (char c in longString)
		{
			if (!shortString.Contains(c))
			{
				return c;
			}
		}
		throw new Exception("IDK what happened here- we didn't find a missing char.");
	}

	// Determine if two strings contain the same characters.
	static bool DoesStringMatch(string firstStr, string secondStr)
	{
		if (firstStr.Length != secondStr.Length) return false;

		foreach (char firstChar in firstStr)
		{
			if (!secondStr.Contains(firstChar))
			{
				return false;
			}
		}
		return true;
	}
	
	static void Main()
	{
		string inputString =
			File.ReadAllText("/home/jack/Dev/Programming-Challenges/AdventOfCode2021/AdventOfCode2021/input.txt");
		
		// List<string> inputs = new List<string>();
		// List<string> outputs = new List<string>();
		
		
		Console.WriteLine(Part2Solution(inputString));
	}
}