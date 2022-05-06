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
			lineArray = line.Split(" | ");
			inputArray = lineArray[0].Split(" ");
			outputArray = lineArray[1].Split(" ");

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
			/* Now, we find some wires. */
			// Top wire
			locationCodes[0] = FindMissingChar(numberCodes[1], numberCodes[7]);
			// Upper right (This is the only missing wire of a 6-length that 1 has)
			// Lower right (This is the other one that 1 has)
			foreach (string input in inputArray)
			{
				if (input.Length == 6)
				{
					if (input.Contains(numberCodes[1][0]) && !input.Contains(numberCodes[1][1]))
					{
						locationCodes[2] = numberCodes[1][1];
						numberCodes[6] = input;
						locationCodes[5] = numberCodes[1][0];
					}
					else if (input.Contains(numberCodes[1][1]) && !input.Contains(numberCodes[1][0]))
					{
						locationCodes[2] = numberCodes[1][0];
						numberCodes[6] = input;
						locationCodes[5] = numberCodes[1][1];
					}
				}
			}
			/*
			* Of the fives, the one that's missing the upper-right is 5, the one that's missing
			* the bottom-right is 2, and the other is 3.
			*/
			foreach (string input in inputArray)
			{
				if (input.Length == 5)
				{
					if (!input.Contains(locationCodes[2]))
					{
						numberCodes[5] = input;
					}
					else if (!input.Contains(locationCodes[5]))
					{
						numberCodes[2] = input;
					}
					else
					{
						numberCodes[3] = input;
					}
				}
			}

			// Remove the inputs we've already mapped from inputArray
			for (int i = 0; i < inputArray.Length; i++)
			{
				foreach (string numberCode in numberCodes)
				{
					if (inputArray[i] == numberCode)
					{
						inputArray[i] = "";
					}
				}
			}
			
			foreach (string input in inputArray)
			{
				bool zeroFoundHere = false;
				if (input == "") continue;
				
				// Check against 4's wires
				foreach (char c in numberCodes[4])
				{
					if (!input.Contains(c))
					{
						numberCodes[0] = input;
						zeroFoundHere = true;
					}
				}
				if (!zeroFoundHere) numberCodes[9] = input;
			}
			
			// Fix the ordering of characters such that matches are possible ("ab" -> "ba")
			// And convert the numberCodes array to a dict (note: if we were to do this again, 
			// I'd just have it as a dict from the get-go). Also, this could probably be simlified
			// because I didn't know that we could have duplicates with different spellings for output.
			Dictionary<string, int> numberCodesDict = new Dictionary<string, int>();
			for (int i = 0; i < 10; i++)
			{
				foreach (string output in outputArray)
				{
					if (DoesStringMatch(numberCodes[i], output) && !numberCodesDict.ContainsValue(i))
					{
						numberCodesDict.Add(output, i);
						break;
					}
					else if (!numberCodesDict.ContainsValue(i))
					{
						numberCodesDict.Add(numberCodes[i], i);
					}
				}
			}
			
			for (int i = 0; i < 4; i++)
			{
				Console.WriteLine($"Checking string {outputArray[i]}");
				foreach (KeyValuePair<string, int> numberCodePair in numberCodesDict)
				{
					if (DoesStringMatch(numberCodePair.Key, outputArray[i]))
					{
						score += numberCodePair.Value * Convert.ToInt16(Math.Pow(10, 3 - i));
						break;		
					}
				}
			}
		}
		return score;
	}
	

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
	
	static void OldMain()
	{
		string inputString =
			File.ReadAllText("/home/jack/Dev/Programming-Challenges/AdventOfCode2021/AdventOfCode2021/input.txt");
		
		
		Console.WriteLine(Part2Solution(inputString));
	}
}