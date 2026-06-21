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
	ExampleInput    = "exampleInput"
	PuzzleInput     = "puzzleInput"
	ExampleSolution = "exampleSolution"
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

	return strings.Split(string(rawFileContent), "\n")
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

	// TODO:
	// Solve example
	calculatedSolution := solutions.SolveDay1P1(getInput(ExampleInput))
	if calculatedSolution != actualExampleSolution {
		log.Fatal("Incorrectly calculated answer as ( ", calculatedSolution, " ) not ( ", actualExampleSolution, " )")
	}
	fmt.Println("Successfully solved example. Solving the big one now.")
	calculatedSolution = solutions.SolveDay1P1(getInput(PuzzleInput))
	fmt.Println("Solution:", calculatedSolution)
}
