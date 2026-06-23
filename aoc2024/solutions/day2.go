/*
 * https://adventofcode.com/2024/day/2
 * Objective: Given a series of reports of power levels,
 * Part 1: Return the number of reports in stable condition. (Successive values differ by 1 to 3, inclusive and are always increasing or always decreasing).
 * Part 2: The same as Part 1, but allow for a single value to be deleted.
 * Part of me learning Go.
 */
package solutions

import (
	"math"
	"strconv"
	"strings"
)

// Does what it says on the tin. Useful for getting into math mode.
func StringSliceToIntSlice(stringSlice []string) []int {
	intSlice := make([]int, len(stringSlice))
	for i, s := range stringSlice {
		intSlice[i], _ = strconv.Atoi(s)
	}
	return intSlice
}

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

func SolveDay2P1(lines []string) int {
	safeReportsCount := 0

	for _, line := range lines {
		levelsStrSlice := strings.Split(line, " ")
		levels := StringSliceToIntSlice(levelsStrSlice)
		if evaluateReport(levels) {
			safeReportsCount++
		}
	}

	return safeReportsCount
}

// Returns a copy of a given slice without the given index.
func removeIndexFromSlice(slice []int, index int) []int {
	newSlice := make([]int, len(slice))
	copy(newSlice, slice)
	// Doing the slice manipulation messes up the underlyiing pointer of the slice; they shouldn't be re-used, hence the copying above.
	return append(newSlice[:index], newSlice[index+1:]...)
}

// TODO: Keep general structure, but create versions with a missing value until successful or the end is reached.
func SolveDay2P2(lines []string) int {
	safeReportsCount := 0

	for _, line := range lines {
		levelsStrSlice := strings.Split(line, " ")
		levels := StringSliceToIntSlice(levelsStrSlice)
		if evaluateReport(levels) {
			safeReportsCount++
		} else {
			for i := range levels {
				// Cut a single value from report and try again
				levelsWithCutValue := removeIndexFromSlice(levels, i)
				if evaluateReport(levelsWithCutValue) {
					safeReportsCount++
					break
				}
			}
		}
	}

	return safeReportsCount
}
