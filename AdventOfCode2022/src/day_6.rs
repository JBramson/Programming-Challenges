/*
 * https://adventofcode.com/2022/day/6
 * Objective: Given a character stream,
 * Part 1: Find the first character where it and the preceding 3 share no chars in common.
 * Part 2:
 * Part of me learning Rust.
 */
use crate::helpers::RunMode;

const MAX_SLICE_SIZE_PART_1: usize = 4;

fn find_last_repeated_char_location(string: &str, end_char_location: usize, max_slice_size: usize) -> Option<usize> {
    for end_offset in 0..(max_slice_size - 1) {
        // If we find the current last character in the given slice,
            // Return the later index as the new slice start
        // Otherwise, remove the last character and repeat, checking against the new last.
        for offset in (end_offset + 1)..max_slice_size {
            if string.chars().nth(end_char_location - offset) == string.chars().nth(end_char_location - end_offset) {
                return Option::from((end_char_location - offset));
            }
        }

    }
    // If we can't find any repeats, we've found our answer!
    return None;
}

pub fn part_1_solution(input_strings: Vec<String>, run_mode: RunMode) -> String {
    let master_string: &str = input_strings.last().unwrap();

    let mut i = MAX_SLICE_SIZE_PART_1 - 1; // Check from the last character of the slice
    while i < master_string.len() {
        match find_last_repeated_char_location(master_string, i, MAX_SLICE_SIZE_PART_1) {
            Some(x) => i = x + MAX_SLICE_SIZE_PART_1,
            None => return (i + 1).to_string(),
        }
    }

    String::from("Didn't find a valid slice :(")
}

pub fn part_2_solution(input_strings: Vec<String>, run_mode: RunMode) -> String {
    let mut final_tops: String = String::from("");


    final_tops
}