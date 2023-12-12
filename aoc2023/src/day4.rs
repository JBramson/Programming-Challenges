/*
* https://adventofcode.com/2023/day/4
* Objective: Given a list of scratch-off cards with winning numbers and our numbers,
* Part 1: Find the number of points yielded from our winning numbers
* Part 2: Find the total number of cards yielded if points grant us additional cards, given only the first for free
* Part of me learning Rust.
*/
use crate::helpers::RunMode;
use crate::helpers::PuzzlePart;

const TWO: u32 = 2;

pub fn solve_part_1(input_strings: Vec<String>, run_mode: RunMode) -> Result<i32, String> {
    let mut points = 0;

    for line in input_strings {
        let mut wins_on_card: u32 = 0;
        let line = line.split(": ").nth(1).unwrap();
        let winners: Vec<&str> = line.split(" | ").nth(0).unwrap().split(" ").filter(|x| *x != "").collect();
        let our_numbers: Vec<&str> = line.split(" | ").nth(1).unwrap().split(" ").filter(|x| *x != "").collect();

        for winner in winners {
            wins_on_card += our_numbers.iter().filter(|&x| *x == winner).count() as u32;
        }
        points += match wins_on_card {
            0 => 0,
            _ => TWO.pow(wins_on_card - 1),
        };
    }
    

    Ok(points as i32)
}

// Idea: Create vector of ints that give the quantity of a given card unlocked, and keep going until we reach the end of the vec
pub fn solve_part_2(input_strings: Vec<String>, run_mode: RunMode) -> Result<i32, String> {
    let mut card_count_total = 1;
    let mut card_counts = vec![0; input_strings.len()];
    card_counts[0] = 1; // First one's free
    let mut current_card_index = 0;

    while current_card_index < card_counts.len() {
        // If the next card hasn't been unlocked, no further cards have been unlocked and we can stop.
        if card_counts[current_card_index] == 0 {
            break;
        }
        // Get the number of wins
        // Use this number to calculate how many successive cards to add to
        // Multiply this count by the current card's count and add this amount to each previously-determined card
    }

    for card_count in card_counts {
        if card_count == 0 {
            break;
        }
        card_count_total += card_count;
    }
    

    Ok(card_count_total as i32)
}

pub fn solve(input_strings: Vec<String>, run_mode: RunMode, puzzle_part: PuzzlePart) -> Result<i32, String> {
    match puzzle_part {
        PuzzlePart::PartOne => solve_part_1(input_strings, run_mode),
        PuzzlePart::PartTwo => solve_part_2(input_strings, run_mode),
    }
}