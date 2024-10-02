/**
 * https://adventofcode.com/2020/day/6
 * Objective: Given a list of lists of characters representing questions affirmed,
 * Part 1: Find the number of UNIQUE affirmations per group.
 * Part 2: Find the number of UNIVERSAL affirmations per group.
 */
import java.util.ArrayList;
import java.util.HashSet;
import java.util.List;

public class Day6 {
    boolean inDebugMode;
    boolean isDayOne;
    List<String> inputStrings;

    Day6(boolean inDebugMode, boolean isDayOne, List<String> inputStrings) {
        this.inDebugMode = inDebugMode;
        this.isDayOne = isDayOne;
        this.inputStrings = inputStrings;
        inputStrings.add(""); // Comment/uncomment this as needed between problems (if we need to end with a blank line)
    }

    private int solvePart1() {
        int totalAffirmations = 0;

        HashSet<Character> affirmedChars = new HashSet<>();
        for (String line : inputStrings) {
            // See if we're at the end of a data batch
            if (line.isEmpty()) {
                totalAffirmations += affirmedChars.size();
                affirmedChars.clear();
            } else {
                for (char c : line.toCharArray()) {
                    affirmedChars.add(c);
                }
            }
        }

        return totalAffirmations;
    }

    private boolean charInAll(final char c, final List<HashSet<Character>> customsForms) {
        for (HashSet<Character> customsForm : customsForms) {
            if (!customsForm.contains(c)) return false;
        }
        return true;
    }

    private int solvePart2() {
        int totalAffirmations = 0;

        List<HashSet<Character>> customsForms = new ArrayList<>();
        for (String line : inputStrings) {
            // See if we're at the end of a data batch
            if (line.isEmpty()) {
                // Pop the first line and check its chars against every other line
                HashSet<Character> charsToCheck = customsForms.removeFirst();
                for (char c : charsToCheck) {
                    if (charInAll(c, customsForms)) totalAffirmations++;
                }
                customsForms.clear();
            } else {
                // Put the current line into a new set
                HashSet<Character> customsForm = new HashSet<>();
                for (char c : line.toCharArray()) {
                    customsForm.add(c);
                }
                customsForms.add(customsForm);
            }
        }

        return totalAffirmations;
    }

    public int solve() {
        return isDayOne ? solvePart1() : solvePart2();
    }
}
