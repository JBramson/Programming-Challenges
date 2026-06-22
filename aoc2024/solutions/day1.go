/*
 * https://adventofcode.com/2024/day/1
 * Objective: Given a list of locations (int pairs),
 * Part 1: Find the sum of the differences when each row's differnces are counted up, from lowest to highest.
 * Part 2: find the similarity score betweent he two lists (each number in the first list multiplied by the number of times it appears in the second list). Numbers from the first list can be repeated.
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

func SolveDay1P1(lines []string) int {
	const locationSpacing = "   "
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

	for i := range lines {
		totalDistance += int(math.Abs(float64(firstPositions[i] - secondPositions[i])))
	}

	return totalDistance
}
func SolveDay1P2(lines []string) int {
	const locationSpacing = "   "
	var firstPositions []int
	var secondPositions []int
	totalSimilarityScore := 0

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

	// Put both lists in ascending order to allow for more efficient comparisons
	slices.Sort(firstPositions)
	slices.Sort(secondPositions)

	for _, firstLocation := range firstPositions {
		localSimilarityScore := 0
		for _, secondLocation := range secondPositions {
			if firstLocation < secondLocation {
				// Since the secondLocations will only grow larger in this section of the loop, once they're bigger than the first location, we can stop looking.
				break
			} else if firstLocation == secondLocation {
				localSimilarityScore += firstLocation
			}
		}
		totalSimilarityScore += localSimilarityScore
	}

	return totalSimilarityScore
}
