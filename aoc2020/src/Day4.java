/**
 * https://adventofcode.com/2020/day/4
 * Objective: Given a list of passports,
 * Part 1: Find the number missing no more than the "cid" field.
 * Part 2:
 */
import java.util.HashMap;
import java.util.List;

public class Day4 {
    boolean inDebugMode;
    boolean isDayOne;
    List<String> inputStrings;

    Day4(boolean inDebugMode, boolean isDayOne, List<String> inputStrings) {
        this.inDebugMode = inDebugMode;
        this.isDayOne = isDayOne;
        this.inputStrings = inputStrings;
    }

    private boolean isValidPassport(final HashMap<String, String> passport, final String[] requiredFields) {
        for (String requiredField : requiredFields) {
            if (!passport.containsKey(requiredField)) return false;
        }

        return true;
    }

    private int solvePart1() {
        int validPassportCount = 0;
        String[] mandatoryFields = {"byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"};

        HashMap<String, String> passport = new HashMap<>();
        for (String line : inputStrings) {
            // See if we're at the end of a data batch
            if (line.isEmpty()) {
                if (isValidPassport(passport, mandatoryFields)) validPassportCount++;
                passport.clear();
            } else {
                // Otherwise, split line by spaces, then for each, split it by ":", and add halves to the passport.
                for (String field : line.split(" ")) {
                    String[] fieldSplit = field.split(":");
                    passport.put(fieldSplit[0], fieldSplit[1]);
                }
            }
        }

        return validPassportCount;
    }

    private int solvePart2() {
        int validPassports = 0;


        return validPassports;
    }

    public int solve() {
        return isDayOne ? solvePart1() : solvePart2();
    }
}
