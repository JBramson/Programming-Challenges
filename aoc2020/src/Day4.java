/**
 * https://adventofcode.com/2020/day/4
 * Objective: Given a list of passports,
 * Part 1: Find the number missing no more than the "cid" field.
 * Part 2: Find the number of valid passports (meeting new criteria)
 */
import java.util.Arrays;
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
        String[] VALID_EYE_COLORS = {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"};
        for (String requiredField : requiredFields) {
            if (!passport.containsKey(requiredField)) return false;
            /**
             * NOTE: There is no difference between solvePart1() and solvePart2()-
             * the only difference is in this function, checking the given values for each key.
             */
            if (!isDayOne) {
                try {
                    switch (requiredField) {
                        case "byr":
                            final int birthYear = Integer.parseInt(passport.get("byr"));
                            if (birthYear < 1920 || birthYear > 2002) return false;
                            break;
                        case "iyr":
                            final int issueYear = Integer.parseInt(passport.get("iyr"));
                            if (issueYear < 2010 || issueYear > 2020) return false;
                            break;
                        case "eyr":
                            final int expirationYear = Integer.parseInt(passport.get("eyr"));
                            if (expirationYear < 2020 || expirationYear > 2030) return false;
                            break;
                        case "hgt":
                            final int height = Integer.parseInt(passport.get("hgt").substring(0, passport.get("hgt").length() - 2));
                            if (passport.get("hgt").contains("cm")) {
                                if (height < 150 || height > 193) return false;
                            } else {
                                if (height < 59 || height > 76) return false;
                            }
                            break;
                        case "hcl":
                            final String hairColor = passport.get("hcl");
                            if (hairColor.charAt(0) != '#' || hairColor.length() != 7) return false;

                            for (char c : hairColor.substring(1, hairColor.length() - 1).toCharArray()) {
                                if (!Character.isLetterOrDigit(c)) return false;
                            }
                            break;
                        case "ecl":
                            final String eyeColor = passport.get("ecl");
                            if (!Arrays.asList(VALID_EYE_COLORS).contains(eyeColor)) return false;
                            break;
                        case "pid":
                            final String passportID = passport.get("pid");
                            Integer.parseInt(passportID); // Check if numeric. Can throw NumberFormatException
                            if (passportID.length() != 9) return false;
                            break;
                    }
                } catch(NumberFormatException e){
                    return false; // This should happen if someone tries to pass a letter as a number.
                }
            }
        }

        return true;
    }

    private int solvePart1() {
        int validPassportCount = 0;
        final String[] MANDATORY_FIELDS = {"byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"};

        HashMap<String, String> passport = new HashMap<>();
        for (String line : inputStrings) {
            // See if we're at the end of a data batch
            if (line.isEmpty()) {
                if (isValidPassport(passport, MANDATORY_FIELDS)) validPassportCount++;
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
        int validPassportCount = 0;
        final String[] MANDATORY_FIELDS = {"byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"};

        HashMap<String, String> passport = new HashMap<>();
        for (String line : inputStrings) {
            // See if we're at the end of a data batch
            if (line.isEmpty()) {
                if (isValidPassport(passport, MANDATORY_FIELDS)) validPassportCount++;
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

    public int solve() {
        return isDayOne ? solvePart1() : solvePart2();
    }
}
