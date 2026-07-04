/*
 * https://adventofcode.com/2024/day/4
 * Objective: Given a grid of letters,
 * Part 1: Find the total number of times "XMAS" appears, be it "horizontal, vertical, diagonal, written backwards, or even overlapping other words."
 * Part 2: Find the total number of times "MAS" appears in an "X" shape.
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
 * Too left, don't check West (5, 6, 7)
 * Too right, don't check East (1, 2, 3)
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
	for lineNum, line := range lines {
		// Add line to grid (must be done before checks)
		grid[lineNum] = []rune(line)
	}

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
		} else if lineNum+3 >= height {
			tooLow = true
		}
		for letterNum, letter := range line {
			if letter != xmasSlice[0] {
				// Skip non-`X`s.
				continue
			}
			tooLeft, tooRight := false, false
			// Horizontal directions are checked once per letter.
			if letterNum-3 < 0 {
				tooLeft = true
			} else if letterNum+3 >= length {
				tooRight = true
			}
			directionChecklist = getSearchDirections(tooHigh, tooLow, tooLeft, tooRight)

			for directionNumber, shouldCheck := range directionChecklist {
				if !shouldCheck {
					continue
				}
				switch directionNumber {
				case 0: // North
					if grid[lineNum-1][letterNum] == xmasSlice[1] && grid[lineNum-2][letterNum] == xmasSlice[2] && grid[lineNum-3][letterNum] == xmasSlice[3] {
						totalXmases++
					}
				case 1: // North-East
					if grid[lineNum-1][letterNum+1] == xmasSlice[1] && grid[lineNum-2][letterNum+2] == xmasSlice[2] && grid[lineNum-3][letterNum+3] == xmasSlice[3] {
						totalXmases++
					}
				case 2: // East
					if grid[lineNum][letterNum+1] == xmasSlice[1] && grid[lineNum][letterNum+2] == xmasSlice[2] && grid[lineNum][letterNum+3] == xmasSlice[3] {
						totalXmases++
					}
				case 3: //South-East
					if grid[lineNum+1][letterNum+1] == xmasSlice[1] && grid[lineNum+2][letterNum+2] == xmasSlice[2] && grid[lineNum+3][letterNum+3] == xmasSlice[3] {
						totalXmases++
					}
				case 4: // South
					if grid[lineNum+1][letterNum] == xmasSlice[1] && grid[lineNum+2][letterNum] == xmasSlice[2] && grid[lineNum+3][letterNum] == xmasSlice[3] {
						totalXmases++
					}
				case 5: // South-West
					if grid[lineNum+1][letterNum-1] == xmasSlice[1] && grid[lineNum+2][letterNum-2] == xmasSlice[2] && grid[lineNum+3][letterNum-3] == xmasSlice[3] {
						totalXmases++
					}
				case 6: // West
					if grid[lineNum][letterNum-1] == xmasSlice[1] && grid[lineNum][letterNum-2] == xmasSlice[2] && grid[lineNum][letterNum-3] == xmasSlice[3] {
						totalXmases++
					}
				case 7: // North-West
					if grid[lineNum-1][letterNum-1] == xmasSlice[1] && grid[lineNum-2][letterNum-2] == xmasSlice[2] && grid[lineNum-3][letterNum-3] == xmasSlice[3] {
						totalXmases++
					}
				}
			}
		}

	}

	return totalXmases
}
func solveDay4P2(lines []string) int {
	totalXmases := 0
	grid := make([][]rune, len(lines))
	xmasSlice := []rune{'M', 'A', 'S'}
	height := len(lines)
	length := len(lines[0])
	// Array of 4 bools representing directions to check. Positions start at North-East, rotating 90 degrees clockwise with each entry.
	directionChecklist := make([]bool, 4)
	for lineNum, line := range lines {
		// Add line to grid (must be done before checks)
		grid[lineNum] = []rune(line)
	}

	// For each new column, check against the length and determine if we should be checking up/down.
	for lineNum, line := range lines {
		// Directions are checked by default and falsified if the range is insufficient.
		for i := range len(directionChecklist) {
			directionChecklist[i] = true
		}
		// Vertical directions are checked once per line.
		if lineNum == 0 || lineNum == height-1 {
			continue
		}
		for letterNum, letter := range line {
			if letter != xmasSlice[1] {
				// Skip non-`A`s.
				continue
			}
			// Horizontal directions are checked once per letter.
			if letterNum == 0 || letterNum == length-1 {
				continue
			}

			fmt.Println("Checking line ", lineNum, " letter ", letterNum, " for XMAS.")

			// Check forward-slash side of the X.
			if grid[lineNum-1][letterNum+1] == xmasSlice[0] {
				// Upper-Right is `M`
				if grid[lineNum+1][letterNum-1] != xmasSlice[2] {
					// Bottom-right is NOT `S`
					continue
				}
			} else if grid[lineNum-1][letterNum+1] == xmasSlice[2] {
				// Upper-Right is `S`
				if grid[lineNum+1][letterNum-1] != xmasSlice[0] {
					// Bottom-right is NOT `M`
					continue
				}
			} else {
				// Upper-Right is neither `M` nor `S`
				continue
			}

			// Check back-slash side of the X.
			if grid[lineNum-1][letterNum-1] == xmasSlice[0] {
				// Upper-Left is `M`
				if grid[lineNum+1][letterNum+1] != xmasSlice[2] {
					// Bottom-left is NOT `S`
					continue
				}
			} else if grid[lineNum-1][letterNum-1] == xmasSlice[2] {
				// Upper-Left is `S`
				if grid[lineNum+1][letterNum+1] != xmasSlice[0] {
					// Bottom-left is NOT `M`
					continue
				}
			} else {
				// Upper-Left is neither `M` nor `S`
				continue
			}

			totalXmases++
		}

	}

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
