/**
 * https://adventofcode.com/2020/day/7
 * Objective: Given a list of bag contents (other bags of different colors and marks),
 * Part 1: Find the number of possible starting bags that could contain (at some point) a shiny gold bag.
 * Part 2: Find the number of
 */
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

public class Day7 {
    boolean inDebugMode;
    boolean isDayOne;
    List<String> inputStrings;

    Day7(boolean inDebugMode, boolean isDayOne, List<String> inputStrings) {
        this.inDebugMode = inDebugMode;
        this.isDayOne = isDayOne;
        this.inputStrings = inputStrings;
//        inputStrings.add(""); // Comment/uncomment this as needed between problems (if we need to end with a blank line)
    }

    private int solvePart1() {
        int goldenPaths = 0;
        HashMap<String, String[]> bagContents = new HashMap<>();

        for (String line: inputStrings) {
            String[] bagContentInfo = line.split(" contain ");
            String holdingBagDescription = bagContentInfo[0].substring(0, bagContentInfo[0].length() - 1);

            if (bagContentInfo[1].equals("no other bags.")) {
                // Add leaf bags (that contain nothing)
                String[] emptyArray = {};
                bagContents.put(holdingBagDescription, emptyArray);
            } else {
                // Otherwise, split the items into an array and add the proper section to bagContents.
                bagContentInfo[1] = bagContentInfo[1].substring(0, bagContentInfo[1].length() - 1); // Before splitting, remove the period at the end.
                String[] bagContentStrArray = bagContentInfo[1].split(", ");
                for (int i = 0; i < bagContentStrArray.length; i++) {
                    if (bagContentStrArray[i].toCharArray()[bagContentStrArray[i].length() - 1] == 's') {
                        // Cut the extra 's' off, if present.
                        bagContentStrArray[i] = bagContentStrArray[i].substring(0, bagContentStrArray[i].length() - 1);
                    }
                    bagContentStrArray[i] = bagContentStrArray[i].substring(2);
                }
                bagContents.put(bagContentInfo[0].substring(0, bagContentInfo[0].length() - 1), bagContentStrArray);
            }
        }
        for (String key: bagContents.keySet()) {
            System.out.println(key + ": ");
            for (String containedBag : bagContents.get(key)) {
                System.out.println("\t" + containedBag);
            }
        }
        return goldenPaths;
    }

    private int solvePart2() {
        int goldenPaths = 0;



        return goldenPaths;
    }

    public int solve() {
        return isDayOne ? solvePart1() : solvePart2();
    }
}
