use std::{env, io::Write};
use std::fs::File;
use std::fs::create_dir_all;

use helpers::{RunMode, PuzzlePart};

mod helpers;
mod day4;

fn main() {
    let mut run_mode = RunMode::Deployment;
    let mut puzzle_part = PuzzlePart::PartOne;

    let mut args = env::args().skip(1);
    if args.len() > 2 {
        panic!("Too many args! I need no more than 2 passed-in.");
    }
    while let Some(arg) = args.next() {
        match &arg[..] {
            "deployment" | "deploy" | "release" => run_mode = RunMode::Deployment,
            "debug" | "dbg" | "testing" => run_mode = RunMode::Debug,
            "1" | "one" => puzzle_part = PuzzlePart::PartOne,
            "2" | "two" => puzzle_part = PuzzlePart::PartTwo,
            "-s" | "-setup" | "setup" => {
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

    let input_location = match run_mode {
        RunMode::Deployment => "input/deployment_input.txt",
        RunMode::Debug => "input/debug_input.txt",
    };
    let input_strings = helpers::get_lines_from_file(input_location).expect(
        "Couldn't extract lines from file. Have you created it yet with the -s flag?");
    let solution = day4::solve(input_strings, run_mode, puzzle_part);

    println!("Result = {solution:?}");

}