package helpers

import "strconv"

// Does what it says on the tin. Useful for getting into math mode.
func StringSliceToIntSlice(stringSlice []string) []int {
	intSlice := make([]int, len(stringSlice))
	for i, s := range stringSlice {
		intSlice[i], _ = strconv.Atoi(s)
	}
	return intSlice
}

// Returns a copy of a given slice without the given index.
func RemoveIndexFromSlice(slice []int, index int) []int {
	newSlice := make([]int, len(slice))
	copy(newSlice, slice)
	// Doing the slice manipulation messes up the underlyiing pointer of the slice; they shouldn't be re-used, hence the copying above.
	return append(newSlice[:index], newSlice[index+1:]...)
}

// Returns the character from a given location of a string
func GetChar(s string, i int) string {
	return s[i : i+1]
}
