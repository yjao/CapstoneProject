from GeneralParser import *

if __name__ == '__main__':
    info = FileInfo("cs", "tsv")
    info.exp_path = "../New Unity Project 1/Assets/Scripts"
    info.exp_file = "DataLoader"

    info.src_file = "Characters - Dialogue"
    program = Program(info)
    input("\nEnter anything to continue...")
