import java.io.File;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;

// @FLAG NOTICE: Only the IS_DEBUG, IS_DAY_1, and Day* solution construction line should be edited.
public class Main {
    public static final boolean IS_DEBUG = false;
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

        Day3 solution = new Day3(IS_DEBUG, IS_DAY_1, inputLines);
        System.out.println("Result = " + solution.solve());
    }
}