/*
 * https://adventofcode.com/2021/day/9
 * Objective: Given a grid of numbers,
 * Part 1: Find which numbers are less than its neighbors (diagonals don't count).
 * Part 2: 
 * Part of me learning C#.
 */

namespace AdventOfCode2021;

public class Day9
{
	// We need to return 1 + the height of each local min position
	static int Part1Solution(string[] inputStrings)
	{
		int height = inputStrings.Length, length = inputStrings[0].Length;
		int minPositionTotals = 0;
		bool checkUp, checkDown, checkLeft, checkRight;
		ushort[,] numbersArray = new ushort[height, length];

		// Convert the strings array to a 2D numbers array.
		// If there's a more efficient way to do this, i.e.
		// Some sort of a = b -> (for each), please let me know.
		for (int i = 0; i < height; i++)
		{
			for (int j = 0; j < length; j++)
			{
				numbersArray[i,j] = (ushort)(inputStrings[i][j] - '0');
			}
		}

		// Check each number against its neighbors
		for (int i = 0; i < height; i++)
		{
			if (i == 0)
			{
				checkUp = false;
				checkDown = true;
			}
			else if (i == height - 1)
			{
				checkUp = true;
				checkDown = false;
			}
			else
			{
				checkUp = true;
				checkDown = true;
			}

			for (int j = 0; j < length; j++)
			{
				if (j == 0)
				{
					checkLeft = false;
					checkRight = true;
				}
				else if (j == length - 1)
				{
					checkLeft = true;
					checkRight = false;
				}
				else
				{
					checkLeft = true;
					checkRight = true;
				}
				// Console.WriteLine($"{i},{j}: {numbersArray[i,j]}: {checkUp}, {checkDown}, {checkLeft}, {checkRight}");

				// Now that the directions to check are set, we can see if the cell is a local min.
				if (checkLeft)
				{
					if (!(numbersArray[i,j] < numbersArray[i,j-1]))
					{
						continue;
					}
				}
				if (checkRight)
				{
					if (!(numbersArray[i,j] < numbersArray[i,j+1]))
					{
						continue;
					}
				}
				if (checkUp)
				{
					if (!(numbersArray[i,j] < numbersArray[i-1,j]))
					{
						continue;
					}
				}
				if (checkDown)
				{
					if (!(numbersArray[i,j] < numbersArray[i+1,j]))
					{
						continue;
					}
				}
				minPositionTotals += numbersArray[i,j] + 1;
			}
		}		

		return minPositionTotals;
	}

	// static int Part2Solution(string inputString)
	// {
		
	// }
	
	static void Main()
	{
		// string inputString =
		// 	File.ReadAllText("/home/jack/Dev/Programming-Challenges/AdventOfCode2021/AdventOfCode2021/input.txt");
		string[] inputStrings =
			File.ReadAllLines("/home/jack/Dev/Programming-Challenges/AdventOfCode2021/AdventOfCode2021/input.txt");
		
		
		Console.WriteLine(Part1Solution(inputStrings));
	}
}