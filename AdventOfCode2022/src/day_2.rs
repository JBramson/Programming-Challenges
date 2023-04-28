/*
 * https://adventofcode.com/2022/day/2
 * Objective: Given a list of RPS instructions,
 * Part 1: Calculate how many points you will score if the second input is your response.
 * Part 2: Calculate how many points you will score if the second input is your outcome (W/L/D).
 * Part of me learning Rust.
 */

const POINTS_ON_WIN:  i32 = 6;
const POINTS_ON_DRAW: i32 = 3;

pub fn part_1_solution(input_strings: Vec<String>) -> i32 {
    let mut score = 0;

    for line in input_strings {
        let opponent_throw = line.chars().nth(0).unwrap();
        let player_throw = line.chars().nth(2).unwrap();
        // Calculate our throw points first
        score += match player_throw {
            'X' => 1,
            'Y' => 2,
            'Z' => 3,
            _   => i32::MIN,
        };

        if (opponent_throw as u32 + 23) == player_throw as u32 {
            score += POINTS_ON_DRAW;
        } else {
            if (opponent_throw == 'A') && (player_throw == 'Y') {score += POINTS_ON_WIN}
            else if (opponent_throw == 'B') && (player_throw == 'Z') {score += POINTS_ON_WIN}
            else if (opponent_throw == 'C') && (player_throw == 'X') {score += POINTS_ON_WIN}
        }
    }

    score
}

pub fn part_2_solution(input_strings: Vec<String>) -> i32 {
    let mut score = 0;

    for line in input_strings {
        let mut opponent_throw = line.chars().nth(0).unwrap();
        let desired_outcome = line.chars().nth(2).unwrap();
        let mut player_throw = ' ';

        // Convert opponent_throw to more easily recognizable value
        opponent_throw = match opponent_throw {
            'A' => 'R',
            'B' => 'P',
            'C' => 'S',
            _ => ' ',
        };

        match desired_outcome {
            // Lose
            'X' => {
                player_throw = match opponent_throw {
                    'R' => 'S',
                    'P' => 'R',
                    'S' => 'P',
                    _ => ' ',
                }
            },
            // Draw
            'Y' => {
                player_throw = opponent_throw;
                score += POINTS_ON_DRAW
            },
            // Win
            'Z' => {
                player_throw = match opponent_throw {
                    'R' => 'P',
                    'P' => 'S',
                    'S' => 'R',
                    _ => ' ',
                };
                score += POINTS_ON_WIN;
            },
            _ => {eprintln!("An error has been encountered- desired_outcome={}", desired_outcome);},
        }

        score += match player_throw {
            'R' => 1,
            'P' => 2,
            'S' => 3,
            _   => i32::MIN,
        };
    }

    score
}