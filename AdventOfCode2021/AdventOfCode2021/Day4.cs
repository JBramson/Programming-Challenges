/*
 * https://adventofcode.com/2021/day/3
 * Objective: Given a series of bingo rolls and cards, find the score of the winning board.
 * Diagonals do not count.
 * Part of me learning C#.
 */
namespace AdventOfCode2021;

public class Card
{
	public Card()
	{
		
	}
}

public class Day4
{
	static void Main()
	{
		string[] lines = File.ReadAllLines("/home/jack/Dev/Programming-Challenges/AdventOfCode2021/AdventOfCode2021/input.txt");
		int numOfCards = (lines.Length - 1) / 6;
		int[ , ] numbers = new int[5,5]; // Holds a card as it's being built
		Console.WriteLine($"There are {numOfCards} cards.");
		
		for (int i = 2; i < lines.Length; i += 6)
		{
			for (int j = 0; j < 5; j++)
			{
				for (int k = 0; k < 5; k++)
				{
					numbers[j, k] = Convert.ToInt16(lines[i + j].Substring(3 * k, 2));
				}
			}
			Console.WriteLine(lines[i]);
		}
	}

}