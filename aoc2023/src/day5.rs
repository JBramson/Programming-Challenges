/*
* https://adventofcode.com/2023/day/5
* Objective: Given a list of seeds and an almanac of maps detailing ranges linking steps of farming,
* Part 1: Find the lowest final location number of any of the given seeds
* Part 2: Find the 
* Part of me learning Rust.
*/
use crate::helpers::RunMode;
use crate::helpers::PuzzlePart;

// Any unmapped sections correspond to their own number
pub fn solve_part_1(input_strings: Vec<String>, run_mode: RunMode) -> Result<i32, String> {
    let mut points = 0;

    
    

    Ok(points as i32)
}

pub fn solve_part_2(input_strings: Vec<String>, run_mode: RunMode) -> Result<i32, String> {
    let mut points = 0;
    

    Ok(points as i32)
}

pub fn solve(input_strings: Vec<String>, run_mode: RunMode, puzzle_part: PuzzlePart) -> Result<i32, String> {
    match puzzle_part {
        PuzzlePart::PartOne => solve_part_1(input_strings, run_mode),
        PuzzlePart::PartTwo => solve_part_2(input_strings, run_mode),
    }
}