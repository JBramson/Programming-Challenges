/**
 * https://adventofcode.com/2020/day/5
 * Objective: Given a list of directions halving the available seats,
 * Part 1: Find the appropriate seat ID.
 * Part 2:
 */
import java.util.List;

public class Day5 {
    boolean inDebugMode;
    boolean isDayOne;
    List<String> inputStrings;

    Day5(boolean inDebugMode, boolean isDayOne, List<String> inputStrings) {
        this.inDebugMode = inDebugMode;
        this.isDayOne = isDayOne;
        this.inputStrings = inputStrings;
    }

    private int getPosition(final String directions, int lower, int upper) {
        for (char c: directions.toCharArray()) {
            if (c == 'F' || c == 'L') {
                // The lower end should be rounded down. If exactly one end is odd, we subtract 1 from the result.
                final int adjustment = (lower % 2 != 0 ^ upper % 2 != 0) ? 1 : 0;
                upper -= (upper - lower) / 2 + adjustment;
            } else {
                // The lower end should be rounded up. If exactly one end is odd, we add 1 to the result.
                final int adjustment = (lower % 2 != 0 ^ upper % 2 != 0) ? 1 : 0;
                lower += (upper - lower) / 2 + adjustment;
            }
        }

        return lower; // They should be equal. If they're not, either I programmed something wrong or space-time is collapsing.
    }

    private int solvePart1() {
        int highestSeatID = 0;

        for (String line : inputStrings) {
            if (line.isEmpty()) break; // Stop once we reach the last line (the blank one)
            final String rowStr = line.substring(0, 7);
            final String colStr = line.substring(7, 10);

            final int currentSeatID = getPosition(rowStr, 0, 127) * 8 + getPosition(colStr, 0, 7);
            highestSeatID = Integer.max(highestSeatID, currentSeatID);
        }

        return highestSeatID;
    }

    private int solvePart2() {
        int highestSeatID = 0;

        for (String line : inputStrings) {

        }

        return highestSeatID;
    }

    public int solve() {
        return isDayOne ? solvePart1() : solvePart2();
    }
}
