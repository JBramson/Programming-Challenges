/**
 * https://adventofcode.com/2020/day/2
 * Objective: Given a list of password policies and passwords, find how many passwords are valid under their given policy.
 * Part 1: The policy gives the number of times a given char must appear in the password.
 * Part 2: The policy gives two positions in the password, and exactly one must contain the given char.
 */
import java.util.List;

public class Day2 {
    boolean inDebugMode;
    boolean isDayOne;
    List<String> inputStrings;

    Day2(boolean inDebugMode, boolean isDayOne, List<String> inputStrings) {
        this.inDebugMode = inDebugMode;
        this.isDayOne = isDayOne;
        this.inputStrings = inputStrings;
    }

    private int solve_part_1() {
        int validPasswordCount = 0;
        for (String line : inputStrings) {
            // Read in information (entry[1] is the password)
            String[] entry = line.split(": ");
            String[] policy = entry[0].split(" ");
            int minQuantity = Integer.parseInt(policy[0].split("-")[0]);
            int maxQuantity = Integer.parseInt(policy[0].split("-")[1]);
            char targetedCharacter = entry[0].charAt(entry[0].length() - 1);

            int charMatches = 0;
            for (char c : entry[1].toCharArray()) {
                if (c == targetedCharacter) charMatches++;
            }

            if (charMatches >= minQuantity && charMatches <= maxQuantity) validPasswordCount++;
        }

        return validPasswordCount;
    }

    private int solve_part_2() {
        int validPasswordCount = 0;
        for (String line : inputStrings) {
            // Read in information (entry[1] is the password)
            String[] entry = line.split(": ");
            String[] policy = entry[0].split(" ");
            // The locations are given in 1-index and must be converted to 0-index.
            int firstLocation = Integer.parseInt(policy[0].split("-")[0]) - 1;
            int secondLocation = Integer.parseInt(policy[0].split("-")[1]) - 1;
            char targetedCharacter = entry[0].charAt(entry[0].length() - 1);

            // "^" is the XOR operator. Cool.
            if (entry[1].charAt(firstLocation) == targetedCharacter
                    ^ entry[1].charAt(secondLocation) == targetedCharacter) validPasswordCount++;
        }

        return validPasswordCount;
    }

    public int solve() {
        return isDayOne ? solve_part_1() : solve_part_2();
    }
}
