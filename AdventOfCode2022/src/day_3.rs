/*
 * https://adventofcode.com/2022/day/2
 * Objective: Given a list of items in a rucksack,
 * Part 1: Find the sum of the priorities of the only items contained in both compartments of each.
 * Part 2:
 * Part of me learning Rust.
 */

fn find_common_item(left_compartment: &str, right_compartment: &str) -> char {
    for left_item in left_compartment.chars() {
        for right_item in right_compartment.chars() {
            if left_item == right_item {
                return left_item
            }
        }
    }
    eprintln!("No common items found between {} and {}", left_compartment, right_compartment);
    ' '
}

fn get_item_priority(item: char) -> i32 {
    -1
}

pub fn part_1_solution(input_strings: Vec<String>) -> i32 {
    let mut priority_total = 0;

    for line in input_strings {
        let compartment_size = line.len() / 2;
        let common_item = find_common_item(&line[..compartment_size],
                                           &line[compartment_size..]);
        priority_total += get_item_priority(common_item);
        println!("{}", common_item);
    }

    priority_total
}

pub fn part_2_solution(input_strings: Vec<String>) -> i32 {
    -1
}