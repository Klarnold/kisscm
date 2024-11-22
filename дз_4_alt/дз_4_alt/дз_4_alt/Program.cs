using System.IO;




/*
namespace VirtualBox
{
    class HomeWork_4
    {
        public static void Catch_State(string[] material, BinaryReader input_reader, BinaryWriter output_writer)
        {

        }

        public static void Do_Compile(string[] code)
        {
            using (BinaryWriter input_writer = new BinaryWriter(File.Open(code[0].Split("\\")[code[0].Split("\\").Length - 1], FileMode.OpenOrCreate)))
            {
                for (int i=0; i<3; i++)
                    input_writer.Write("000111");
            }

            using (BinaryReader input_reader = new BinaryReader(File.Open(code[0].Split("\\")[code[0].Split("\\").Length - 1], FileMode.OpenOrCreate)))
            {
                using (BinaryWriter output_writer = new BinaryWriter(File.Open(code[1].Split("\\")[code[1].Split("\\").Length - 1], FileMode.OpenOrCreate)))
                {
                    //step 1


                    //step 2
                    string[] material = new string[3];
                    
                    while (input_reader.BaseStream.Position != input_reader.BaseStream.Length)
                    {
                        material[0] = input_reader.ReadString();
                        material[1] = input_reader.ReadString();
                        material[2] = input_reader.ReadString();
                        foreach ()
                        System.Console.WriteLine(material);
                        Catch_State(material, input_reader, output_writer);
                    }
                }
            }
                    

        }
        public static void Run_App()
        {
            string code = string.Empty;
            


            while (true)
            {
                System.Console.Write("virtualBox$: ");
                code = Console.ReadLine();
                if (code == "exit")
                    break;
                string[] temp_code = code.Trim().Split(" ");

                Do_Compile(temp_code);
            }
        }
        public static void Main()
        {
            Run_App();

            File.WriteAllText("input.dat", string.Empty);
            using (BinaryWriter first = new BinaryWriter(File.Open("input.dat", FileMode.OpenOrCreate))) {
                for (int i = 91; i < 101; i++)
                    first.Write($"{i}");
                first.Write("123456789987654321");
            }

            using (BinaryReader first_reader = new BinaryReader(File.Open("input.dat", FileMode.OpenOrCreate)))
            {
                while (first_reader.BaseStream.Position != first_reader.BaseStream.Length)
                    System.Console.Write($"{first_reader.ReadString()}-");
            }
        }
    }
}
*/


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
            using (BinaryReader memory_reader = new BinaryReader(File.Open("memory.dat", FileMode.OpenOrCreate)))
            {
                using (BinaryWriter temp_memory_writer = new BinaryWriter(File.Open("temp_memory.dat", FileMode.OpenOrCreate)))
                {
                    int count = 0;
                    int final_count = Make_Int(adress);
                    while (memory_reader.BaseStream.Position != memory_reader.BaseStream.Length)
                    {
                        if (count == final_count)
                        {
                            temp_memory_writer.Write(value);
                            memory_reader.ReadString();
                        }
                        else
                            temp_memory_writer.Write(memory_reader.ReadString());
                        count++;
                    }
                }

            }
            File.Delete("memory.dat");
            File.Move("temp_memory.dat", "memory.dat");
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
                    using (BinaryReader memory_reader = new BinaryReader(File.Open("memory.dat", FileMode.OpenOrCreate)))
                    {
                        for (int i = 0; i < (Make_Int(material[1])); i++)
                        {
                            memory_reader.ReadString();
                        }
                        using (StreamReader compile_reader = new StreamReader("compile.csv", true))
                        {
                            using (StreamWriter temp_compile_writer = new StreamWriter("temp_compile.csv", true))
                            {
                                while (!compile_reader.EndOfStream)
                                    temp_compile_writer.WriteLine(compile_reader.ReadLine());
                                temp_compile_writer.WriteLine(memory_reader.ReadString());
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
                    using (BinaryReader memory_reader = new BinaryReader(File.Open("memory.dat", FileMode.OpenOrCreate)))
                    {
                        for (int i = 0; i < (Make_Int(material[2])); i++)
                        {
                            memory_reader.ReadString();
                        }

                        value = memory_reader.ReadString();
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
                    using (BinaryReader memory_reader = new BinaryReader(File.Open("memory.dat", FileMode.OpenOrCreate)))
                    {
                        int count = Make_Int(material[2]);
                        for (int i = 0; i < count; i++)
                            memory_reader.ReadString();
                        str_value = memory_reader.ReadString();
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


            File.WriteAllText("memory.dat", string.Empty);
            using (BinaryWriter fwriter = new BinaryWriter(File.Open("memory.dat", FileMode.OpenOrCreate)))
            {
                for (int i = 0; i < Math.Pow(2, 6); i++)
                    fwriter.Write("-");
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

            using (BinaryReader memory_reader = new BinaryReader(File.Open("memory.dat", FileMode.OpenOrCreate)))
            {
                while (memory_reader.BaseStream.Position != memory_reader.BaseStream.Length)
                    System.Console.WriteLine(memory_reader.ReadString());
            }
        }

    }
}