/*
 * https://adventofcode.com/2022/day/6
 * Objective: Given a character stream,
 * Part 1: Find the first character where it and the preceding 3 share no chars in common.
 * Part 2:
 * Part of me learning Rust.
 */
use crate::helpers::RunMode;

const SLICE_SIZE: usize = 4;

fn find_last_repeated_char_location(string: &str, end_char_location: usize) -> Option<usize> {
    for offset in 1..SLICE_SIZE { // TODO: This top-level search isn't set in stone
        // If we find the current last character in the given slice,
            // Find the location in the slice, and return it.
        // Otherwise, remove the last character and repeat, checking against the new last.

    }
    // If we can't find any repeats, we've found our answer!
    return None;
}

pub fn part_1_solution(input_strings: Vec<String>, run_mode: RunMode) -> String {
    let master_string: &str = input_strings.last().unwrap();

    let mut i = SLICE_SIZE - 1; // Check from the last character of the slice
    while i < master_string.len() {
        match find_last_repeated_char_location(master_string, i) {
            Some(x) => i = x + (SLICE_SIZE - 1),
            None => return (i + 1).to_string(),
        }
    }

    String::from("Didn't find a valid slice :(")
}

pub fn part_2_solution(input_strings: Vec<String>, run_mode: RunMode) -> String {
    let mut final_tops: String = String::from("");


    final_tops
}