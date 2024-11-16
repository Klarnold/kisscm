
using System.Net.Quic;
using System.Xml.Linq;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Specialized;

namespace VirualLanguage
{
    class Syntax
    {
        public static void Make_Dict(string false_name, int tabulation, XElement main_dict, bool main = false)
        {
            XElement false_main_dict = new XElement(false_name);

            string tempString = "";
            while (true)
            {
                for (int i = 0; i < tabulation; i++)
                    System.Console.Write('\t');
                tempString = System.Console.ReadLine();
                if (tempString.Trim().Substring(0, 1) == ")") //конец словаря
                {
                    if (main)
                        return;
                    else
                    {
                        main_dict.Add(false_main_dict);
                        return;
                    }
                }
                else if (tempString.TrimStart().Contains("=")) // новая переменная словаря
                {
                    if (tempString.Trim().Contains(","))
                        tempString = tempString.Trim().Substring(0, tempString.Trim().Length - 1);
                    else
                        tempString = tempString.Trim();
                    int letters = 0;

                    foreach (char letter in tempString.Split('=')[1])
                        if (Char.IsLetter(letter))
                            letters++;

                    if (main)
                        if (letters == 0)
                            main_dict.Add(new XElement(tempString.Split('=')[0], int.Parse(tempString.Trim().Split('=')[1])));
                        else
                            main_dict.Add(new XElement(tempString.Split('=')[0], tempString.Trim().Split('=')[1]));
                    else
                        if (letters == 0)
                        false_main_dict.Add(new XElement(tempString.Split('=')[0], int.Parse(tempString.Trim().Split('=')[1])));
                    else
                        false_main_dict.Add(new XElement(tempString.Split('=')[0], tempString.Trim().Split('=')[1]));
                }
                else // словарь переменная
                    if (main)
                    Make_Dict(tempString.Trim().Split(' ')[0], tabulation + 1, main_dict);
                else
                    Make_Dict(tempString.Trim().Split(' ')[0], tabulation + 1, false_main_dict);
            }

        }
        public static void Elements_Incoming(XDocument xdoc)
        {

            string code = "";
            System.Console.Write("virtual_language(writing)$: ");
            code = Console.ReadLine();
            if (code.Trim() == "")
                Elements_Incoming(xdoc);
            else if (code.Trim() == "exit")
            {
                return;
            }
            else if (code.Trim() == "<#")
            {
                // нужно дописать проверку на место записи комментария 
                string tempComment = "";
                string tempCheckComment = "";
                bool going = true;
                int tabulation = 1;
                tempCheckComment = System.Console.ReadLine();
                if (tempCheckComment.Trim() == "#>")
                {
                    xdoc.Elements("All").Single().Elements("variables").Single().Add(new XComment(tempComment));
                    going = false;
                }
                else
                    tempComment += tempCheckComment;
                if (going) {
                    while (true)
                    {
                        tempCheckComment = System.Console.ReadLine();
                        if (tempCheckComment.Trim() == "#>")
                            break;
                        tempComment += "\n";
                        for (int i = 0; i < tabulation; i++)
                            tempCheckComment = "    " + tempCheckComment;
                        tempComment += tempCheckComment;
                    }
                    xdoc.Elements("All").Single().Elements("variables").Single().Add(new XComment(tempComment));
                }
            }
            else if (code.Contains('=')) {
                string[] tempString = code.Trim().Split('=');
                int letterCount = 0;
                foreach (char letter in tempString[0])
                    if (Char.IsLetter(letter))
                        letterCount++;
                if (letterCount == 0)
                    System.Console.WriteLine("Incorrect identifier: \"" + tempString[0] + '\"');
                else
                {
                    letterCount = 0;
                    foreach (char letter in tempString[1])
                        if (Char.IsLetter(letter))
                            letterCount++;
                    if (letterCount == 0)
                    {
                        xdoc.Elements("All").Single().Elements("variables").Single().Add(new XElement(tempString[0], int.Parse(tempString[1])));
                    }
                    else
                    {
                        xdoc.Elements("All").Single().Elements("variables").Single().Add(new XElement(tempString[0], tempString[1]));
                    }
                }
            }
            else if (code.Trim().Contains(" ") && code.Trim().Split(" ")[1] == "dict(") // сначала пишем имя и через пробел "dict("
            {
                int tabulation = 1;
                XElement dict = new XElement(code.Trim().Split(" ")[0]);
                Make_Dict("none", tabulation, dict, true);
                xdoc.Elements("All").Single().Elements("variables").Single().Add(dict);
            }
            else
                System.Console.WriteLine("There is no such command as: \"" + code.Trim() + "\"");


            Elements_Incoming(xdoc);
        }

        public static void Elements_Outgoing(XDocument xdoc) {

            string code = "";
            int first_variable = 0;
            int second_variable = 0;
            bool first_bool = false;
            bool second_bool = false;
            while (true)
            {
                System.Console.Write("virtual_language(working)$: ");
                code = System.Console.ReadLine();

                if (code == "")
                    continue;
                else if (code.Trim() == "exit")
                    return;
                else if (code.Trim().Length == 1)
                {
                    if (second_bool)
                    {
                        switch (code.Trim())
                        {
                            case "+":
                                xdoc.Elements("All").Single().Elements("output").Single().Add(new XElement($"_{first_variable}_addition_{second_variable}", first_variable + second_variable));
                                break;
                            case "-":
                                xdoc.Elements("All").Single().Elements("output").Single().Add(new XElement($"_{first_variable}-{second_variable}", first_variable - second_variable));
                                break;
                            case "*":
                                xdoc.Elements("All").Single().Elements("output").Single().Add(new XElement($"_{first_variable}*{second_variable}", first_variable * second_variable));
                                break;
                            case "/":
                                xdoc.Elements("All").Single().Elements("output").Single().Add(new XElement($"_{first_variable}_division_{second_variable}", first_variable / second_variable));
                                break;
                            case "^":
                                xdoc.Elements("All").Single().Elements("output").Single().Add(new XElement($"_{first_variable}_pow_{second_variable}", Math.Pow(first_variable, second_variable)));
                                break;
                        }
                        first_bool = second_bool = false;
                    }
                    else
                        System.Console.WriteLine($"Can not do opereation due to the lack of active variables: firs_variable = {first_bool}; second_variable = {second_bool}");
                }
                else if (code.Trim()[0] == '(')
                {
                    code = code.Trim().Substring(0, code.Trim().Length - 1);
                    xdoc.Elements("All").Single().Elements("output").Single().Add(new XElement(code.Split(" ")[1], code.Split(" ")[2]));
                }
                else if (code.Trim()[0] == '[')
                {
                    if (!first_bool)
                    {
                        first_bool = true;
                        first_variable = int.Parse(xdoc.Elements("All").Single().Elements("output").Single().Elements(code.Trim().Substring(1, code.Trim().Length - 2)).Single().Value);
                    }
                    else if (!second_bool)
                    {
                        second_bool = true;
                        second_variable = int.Parse(xdoc.Elements("All").Single().Elements("output").Single().Elements(code.Trim().Substring(1, code.Trim().Length - 2)).Single().Value);
                    }
                    else
                        System.Console.WriteLine("Too much active variables!");
                }
                else if (code.Trim()[0] == '!')
                {
                    if (code.Trim().Length > 4 && code.Trim().Substring(1, 4) == "find")
                    {
                        if (!first_bool)
                        {
                            XElement temp_xelem = xdoc.Elements("All").Single().Elements("variables").Single();
                            first_bool = true;

                            foreach (string part_of_path in code.Trim().Split(" ")[1].Split("/"))
                                temp_xelem = temp_xelem.Elements(part_of_path).Single();

                            first_variable = int.Parse(temp_xelem.Value);
                        }
                        else if (!second_bool)
                        {
                            XElement temp_xelem = xdoc.Elements("All").Single().Elements("variables").Single();
                            second_bool = true;

                            foreach (string part_of_path in code.Trim().Split(" ")[1].Split("/"))
                                temp_xelem = temp_xelem.Elements(part_of_path).Single();

                            second_variable = int.Parse(temp_xelem.Value);
                        }
                        else
                            System.Console.WriteLine("Too much active variables!");
                    }
                    else
                        System.Console.WriteLine("There's no such a command: \"" + code.Trim() + "\"");
                }
                else
                {
                    System.Console.WriteLine($"There's no such a command: \"{code.Trim()}\"");
                }
            }

        }


        public static void Main()
        {
            XDocument xdoc = new XDocument(new XElement("All", new XElement("variables")));
            xdoc.Elements("All").Single().Add(new XElement("output"));
            System.Console.WriteLine("----- Type your elements in this part -----");
            Elements_Incoming(xdoc);

            System.Console.WriteLine('\n');
            System.Console.WriteLine("----- Type your code in this part -----");
            Elements_Outgoing(xdoc);
            System.Console.WriteLine('\n');

            xdoc.Save("object.xml");
            xdoc.Save(Console.Out);
            System.Console.WriteLine('\n');
        }
    }
}