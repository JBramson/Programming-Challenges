// https://adventofcode.com/2020/day/1
// Objective: Given a range of occurances of a certain char in a string, see if
// a given password is valid, returning the number of valid passwords from a list.

#include <iostream>
#include <vector>
#include <fstream>
#include <string>

using namespace std;

bool is_password_valid(char given_char, int lower, int upper, string password) {
    int num_matches = 0;
    for (char c : password) {
        if (c == given_char) {
            num_matches++;
            if (num_matches > upper) return false; // If we've exceeded the maximum, the password is invalid
        }
    }
    return num_matches >= lower; // Return if we have at least the minimum number of matches
}


int main() {
    ifstream f;// Open reader
    f.open("input.txt");
    if (!f.is_open()){ // If we didn't open the input successfully, announce this and exit.
        cout << "Couldn't open input.txt";
        return 1;
    }

    int num_valid = 0;
    string range_str, char_str, pass;
    int min, max; // Hold the upper and lower bounds for a given char
    char character; // The character of interest

    while (!f.eof()) {
        f >> range_str; // Get range with hyphen (-)
        bool on_min = true;
        string min_str, max_str;
        
        // Convert range to ints
        for (char c : range_str) {
            if (c == '-') { // If we've hit a midpoint, start building the max
                on_min = false;
                continue;
            }
            if (on_min) {
                min_str += c;
            } else{
                max_str += c;
            }
        }

        // Convert string bounds to int bounds
        min = stoi(min_str);
        max = stoi(max_str);
        f >> char_str >> pass; // "char:", "password"

        character = char_str[0]; // Extract the char (ignore the ':')

        num_valid += is_password_valid(character, min, max, pass);
    }

    cout << num_valid << endl;


    return 0;
}