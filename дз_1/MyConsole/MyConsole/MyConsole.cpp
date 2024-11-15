#include<iostream>
#include<string>
#include<filesystem>
namespace fs = std::filesystem;

void ls() {

}

void main() {

	std::string location = "localhost";
	std::string input;
	while (true) {
		std::cout << location << ":~# ";

		std::cout << "Cur path is " << fs::current_path() << std::endl;
        fs::current_path(fs::temp_directory_path());
        std::cout << "Current path is " << fs::current_path() << '\n';
		std::getline(std::cin, input);

		if (input == "ls")
			ls();



	}
}