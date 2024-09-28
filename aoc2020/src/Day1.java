/**
 * https://adventofcode.com/2020/day/1
 * Objective: Given a list of expenses,
 * Part 1: Find the TWO entries that sum to 2020 and return their product.
 * Part 2: Find the THREE entries that sum to 2020 and return their product.
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

    private int solvePart1() {
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

        /*
         * An even and an odd number cannot sum to an even number (2020), so we will only test them against each other.
         */
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

    private int solvePart2() {
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

        /*
         * Now, it is possible for a mix, but only to combine a single even with two odds.
         * This means that if two given odds don't work with any evens, the combo can be discarded.
         * If none of these work, the solution must be three evens.
         */

        // Check the odd combos against the evens
        for (int i = 0; i < odds.size() - 1; i++) {
            for (int j = i + 1; j < odds.size(); j++) {
                int oddSum = odds.get(i) + odds.get(j);
                for (Integer evenNum : evens) {
                    if (oddSum + evenNum == 2020) {
                        return odds.get(i) * odds.get(j) * evenNum;
                    }
                }
            }
        }
        // Here, we're strong, independent evens who don't need no odd.
        for (int i = 0; i < evens.size() - 2; i++) {
            for (int j = 0; j < evens.size() - 1; j++) {
                int firstTwoSum = evens.get(i) + evens.get(j);
                if (firstTwoSum >= 2020) {
                    continue; // There aren't negative values; we don't need to check 2-combos exceeding 2019.
                }
                for (Integer thirdEven : evens) {
                    if (firstTwoSum + thirdEven == 2020) {
                        return evens.get(i) * evens.get(j) * thirdEven;
                    }
                }
            }
        }
        return -1;
    }

    public int solve() {
        return isDayOne ? solvePart1() : solvePart2();
    }
}
