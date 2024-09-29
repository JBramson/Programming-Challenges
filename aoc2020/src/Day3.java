/**
 * https://adventofcode.com/2020/day/3
 * Objective: Given a geological map of trees repeating infinitely to the right,
 * Part 1: Find the number of trees encountered while moving down 1 and right 3.
 * Part 2: Find the number of trees encountered while moving in various patterns.
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

    private int getTreeImpacts(final int horizontalJumpSize, final int verticalJumpSize, final int lineSize) {
        int treeCount = 0, horizontalPosition = 0, currentLine = verticalJumpSize - 1;
        while (currentLine < inputStrings.size()) {
            horizontalPosition = (horizontalPosition + horizontalJumpSize) % lineSize;
            if (inputStrings.get(currentLine).charAt(horizontalPosition) == '#') treeCount++;
            currentLine += verticalJumpSize; // We keep the vertical jump below for continuity purposes- we still delete the first line to get lineSize.
        }

        return treeCount;
    }

    private int solvePart2() {
        long treeCount = 0;
        final int lineSize = inputStrings.removeFirst().length(); // We don't need to check the starting position on line 0.

        treeCount += getTreeImpacts(1, 1, lineSize);
        treeCount *= getTreeImpacts(3, 1, lineSize);
        treeCount *= getTreeImpacts(5, 1, lineSize);
        treeCount *= getTreeImpacts(7, 1, lineSize);
        treeCount *= getTreeImpacts(1, 2, lineSize);

        System.out.println("@FLAG=solution: The answer is [ " + treeCount + " ], since we've passed the INT_MAX.");

        return -1;
    }

    public int solve() {
        return isDayOne ? solvePart1() : solvePart2();
    }
}
