/*
* https://adventofcode.com/2023/day/1
* Objective: Given a series of characters,
* Part 1: Find the embedded number based on the first and last digits.
* Part 2: Do the same, but count the spelled versions ("one", "two", etc.) as digits
* Part of me learning Rust.
*/
use crate::helpers::RunMode;
use crate::helpers::PuzzlePart;

const RADIX: u32 = 10; // Used to convert from char to usize

pub fn solve_part_1(input_strings: Vec<String>, run_mode: RunMode) -> Result<i32, String> {
    let mut calibration_value = 0;

    for line in input_strings {
        let mut first_digit_index: usize = usize::MAX;
        let mut last_digit_index: usize = 0;

        for (i, character) in line.chars().enumerate() {
            if character.is_digit(RADIX) {
                if i < first_digit_index {
                    first_digit_index = i;
                }
                if i > last_digit_index {
                    last_digit_index = i;
                }
            }
        }

        calibration_value += line.chars().nth(first_digit_index).unwrap().to_digit(RADIX).unwrap() * 10
            + line.chars().nth(last_digit_index).unwrap().to_digit(RADIX).unwrap();
    }

    Ok(calibration_value as i32)
}

pub fn solve_part_2(input_strings: Vec<String>, run_mode: RunMode) -> Result<i32, String> {
    let mut calibration_value = 0;

    for mut line in input_strings {
        // The numbers are inserted in the middle to avoid mangling a word with shared chars, i.e. "oneight" => "on1eig8ht"
        line = line.replace("one", "on1e");
        line = line.replace("two", "tw2o");
        line = line.replace("three", "thr3ee");
        line = line.replace("four", "fo4ur");
        line = line.replace("five", "fi5ve");
        line = line.replace("six", "si6x");
        line = line.replace("seven", "sev7en");
        line = line.replace("eight", "eig8ht");
        line = line.replace("nine", "ni9ne");
        let mut first_digit_index: usize = usize::MAX;
        let mut last_digit_index: usize = 0;

        for (i, character) in line.chars().enumerate() {
            if character.is_digit(RADIX) {
                if i < first_digit_index {
                    first_digit_index = i;
                }
                if i > last_digit_index {
                    last_digit_index = i;
                }
            }
        }

        calibration_value += line.chars().nth(first_digit_index).unwrap().to_digit(RADIX).unwrap() * 10
            + line.chars().nth(last_digit_index).unwrap().to_digit(RADIX).unwrap();
    }

    Ok(calibration_value as i32)
}

pub fn solve(input_strings: Vec<String>, run_mode: RunMode, puzzle_part: PuzzlePart) -> Result<i32, String> {
    match puzzle_part {
        PuzzlePart::PartOne => solve_part_1(input_strings, run_mode),
        PuzzlePart::PartTwo => solve_part_2(input_strings, run_mode),
    }
}