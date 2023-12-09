/*
* https://adventofcode.com/2023/day/3
* Objective: Given a map of numbers and symbols,
* Part 1: Find the sum of values bordering a symbol
* Part 2: 
* Part of me learning Rust.
*/
use crate::helpers::RunMode;
use crate::helpers::PuzzlePart;

const RADIX: u32 = 10; // Used to convert from char to usize

#[derive(Debug, PartialEq, Copy, Clone)]
struct Number {
    val: i32,
    y: usize,
    x: usize,
    size: i32,
}

#[derive(Debug)]
struct SybmolPoint {
    y: usize,
    x: usize,
}

fn get_num_size(num: &i32) -> i32 {
    let mut manipulatable_num = num.clone();
    let mut size = 1;
    while manipulatable_num > 9 {
        size += 1;
        manipulatable_num /= 10;
    }

    size
}

fn find_number_at_point(numbers: &Vec<Number>, y: usize, x: usize) -> Option<&Number> {
    Some(numbers.first().unwrap())
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
        let mut current_num = 0;
        let check_up = row_num > 0;
        let check_down = row_num < row_count;
        for col_num in 0..col_count {
            let check_left = col_num > 0;
            let check_right = col_num < col_count;
            match input_strings[row_num].chars().nth(col_num).unwrap() {
                '0'..='9' => current_num = current_num * 10 + input_strings[row_num].chars().nth(col_num).unwrap().to_digit(RADIX).unwrap() as i32,
                '.' => {},
                symbol => {
                    // TODO: Stop building a current_num here- instead, when we hit a number, send it and a mutable reference
                    // TODO: of the line to a function that gets the whole number, setting all digits of the number to be periods,
                    // TODO: then returning the number to be added into part_sums.
                    if current_num != 0 {
                        numbers.push(Number { val: current_num.clone(), y: row_num, x: col_num, size: get_num_size(&current_num)});
                        current_num = 0;
                    }
                    if symbol != '.' {
                        symbols.push(SybmolPoint { y: row_num, x: col_num })
                    }
                }, 
            }
        }
    }

    for symbol in symbols {
        let check_left = symbol.x > 0;
        let check_right = symbol.x < col_count;
        let check_up = symbol.y > 0;
        let check_down = symbol.y < row_count;
        let mut found_numbers: Vec<Number> = vec![];

        if check_left {
            let found_number = find_number_at_point(&numbers, symbol.y, symbol.x - 1);
            if let Some(number) = found_number {
                part_sums += number.val;
                numbers.retain(|x| number != found_number.unwrap());      
            }
        }
    }
    for number in numbers {
        println!("{number:?}");
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