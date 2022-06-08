/*
 * https://adventofcode.com/2021/day/9
 * Objective: Given a grid of numbers,
 * Part 1: Find which numbers are less than its neighbors (diagonals don't count).
 * Part 2: Find the three largest basins.
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

	static int Part2Solution(string[] inputStrings)
	{
		int height = inputStrings.Length, length = inputStrings[0].Length;
		int smallestBasinSize = 0, middleBasinSize = 0, largestBasinSize = 0;
		List<Tuple<int, int>> basinPositions = new List<Tuple<int, int>>();
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
				basinPositions.Add(new Tuple<int, int>(i,j));
			}
		}		

		// Console.WriteLine($"Basin position count: {basinPositions.Count}");
		foreach (Tuple<int, int> startingPosition in basinPositions)
		{
			HashSet<Tuple<int, int>> observedPositions = new HashSet<Tuple<int, int>>();
			Stack<Tuple<int, int>> positionStack = new Stack<Tuple<int, int>>();
			positionStack.Push(startingPosition); // Front-load the starting position

			while (positionStack.Any()) // Runs until the stack is empty (or we break out)
			{
				Tuple<int, int> currentPosition = positionStack.Pop();
				observedPositions.Add(currentPosition);
				
				if (currentPosition.Item1 == 0)
				{
					checkUp = false;
					checkDown = true;
				}
				else if (currentPosition.Item1 == height - 1)
				{
					checkUp = true;
					checkDown = false;
				}
				else
				{
					checkUp = true;
					checkDown = true;
				}

				if (currentPosition.Item2 == 0)
				{
					checkLeft = false;
					checkRight = true;
				}
				else if (currentPosition.Item2 == length - 1)
				{
					checkLeft = true;
					checkRight = false;
				}
				else
				{
					checkLeft = true;
					checkRight = true;
				}

				
				// Now that the directions to check are set, we can see if we can add new locations or stop checking.
				if (checkLeft)
				{
					if ((numbersArray[currentPosition.Item1,currentPosition.Item2] < numbersArray[currentPosition.Item1,currentPosition.Item2 - 1])
						&& (9 != numbersArray[currentPosition.Item1,currentPosition.Item2 - 1])
						&& !observedPositions.Contains(new Tuple<int, int>(currentPosition.Item1, currentPosition.Item2 - 1)))
					{
						positionStack.Push(new Tuple<int, int>(currentPosition.Item1,currentPosition.Item2 - 1));
					}
				}
				if (checkRight)
				{
					if ((numbersArray[currentPosition.Item1,currentPosition.Item2] < numbersArray[currentPosition.Item1,currentPosition.Item2 + 1])
						&& (9 != numbersArray[currentPosition.Item1,currentPosition.Item2 + 1])
						&& !observedPositions.Contains(new Tuple<int, int>(currentPosition.Item1, currentPosition.Item2 + 1)))
					{
						positionStack.Push(new Tuple<int, int>(currentPosition.Item1,currentPosition.Item2 + 1));
					}
				}
				if (checkUp)
				{
					if ((numbersArray[currentPosition.Item1,currentPosition.Item2] < numbersArray[currentPosition.Item1 - 1,currentPosition.Item2])
						&& (9 != numbersArray[currentPosition.Item1 - 1,currentPosition.Item2])
						&& !observedPositions.Contains(new Tuple<int, int>(currentPosition.Item1 - 1, currentPosition.Item2)))
					{
						positionStack.Push(new Tuple<int, int>(currentPosition.Item1 - 1,currentPosition.Item2));
					}
				}
				if (checkDown)
				{
					if ((numbersArray[currentPosition.Item1,currentPosition.Item2] < numbersArray[currentPosition.Item1 + 1,currentPosition.Item2])
						&& (9 != numbersArray[currentPosition.Item1 + 1,currentPosition.Item2])
						&& !observedPositions.Contains(new Tuple<int, int>(currentPosition.Item1 + 1, currentPosition.Item2)))
					{
						positionStack.Push(new Tuple<int, int>(currentPosition.Item1 + 1,currentPosition.Item2));
					}
				}
			}
			
			if (observedPositions.Count > largestBasinSize)
			{
				smallestBasinSize = middleBasinSize;
				middleBasinSize = largestBasinSize;
				largestBasinSize = observedPositions.Count;
			}
			else if (observedPositions.Count > middleBasinSize)
			{
				smallestBasinSize = middleBasinSize;
				middleBasinSize = observedPositions.Count;
			}
			else if (observedPositions.Count > smallestBasinSize)
			{
				smallestBasinSize = observedPositions.Count;
			}
		}

		return smallestBasinSize * middleBasinSize * largestBasinSize;
	}
	
	static void OldMain()
	{
		string[] inputStrings =
			File.ReadAllLines("/home/jack/Dev/Programming-Challenges/AdventOfCode2021/AdventOfCode2021/input.txt");
		
		
		// Console.WriteLine(Part1Solution(inputStrings));
		Console.WriteLine(Part2Solution(inputStrings));
	}
}