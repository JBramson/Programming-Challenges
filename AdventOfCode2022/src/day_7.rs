/*
 * https://adventofcode.com/2022/day/7
 * Objective: Given a series of console commands and output,
 * Part 1: Find the sum of all folders that have a size no greater than 100,000
 * Part 2:
 * Part of me learning Rust.
 */
use crate::day_7::CommandMode::{CHANGE_DIR, LIST_DIR};
use crate::helpers::RunMode;
use std::collections::HashMap;

// Files are stored as dirs with a non-zero size
struct Directory {
    parent: String,
    children: Vec<String>,
    size: i32, // 0 for dirs, non-zero for files
}

pub fn part_1_solution(input_strings: Vec<String>, run_mode: RunMode) -> String {
    let mut directories: HashMap<&str, Directory> = HashMap::new();
    let mut current_dir = "/"; // The root dir is its own parent

    // Hard-set the root dir, as it is its own parent
    directories.insert(current_dir,  Directory{parent: String::from(current_dir), children: vec![], size: 0});

    for line in input_strings {
        if line.chars().nth(0).unwrap() == '$' {
            if &line[2..4] == "cd" {
                if &line[5..] == ".." {
                    todo!("Move up 1 dir");
                } else {
                    current_dir = &line[5..];
                }
            }
        } else {
            // Read in dir contents
            //TODO: For BOTH, don't forget to add the new location's name as a child to the parent dir.
            if &line[0..3] == "dir" {
                directories.insert(current_dir,  Directory{parent: String::from(current_dir), children: vec![], size: 0});
            } else {
                //TODO: Split line input by the space and use the values to add the "dir".
            }
        }
        println!(" {line}");
    }

    String::from("Solution in-progress.")
}

pub fn part_2_solution(input_strings: Vec<String>, run_mode: RunMode) -> String {


    String::from("Solution in-progress.")
}