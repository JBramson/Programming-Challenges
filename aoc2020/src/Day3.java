/**
 * https://adventofcode.com/2020/day/3
 * Objective: Given a geological map of trees repeating infinitely to the right,
 * Part 1: Find the number of trees encountered while moving down 1 and right 3.
 * Part 2:
 */
import java.util.List;

public class Day3 {
    boolean inDebugMode;
    boolean isDayOne;
    List<String> inputStrings;

    Day3(boolean inDebugMode, boolean isDayOne, List<String> inputStrings) {
        this.inDebugMode = inDebugMode;
        this.isDayOne = isDayOne;
        this.inputStrings = inputStrings;
    }

    private int solvePart1() {
        int treeCount = 0, horizontalPosition = 0;
        int lineSize = inputStrings.removeFirst().length(); // We don't need to check the starting position on line 0.
        for (String line : inputStrings) {
            horizontalPosition = (horizontalPosition + 3) % lineSize;
            if (line.charAt(horizontalPosition) == '#') treeCount++;
        }

        return treeCount;
    }


    private int solvePart2() {
        int treeCount = 0;

        return treeCount;
    }

    public int solve() {
        return isDayOne ? solvePart1() : solvePart2();
    }
}
