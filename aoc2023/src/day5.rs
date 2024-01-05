/*
* https://adventofcode.com/2023/day/5
* Objective: Given a list of seeds and an almanac of maps detailing ranges linking steps of farming,
* Part 1: Find the lowest final location number of any of the given seeds
* Part 2: Do the same as above, with a list of seed ranges instead of being given seeds directly
* Part of me learning Rust.
*/
use crate::helpers::RunMode;
use crate::helpers::PuzzlePart;

fn get_offset_location(start_loc: u32, destination_range_start: u32, source_range_start: u32, range: u32) -> Option<u32> {
    if start_loc < source_range_start || start_loc > source_range_start + range - 1 { // The -1 is required to remove end-range hits
        None
    } else {
        let offset = start_loc - source_range_start;
        Some(destination_range_start + offset)
    }
}

// Any unmapped sections correspond to their own number
pub fn solve_part_1(input_strings: Vec<String>, run_mode: RunMode) -> Result<i32, String> {
    let mut relevant_locations: Vec<u32> = input_strings[0].split(": ").nth(1).unwrap().split(" ").map(|x| x.parse::<u32>().unwrap()).collect();
    let mut new_locations: Vec<u32> = vec![];
    let mut changed_locations: Vec<u32> = vec![];

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
        
        let almanac_entry: Vec<u32> = line.split(" ").map(|x| x.parse::<u32>().unwrap()).collect();
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
    let mut relevant_locations: Vec<u32> = input_strings[0].split(": ").nth(1).unwrap().split(" ").map(|x| x.parse::<u32>().unwrap()).collect();
    
    let mut starts: Vec<u32> = vec![];
    let mut lengths: Vec<u32> = vec![];

    for (index, value) in relevant_locations.iter().enumerate() {
        if index % 2 == 0 {
            starts.push(*value);
        } else {
            lengths.push(*value);
        }
    }

    relevant_locations.clear();
    for i in 0..starts.len() {
        relevant_locations.append(&mut (starts[i]..(starts[i]+lengths[i])).collect());
    }

    
    relevant_locations.sort();
    println!("relevant_locations.len()={}", relevant_locations.len());
    relevant_locations.dedup();
    println!("relevant_locations.len() post-dedup()={}", relevant_locations.len());
    
    return Err(String::from("Still working on it."));

    /*
     * TODO: This is going to have to go by ranges and holes. We'll start with a range of values for the seeds with no holes.
     * For each step, see if there's a subrange with changed values.
     * If there are, add a new, shifted range of the updated values and add a hole range with the old values (AFTER ALL CHECKS OF THE ROUND).
     * During future checks, delete hits from hole ranges (if 10-19 is a hole, 1-30 becomes 1-9 and 20-30).
     */
    
    // let mut new_locations: Vec<u32> = vec![];
    // let mut changed_locations: Vec<u32> = vec![];

    // for line in &input_strings[3..] {
    //     if line.is_empty() {
    //         continue;
    //     } else if line.contains(":") {
    //         relevant_locations.retain(|x| !changed_locations.contains(x));
    //         relevant_locations.append(&mut new_locations);
    //         changed_locations.clear();
    //         new_locations.clear();
    //         continue;
    //     }
        
    //     let almanac_entry: Vec<u32> = line.split(" ").map(|x| x.parse::<u32>().unwrap()).collect();
    //     for relevant_location in &relevant_locations {
    //         match get_offset_location(*relevant_location, almanac_entry[0], almanac_entry[1], almanac_entry[2]) {
    //             Some(new_location) => {
    //                 changed_locations.push(*relevant_location);
    //                 new_locations.push(new_location);
    //             },
    //             _ => {},
    //         }
    //     }
    // }

    // // Run the cleanup stuff afterwards due to lack of ending colon line
    // relevant_locations.retain(|x| !changed_locations.contains(x));
    // relevant_locations.append(&mut new_locations);
    // changed_locations.clear();
    // new_locations.clear();

    // Ok(*relevant_locations.iter().min().unwrap() as i32)
}

pub fn solve(input_strings: Vec<String>, run_mode: RunMode, puzzle_part: PuzzlePart) -> Result<i32, String> {
    match puzzle_part {
        PuzzlePart::PartOne => solve_part_1(input_strings, run_mode),
        PuzzlePart::PartTwo => solve_part_2(input_strings, run_mode),
    }
}