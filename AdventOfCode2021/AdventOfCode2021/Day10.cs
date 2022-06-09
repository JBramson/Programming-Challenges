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
		List<float> listScores = new List<float>();

		foreach (string line in inputStrings)
		{
			if (FindFirstIncorrectChar(line) == '0')
			{
				float totalScore = 0;
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
		Console.WriteLine(listScores.Count);
		Console.WriteLine((decimal) listScores[listScores.Count / 2 - 1]);
		Console.WriteLine((decimal) listScores[listScores.Count / 2 + 1]);
		// Console.WriteLine(listScores[listScores.Count / 2]);
		return (decimal) listScores[listScores.Count / 2];
	}
	
	static void Main()
	{
		string[] inputStrings = File.ReadAllLines("/home/jack/Dev/Programming-Challenges/AdventOfCode2021/AdventOfCode2021/input.txt");
		// string[] inputStrings = File.ReadAllLines("/home/jack/Dev/Programming-Challenges/AdventOfCode2021/AdventOfCode2021/practiceInput.txt");
		
		// 78897227 is too low.
		// 2429644500 is also too low.
		// 2429645000 is too high.
		// Console.WriteLine(Part1Solution(inputStrings));
		Console.WriteLine(Part2Solution(inputStrings));
	}
}