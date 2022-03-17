// https://adventofcode.com/2020/day/1
// Objective: Given a list of numbers, find the two numbers
// that sum to 2020 and return their product.

#include <iostream>
#include <vector>
#include <fstream>

using namespace std;

const int find_2020(vector<int>& first_vec, vector<int>& second_vec) {
    // Loop through all of one vector and see if a pair in them sums to 2020
    for (int i = 0; i < first_vec.size() - 1; i++) {
        for (int j = i + 1; j < first_vec.size(); j++) {
            if (first_vec.at(i) + first_vec.at(j) == 2020) {
                return first_vec.at(i) * first_vec.at(j);
            }
        }
    }
    // If none is found, find if it's in the second
    for (int i = 0; i < second_vec.size() - 1; i++) {
        for (int j = i + 1; j < second_vec.size(); j++) {
            if (second_vec.at(i) + second_vec.at(j) == 2020) {
                return second_vec.at(i) * second_vec.at(j);
            }
        }
    }

    // Something should have been found.
    cerr << "No valid value found. :(" << endl;
    return -1;
}

int main() {
    ifstream f;// Open reader
    f.open("input.txt");
    if (!f.is_open()){ // If we didn't open the input successfully, announce this and exit.
        cout << "Couldn't open input.txt";
        return 1;
    }

    // An odd and an even will never sum to 2020, so we can put them in separate containers and check numbers from the container against each other
    vector<int> odds;
    vector<int> evens;
    int new_num;

    while (!f.eof()) {
        f >> new_num;
        if (new_num & 1) { // If new_num's last bit is 1, add it to the odds. 
            odds.push_back(new_num);
        } else { // Otherwise, add it to the evens
            evens.push_back(new_num);
        }
    }

    cout << find_2020(odds, evens) << endl;


    return 0;
}