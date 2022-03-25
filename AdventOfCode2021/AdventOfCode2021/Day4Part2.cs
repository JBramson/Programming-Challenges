/*
 * https://adventofcode.com/2021/day/3
 * Objective: Given a series of bingo rolls and cards, find the score of the LAST winning board.
 * Diagonals do not count.
 * Part of me learning C#.
 */
namespace AdventOfCode2021;

public class LosingCard
{
	private static int _remainingBingos = 0;
	int[ , ] numbers; // Holds this card's numbers
	bool[ , ] markedZones = new bool[5, 5]; // Holds this card's numbers (false [unmarked] by default)
	private bool hasBingo = false;
	public LosingCard(int[,] _numbers)
	{
		// I learned the hard way that _number is just a reference and would assign as such,
		// causing all cards to be overridden every time a new numbers[,] array was created.
		// For this reason, we make a shallow copy (clone) of the 2D array.
		numbers = (int[,]?)_numbers.Clone();
		_remainingBingos++;
	}

	public void MarkCard(int calledNum)
	{
		if (hasBingo)
		{
			return;
		}
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				// If we've hit a number, mark it and check to see if we have bingo
				if (numbers[i, j] == calledNum)
				{
					markedZones[i, j] = true;
					if (CheckBingo(i, j))
					{
						hasBingo = true;
						_remainingBingos--;
						if (_remainingBingos == 0)
						{
							AnnounceVictory(calledNum);
						}
					}
				}
			}
		}
	}
	private bool CheckBingo(int row, int column)
	{
		// We only need to check the given row and column of the new mark
		bool rowDisqualified = false, columnDisqualified = false;
		for (int i = 0; i < 5; i++)
		{
			if (markedZones[row, i] == false)
			{
				rowDisqualified = true;
			}
			if (markedZones[i, column] == false)
			{
				columnDisqualified = true;
			}
			
			if (rowDisqualified && columnDisqualified)
            {
             	return false;
            }
		}
		return true;
	}

	private void AnnounceVictory(int calledNumber)
	{
		int scoreMultiplier = 0;
		
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				if (markedZones[i, j] == false)
				{
					scoreMultiplier += numbers[i, j];
				}
			}
		}
		Console.WriteLine("\n" + calledNumber * scoreMultiplier);
		Environment.Exit(0); // We don't need to do anything else once we've finished.
	}
}

public class Day4Part2
{
	static void OldMain()
	{
		string[] lines = File.ReadAllLines("/home/jack/Dev/Programming-Challenges/AdventOfCode2021/AdventOfCode2021/input.txt");
		string drawsStr = lines[0];
		int numOfCards = (lines.Length - 1) / 6;
		int[ , ] numbers = new int[5,5]; // Holds a card as it's being built
		LosingCard[] cards = new LosingCard[numOfCards];
		
		// Make the cards
		for (int i = 2; i < lines.Length; i += 6)
		{
			for (int j = 0; j < 5; j++)
			{
				for (int k = 0; k < 5; k++)
				{
					numbers[j, k] = Convert.ToInt16(lines[i + j].Substring(3 * k, 2));
				}
			}
			cards[(i - 2) / 6] = new LosingCard(numbers);
		}
		// Call the numbers
		foreach (string drawStr in drawsStr.Split(','))
		{
			foreach (LosingCard card in cards)
			{
				card.MarkCard(Convert.ToInt16(drawStr));
			}
		}
		
		
	}

}