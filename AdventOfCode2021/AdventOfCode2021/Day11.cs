/*
 * https://adventofcode.com/2021/day/11
 * Objective: Given a grid of ocopti energy levels,
 * Part 1: Track their changing values and flashes.
 * Part 2: 
 * Part of me learning C#.
 */

namespace AdventOfCode2021;

public class Grid
{
	const int GridSize = 10;
	int totalFlashes = 0;
	int[,] charges = new int[GridSize, GridSize];

	public Grid(string[] lines)
	{
		for (int i = 0; i < GridSize; i++)
		{
			for (int j = 0; j < GridSize; j++)
			{
				charges[i, j] = Convert.ToInt16(lines[i][j]) - '0';
			}
		}
	}

	public void PrintGrid()
	{
		for (int i = 0; i < GridSize; i++)
		{
			for (int j = 0; j < GridSize; j++)
			{
				if (charges[i, j] == 0) Console.Write("#");
				else Console.Write(charges[i, j]);
			}
			Console.WriteLine();
		}
	}

	public int StartCharging(int timesToCharge)
	{
		for (int i = 0; i < timesToCharge; i++)
		{
			Charge();
		}

		return totalFlashes;
	}

	public int FindMassFlashStep()
	{
		const int MaxSteps = 1000;
		int previousTotalFlashes = 0;

		for (int i = 0; i < 1000; i++)
		{
			previousTotalFlashes = totalFlashes;
			Charge();

			if (previousTotalFlashes + 100 == totalFlashes)
			{
				return i + 1;
			}
		}

		return 0;
	}

	private void Charge()
	{
		bool checksNecessary = false;
		HashSet<Tuple<int, int>> flashedPoints = new HashSet<Tuple<int, int>>();

		for (int i = 0; i < GridSize; i++)
		{
			for (int j = 0; j < GridSize; j++)
			{
				charges[i, j] += 1;
				if (charges[i, j] > 9)
				{
					checksNecessary = true;
				}
			}
		}
		
		while (checksNecessary)
		{
			checksNecessary = false;
			for (int i = 0; i < GridSize; i++)
			{
				for (int j = 0; j < GridSize; j++)
				{
					if (charges[i, j] > 9 && !flashedPoints.Contains(new Tuple<int, int>(i, j)))
					{
						Flash(i, j);
						flashedPoints.Add(new Tuple<int, int>(i, j));
						checksNecessary = true;
					}
				}
			}
		}
		
		foreach (var flashedPoint in flashedPoints)
		{
			charges[flashedPoint.Item1, flashedPoint.Item2] = 0;
		}
	}

	private void Flash(int row, int column)
	{
		bool checkLeft = true, checkRight = true, checkUp = true, checkDown = true;

		totalFlashes++;
		
		// We need these checks so that they canbe combined to check the diagonals.
		if (row == 0) checkUp = false;
		if (row == GridSize - 1) checkDown = false;
		if (column == 0) checkLeft = false;
		if (column == GridSize - 1) checkRight = false;
		
		// Ordered as if reading a book (Top-left to to bottom-right).
		if (checkLeft && checkUp) charges[row - 1, column - 1]++;
		if (checkUp) charges[row - 1, column]++;
		if (checkRight && checkUp) charges[row - 1, column + 1]++;
		if (checkLeft) charges[row, column - 1]++;
		if (checkRight) charges[row, column + 1]++;
		if (checkLeft && checkDown) charges[row + 1, column - 1]++;
		if (checkDown) charges[row + 1, column]++;
		if (checkRight && checkDown) charges[row + 1, column + 1]++;
	}
}
public class Day11
{
	static int Part1Solution(string[] inputStrings)
	{
		Grid grid = new Grid(inputStrings);

		return grid.StartCharging(100);
	}

	static int Part2Solution(string[] inputStrings)
	{
		Grid grid = new Grid(inputStrings);

		return grid.FindMassFlashStep();
	}
	
	static void Main()
	{
		const bool OnPart1 = false;
		const bool InPracticeMode = false;
		string[] inputStrings;

		if (InPracticeMode) inputStrings = File.ReadAllLines("/home/jack/Dev/Programming-Challenges/AdventOfCode2021/AdventOfCode2021/practiceInput.txt");
		else inputStrings = File.ReadAllLines("/home/jack/Dev/Programming-Challenges/AdventOfCode2021/AdventOfCode2021/input.txt");
		
		if (OnPart1) Console.WriteLine(Part1Solution(inputStrings));
		else Console.WriteLine(Part2Solution(inputStrings));
	}
}