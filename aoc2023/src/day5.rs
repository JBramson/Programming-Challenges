use std::cmp::min;
use std::cmp::max;

/*
* https://adventofcode.com/2023/day/5
* Objective: Given a list of seeds and an almanac of maps detailing ranges linking steps of farming,
* Part 1: Find the lowest final location number of any of the given seeds
* Part 2: Do the same as above, with a list of seed ranges instead of being given seeds directly
* Part of me learning Rust.
*/
use crate::helpers::RunMode;
use crate::helpers::PuzzlePart;

fn get_offset_location(start_loc: i32, destination_range_start: i32, source_range_start: i32, range: i32) -> Option<i32> {
    if start_loc < source_range_start || start_loc > source_range_start + range - 1 { // The -1 is required to remove end-range hits
        None
    } else {
        let offset = start_loc - source_range_start;
        Some(destination_range_start + offset)
    }
}

// Range is inclusive ([start, end])
#[derive(Debug, PartialEq)]
struct Range {
    start: i32,
    end: i32,
}

impl Range {
    fn update_start(&mut self, new_start: i32) {
        self.start = new_start;
    }

    fn update_end(&mut self, new_end: i32) {
        self.end = new_end;
    }
}

// Any unmapped sections correspond to their own number
pub fn solve_part_1(input_strings: Vec<String>, run_mode: RunMode) -> Result<i32, String> {
    let mut relevant_locations: Vec<i32> = input_strings[0].split(": ").nth(1).unwrap().split(" ").map(|x| x.parse::<i32>().unwrap()).collect();
    let mut new_locations: Vec<i32> = vec![];
    let mut changed_locations: Vec<i32> = vec![];

    for line in &input_strings[3..] {
        if line.is_empty() {
            continue;
        } else if line.contains(":") {
            relevant_locations.retain(|x| !changed_locations.contains(x));
            relevant_locations.append(&mut new_locations);
            changed_locations.clear();
            new_locations.clear();
            continue;
        }
        
        let almanac_entry: Vec<i32> = line.split(" ").map(|x| x.parse::<i32>().unwrap()).collect();
        for relevant_location in &relevant_locations {
            match get_offset_location(*relevant_location, almanac_entry[0], almanac_entry[1], almanac_entry[2]) {
                Some(new_location) => {
                    changed_locations.push(*relevant_location);
                    new_locations.push(new_location);
                },
                _ => {},
            }
        }
    }

    // Run the cleanup stuff afterwards due to lack of ending colon line
    relevant_locations.retain(|x| !changed_locations.contains(x));
    relevant_locations.append(&mut new_locations);
    changed_locations.clear();
    new_locations.clear();

    Ok(*relevant_locations.iter().min().unwrap() as i32)
}

pub fn solve_part_2(input_strings: Vec<String>, run_mode: RunMode) -> Result<i32, String> {
    let starting_line: Vec<i32> = input_strings[0].split(": ").nth(1).unwrap().split(" ").map(|x| x.parse::<i32>().unwrap()).collect();

    let mut relevant_ranges: Vec<Range> = vec![];
    let mut starts: Vec<i32> = vec![];
    let mut lengths: Vec<i32> = vec![];

    for (index, value) in starting_line.iter().enumerate() {
        if index % 2 == 0 {
            starts.push(*value);
        } else {
            lengths.push(*value);
        }
    }

    for i in 0..starts.len() {
        let new_range = Range{start: starts[i], end: starts[i] + lengths[i] - 1};
        relevant_ranges.push(new_range);

    }

    println!("current_ranges.len()={}", relevant_ranges.len());
    println!("current_ranges={:?}", relevant_ranges);
    

    /*
     * TODO: This is going to have to go by ranges and holes. We'll start with a range of values for the seeds with no holes.
     * For each step, see if there's a subrange with changed values.
     * If there are, add a new, shifted range of the updated values and add a hole range with the old values (AFTER ALL CHECKS OF THE ROUND).
     * During future checks, delete hits from hole ranges (if 10-19 is a hole, 1-30 becomes 1-9 and 20-30).
     */

    let mut new_ranges: Vec<Range> = vec![];

    for line in &input_strings[3..] {
        if line.is_empty() {
            continue;
        } else if line.contains(":") {
            relevant_ranges.append(&mut new_ranges);
            new_ranges.clear();
            println!("\n\n{:?}", relevant_ranges);
            continue;
        }
        
        let almanac_entry: Vec<i32> = line.split(" ").map(|x| x.parse::<i32>().unwrap()).collect();
        for i in 0..relevant_ranges.len() {
            let old_end = almanac_entry[1] + almanac_entry[2] - 1;
            if relevant_ranges[i].start >= almanac_entry[1] && relevant_ranges[i].start <= old_end {
                println!("Hit detected, starting at {}, in {:?}", relevant_ranges[i].start, almanac_entry);
                let new_start = max(relevant_ranges[i].start, almanac_entry[1]);
                let new_end = min(relevant_ranges[i].end, old_end);
                let offset = almanac_entry[1] - almanac_entry[0];
                // TODO: Update the old values and create new ones with correct values
                if new_start == relevant_ranges[i].start && new_end == old_end {
                    // Both endpoints are matched- (a, d) -> (x, y)
                    println!("Moving the entire section.");
                    relevant_ranges[i].update_start(new_start);
                    relevant_ranges[i].update_end(new_end);
                } else if new_start == relevant_ranges[i].start {
                    // Left endpoint is matched- (a, d) -> (c, d) + create (x, y)
                    println!("Moving left section.");
                    relevant_ranges[i].update_start(new_start);
                    let new_range = Range {start: relevant_ranges[i].start + offset, end: old_end};
                    new_ranges.push(new_range);
                } else if new_end == old_end {
                    // Right endpoint is matched- (a, d) -> (a, b) + create (x, y)
                    println!("Moving right section.");
                    relevant_ranges[i].update_end(new_end);
                    let new_range = Range {start: relevant_ranges[i].start, end: relevant_ranges[i].end + offset};
                    new_ranges.push(new_range);
                } else {
                    // Neither endpoint is matched- (a, d) -> (x, y) + create (a, b) and (c, d)
                    // @DBG Potential error: if a second match occurs in (a, b) or (c, d) after this hit, it may be missed.
                    println!("Moving middle section.");
                    let new_range_1 = Range {start: relevant_ranges[i].start, end: old_end + offset};
                    let new_range_2 = Range {start: relevant_ranges[i].start + offset, end: old_end};
                    relevant_ranges[i].update_start(new_start);
                    relevant_ranges[i].update_end(new_end);
                    new_ranges.push(new_range_1);
                    new_ranges.push(new_range_2);
                }
            }
            
            // match get_offset_location(*relevant_location, almanac_entry[0], almanac_entry[1], almanac_entry[2]) {
            //     Some(new_location) => {
            //         // changed_locations.push(*relevant_location);
            //         new_locations.push(new_location);
            //     },
            //     _ => {},
            // }
        }
    }

    // Ok(*relevant_locations.iter().min().unwrap() as i32)

    return Err(String::from("Still working on it."));
}

pub fn solve(input_strings: Vec<String>, run_mode: RunMode, puzzle_part: PuzzlePart) -> Result<i32, String> {
    match puzzle_part {
        PuzzlePart::PartOne => solve_part_1(input_strings, run_mode),
        PuzzlePart::PartTwo => solve_part_2(input_strings, run_mode),
    }
}