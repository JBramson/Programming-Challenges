/*
 * https://adventofcode.com/2021/day/3
 * Objective: Given a series of binary numbers, count which bit is more popular in each place,
 * pruning the less popular values, with 1 winning the tiebreaker for Oxygen. Reverse all this for CO2.
 *
 * Were I to return to this, I would likely create a new function that would take in the list and find the appropriate
 * value instead of doing it all in Main(). I'll keep my eyes peeled for the next few problems- we appear to be
 * getting to that level.
 * Part of me learning C#.
 */
namespace AdventOfCode2021;

public class Day3Part2
{
	static void OldMain()
	{
		IEnumerable<string> numbers = System.IO.File.ReadLines("/home/jack/Dev/Programming-Challenges/AdventOfCode2021/AdventOfCode2021/input.txt");
		int numberLength = numbers.ElementAt(0).Length;
		int oxygenRating, co2Rating, currentCountOxygen, currentCountCO2; // currentCount tracks the current majority number for the current bit
		List<string> oxygenList = new List<string>(), co2List = new List<string>();

		// Get the initial list into our modifiable lists
		foreach (string number in numbers)
		{
			oxygenList.Add(number);
			co2List.Add(number);
		}
		// Loop through each remaining list of values until we have the proper one of majority
		for (int i = 0; i < numberLength; i++)
		{
			currentCountOxygen = 0;
			currentCountCO2 = 0;
			foreach (string oxygenStr in oxygenList)
			{
				if (oxygenStr[i] == '1')
				{
					currentCountOxygen += 1;
				}
				else
				{
					currentCountOxygen -= 1;
				}
			}
			foreach (string co2Str in co2List)
			{
				if (co2Str[i] == '1')
				{
					currentCountCO2 += 1;
				}
				else
				{
					currentCountCO2 -= 1;
				}
			}
			// Start pruning values for Oxygen (keep more popular)
			if (currentCountOxygen >= 0)
			{
				for (int j = 0; j < oxygenList.Count; j++)
				{
					if (oxygenList.Count == 1) // We can't eliminate our last value
					{
						break;
					}
					if (oxygenList[j][i] == '0')
					{
						oxygenList.RemoveAt(j);
						j--; // We don't want to increment j if we're deleting a value
					}
				}
			}
			else
			{
				for (int j = 0; j < oxygenList.Count; j++)
				{
					if (oxygenList.Count == 1) // We can't eliminate our last value
					{
						break;
					}
					if (oxygenList[j][i] == '1')
					{
						oxygenList.RemoveAt(j);
						j--; // We don't want to increment j if we're deleting a value
					}
				}
			}
			// Start pruning values for CO2 (keep less popular)
			if (currentCountCO2 >= 0)
			{
				for (int j = 0; j < co2List.Count; j++)
				{
					if (co2List.Count == 1) // We can't eliminate our last value
					{
						break;
					}
					if (co2List[j][i] == '1')
					{
						co2List.RemoveAt(j);
						j--; // We don't want to increment j if we're deleting a value
					}
				}
			}
			else
			{
				for (int j = 0; j < co2List.Count; j++)
				{
					if (co2List.Count == 1) // We can't eliminate our last value
					{
						break;
					}
					if (co2List[j][i] == '0')
					{
						co2List.RemoveAt(j);
						j--; // We don't want to increment j if we're deleting a value
					}
				}
			}
		}

		oxygenRating = Convert.ToInt32(oxygenList[0], 2);
		co2Rating = Convert.ToInt32(co2List[0], 2);

		Console.WriteLine(oxygenRating * co2Rating);
	}

}