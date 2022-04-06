/*
 * https://adventofcode.com/2021/day/5
 * Objective: Given a series of line segments, find how many locations multiple lines overlap.
 * Part 1: Consider only vertical and horizontal lines.
 * Part 2: Also consider diagonals.
 * Part of me learning C#.
 */

using System.Security.Cryptography;

namespace AdventOfCode2021;

public class Day5
{
	// static Int16[ , ] ventCounts = new Int16[10,10];
	static Int16[ , ] ventCounts = new Int16[1000,1000];

	private static void MarkGridPart1(int startX, int startY, int endX, int endY)
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
			// Move the line rightwards (swap start and end positions if necessary)
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
	private static void MarkGridPart2(int startX, int startY, int endX, int endY)
	{
		int intermediateSwap;
		// Diagonal lines
		if ( (startX != endX) && (startY != endY) )
		{
			// Left-moving lines
			if (startX > endX)
			{
				// South-west
				if (startY < endY)
				{
					for (int i = 0; i <= (endY - startY); i++)
					{
						ventCounts[startY + i, startX - i] += 1;
					}
				}
				// North-west
				else
				{
					for (int i = 0; i <= (startY - endY); i++)
					{
						ventCounts[startY - i, startX - i] += 1;
					}
				}
			}
			// Right-moving lines
			else
			{
				// South-east
                if (startY < endY)
                {
                	for (int i = 0; i <= (endY - startY); i++)
                	{
                		ventCounts[startY + i, startX + i] += 1;
                	}
                }
                // North-east
                else
                {
                	for (int i = 0; i <= (startY - endY); i++)
                	{
                		ventCounts[startY - i, startX + i] += 1;
                	}
                }
			}
			
		}
		// Vertical lines
		else if (startX == endX)
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
			// Move the line rightwards (swap start and end positions if necessary)
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

	// Prints the current board. Used for debugging (the 10x10 board).
	static void PrintVentCounts()
	{
		Console.WriteLine("*************");
		for (int i = 0; i < 10; i++)
		{
			for (int j = 0; j < 10; j++)
			{
				if (ventCounts[i,j] == 0)
				{
					Console.Write('.');
				}
				else
				{
					Console.Write(ventCounts[i, j]);
				}
			}
			Console.WriteLine();
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
			// MarkGridPart1(startX, startY, endX, endY);
			MarkGridPart2(startX, startY, endX, endY);
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