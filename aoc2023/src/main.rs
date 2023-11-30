use std::env;

use helpers::{RunMode, PuzzlePart};

mod helpers;

// ./aoc2023 <run_mode=DEPLOYMENT> <puzzle_part=PART_ONE>
fn main() {
    let mut run_mode = RunMode::DEPLOYMENT;
    let mut puzzle_part = PuzzlePart::PART_ONE;

    let args = env::args().skip(1);
    while let Some(arg) = args.next() {
        match &arg[..] {
            "deployment" | "deploy" | "release" => run_mode = RunMode::DEPLOYMENT,
            "debug" | "dbg" | "testing" => run_mode = RunMode::DEBUG,
            "1" | "one" => puzzle_part = PuzzlePart::PART_ONE,
            "2" | "two" => puzzle_part = PuzzlePart::PART_TWO,
        }
    }

    match args.len() {
        2 => {
            run_mode = match args[1].as_str().to_uppercase() {
                String::from("DEPLOYMENT") => RunMode::DEPLOYMENT,
                "DEBUG" => RunMode::DEBUG,
                _ => panic!("Invalid input {args[1]}")
            }
        }
    }
    for arg in args {
        println!("\t{arg}");
    }
}
