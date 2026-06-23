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
