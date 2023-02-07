/*
 * https://adventofcode.com/2022/day/2
 * Objective: Given a list of items in a rucksack,
 * Part 1: Find the sum of the priorities of the only items contained in both compartments of each.
 * Part 2: Find the sum of the priorities of only the items contained in triples of backpacks.
 * Part of me learning Rust.
 */

const LOWERCASE_MIN:  i32 = 97; // 'a' as i32
const UPPERCASE_MIN:  i32 = 65; // 'A' as i32
const CAPITAL_OFFSET: i32 = 26; // Separation of space between capitals and lowercase values

/// Used for loop to iterate through both strings
fn find_common_item_from_2(left_compartment: &str, right_compartment: &str) -> char {
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

/// Used for loop only on first string- used lookup for others
fn find_common_item_from_3(first_compartment: &str, second_compartment: &str,
                           third_compartment: &str) -> char {
    for item in first_compartment.chars() {
        if second_compartment.contains(item) && third_compartment.contains(item) {
            return item
        }
    }
    eprintln!("No common items found between {}, {}, and {}", first_compartment, second_compartment,
              third_compartment);
    ' '
}

fn get_item_priority(item: char) -> i32 {
    let item_i32 = item as i32;

    return if item_i32 >= LOWERCASE_MIN {
        item_i32 - LOWERCASE_MIN + 1
    } else {
        item_i32 - UPPERCASE_MIN + 1 + CAPITAL_OFFSET
    }
}

pub fn part_1_solution(input_strings: Vec<String>) -> i32 {
    let mut priority_total: i32 = 0;

    for line in input_strings {
        let compartment_size = line.len() / 2;
        let common_item = find_common_item_from_2(&line[..compartment_size],
                                                  &line[compartment_size..]);
        priority_total += get_item_priority(common_item);
    }

    priority_total
}

pub fn part_2_solution(input_strings: Vec<String>) -> i32 {
    let mut priority_total: i32 = 0;
    let mut i = 0;

    while i < input_strings.len() {
        let common_item = find_common_item_from_3(&input_strings[i],
                                                  &input_strings[i + 1],
                                                  &input_strings[i + 2]);
        priority_total += get_item_priority(common_item);
        i += 3
    }

    priority_total
}