/*
 * https://adventofcode.com/2024/day/1
 * Objective: Given a list of locations (int pairs),
 * Part 1: Find the sum of the differences when each row's differnces are counted up, from lowest to highest.
 * Part 2:
 * Part of me learning Go.
 */
package solutions

import (
	"log"
	"math"
	"slices"
	"strconv"
	"strings"
)

const locationSpacing = "   "

func SolveDay1P1(lines []string) int {
	var firstPositions []int
	var secondPositions []int
	totalDistance := 0

	for _, line := range lines {
		locationPair := strings.Split(line, locationSpacing)

		firstValue, err := strconv.Atoi(locationPair[0])
		if err != nil {
			log.Fatal("Couldn't parse first value. err:", err)
		}
		firstPositions = append(firstPositions, firstValue)
		secondValue, err := strconv.Atoi(locationPair[1])
		if err != nil {
			log.Fatal("Couldn't parse second value. err:", err)
		}
		secondPositions = append(secondPositions, secondValue)
	}

	// Put both lists in ascending order to minimize distances
	slices.Sort(firstPositions)
	slices.Sort(secondPositions)

	for i := 0; i < len(lines); i++ {
		totalDistance += int(math.Abs(float64(firstPositions[i] - secondPositions[i])))
	}

	return totalDistance
}
