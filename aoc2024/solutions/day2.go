/*
 * https://adventofcode.com/2024/day/2
 * Objective: Given a series of reports of power levels,
 * Part 1: Return the number of reports in stable condition. (Successive values differ by 1 to 3, inclusive and are always increasing or always decreasing).
 * Part 2:
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

func SolveDay2P1(lines []string) int {
	safeReportsCount := 0

	for _, line := range lines {
		levelsStrSlice := strings.Split(line, " ")
		levels := StringSliceToIntSlice(levelsStrSlice)
		for i := 0; i < len(levels)-1; i++ {
			absoluteValueDiff := math.Abs(float64(levels[i] - levels[i+1]))
			// Ensure values are the proper distance
			if absoluteValueDiff == 0 || absoluteValueDiff > 3 {
				// This report is unsafe. Counter the incoming incrementation and skip to the next one.
				safeReportsCount--
				break
			}
			if i == 0 {
				continue
			}
			// Ensure values are moving in the same direction (middle values aren't local peaks or troughs). This is impossible (and unnecessary) to check for the first value.
			if (levels[i] > levels[i-1] && levels[i] > levels[i+1]) || (levels[i] < levels[i-1] && levels[i] < levels[i+1]) {
				// This report is unsafe. Counter the incoming incrementation and skip to the next one.
				safeReportsCount--
				break
			}
		}
		safeReportsCount++
	}

	return safeReportsCount
}
func SolveDay2P2(lines []string) int {

	return 0
}
