/*
* https://adventofcode.com/2023/day/2
* Objective: Given a series of characters,
* Part 1: Find the embedded number based on the first and last digits.
* Part 2: Do the same, but count the spelled versions ("one", "two", etc.) as digits
* Part of me learning Rust.
*/
use crate::helpers::RunMode;
use crate::helpers::PuzzlePart;

const BLUE_MAX: u32 = 14;
const RED_MAX: u32 = 12;
const GREEN_MAX: u32 = 13;

struct Bag {
    blue_min: u32,
    red_min: u32,
    green_min: u32,
}

fn check_pull(line_num: u32, pulls: Vec<&str>) -> u32 {
    println!("Line {line_num}= {pulls:?}");
    for pull in pulls {
        if pull.contains("blue") {
            let count = pull.split(" ").nth(0).unwrap().parse::<u32>().unwrap();
            if count > BLUE_MAX { return 0 }; 
        }
        if pull.contains("red") {
            let count = pull.split(" ").nth(0).unwrap().parse::<u32>().unwrap();
            if count > RED_MAX { return 0 }; 
        }
        if pull.contains("green") {
            let count = pull.split(" ").nth(0).unwrap().parse::<u32>().unwrap();
            if count > GREEN_MAX { return 0 }; 
        }
    }

    line_num
}

pub fn solve_part_1(input_strings: Vec<String>, run_mode: RunMode) -> Result<i32, String> {
    let mut game_sums = 0;

    for line in input_strings {
        let line_num = line.split(":").nth(0).unwrap().replace("Game ", "").parse::<u32>().unwrap();
        let pulls = line.split(": ").nth(1).unwrap().replace(";", ",");
        let pulls = pulls.split(", ").collect();

        game_sums += check_pull(line_num, pulls);
    }

    Ok(game_sums as i32)
}

pub fn solve_part_2(input_strings: Vec<String>, run_mode: RunMode) -> Result<i32, String> {
    let mut game_power_sets = 0;

    for line in input_strings {
        let pulls = line.split(": ").nth(1).unwrap().replace(";", ",");
        let pulls: Vec<&str> = pulls.split(", ").collect();
        
        let mut bag = Bag {blue_min: 0, red_min: 0, green_min: 0};

        for pull in pulls {
            if pull.contains("blue") {
                let count = pull.split(" ").nth(0).unwrap().parse::<u32>().unwrap();
                if count > bag.blue_min { bag.blue_min = count }; 
            }
            if pull.contains("red") {
                let count = pull.split(" ").nth(0).unwrap().parse::<u32>().unwrap();
                if count > bag.red_min { bag.red_min = count }; 
            }
            if pull.contains("green") {
                let count = pull.split(" ").nth(0).unwrap().parse::<u32>().unwrap();
                if count > bag.green_min { bag.green_min = count };
            }
        }

        game_power_sets += bag.blue_min * bag.red_min * bag.green_min;
    }

    Ok(game_power_sets as i32)
}

pub fn solve(input_strings: Vec<String>, run_mode: RunMode, puzzle_part: PuzzlePart) -> Result<i32, String> {
    match puzzle_part {
        PuzzlePart::PartOne => solve_part_1(input_strings, run_mode),
        PuzzlePart::PartTwo => solve_part_2(input_strings, run_mode),
    }
}