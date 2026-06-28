/*
 * https://adventofcode.com/2024/day/3
 * Objective: Given a series of jumbled characters containing hidden instructions,
 * Part 1: Find those of the form "mul(X,Y)" and return the sum of the products of the (X,Y) pairs.
 * Part 2: Same as above, but checking for "do()" and "don't()" instructions to start/stop receiving instructions.
 * Part of me learning Go.
 */
package solutions

import (
	"fmt"
	"log"
	"regexp"
	"strconv"
	"strings"
)

func solveDay3P1(lines []string) int {
	productSum := 0
	mulRegex, _ := regexp.Compile(`mul\(\d+,\d+\)`)

	for _, line := range lines {
		// Get multiplcation instructions
		instructions := mulRegex.FindAllString(line, -1)

		// Do the splitting and multiplying
		for _, instruction := range instructions {
			numbers := strings.Split(instruction, ",")
			firstNumber, _ := strconv.Atoi(numbers[0][4:])
			secondNumber, _ := strconv.Atoi(numbers[1][:len(numbers[1])-1])

			productSum += firstNumber * secondNumber
		}
	}

	return productSum
}
func solveDay3P2(lines []string) int {
	productSum := 0
	instructionsRegex, _ := regexp.Compile(`mul\(\d+,\d+\)|do\(\)|don't\(\)`)
	inDoMode := true // We start adding until we receive the first "don't()" instruction, ACROSS INPUT LINES
	for _, line := range lines {
		// Get all instructions
		instructions := instructionsRegex.FindAllString(line, -1)
		var mulInstructions []string

		// Isolate the multiplication instructions
		for _, instruction := range instructions {
			switch instruction {
			case "do()":
				inDoMode = true
			case "don't()":
				inDoMode = false
			default:
				if inDoMode {
					mulInstructions = append(mulInstructions, instruction)
				}
			}
		}

		// Do the splitting and multiplying
		for _, instruction := range mulInstructions {
			numbers := strings.Split(instruction, ",")
			firstNumber, _ := strconv.Atoi(numbers[0][4:])
			secondNumber, _ := strconv.Atoi(numbers[1][:len(numbers[1])-1])

			productSum += firstNumber * secondNumber
		}
	}

	return productSum
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
