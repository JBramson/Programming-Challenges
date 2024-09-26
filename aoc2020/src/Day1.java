/**
 * https://adventofcode.com/2020/day/1
 * Objective: Given a list of expenses,
 * Part 1: Find the two entries that sum to 2020 and return their product.
 * Part 2:
 */
import java.util.ArrayList;
import java.util.List;

public class Day1 {
    boolean inDebugMode;
    boolean isDayOne;
    List<String> inputStrings;

    Day1(boolean inDebugMode, boolean isDayOne, List<String> inputStrings) {
        this.inDebugMode = inDebugMode;
        this.isDayOne = isDayOne;
        this.inputStrings = inputStrings;
    }

    private int solve_part_1() {
        List<Integer> evens = new ArrayList<Integer>();
        List<Integer> odds = new ArrayList<Integer>();

        for (String line : inputStrings) {
            Integer nextNumber = Integer.parseInt(line);
            if (nextNumber % 2 == 0) {
                evens.add(nextNumber);
            } else {
                odds.add(nextNumber);
            }
        }

        // An even and an odd number cannot sum to an even number (2020), so we will only test them against each other.
        for (int i = 0; i < evens.size() - 1; i++) {
            for (int j = i + 1; j < evens.size(); j++) {
                int sum = evens.get(i) + evens.get(j);
                if (sum == 2020) {
                    return evens.get(i) * evens.get(j);
                }
            }
        }

        for (int i = 0; i < odds.size() - 1; i++) {
            for (int j = i + 1; j < odds.size(); j++) {
                int sum = odds.get(i) + odds.get(j);
                if (sum == 2020) {
                    return odds.get(i) * odds.get(j);
                }
            }
        }

        return -1; // If we get here, something's gone wrong.
    }

    private int solve_part_2() {


        return -1;
    }

    public int solve() {
        return isDayOne ? solve_part_1() : solve_part_2();
    }
}
