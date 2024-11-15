#include <iostream>
#include <string>
#include <vector>
#include <algorithm>
using namespace std;
// Function
void printError(const std::string &errorMessage);
void pwd();
void ls();
void cat(const std::string &filename);

int main() {
    std::string input;
    int count = 0;
    std::string* dir = new string[1];
    while (true) {
        std::cout << "localhost:~# ";
        std::getline(std::cin, input);
        dir->append("1" + (char)count++);
        if (input == "pwd") {
            pwd();
        } else if (input == "ls") {
            ls();
        } else if (input.substr(0, 4) == "cat ") {
            std::string filename = input.substr(4);
            cat(filename);
        } else if (input == "exit") {
            for (int i = 0; i < dir->size(); i++)
                cout << dir[i] << " ";
            break;
        } else {
            printError("Command not found.");
        }
    }

    return 0;
}

void printError(const std::string &errorMessage) {
    std::cerr << errorMessage << std::endl;
}

void pwd() {
    std::cout << "/home/user" << std::endl; 
}

void ls() {
    // List files 
    std::vector<std::string> files = {"file1.txt", "file2.txt", "file3.txt"};

    for (const std::string &file : files) {
        std::cout << file << std::endl;
    }
}

void cat(const std::string &filename) {
    // Read and display content of the specified file
    if (filename == "file1.txt") {
        std::cout << "Content of file1.txt" << std::endl;
    } else if (filename == "file2.txt") {
        std::cout << "Content of file2.txt" << std::endl;
    } else if (filename == "file3.txt") {
        std::cout << "Content of file3.txt" << std::endl;
    } else {
        printError("File not found.");
    }
}