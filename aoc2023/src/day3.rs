/*
* https://adventofcode.com/2023/day/3
* Objective: Given a map of numbers and symbols,
* Part 1: Find the sum of values bordering a symbol
* Part 2: 
* Part of me learning Rust.
*/
use crate::helpers::RunMode;
use crate::helpers::PuzzlePart;

// Numbers appear across a single line- only 1 needs to be checked at a time
fn find_number_at_x(line: &mut String, x: usize, x_max: usize) -> i32 {
    let mut value_string = "".to_string();
    if line.chars().nth(x).unwrap().is_numeric() == false {
        return 0
    } else {
        for x_pos in x..x_max {
            // Skip extra checks once the number ends
            if line.chars().nth(x_pos).unwrap().is_numeric() == false {
                break;
            }
            value_string.push(line.chars().nth(x_pos).unwrap());
            line.replace_range(x_pos..x_pos+1, ".");
        }
        for x_pos in (0..x).rev() {
            // Skip extra checks once the number ends
            if line.chars().nth(x_pos).unwrap().is_numeric() == false {
                break;
            }
            value_string.insert(0, line.chars().nth(x_pos).unwrap());
            line.replace_range(x_pos..x_pos+1, ".");
        }
    }

    value_string.parse::<i32>().unwrap()
}

/**
 * New idea:
 * Search for the symbols only.
 * If a symbol is detected, check its surroundings.
 *      From left to right, add the numbers starting from the left and replace the values with periods to avoid double-counting.
 */

pub fn solve_part_1(input_strings: Vec<String>, run_mode: RunMode) -> Result<i32, String> {
    let mut part_sums = 0;
    let mut map = input_strings.to_owned();
    let row_count = input_strings.len();
    let col_count = input_strings.first().unwrap().len();

    // Fill out the number and symbol vectors
    for row_num in 0..row_count {
        let check_up = row_num > 0;
        let check_down = row_num < row_count - 1;
        for col_num in 0..col_count {
            let check_left = col_num > 0;
            let check_right = col_num < col_count - 1;
            match input_strings[row_num].chars().nth(col_num).unwrap() {
                '0'..='9' | '.' => {},
                _ => {
                    if check_up {
                        // Check (y-1, x)
                        part_sums += find_number_at_x(&mut map[row_num - 1], col_num, col_count);
                        if check_left {
                            // Check (y-1, x-1)
                            part_sums += find_number_at_x(&mut map[row_num - 1], col_num - 1, col_count);
                        }
                        if check_right {
                            // Check (y-1, x+1)
                            part_sums += find_number_at_x(&mut map[row_num - 1], col_num + 1, col_count);
                        }
                    }
                    if check_down {
                        // Check (y+1, x)
                        part_sums += find_number_at_x(&mut map[row_num + 1], col_num, col_count);
                        if check_left {
                            // Check (y+1, x-1)
                            part_sums += find_number_at_x(&mut map[row_num + 1], col_num - 1, col_count);
                        }
                        if check_right {
                            // Check (y+1, x+1)
                            part_sums += find_number_at_x(&mut map[row_num + 1], col_num + 1, col_count);
                        }
                    }
                    if check_left {
                        // Check (y, x-1)
                        part_sums += find_number_at_x(&mut map[row_num], col_num - 1, col_count);
                    }
                    if check_right {
                        // Check (y, x+1)
                        part_sums += find_number_at_x(&mut map[row_num], col_num + 1, col_count);
                    }
                }, 
            }
        }
    }

    Ok(part_sums as i32)
}

pub fn solve_part_2(input_strings: Vec<String>, run_mode: RunMode) -> Result<i32, String> {
    let mut game_power_sets = 0;



    Ok(game_power_sets as i32)
}

pub fn solve(input_strings: Vec<String>, run_mode: RunMode, puzzle_part: PuzzlePart) -> Result<i32, String> {
    match puzzle_part {
        PuzzlePart::PartOne => solve_part_1(input_strings, run_mode),
        PuzzlePart::PartTwo => solve_part_2(input_strings, run_mode),
    }
}