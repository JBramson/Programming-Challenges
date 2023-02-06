/*
 * https://adventofcode.com/2022/day/1
 * Objective: Given a list of calories carried by elves,
 * Part 1: Find the greatest total of calories carried by an elf.
 * Part 2: Find the total of the three greatest calorie-holders.
 * Part of me learning Rust.
 */

/// Manual looping and checking solution
pub fn part_1_solution(input_strings: Vec<String>) -> i32 {
    let mut max_calorie_count = 0;
    let mut current_calorie_count = 0;

    for line in input_strings {
        if line == "" {
            if current_calorie_count > max_calorie_count {
                max_calorie_count = current_calorie_count;
            }
            current_calorie_count = 0;
        } else {
            current_calorie_count += str::parse::<i32>(&*line).unwrap();
        }
    }
    // Last input line shouldn't be blank, so we need to check the final elf
    if current_calorie_count > max_calorie_count {
        max_calorie_count = current_calorie_count;
    }

    max_calorie_count
}

/// Smarter attempt with standard sort
pub fn part_2_solution(input_strings: Vec<String>) -> i32 {
    let mut calorie_counts: Vec<i32> = Vec::new();
    let mut current_calorie_count = 0;

    for line in input_strings {
        if line == "" {
            calorie_counts.push(current_calorie_count);
            current_calorie_count = 0;
        } else {
            current_calorie_count += str::parse::<i32>(&*line).unwrap();
        }
    }
    // Last input line shouldn't be blank, so we need to insert the final elf
    calorie_counts.push(current_calorie_count);
    calorie_counts.sort();

    calorie_counts[calorie_counts.len() - 1]
        + calorie_counts[calorie_counts.len() - 2]
        + calorie_counts[calorie_counts.len() - 3]
}