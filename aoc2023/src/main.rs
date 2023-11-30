use std::{env, io::Write};
use std::fs::File;
use std::fs::create_dir_all;

use helpers::{RunMode, PuzzlePart};

mod helpers;

// ./aoc2023 <run_mode=DEPLOYMENT> <puzzle_part=PART_ONE>
fn main() {
    let mut run_mode = RunMode::DEPLOYMENT;
    let mut puzzle_part = PuzzlePart::PART_ONE;

    let mut args = env::args().skip(1);
    if args.len() > 2 {
        panic!("Too many args! I need no more than 2 passed-in.");
    }
    while let Some(arg) = args.next() {
        match &arg[..] {
            "deployment" | "deploy" | "release" => run_mode = RunMode::DEPLOYMENT,
            "debug" | "dbg" | "testing" => run_mode = RunMode::DEBUG,
            "1" | "one" => puzzle_part = PuzzlePart::PART_ONE,
            "2" | "two" => puzzle_part = PuzzlePart::PART_TWO,
            "setup" => {
                create_dir_all("input/").expect("Can't make input/ dir.");
                let mut file = File::create("input/debug_input.txt").expect("Couldn't create debug_input.txt");
                file.write_all(b"Put the debug input here!").expect("Couldn't write to debug_input.txt");
                file = File::create("input/deployment_input.txt").expect("Couldn't create deployment_input.txt");
                file.write_all(b"Put the deployment input here!").expect("Couldn't write to deployment_input.txt");
                print!("Created files. Please insert values before running again.");
                return
            },
            _ => panic!("I don't understand the \"{arg}\" arg. :("),
        }
    }

}