﻿using System.Globalization;
using System.Text;

namespace System
{

    class VirtualBox
    {
        public static string Make_Binary(int value)
        {
            string temp_string = "";
            int temp_value = 0;
            int count = 0;
            for (; count < 29; count++)
            {
                if (temp_value >= value)
                    break;
                temp_value = (int)Math.Pow(2, count);
            }

            while (value != 0)
            {
                if (value >= temp_value)
                {
                    value -= temp_value;
                    temp_string += '1';
                }
                else
                    temp_string += '0';
                count--;
                temp_value /= 2;
            }
            for (; count > 0; count--)
                temp_string += '0';

            return temp_string;
        }
        public static int Make_Int(string str)
        {
            int value = 0;

            for (int i = str.Length; i > 1; i--)
                value += (int)Math.Pow(2 * int.Parse(str[str.Length - i] + ""), i - 1);

            if (str[str.Length - 1] == '1')
                value++;

            return value;
        }
        public static string Make_Binary_Multiplication(string first_str, string second_str)
        {
            string temp_string = string.Empty;
            int pos = 0;
            if (first_str.Length > second_str.Length)
            {
                int count = first_str.Length - second_str.Length;
                for (int i = 0; i < count; i++)
                    temp_string += '0';

                foreach (char obj in first_str.Substring(count))
                    temp_string += obj == second_str[pos++] ? obj : '0';
            }
            else if (first_str.Length < second_str.Length)
            {
                int count = second_str.Length - first_str.Length;
                for (int i = 0; i < count; i++)
                    temp_string += '0';

                foreach (char obj in second_str.Substring(count))
                    temp_string += obj == first_str[pos++] ? obj : '0';
            }
            else
                for (int i = 0; i < first_str.Length; i++)
                    temp_string += first_str[i] == second_str[i] ? first_str[i] : '0';
            return temp_string;
        }

        public static void Add_To_Memory(string value, string adress)
        {
            using (StreamReader memory_reader = new StreamReader("memory.csv", true))
            {
                using (StreamWriter temp_memory_writer = new StreamWriter("temp_memory.csv", true))
                {
                    int count = 0;
                    int final_count = Make_Int(adress);
                    while (!memory_reader.EndOfStream)
                    {
                        if (count == final_count)
                        {
                            temp_memory_writer.WriteLine(value);
                            memory_reader.ReadLine();
                        }
                        else
                            temp_memory_writer.WriteLine(memory_reader.ReadLine());
                        count++;
                    }
                }

            }
            File.Delete("memory.csv");
            File.Move("temp_memory.csv", "memory.csv");
        }
        public static void Do_Match(string[] material, StreamWriter _output_csv_writer, StreamReader _input_csv_reader, string output_path, string input_path)
        {
            switch (material[0]) // material - это строка со всеми необходимыми данными (1 - команда, 2 - по ситуации, 3 - по ситуации)
            {
                case "11100": //1 - загрузка константы, 2 - адресный регистр, 3 - константа
                    Add_To_Memory(material[2], material[1]);

                    using (StreamWriter output_writer = _output_csv_writer)
                    {
                        output_writer.WriteLine(material[1]);
                    }
                    break;
                case "11000": // 1 - чтение значения из памяти, 2 - добавляем значение из memory по адресному регистру, 3 - адресный регистр в output
                    using (StreamReader memory_reader = new StreamReader("memory.csv", true))
                    {
                        for (int i = 0; i < (Make_Int(material[1])); i++)
                        {
                            memory_reader.ReadLine();
                        }
                        using (StreamReader compile_reader = new StreamReader("compile.csv", true))
                        {
                            using (StreamWriter temp_compile_writer = new StreamWriter("temp_compile.csv", true))
                            {
                                while (!compile_reader.EndOfStream)
                                    temp_compile_writer.WriteLine(compile_reader.ReadLine());
                                temp_compile_writer.WriteLine(memory_reader.ReadLine());
                            }
                        }
                        File.Delete("compile.csv");
                        File.Move("temp_compile.csv", "compile.csv");

                        using (StreamWriter output_writer = _output_csv_writer)
                        {
                            output_writer.WriteLine(material[2]);
                        }
                    }
                    break;
                case "10010": // 1 - запись значения в память, 2 - итоговый адресный регистр в memory, 3 - искомый адресный регистр (а значение идёт в output)
                    string value;
                    using (StreamReader memory_reader = new StreamReader("memory.csv", true))
                    {
                        for (int i = 0; i < (Make_Int(material[2])); i++)
                        {
                            memory_reader.ReadLine();
                        }

                        value = memory_reader.ReadLine();
                    }

                    Add_To_Memory(value, material[1]);
                    using (StreamWriter output_writer = _output_csv_writer)
                    {
                        output_writer.WriteLine(value);
                    }
                    break;
                case "11101":

                    using (_output_csv_writer)
                    {
                        _output_csv_writer.WriteLine(material[2]);
                    }
                    string str_value = string.Empty;
                    using (StreamReader memory_reader = new StreamReader("memory.csv"))
                    {
                        int count = Make_Int(material[2]);
                        for (int i = 0; i < count; i++)
                            memory_reader.ReadLine();
                        str_value = memory_reader.ReadLine();
                    }
                    if (str_value[0] == '-')
                    {
                        System.Console.WriteLine("you used an empty cell, what a shame!");
                        break;
                    }
                    using (StreamReader compile_reader = new StreamReader("compile.csv"))
                    {
                        string temp_str_value = compile_reader.ReadLine();
                        while (temp_str_value != null)
                        {
                            str_value = Make_Binary_Multiplication(temp_str_value, str_value);
                            temp_str_value = compile_reader.ReadLine();
                        }
                    }
                    File.WriteAllText("compile.csv", string.Empty);
                    Add_To_Memory(str_value, material[1]);


                    break;
                default:
                    System.Console.WriteLine($"There's no such a program called: \"{material[0]}\", like the hope in the wolrd of ours");
                    break;
            }
        }
        public static void Do_Compile(string input, string output)
        {
            File.WriteAllText(output, string.Empty);
            using (StreamReader csv_reader = new StreamReader(input, true))
            {
                while (!csv_reader.EndOfStream)
                {
                    StreamReader input_reader = new StreamReader(input, true);
                    StreamWriter output_writer = new StreamWriter(output, true);

                    Do_Match(csv_reader.ReadLine().Trim().Split(","), output_writer, input_reader, output, input);

                    input_reader.Close();
                    output_writer.Close();
                }
            }
        }

        public static void Main()
        {
            System.Console.WriteLine("----- Welcome to the work with your VirtualBox! -----");


            File.WriteAllText("memory.csv", string.Empty);
            using (StreamWriter fwriter = new StreamWriter("memory.csv", true))
            {
                for (int i = 0; i < Math.Pow(2, 6); i++)
                    fwriter.WriteLine("-");
                fwriter.Close();
            }
            while (true)
            {
                System.Console.Write("virtualBox_$: ");
                string code = System.Console.ReadLine();
                if (code.Trim() == "exit")
                    break;
                else if (code.Trim().Contains(" "))
                    Do_Compile(code.Trim().Split(" ")[0], code.Trim().Split(" ")[1]);
            }
        }

        }
    }