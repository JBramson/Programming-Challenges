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
	const int gridSize = 10;
	int totalFlashes = 0;
	int[,] charges = new int[gridSize, gridSize];

	public Grid(string[] lines)
	{
		for (int i = 0; i < gridSize; i++)
		{
			for (int j = 0; j < gridSize; j++)
			{
				charges[i, j] = Convert.ToInt16(lines[i][j]) - '0';
			}
		}
	}

	public void PrintGrid()
	{
		for (int i = 0; i < gridSize; i++)
		{
			for (int j = 0; j < gridSize; j++)
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

	private void Charge()
	{
		bool checksNecessary = false;
		HashSet<Tuple<int, int>> flashedPoints = new HashSet<Tuple<int, int>>();

		for (int i = 0; i < gridSize; i++)
		{
			for (int j = 0; j < gridSize; j++)
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
			for (int i = 0; i < gridSize; i++)
			{
				for (int j = 0; j < gridSize; j++)
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
		
		// Some of these have the wrong assignments- flip row and column. ALSO, CHECK DIAGONALS!!!!!!
		if (row == 0) checkLeft = false;
		if (row == gridSize - 1) checkRight = false;
		if (column == 0) checkUp = false;
		if (column == gridSize - 1) checkDown = false;
		
		if (row != 0) charges[row - 1, column]++;
		if (row != gridSize - 1) charges[row + 1, column]++;
		if (column != 0) charges[row, column - 1]++;
		if (column != gridSize - 1) charges[row, column + 1]++;		
	}
}
public class Day11
{
	static int Part1Solution(string[] inputStrings)
	{
		Grid grid = new Grid(inputStrings);
		grid.StartCharging(2);
		grid.PrintGrid();

		// return grid.StartCharging(10);
		return 0;
	}

	static int Part2Solution(string[] inputStrings)
	{
		return 0;
	}
	
	static void Main()
	{
		// string[] inputStrings = File.ReadAllLines("/home/jack/Dev/Programming-Challenges/AdventOfCode2021/AdventOfCode2021/input.txt");
		string[] inputStrings = File.ReadAllLines("/home/jack/Dev/Programming-Challenges/AdventOfCode2021/AdventOfCode2021/practiceInput.txt");
		
		Console.WriteLine(Part1Solution(inputStrings));
		// Console.WriteLine(Part2Solution(inputStrings));
	}
}