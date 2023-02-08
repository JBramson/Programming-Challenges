/*
 * https://adventofcode.com/2022/day/2
 * Objective: Given a list of range pairs,
 * Part 1: Find the number of ranges entirely contained by their pair.
 * Part 2:
 * Part of me learning Rust.
 */

pub fn part_1_solution(input_strings: Vec<String>) -> i32 {
    let mut subset_count = 0;

    for line in input_strings {
        let vec: Vec<&str> = line.split(',').collect();
        let left_start:  i32 = vec[0].split('-').nth(0).unwrap().parse().unwrap();
        let left_end:    i32 = vec[0].split('-').nth(1).unwrap().parse().unwrap();
        let right_start: i32 = vec[1].split('-').nth(0).unwrap().parse().unwrap();
        let right_end:   i32 = vec[1].split('-').nth(1).unwrap().parse().unwrap();
        println!("{}, {} - {}, {}", left_start, left_end, right_start, right_end);
    }

    -1
}

pub fn part_2_solution(input_strings: Vec<String>) -> i32 {
    -1
}