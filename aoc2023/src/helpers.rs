/*
* To smooth communication between files/functions and
* allow for easier re-use of common patterns.
*/

use std::fs::File;
use std::io::BufRead;
use std::path::Path;



pub enum RunMode {
    DEBUG,
    DEPLOYMENT,
}

pub enum PuzzlePart {
    PART_ONE,
    PART_TWO,
}

pub fn get_lines_from_file(filename: impl AsRef<Path>) -> std::io::Result<Vec<String>> {
    std::io::BufReader::new(File::open(filename)?).lines().collect()
}