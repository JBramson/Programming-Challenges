/*
 * https://adventofcode.com/2022/day/2
 * Objective: Given a stack of crates and a list of moves,
 * Part 1: Perform the moves and return the top crates.
 * Part 2:
 * Part of me learning Rust.
 */

// The starting stack for the practice input:
// let mut stacks: [Vec<char>; 3] = [vec!['Z', 'N'], vec!['M', 'C', 'M'], vec!['P']];
const MINIMUM_NUMBER_OF_STACKS: usize = 3;
const MAXIMUM_NUMBER_OF_STACKS: usize = 9;

pub fn part_1_solution(input_strings: Vec<String>) -> &'static str {
    let mut final_tops = "";
    let mut initial_stacks_lines: Vec<String> = vec![];
    let mut stacks: Vec<Vec<char>> = vec![vec![]; MAXIMUM_NUMBER_OF_STACKS];

    for line in input_strings {
        if line.contains('[') { // Stack lines
            initial_stacks_lines.push(line);
        } else if line.as_bytes()[1] == '1' as u8 { // Number line
            let number_of_stacks = line.len() / 4;
            if (number_of_stacks != MINIMUM_NUMBER_OF_STACKS)
                && (number_of_stacks != MAXIMUM_NUMBER_OF_STACKS) {
                panic!("Incorrect stack size generated: number_of_stacks={}", number_of_stacks);
            }
            todo!("Populate array vectors accordingly.");

        } else if line.as_bytes()[0] == 'm' as u8 { // Movement lines
            todo!("Register and make a move.");
        }
    }

    final_tops
}

pub fn part_2_solution(input_strings: Vec<String>) -> &'static str {
    let mut final_tops = "";

    for line in input_strings {

    }

    final_tops
}