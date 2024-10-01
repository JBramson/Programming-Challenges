/**
 * https://adventofcode.com/2020/day/5
 * Objective: Given a list of directions for getting to one's seat,
 * Part 1: Find the highest seat ID.
 * Part 2: Find our seat ID, given that it's the only one missing from the list that has neighbors on both sides.
 */
import java.util.ArrayList;
import java.util.Collections;
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

    // @FLAG: Note: Part 2 won't work with the debug input, since it doesn't cover the whole plane (minus the ends).
    private int solvePart2() {
        List<Integer> filledSeatIDs = new ArrayList<>();

        for (String line : inputStrings) {
            if (line.isEmpty()) break; // Stop once we reach the last line (the blank one)
            final String rowStr = line.substring(0, 7);
            final String colStr = line.substring(7, 10);

            final int currentSeatID = getPosition(rowStr, 0, 127) * 8 + getPosition(colStr, 0, 7);
            filledSeatIDs.add(currentSeatID);
        }

        Collections.sort(filledSeatIDs); // Please don't shoot me for not doing it manually.
        for (int i = 0; i < filledSeatIDs.size() - 1; i++) {
            // If we skip over a number to get to the next ID, that skipped number is our ID.
            if (filledSeatIDs.get(i) + 1 != filledSeatIDs.get(i + 1)) {
                return filledSeatIDs.get(i) + 1;
            }
        }

        return 0;
    }

    public int solve() {
        return isDayOne ? solvePart1() : solvePart2();
    }
}
