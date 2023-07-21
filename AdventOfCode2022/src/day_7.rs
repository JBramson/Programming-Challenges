/*
 * https://adventofcode.com/2022/day/7
 * Objective: Given a series of console commands and output,
 * Part 1: Find the sum of all folders that have a size no greater than 100,000
 * Part 2:
 * Part of me learning Rust.
 */
use crate::helpers::RunMode;

enum CommandMode {
    CHANGE_DIR,
    LIST_DIR,
}

pub fn part_1_solution(input_strings: Vec<String>, run_mode: RunMode) -> String {
    let mut current_dir = "";
    for line in input_strings {
        if line.chars().nth(0).unwrap() == '$' {
            print!("Command detected.");
        } else {
            print!("Output detected.");
        }
        println!(" {line}");
    }

    String::from("Solution in-progress.")
}

pub fn part_2_solution(input_strings: Vec<String>, run_mode: RunMode) -> String {


    String::from("Solution in-progress.")
}