import java.io.File;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;

// @FLAG NOTICE: Only the IS_DEBUG, IS_DAY_1, and Day* solution construction line should be edited.
public class Main {
    public static final boolean IS_DEBUG = true;
    public static final boolean IS_DAY_1 = false;

    public static void main(String[] args) throws IOException {
        String inputFileStr = IS_DEBUG ? "input/debug_input.txt" : "input/deployment_input.txt";
        File inputFile = new File(inputFileStr);

        inputFile.getParentFile().mkdirs();
        try {
            if (inputFile.createNewFile()) {
                System.out.println("Input file " + inputFile + " created. Please put the input in there and restart.");
                return;
            }
        } catch (IOException e) {
            throw new RuntimeException(e);
        }
        Scanner sc = new Scanner(inputFile);
        List<String> inputLines = new ArrayList<>();
        while (sc.hasNextLine()) {
            inputLines.add(sc.nextLine());
        }

        // Some solutions check for empty lines at the end. By default, we assume a blank line is unneeded.
        // We'll remove them here and add one in if needed in the constructor. (Uncomment the last line)
        while (inputLines.getLast().isBlank()) {
            inputLines.removeLast();
        }

        Day6 solution = new Day6(IS_DEBUG, IS_DAY_1, inputLines);
        System.out.println("Result = " + solution.solve());
    }
}