/*
 * Advent of Code 2024 (aoc2024) is a series of advent calendars that release a pair of programming problems instead of candy each Christmas season. These are my solutions, as part of learning Go.
 * This main.go file has been designed to require minimal changes when solving different problems. The only necessary changes at the moment are:
 * -> Changing the functions called towards the bottom (solutions.SolveDayXXPY)
 * -> Updating the exampleSolution file with the second daily problem's value.
 * Ideas for general improvement:
 * -> Add helper functions in a new file to support file working, allowing for the different problem parts to source them seperately.
 */
package main

import (
	"aoc2024/solutions"
	"fmt"
	"log"
	"os"
	"strconv"
	"strings"
)

const (
	InputDir        = "input/"
	ExampleInput    = "exampleInput.txt"
	PuzzleInput     = "puzzleInput.txt"
	ExampleSolution = "exampleSolution.txt"
)

var files = [...]string{ExampleInput, PuzzleInput, ExampleSolution}

// Checks to see if input files are set up. Will attempt to create dir/empty files if they don't exist.
// Returns true if they're already ready and false if they aren't. Also returns a string of the files dir.
func handleFileSetup() (bool, string) {
	fileStatusIsPerfect := true
	inputFolderFullPath, _ := os.Getwd()
	// Create dir if not present
	if _, err := os.Stat(InputDir); err != nil {
		fileStatusIsPerfect = false
		os.Mkdir(InputDir, os.ModePerm)
	}
	// Create files if not present
	for _, fileStr := range files {
		if _, err := os.Stat(InputDir + fileStr); err != nil {
			fileStatusIsPerfect = false
			_, err = os.Create(InputDir + fileStr)
			if err != nil {
				log.Fatal("Could not get full input folder dir. Error:", err)
			}
		}
	}

	return fileStatusIsPerfect, inputFolderFullPath
}

func getInput(inputFileStr string) []string {
	rawFileContent, err := os.ReadFile(InputDir + inputFileStr)
	if err != nil {
		log.Fatal("Couldn't read ", inputFileStr, "err:", err)
	}

	lines := strings.Split(string(rawFileContent), "\n")

	// Trim empty lines at bottom
	for lines[len(lines)-1] == "" {
		lines = lines[:len(lines)-1]
	}

	return lines
}

func main() {
	inputsReady, inputDirLocation := handleFileSetup() // Set up files first
	if !inputsReady {
		fmt.Println("Blank files have been created at", inputDirLocation)
		return
	}

	actualExampleSolution, err := strconv.Atoi(getInput(ExampleSolution)[0])
	if err != nil {
		log.Fatal("Couldn't convert example solution to int. err:", err)
	}

	// Solve Example
	calculatedSolution := solutions.SolveDay2P1(getInput(ExampleInput)) // @FLAG: Function must be changed
	if calculatedSolution != actualExampleSolution {
		log.Fatal("Incorrectly calculated answer as ( ", calculatedSolution, " ) not ( ", actualExampleSolution, " )")
	}
	fmt.Println("Successfully solved example. Solving the big one now.")
	// Solve Puzzle
	calculatedSolution = solutions.SolveDay2P1(getInput(PuzzleInput)) // @FLAG: Function must be changed
	fmt.Println("Solution:", calculatedSolution)
}
