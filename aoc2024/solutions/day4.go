/*
 * https://adventofcode.com/2024/day/4
 * Objective: Given a grid of letters,
 * Part 1: Find the total number of times "XMAS" appears, be it "horizontal, vertical, diagonal, written backwards, or even overlapping other words."
 * Part 2:
 * Part of me learning Go.
 */
package solutions

import (
	"fmt"
	"log"
)

/*
 * Declare array of 8 bools representing directions to check. Positions start at North, rotating 45 degrees clockwise with each entry.
 * Too high, don't check North (7, 0, 1)
 * Too low, don't check South (3, 4, 5)
 * Too left, don't check East (5, 6, 7)
 * Too right, don't check West (1, 2, 3)
 */
func getSearchDirections(tooHigh bool, tooLow bool, tooLeft bool, tooRight bool) []bool {
	directionChecklist := make([]bool, 8)

	// Default to true and falsify with "tooX" arguments
	for i := range len(directionChecklist) {
		directionChecklist[i] = true
	}

	if tooHigh {
		directionChecklist[7] = false
		directionChecklist[0] = false
		directionChecklist[1] = false
	} else if tooLow {
		directionChecklist[3] = false
		directionChecklist[4] = false
		directionChecklist[5] = false
	}
	if tooLeft {
		directionChecklist[5] = false
		directionChecklist[6] = false
		directionChecklist[7] = false
	} else if tooRight {
		directionChecklist[1] = false
		directionChecklist[2] = false
		directionChecklist[3] = false
	}

	return directionChecklist
}

func solveDay4P1(lines []string) int {
	totalXmases := 0
	grid := make([][]rune, len(lines))
	xmasSlice := []rune{'X', 'M', 'A', 'S'}
	height := len(lines)
	length := len(lines[0])
	// Array of 8 bools representing directions to check. Positions start at North, rotating 45 degrees clockwise with each entry.
	directionChecklist := make([]bool, 8)

	// For each new column, check against the length and determine if we should be checking up/down.
	for lineNum, line := range lines {
		// Directions are checked by default and falsified if the range is insufficient.
		for i := range len(directionChecklist) {
			directionChecklist[i] = true
		}
		tooHigh, tooLow := false, false
		// Vertical directions are checked once per line.
		if lineNum-3 < 0 {
			tooHigh = true
		} else if lineNum+3 > height {
			tooLow = true
		}
		// fmt.Println(lineNum, line, directionChecklist)
		for letterNum, letter := range line {
			if letter != xmasSlice[0] {
				// Skip non-`X`s.
				continue
			}
			tooLeft, tooRight := false, false
			// Horizontal directions are checked once per letter.
			if letterNum-3 < 0 {
				tooLeft = true
			} else if letterNum+3 > length {
				tooRight = true
			}
			directionChecklist = getSearchDirections(tooHigh, tooLow, tooLeft, tooRight)
		}

	}
	// For each new row, check against the width and determine if we should be checking right/left.
	// Check if we're starting a new "XMAS".
	// If we are, check every valid direction until we hit a bad value or finish the "XMAS".
	// Increment for each finish, then continue.

	for i, line := range lines {
		grid[i] = []rune(line)
	}

	return totalXmases
}
func solveDay4P2(lines []string) int {
	totalXmases := 0
	return totalXmases
}

func SolveDay4(exampleLines []string, puzzleLines []string, part int, exampleSolution int) int {
	var answer int

	// Solve for example
	if part == 1 {
		answer = solveDay4P1(exampleLines)
	} else {
		answer = solveDay4P2(exampleLines)
	}

	if answer != exampleSolution {
		log.Fatalln("Incorrectly calculated answer as ( ", answer, " ) not ( ", exampleSolution, " )")
	} else {
		fmt.Println("Successfully solved example. Solving the big one now.")
	}

	// If the example is correct, solve the puzzle
	if part == 1 {
		answer = solveDay4P1(puzzleLines)
	} else {
		answer = solveDay4P2(puzzleLines)
	}

	return answer
}
