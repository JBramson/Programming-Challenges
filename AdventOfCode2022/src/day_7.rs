/*
 * https://adventofcode.com/2022/day/7
 * Objective: Given a series of console commands and output,
 * Part 1: Find the sum of all folders that have a size no greater than 100,000
 * Part 2:
 * Part of me learning Rust.
 */
use crate::helpers::RunMode;
use std::collections::HashMap;

// Files are stored as dirs with a non-zero size
struct Directory {
    parent: String,
    children: Vec<String>,
    size: i32, // 0 for dirs, non-zero for files
}
impl Directory {
    fn add_child(&mut self, child: String) {
        self.children.push(child);
    }
}

fn assign_children(directories: &mut HashMap<String, Directory>) {
    for mut directory in directories {
        // Avoid having root list itself
        if directory.0 != "/" {
            for second_dir in directories {
                if second_dir.1.parent == *directory.0 {
                    directory.1.add_child(second_dir.0.clone());
                }
            }
        }
        // println!("{}: parent={}, children={:?}, size={}", directory.0, directory.1.parent, directory.1.children, directory.1.size);
    }
}

// TODO: make a function that recursively finds & assigns the size of each dir

pub fn part_1_solution(input_strings: Vec<String>, run_mode: RunMode) -> String {
    let mut directories: HashMap<String, Directory> = HashMap::new();
    let mut current_dir: String = String::from("/"); // The root dir is its own parent

    // Hard-set the root dir, as it is its own parent
    directories.insert(current_dir.clone(),  Directory{parent: current_dir.clone(), children: vec![], size: 0});

    for line in input_strings {
        // Interpret commands
        if line.chars().nth(0).unwrap() == '$' {
            if &line[2..4] == "cd" {
                if &line[5..] == ".." {
                    current_dir = match directories.get(&current_dir) {
                        None => {String::from("Error at {current_dir}- couldn't find dir.")}
                        Some(parent_dir) => {parent_dir.parent.clone()}
                    }
                } else {
                    current_dir = String::from(&line[5..]);
                }
            }
        } else {
            // Read in dir contents
            if &line[0..3] == "dir" {
                directories.insert(String::from(&line[4..]),  Directory{parent: current_dir.clone(), children: vec![], size: 0});
            } else {
                let file: Vec<&str> = line.split(' ').collect();
                directories.insert(String::from(file[1]), Directory{parent: current_dir.clone(), children: vec![], size: file[0].clone().parse().unwrap() });
            }
        }
        println!(" {line}");
    }

    // TODO: For each 'dir', add it (the string) to its parent's children vec.
    assign_children(&mut directories);

    String::from("Solution in-progress.")
}

pub fn part_2_solution(input_strings: Vec<String>, run_mode: RunMode) -> String {


    String::from("Solution in-progress.")
}