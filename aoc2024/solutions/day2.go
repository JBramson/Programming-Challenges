/*
 * https://adventofcode.com/2024/day/2
 * Objective: Given a series of reports of power levels,
 * Part 1: Return the number of reports in stable condition. (Successive values differ by 1 to 3, inclusive and are always increasing or always decreasing).
 * Part 2: The same as Part 1, but allow for a single value to be deleted.
 * Part of me learning Go.
 */
package solutions

import (
	"aoc2024/helpers"
	"fmt"
	"log"
	"math"
	"strings"
)

func evaluateReport(levels []int) bool {
	for i := 0; i < len(levels)-1; i++ {
		absoluteValueDiff := math.Abs(float64(levels[i] - levels[i+1]))
		// Ensure values are the proper distance
		if absoluteValueDiff == 0 || absoluteValueDiff > 3 {
			// This report is unsafe. Counter the incoming incrementation and skip to the next one.
			return false
		}
		if i == 0 {
			continue
		}
		// Ensure values are moving in the same direction (middle values aren't local peaks or troughs). This is impossible (and unnecessary) to check for the first value.
		if (levels[i] > levels[i-1] && levels[i] > levels[i+1]) || (levels[i] < levels[i-1] && levels[i] < levels[i+1]) {
			// This report is unsafe. Counter the incoming incrementation and skip to the next one.
			return false
		}
	}

	return true
}

func solveDay2P1(lines []string) int {
	safeReportsCount := 0

	for _, line := range lines {
		levelsStrSlice := strings.Split(line, " ")
		levels := helpers.StringSliceToIntSlice(levelsStrSlice)
		if evaluateReport(levels) {
			safeReportsCount++
		}
	}

	return safeReportsCount
}

func solveDay2P2(lines []string) int {
	safeReportsCount := 0

	for _, line := range lines {
		levelsStrSlice := strings.Split(line, " ")
		levels := helpers.StringSliceToIntSlice(levelsStrSlice)
		if evaluateReport(levels) {
			safeReportsCount++
		} else {
			for i := range levels {
				// Cut a single value from report and try again
				levelsWithCutValue := helpers.RemoveIndexFromSlice(levels, i)
				if evaluateReport(levelsWithCutValue) {
					safeReportsCount++
					break
				}
			}
		}
	}

	return safeReportsCount
}

func SolveDay2(exampleLines []string, puzzleLines []string, part int, exampleSolution int) int {
	var answer int

	// Solve for example
	if part == 1 {
		answer = solveDay2P1(exampleLines)
	} else {
		answer = solveDay2P2(exampleLines)
	}

	if answer != exampleSolution {
		log.Fatalln("Incorrectly calculated answer as ( ", answer, " ) not ( ", exampleSolution, " )")
	} else {
		fmt.Println("Successfully solved example. Solving the big one now.")
	}

	// If the example is correct, solve the puzzle
	if part == 1 {
		answer = solveDay2P1(puzzleLines)
	} else {
		answer = solveDay2P2(puzzleLines)
	}

	return answer
}
