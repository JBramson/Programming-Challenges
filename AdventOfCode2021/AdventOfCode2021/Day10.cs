/*
 * https://adventofcode.com/2021/day/10
 * Objective: Given a series of lines of chunks,
 * Part 1: Find the first character that is an improper closer for each line and sum their score.
 * Part 2: Find the missing characters on the other lines and compute thier scores.
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

	static char FindCorrespondingChar(char c)
	{
		switch (c)
		{
			case '(':
				return ')';
			case '[':
				return ']';
			case '{':
				return '}';
			case '<':
				return '>';
			default:
				throw new ArgumentException($"Invalid input: {c}");
		}
	}

	static string FindCompletionString(string line)
	{
		Stack<char> chars = new Stack<char>();
		string completionString = "";

		// Create the stack
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
		}

		// Find what's missing from this stack
		while (chars.Any())
		{
			completionString += FindCorrespondingChar(chars.Pop());
		}
		return completionString;
	}

	static decimal Part2Solution(string[] inputStrings)
	{
		string completionString;
		List<decimal> listScores = new List<decimal>();

		foreach (string line in inputStrings)
		{
			if (FindFirstIncorrectChar(line) == '0')
			{
				decimal totalScore = 0;
				completionString = FindCompletionString(line);
				foreach (char c in completionString)
				{
					totalScore *= 5;
					switch (c)
					{
						case ')':
							totalScore += 1;
							break;
						case ']':
							totalScore += 2;
							break;
						case '}':
							totalScore += 3;
							break;
						case '>':
							totalScore += 4;
							break;
					}
				}
				listScores.Add(totalScore);
			}
		}
		
		listScores.Sort();
		return listScores[listScores.Count / 2];
	}
	
	static void Main()
	{
		string[] inputStrings = File.ReadAllLines("/home/jack/Dev/Programming-Challenges/AdventOfCode2021/AdventOfCode2021/input.txt");
		// string[] inputStrings = File.ReadAllLines("/home/jack/Dev/Programming-Challenges/AdventOfCode2021/AdventOfCode2021/practiceInput.txt");
		
		// Console.WriteLine(Part1Solution(inputStrings));
		Console.WriteLine(Part2Solution(inputStrings));
	}
}