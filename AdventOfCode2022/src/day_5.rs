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

fn move_top_crates(stacks: &mut Vec<Vec<char>>, quantity: u8, origin_stack: u8, target_stack: u8) -> bool {
    todo!("Implement moving function");

    false
}

pub fn part_1_solution(input_strings: Vec<String>, run_mode: RunMode) -> &'static str {
    let mut final_tops = "";
    let stack_count = if matches!(run_mode, RunMode::DEBUG) {MINIMUM_NUMBER_OF_STACKS} else {MAXIMUM_NUMBER_OF_STACKS};
    let mut initial_stacks_lines: Vec<Vec<char>> = vec![];
    let mut stacks: Vec<Vec<char>> = vec![];

    for line in input_strings {
        if line.contains('[') { // Stack lines
            let mut filtered_line: Vec<char> = vec![' '; stack_count];
            for i in 0..line.len() {
                if i % 4 == 1 {
                    filtered_line[i / 4] = line.chars().collect::<Vec<char>>()[i]; // i / 4 yields ints
                }
            }
            initial_stacks_lines.push(filtered_line);
        } else if line.as_bytes()[1] == '1' as u8 { // Number line
            // Top loop iterates 0..stack_count to get stack num
            // For each stack num, top to bottom, adding to the back of the stack (pop later from the front)
            // Insert the stack
            for i in 0..stack_count {
                let mut new_stack: Vec<char> = vec![];
                let mut next_char: char;

                for j in (0..initial_stacks_lines.len()).rev() {
                    next_char = initial_stacks_lines[j][i];
                    if next_char == ' ' {
                        break;
                    }
                    new_stack.push(next_char);
                }
                stacks.push(new_stack);
            }

            println!("{:?}", stacks);
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