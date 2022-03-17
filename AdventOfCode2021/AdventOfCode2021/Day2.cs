/*
 * https://adventofcode.com/2021/day/2
 * Objective: Given a series of movements, calculate the final position,
 * multiplying the depth by the horizontal position.
 * Part of me learning C#.
 */
namespace AdventOfCode2021;

public class Day2
{
	static void Main()
	{
		IEnumerable<string> movements = System.IO.File.ReadLines("/home/jack/Dev/Programming-Challenges/AdventOfCode2021/AdventOfCode2021/input.txt");
		int depth = 0, horizontalPos = 0, magnitude;
		string[] movementArray;
		string direction;

		foreach (string movement in movements) // Direction, magnitude
		{
			movementArray = movement.Split();
			direction = movementArray[0];
			magnitude = Convert.ToInt16(movementArray[1]);

			switch (direction)
			{
				case "forward":
					horizontalPos += magnitude;
					break;
				case "down":
					depth += magnitude;
					break;
				case "up":
					depth -= magnitude;
					break;
			}
		}
		
		Console.WriteLine(depth * horizontalPos);
	}

}