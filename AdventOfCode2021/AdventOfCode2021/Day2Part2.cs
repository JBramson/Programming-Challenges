/*
 * https://adventofcode.com/2021/day/2#part2
 * Objective: Given a series of movements and re-orientations, calculate the final position,
 * multiplying the depth by the horizontal position.
 * Part of me learning C#.
 */
namespace AdventOfCode2021;

public class Day2Part2
{
	static void OldMain()
	{
		IEnumerable<string> movements = System.IO.File.ReadLines("/home/jack/Dev/Programming-Challenges/AdventOfCode2021/AdventOfCode2021/input.txt");
		int depth = 0, horizontalPos = 0, aim = 0, magnitude;
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
					depth += aim * magnitude;
					break;
				case "down":
					aim += magnitude;
					break;
				case "up":
					aim -= magnitude;
					break;
			}
		}
		
		Console.WriteLine(depth * horizontalPos);
	}

}