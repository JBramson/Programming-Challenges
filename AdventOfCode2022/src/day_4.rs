/*
 * https://adventofcode.com/2022/day/2
 * Objective: Given a list of range pairs,
 * Part 1: Find the number of ranges entirely contained by their pair.
 * Part 2: Find the number of ranges partially contained by their pair.
 * Part of me learning Rust.
 */

fn is_subset_present(left_start: i32, left_end: i32, right_start: i32, right_end: i32) -> bool {
    // Check if left contains right
    if (left_start <= right_start) && (left_end >= right_end) {
        return true;
    }
    // Check if right contains left
    if (right_start <= left_start) && (right_end >= left_end) {
        return true;
    }

    false
}

fn is_overlap_present(left_start: i32, left_end: i32, right_start: i32, right_end: i32) -> bool {
    // Check if left's entirety is less or more than right
    if (left_end < right_start) || (left_start > right_end) {
        return false;
    }
    // Check if right's entirety is less or more than left
    if (right_end < left_start) || (right_start > left_end) {
        return false;
    }

    true
}

pub fn part_1_solution(input_strings: Vec<String>) -> i32 {
    let mut subset_count = 0;

    for line in input_strings {
        let vec: Vec<&str> = line.split(',').collect();
        let left_start:  i32 = vec[0].split('-').nth(0).unwrap().parse().unwrap();
        let left_end:    i32 = vec[0].split('-').nth(1).unwrap().parse().unwrap();
        let right_start: i32 = vec[1].split('-').nth(0).unwrap().parse().unwrap();
        let right_end:   i32 = vec[1].split('-').nth(1).unwrap().parse().unwrap();

        if is_subset_present(left_start, left_end, right_start, right_end) { subset_count += 1; }
    }

    subset_count
}

pub fn part_2_solution(input_strings: Vec<String>) -> i32 {
    let mut subset_count = 0;

    for line in input_strings {
        let vec: Vec<&str> = line.split(',').collect();
        let left_start:  i32 = vec[0].split('-').nth(0).unwrap().parse().unwrap();
        let left_end:    i32 = vec[0].split('-').nth(1).unwrap().parse().unwrap();
        let right_start: i32 = vec[1].split('-').nth(0).unwrap().parse().unwrap();
        let right_end:   i32 = vec[1].split('-').nth(1).unwrap().parse().unwrap();

        if is_overlap_present(left_start, left_end, right_start, right_end) { subset_count += 1; }
    }

    subset_count
}