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
		int basinTotals = 0;

		/*
		 * Theory:
		 * Make a list of the low points' locations, and for each:
		 * 	Pop the location off of the stack and increment our total.
		 * 	Add each bordering cell to the stack if it's one greater than the current position.
		 	* If the bordering cell is less than the current one, stop checking. The smaller one will have a bigger basin.
		 * 	Repeat the above until the stack is empty.
		 * Record the size of the stack.
		 * Take the three biggest stacks and return the product of their sizes.
		 */
		int height = inputStrings.Length, length = inputStrings[0].Length;
		// int minPositionTotals = 0;
		List<Tuple<int, int>> basinPositions = new List<Tuple<int, int>>();
		bool checkUp, checkDown, checkLeft, checkRight;
		ushort[,] numbersArray = new ushort[height, length];
		int[] maxBasinSizes = {0, 0, 0};

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
			int currentBasinSize = 0;
			Stack<Tuple<int, int>> positionStack = new Stack<Tuple<int, int>>();
			HashSet<Tuple<int, int>> observedPositions = new HashSet<Tuple<int, int>>();
			positionStack.Push(startingPosition); // Front-load the starting position
			// Console.WriteLine(positionStack.First().Item1);

			while (positionStack.Any()) // Runs until the stack is empty (or we break out)
			{
				Tuple<int, int> currentPosition = positionStack.Pop();
				observedPositions.Add(currentPosition);
				currentBasinSize++;

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

				Console.Write(checkLeft);
				Console.Write(checkRight);
				Console.Write(checkUp);
				Console.WriteLine(checkDown);
				// Now that the directions to check are set, we can see if we can add new locations or stop checking.
				if (checkLeft)
				{
					Console.WriteLine($"Checked left: {numbersArray[currentPosition.Item1,currentPosition.Item2]} vs {numbersArray[currentPosition.Item1,currentPosition.Item2 - 1]}");
					// Console.WriteLine(!observedPositions.Contains(new Tuple<int, int>(currentPosition.Item1, currentPosition.Item2 - 1)));
					if ((numbersArray[currentPosition.Item1,currentPosition.Item2] + 1 == numbersArray[currentPosition.Item1,currentPosition.Item2 - 1])
						&& !observedPositions.Contains(new Tuple<int, int>(currentPosition.Item1, currentPosition.Item2 - 1)))
					{
						positionStack.Push(new Tuple<int, int>(currentPosition.Item1,currentPosition.Item2 - 1));
						Console.WriteLine("Appended left");
					}
					// else if ((numbersArray[currentPosition.Item1,currentPosition.Item2] - 1 == numbersArray[currentPosition.Item1,currentPosition.Item2 - 1])
					// 	&& !observedPositions.Contains(new Tuple<int, int>(currentPosition.Item1, currentPosition.Item2 - 1)))
					// {
					// 	Console.WriteLine($"Supposed disqualifier detected at ({currentPosition.Item1}, {currentPosition.Item2})");
					// 	currentBasinSize = 0;
					// 	break;
					// }
				}
				if (checkRight)
				{
					Console.WriteLine("Checked right");
					if ((numbersArray[currentPosition.Item1,currentPosition.Item2] + 1 == numbersArray[currentPosition.Item1,currentPosition.Item2 + 1])
						&& !observedPositions.Contains(new Tuple<int, int>(currentPosition.Item1, currentPosition.Item2 + 1)))
					{
						positionStack.Push(new Tuple<int, int>(currentPosition.Item1,currentPosition.Item2 + 1));
						Console.WriteLine("Appended right");
					}
					// else if ((numbersArray[currentPosition.Item1,currentPosition.Item2] - 1 == numbersArray[currentPosition.Item1,currentPosition.Item2 + 1])
					// 	&& !observedPositions.Contains(new Tuple<int, int>(currentPosition.Item1, currentPosition.Item2 + 1)))
					// {
					// 	Console.WriteLine($"Supposed disqualifier detected at ({currentPosition.Item1}, {currentPosition.Item2})");
					// 	currentBasinSize = 0;
					// 	break;
					// }
				}
				if (checkUp)
				{
					Console.WriteLine("Checked up");
					if ((numbersArray[currentPosition.Item1,currentPosition.Item2] + 1 == numbersArray[currentPosition.Item1 - 1,currentPosition.Item2])
						&& !observedPositions.Contains(new Tuple<int, int>(currentPosition.Item1 - 1, currentPosition.Item2)))
					{
						positionStack.Push(new Tuple<int, int>(currentPosition.Item1 - 1,currentPosition.Item2));
						Console.WriteLine("Appended up");
					}
					// else if ((numbersArray[currentPosition.Item1,currentPosition.Item2] - 1 == numbersArray[currentPosition.Item1 - 1,currentPosition.Item2])
					// 	&& !observedPositions.Contains(new Tuple<int, int>(currentPosition.Item1 - 1, currentPosition.Item2)))
					// {
					// 	Console.WriteLine($"Supposed disqualifier detected at ({currentPosition.Item1}, {currentPosition.Item2})");
					// 	currentBasinSize = 0;
					// 	break;
					// }
				}
				if (checkDown)
				{
					Console.WriteLine("Checked down");
					if ((numbersArray[currentPosition.Item1,currentPosition.Item2] + 1 == numbersArray[currentPosition.Item1 + 1,currentPosition.Item2])
						&& !observedPositions.Contains(new Tuple<int, int>(currentPosition.Item1 + 1, currentPosition.Item2)))
					{
						positionStack.Push(new Tuple<int, int>(currentPosition.Item1 + 1,currentPosition.Item2));
						Console.WriteLine("Appended down");
					}
					// else if ((numbersArray[currentPosition.Item1,currentPosition.Item2] - 1 == numbersArray[currentPosition.Item1 + 1,currentPosition.Item2])
					// 	&& !observedPositions.Contains(new Tuple<int, int>(currentPosition.Item1 + 1, currentPosition.Item2)))
					// {
					// 	Console.WriteLine($"Supposed disqualifier detected at ({currentPosition.Item1}, {currentPosition.Item2})");
					// 	currentBasinSize = 0;
					// 	break;
					// }
				}
			}
			Console.WriteLine($"Basin size: {currentBasinSize}");
		}

		return basinTotals;
	}
	
	static void Main()
	{
		string[] inputStrings =
			File.ReadAllLines("/home/jack/Dev/Programming-Challenges/AdventOfCode2021/AdventOfCode2021/input.txt");
		
		
		// Console.WriteLine(Part1Solution(inputStrings));
		Console.WriteLine(Part2Solution(inputStrings));
	}
}