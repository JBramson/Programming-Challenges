/*
 * https://adventofcode.com/2021/day/5
 * Objective: Given a series of line segments, find how many locations multiple lines overlap.
 * Considers only vertical and horizontal lines.
 * Part of me learning C#.
 */
namespace AdventOfCode2021;

public class Day5
{
	static Int16[ , ] ventCounts = new Int16[1000,1000];

	private static void MarkGrid(int startX, int startY, int endX, int endY)
	{
		int intermediateSwap;
		// Ignore diagonals
		if ( (startX != endX) && (startY != endY) )
		{
			return;
		}
		// Vertical lines
		if (startX == endX)
		{
			// Move the line downwards (swap start and end positions if necessary)
			if (startY > endY)
			{
				intermediateSwap = startY;
				startY = endY;
				endY = intermediateSwap;
			}
			for (int i = startY; i <= endY; i++)
			{
				ventCounts[i, startX] += 1;
			}
		}
		// Horizontal lines
		else if (startY == endY)
		{
			// Move the line downwards (swap start and end positions if necessary)
			if (startX > endX)
			{
				intermediateSwap = startX;
				startX = endX;
				endX = intermediateSwap;
			}
			for (int i = startX; i <= endX; i++)
			{
				ventCounts[startY, i] += 1;
			}
		}
	}
	
	static void Main()
	{
		string[] lines = File.ReadAllLines("/home/jack/Dev/Programming-Challenges/AdventOfCode2021/AdventOfCode2021/input.txt");
		string[] positions;
		int startX, startY, endX, endY, overlapCount = 0;

		foreach (string line in lines)
		{
			positions = line.Split(" -> ");
			startX = Convert.ToInt32(positions[0].Split(",")[0]);
			startY = Convert.ToInt32(positions[0].Split(",")[1]);
			endX = Convert.ToInt32(positions[1].Split(",")[0]);
			endY = Convert.ToInt32(positions[1].Split(",")[1]);
			// Console.WriteLine($"({startX}, {startY}) ==> ({endX}, {endY})");
			MarkGrid(startX, startY, endX, endY);
		}

		foreach (short ventCount in ventCounts)
		{
			if (ventCount > 1)
			{
				overlapCount++;
			}
		}
		
		Console.WriteLine(overlapCount);
	}
}