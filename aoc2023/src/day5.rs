/*
* https://adventofcode.com/2023/day/5
* Objective: Given a list of seeds and an almanac of maps detailing ranges linking steps of farming,
* Part 1: Find the lowest final location number of any of the given seeds
* Part 2: Find the 
* Part of me learning Rust.
*/
use crate::helpers::RunMode;
use crate::helpers::PuzzlePart;

fn get_offset_location(start_loc: u32, destination_range_start: u32, source_range_start: u32, range: u32) -> Option<u32> {
    if start_loc < source_range_start || start_loc > source_range_start + range {
        None
    } else {
        let offset = start_loc - source_range_start;
        Some(destination_range_start + offset)
    }
    
}

// Any unmapped sections correspond to their own number
pub fn solve_part_1(input_strings: Vec<String>, run_mode: RunMode) -> Result<i32, String> {
    let mut lowest_location = u32::MAX;
    let mut relevant_locations: Vec<u32> = input_strings[0].split(": ").nth(1).unwrap().split(" ").map(|x| x.parse::<u32>().unwrap()).collect();
    let mut new_relevant_locations: Vec<u32>;
    let mut changed_locations: Vec<u32>;

    for line in &input_strings[3..] {
        if line.contains(":") {
            relevant_locations.retain(|&x| !changed_locations.contains(x));
        }
    }

    for relevant_location in relevant_locations {
        println!("{relevant_location}: {:?}", get_offset_location(relevant_location, 52, 50, 48));
    }

    Ok(lowest_location as i32)
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