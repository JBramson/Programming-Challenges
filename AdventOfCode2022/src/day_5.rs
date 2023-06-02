/*
 * https://adventofcode.com/2022/day/2
 * Objective: Given a stack of crates and a list of moves,
 * Part 1: Perform the moves one-by-one and return the top crates.
 * Part 2: Perform the moves all at once and return the top crates.
 * Part of me learning Rust.
 */
// mod helpers;
use crate::helpers::RunMode;

const MINIMUM_NUMBER_OF_STACKS:       usize = 3;
const MAXIMUM_NUMBER_OF_STACKS:       usize = 9;
const INPUT_CHAR_HORIZONTAL_DISTANCE: usize = 4;

fn move_top_crates_single(stacks: &mut Vec<Vec<char>>, quantity: u8, origin_stack: usize, target_stack: usize) {
    for i in 0..quantity {
        let popped_crate = stacks[origin_stack].pop().unwrap();
        stacks[target_stack].push(popped_crate);
    }
}

fn move_top_crates_group(stacks: &mut Vec<Vec<char>>, quantity: u8, origin_stack: usize, target_stack: usize) {
    let mut moving_stack: Vec<char> = vec![];
    for i in 0..quantity {
        moving_stack.push(stacks[origin_stack].pop().unwrap());
    }
    moving_stack.reverse();
    stacks[target_stack].append(&mut moving_stack);
}

pub fn part_1_solution(input_strings: Vec<String>, run_mode: RunMode) -> String {
    let mut final_tops: String = String::from("");
    let stack_count = if matches!(run_mode, RunMode::DEBUG) {MINIMUM_NUMBER_OF_STACKS} else {MAXIMUM_NUMBER_OF_STACKS};
    let mut initial_stacks_lines: Vec<Vec<char>> = vec![];
    let mut stacks: Vec<Vec<char>> = vec![];

    for mut line in input_strings {
        if line.len() == 0 {
            continue; // We can't test against the blank line
        } else if line.contains('[') { // Stack lines
            let mut filtered_line: Vec<char> = vec![' '; stack_count];
            for i in 0..line.len() {
                if i % 4 == 1 {
                    filtered_line[i / INPUT_CHAR_HORIZONTAL_DISTANCE] = line.chars().collect::<Vec<char>>()[i];
                }
            }
            initial_stacks_lines.push(filtered_line);
        } else if line.as_bytes()[1] == '1' as u8 { // Number line
            // Rotate the stacks
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
        } else if line.as_bytes()[0] == 'm' as u8 { // Movement lines
            line.retain(|c| c.is_digit(10) || c == ' '); // Remove letters from line
            let move_instructions: Vec<&str> = line.split_whitespace().collect();
            move_top_crates_single(&mut stacks, move_instructions[0].parse::<u8>().unwrap(),
                                   move_instructions[1].parse::<usize>().unwrap() - 1,
                                   move_instructions[2].parse::<usize>().unwrap() - 1);
        }
    }

    // Get the final tops
    for i in 0..stack_count {
        final_tops.push(*stacks[i].last().unwrap());
    }

    final_tops
}

pub fn part_2_solution(input_strings: Vec<String>, run_mode: RunMode) -> String {
    let mut final_tops: String = String::from("");
    let stack_count = if matches!(run_mode, RunMode::DEBUG) {MINIMUM_NUMBER_OF_STACKS} else {MAXIMUM_NUMBER_OF_STACKS};
    let mut initial_stacks_lines: Vec<Vec<char>> = vec![];
    let mut stacks: Vec<Vec<char>> = vec![];

    for mut line in input_strings {
        if line.len() == 0 {
            continue; // We can't test against the blank line
        } else if line.contains('[') { // Stack lines
            let mut filtered_line: Vec<char> = vec![' '; stack_count];
            for i in 0..line.len() {
                if i % 4 == 1 {
                    filtered_line[i / INPUT_CHAR_HORIZONTAL_DISTANCE] = line.chars().collect::<Vec<char>>()[i];
                }
            }
            initial_stacks_lines.push(filtered_line);
        } else if line.as_bytes()[1] == '1' as u8 { // Number line
            // Rotate the stacks
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
        } else if line.as_bytes()[0] == 'm' as u8 { // Movement lines
            line.retain(|c| c.is_digit(10) || c == ' '); // Remove letters from line
            let move_instructions: Vec<&str> = line.split_whitespace().collect();
            move_top_crates_group(&mut stacks, move_instructions[0].parse::<u8>().unwrap(),
                                   move_instructions[1].parse::<usize>().unwrap() - 1,
                                   move_instructions[2].parse::<usize>().unwrap() - 1);
        }
    }

    // Get the final tops
    for i in 0..stack_count {
        final_tops.push(*stacks[i].last().unwrap());
    }

    final_tops
}