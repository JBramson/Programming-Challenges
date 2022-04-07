/*
 * https://adventofcode.com/2021/day/6
 * Objective: Given the initial positions of some crab ships,
 * find the minimum fuel needed to move each to the same spot.
 * Part 1: Each movement costs 1 fuel unit. (1, 1, 1, ... , 1)
 * Part 2: The first movement costs 1 fuel unit, and each successive one costs one extra. (1, 2, 3, ... , +INF)
 * Part of me learning C#.
 */

namespace AdventOfCode2021;

public class Day7
{
	static int GetCost(int distance)
	{
		if (distance == 0 || distance == 1)
		{
			return distance;
		}

		return distance + GetCost(distance - 1);
	}
	
	static void Main()
	{
		string inputString =
			File.ReadAllText("/home/jack/Dev/Programming-Challenges/AdventOfCode2021/AdventOfCode2021/input.txt");
		int minFuel = Int32.MaxValue, maxPosition = 0, newPosition, currentFuel;
		List<int> positions = new List<int>();

		foreach (string positionString in inputString.Split(","))
		{
			newPosition = Convert.ToInt16(positionString);
			positions.Add(newPosition);
			if (newPosition > maxPosition)
			{
				maxPosition = newPosition;
			}
		}

		for (int i = 0; i < maxPosition; i++)
		{
			currentFuel = 0;
			foreach (int position in positions)
			{
				currentFuel += GetCost(Math.Abs(position - i));
				// If we've already eclipsed the previous minimum expense, we don't need to check any more.
				if (currentFuel > minFuel)
				{
					break;
				}
			}
			if (currentFuel < minFuel)
			{
				minFuel = currentFuel;
			}
		}
		
		Console.WriteLine(minFuel);
	}
}