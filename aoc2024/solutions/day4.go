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

func solveDay4P1(lines []string) int {
	totalXmases := 0
	grid := make([][]rune, len(lines))
	xmasSlice := []rune{'X', 'M', 'A', 'S'}
	// Declare array of 8 bools representing directions to check, defaulting to true
	// Get the grid's width and length, determining the checking limits for each

	// For each new column, check against the length and determine if we should be checking up/down.
	// For each new row, check against the width and determine if we should be checking right/left.
	// Check if we're starting a new "XMAS".
	// If we are, check every valid direction until we hit a bad value or finish the "XMAS".
	// Increment for each finish, then continue.

	for i, line := range lines {
		grid[i] = []rune(line)
	}

	fmt.Println(grid)

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
