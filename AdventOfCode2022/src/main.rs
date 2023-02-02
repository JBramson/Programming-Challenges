use std::fs::File;
use std::io::BufRead;
use std::path::Path;

mod day_1;

fn get_lines_from_file(filename: impl AsRef<Path>) -> std::io::Result<Vec<String>> {
    std::io::BufReader::new(File::open(filename)?).lines().collect()
}

fn main() {
    let on_part_1 = true;
    let in_practice_mode = true;

    let input_location = if in_practice_mode { "inputs/practiceInput.txt" } else { "inputs/input.txt" };
    let input_strings = get_lines_from_file(input_location)
        .expect("File loading failed. :(");

    // Ideally, the only object we need to change in each problem is the referenced day.
    if on_part_1 {
        day_1::part_1_solution(input_strings);
    } else {
        day_1::part_2_solution(input_strings);
    }
}
