/*
 * https://adventofcode.com/2021/day/10
 * Objective: Given a series of lines of chunks,
 * Part 1: Find the first character that is an improper close for each line and sum their score.
 * Part 2: 
 * Part of me learning C#.
 */

namespace AdventOfCode2021;

public class Day10
{
	static bool IsOpener(char c)
	{
		if (c == '(' || c == '{' || c == '<' || c == '[')
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	
	static bool IsMatch(char c1, char c2)
	{
		// Ensure that c1 is less than c2 for future check
		if (c1 > c2)
		{
			char temp = c1;
			c1 = c2;
			c2 = temp;
		}

		if (c1 == '(' && c2 == ')') return true;
		else if (c1 == '{' && c2 == '}') return true;
		else if (c1 == '<' && c2 == '>') return true;
		else if (c1 == '[' && c2 == ']') return true;
		else return false;
	}
	static char FindFirstIncorrectChar(string line)
	{
		Stack<char> chars = new Stack<char>();

		// Console.WriteLine(line);
		foreach (char c in line)
		{
			if (IsOpener(c))
			{
				chars.Push(c);
			}
			else if (IsMatch(c, chars.Peek()))
			{
				chars.Pop();
			}
			else
			{
				// Console.WriteLine($"{c} vs. {chars.Peek()}");
				return c;
			}
		}
		// We only get here if there's no mismatched end.
		return '0';
	}
	static int Part1Solution(string[] inputStrings)
	{
		int totalScore = 0;

		foreach (string line in inputStrings)
		{
			// Console.WriteLine(FindFirstIncorrectChar(line));
			switch (FindFirstIncorrectChar(line))
			{
				case ')':
					totalScore += 3;
					break;
				case ']':
					totalScore += 57;
					break;
				case '}':
					totalScore += 1197;
					break;
				case '>':
					totalScore += 25137;
					break;
			}
		}
		return totalScore;
	}

	static int Part2Solution(string[] inputStrings)
	{
		return 0;
	}
	
	static void Main()
	{
		string[] inputStrings =
			File.ReadAllLines("/home/jack/Dev/Programming-Challenges/AdventOfCode2021/AdventOfCode2021/input.txt");
		// string[] inputStrings =
		// 	File.ReadAllLines("/home/jack/Dev/Programming-Challenges/AdventOfCode2021/AdventOfCode2021/practiceInput.txt");
		
		
		Console.WriteLine(Part1Solution(inputStrings));
		// Console.WriteLine(Part2Solution(inputStrings));
	}
}