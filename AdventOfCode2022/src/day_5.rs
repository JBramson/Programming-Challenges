/*
 * https://adventofcode.com/2022/day/2
 * Objective: Given a stack of crates and a list of moves,
 * Part 1: Perform the moves and return the top crates.
 * Part 2:
 * Part of me learning Rust.
 */
// mod helpers;
use crate::helpers::RunMode;

// The base starting stack for the practice input:
// let mut stacks: [Vec<char>; 3] = [vec!['Z', 'N'], vec!['M', 'C', 'M'], vec!['P']];
const MINIMUM_NUMBER_OF_STACKS: usize = 3;
const MAXIMUM_NUMBER_OF_STACKS: usize = 9;

pub fn part_1_solution(input_strings: Vec<String>, run_mode: RunMode) -> &'static str {
    let mut final_tops = "";
    let mut initial_stacks_lines: Vec<String> = vec![];
    let stack_count = if matches!(run_mode, RunMode::DEBUG) {MINIMUM_NUMBER_OF_STACKS} else {MAXIMUM_NUMBER_OF_STACKS};
    let mut stacks: Vec<Vec<char>> = vec![vec![]; stack_count];

    // TODO: Rework the replacement functions- we need to preserve spacing

    for line in input_strings {
        if line.contains('[') { // Stack lines
            let mut filtered_line: String = String::from("");
            // Idea: Iterate with a counter through chars in string, deleting all chars that aren't
            // at an appropriate index (with a value of 1 when modulo 4'd.
            for i in 0..line.len() {
                if i % 4 == 1 {
                    filtered_line.push(line.chars().collect::<Vec<char>>()[i]);
                }
            }
            initial_stacks_lines.push(filtered_line);
        } else if line.as_bytes()[1] == '1' as u8 { // Number line
            // for i in 0..number_of_stacks {
            //
            // }

            println!("{:?}", initial_stacks_lines);
            todo!("Populate array vectors accordingly.");

        } else if line.as_bytes()[0] == 'm' as u8 { // Movement lines
            todo!("Register and make a move.");
        }
    }

    final_tops
}

pub fn part_2_solution(input_strings: Vec<String>, run_mode: RunMode) -> &'static str {
    let mut final_tops = "";

    for line in input_strings {

    }

    final_tops
}