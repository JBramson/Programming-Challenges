/*
 * Advent of Code 2024 (aoc2024) is a series of advent calendars that release a pair of programming problems instead of candy each Christmas season. These are my solutions, as part of learning Go.
 * This main.go file has been designed to require minimal changes when solving different problems. The only necessary changes at the moment are:
 * -> Changing the Solve function called towards the bottom (solutions.SolveDayXX)
 */
package main

import (
	"aoc2024/solutions"
	"flag"
	"fmt"
	"log"
	"os"
	"strings"
)

const (
	InputDir               = "input/"
	ExampleInput           = "exampleInput.txt"
	PuzzleInput            = "puzzleInput.txt"
	DefaultPart            = 1
	DefaultExampleSolution = -1
)

var files = [...]string{ExampleInput, PuzzleInput}

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
	partPtr := flag.Int("part", DefaultPart, "The part (of the given puzzle) to be solved")
	exampleSolutionPtr := flag.Int("exampleSolution", DefaultExampleSolution, "The solution to the given part's example")
	flag.Parse()

	if *partPtr != 1 && *partPtr != 2 {
		log.Fatalln("ERROR: -part must be 1 or 2, but received (", *partPtr, ")")
	}
	if *exampleSolutionPtr == DefaultExampleSolution {
		log.Fatalln("ERROR: -exampleSolution must be given.")
	}

	inputsReady, inputDirLocation := handleFileSetup() // Set up files first
	if !inputsReady {
		fmt.Println("Blank files have been created at", inputDirLocation)
		return
	}

	// Solve Example
	calculatedSolution := solutions.SolveDay2(getInput(ExampleInput), getInput(PuzzleInput), *partPtr, *exampleSolutionPtr) // @FLAG: Function must be changed based on the day.
	fmt.Println("Solution:", calculatedSolution)
}
