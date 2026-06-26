/*
 * https://adventofcode.com/2024/day/3
 * Objective: Given a series of jumbled characters containing hidden instructions,
 * Part 1: Find those of the form "mul(X,Y)" and return the sum of the products of the (X,Y) pairs.
 * Part 2:
 * Part of me learning Go.
 */
package solutions

import (
	"aoc2024/helpers"
	"fmt"
	"log"
	"strconv"
	"strings"
	"unicode"
)

func solveDay3P1(lines []string) int {
	productSum := 0
	mulInstructionStr := "mul(X,Y)"

	for _, line := range lines {
		nextMulInstructionIndex := 0
		numbersValid := false
		var firstNumberStrBuilder strings.Builder
		var secondNumberStrBuilder strings.Builder

		for _, c_rune := range line {
			c := string(c_rune)
			// When we're in number mode, we don't iterate as normal.
			if numbersValid {
				if unicode.IsDigit(c_rune) {
					if helpers.GetChar(mulInstructionStr, nextMulInstructionIndex) == "X" {
						firstNumberStrBuilder.WriteString(c)
					} else {
						secondNumberStrBuilder.WriteString(c)
					}
				} else if c == "," && firstNumberStrBuilder.Len() != 0 {
					// Move to the second number
					nextMulInstructionIndex += 2
					fmt.Println("First:", firstNumberStrBuilder.String())
				} else if c == ")" && secondNumberStrBuilder.Len() != 0 {
					// We're done with the nums
					fmt.Println("Second:", secondNumberStrBuilder.String())
					xValue, _ := strconv.Atoi(firstNumberStrBuilder.String())
					yValue, _ := strconv.Atoi(secondNumberStrBuilder.String())
					productSum += xValue * yValue
					fmt.Printf("Added %d*%d=%d to new total of %d.\n", xValue, yValue, xValue*yValue, productSum)
					// Reset values
					firstNumberStrBuilder.Reset()
					secondNumberStrBuilder.Reset()
					nextMulInstructionIndex = 0
					numbersValid = false
				} else {
					// The closing value is neither a ")" nor a number. Blast'em.
					// Reset values
					firstNumberStrBuilder.Reset()
					secondNumberStrBuilder.Reset()
					nextMulInstructionIndex = 0
					numbersValid = false
				}
				continue
			}
			// For generic, non-number values ( "mul(" )
			if c == helpers.GetChar(mulInstructionStr, nextMulInstructionIndex) {
				nextMulInstructionIndex++
				// If the next valid instruction is an "X" or "Y", we should be hunting numbers.
				if helpers.GetChar(mulInstructionStr, nextMulInstructionIndex) == "X" || helpers.GetChar(mulInstructionStr, nextMulInstructionIndex) == "Y" {
					numbersValid = true
				}
			}
		}
	}

	return productSum
}
func solveDay3P2(lines []string) int {
	return 0
}

func SolveDay3(exampleLines []string, puzzleLines []string, part int, exampleSolution int) int {
	var answer int

	// Solve for example
	if part == 1 {
		answer = solveDay3P1(exampleLines)
	} else {
		answer = solveDay3P2(exampleLines)
	}

	if answer != exampleSolution {
		log.Fatalln("Incorrectly calculated answer as ( ", answer, " ) not ( ", exampleSolution, " )")
	} else {
		fmt.Println("Successfully solved example. Solving the big one now.")
	}

	// If the example is correct, solve the puzzle
	if part == 1 {
		answer = solveDay3P1(puzzleLines)
	} else {
		answer = solveDay3P2(puzzleLines)
	}

	return answer
}
