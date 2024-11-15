using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace AllFiles
{
    class Console
    {
        public static void CreateFile(string filename, string containment)
        {
            File.Create(filename).Close();
            FileInfo temp = new FileInfo(filename);
            File.WriteAllText(temp.FullName, containment);
           // string folderPath = Path.Combine(Directory.GetCurrentDirectory() + "/folder", "aaa");
            //Directory.CreateDirectory(folderPath);
        }

        public static void CreateDirectory(string path, string name)
        {
            string folderPath = Path.Combine(path, name);
            Directory.CreateDirectory(folderPath);
        }

        public static void Main()
        {
            var watch = Stopwatch.StartNew(); // часыыы
            watch.Start();


            string dir = "localhost:~"; // это для вывода на консоль
            string input; // получаемый ввод
            var direct = Directory.GetCurrentDirectory(); // текущая директория
            var absDirect = direct; // на случай, если основная директория потеряется
            try
            {
                while (true)
                {
                    
                    System.Console.Write(dir + "# ");
                    input = System.Console.ReadLine();
                    if (input == "")
                    {
                        continue;
                    }
                    else if (input.Length < 2) // таких команд точно нет
                        System.Console.WriteLine("no command " + input + " found");
                    else if (input == "exit")
                    {
                        break;
                    }
                    else if (input.Length > 1 && input.Substring(0, 2) == "cd") // меняет текущую директорию
                    {
                        if (input == "cd")
                        {
                            string[] tempDir = dir.Split('\\');
                            string[] tempDirect = direct.Split("\\");
                            string[] tempAbsDirect = absDirect.Split("\\");
                            if ( (tempAbsDirect.Length) >= tempDirect.Length)
                            { 
                            dir = "localhost:~";
                            direct = absDirect;
                            }
                            else
                            {
                               // string[] tempDir = dir.Split('\\');
                               // string[] tempDirect = direct.Split("\\");
                                dir = tempDir[0];
                                direct = tempDirect[0];
                                for (int i=1; i<(tempDir.Length-1); i++)
                                {
                                    dir += "\\" + tempDir[i];
                                }
                                for (int i=1; i<(tempDirect.Length - 1); i++)
                                {
                                    direct += "\\" + tempDirect[i];
                                }
                            }
                        }

                        else if (Directory.Exists(direct + "\\" + input.Substring(3)) && input.Substring(3, 1)!=" ")
                        {
                            dir += "\\" + input.Substring(3);
                            direct += "\\" + input.Substring(3);
                        }
                        else
                        {
                            
                            System.Console.WriteLine("cd: can't do to " + input.Substring(3) + ": No such file or directory");
                        }
                    }
                    else if (input == "clear") // очищает консоль
                    {
                        watch.Restart();
                        System.Console.Clear();
                    }
                    else if ((input.Length == 2 && input.Substring(0, 2) == "ls") | (input.Length >= 3 && input.Substring(0, 3) == "ls ")) // проверка на вывод данных
                    {
                        if (input.Length < 4) // что помельче
                        {
                            var x = new DirectoryInfo(direct);
                            FileInfo[] files = x.GetFiles();
                            string str = "";
                            foreach (var director in System.IO.Directory.GetDirectories(direct))
                            {
                                var tempDir = new DirectoryInfo(director);
                                var tempDirName = tempDir.Name;
                                System.Console.ForegroundColor = ConsoleColor.Magenta;
                                System.Console.Write(tempDirName + '\t');
                                //str += tempDirName + "\t";
                            }
                            System.Console.ForegroundColor = ConsoleColor.White;
                            foreach (FileInfo file in files)
                            {
                                str = str + file.Name + '\t';
                            }
                            System.Console.WriteLine(str);
                        }
                        else // вывод из другого файла
                        {
                            if (File.Exists(direct + "\\" + input.Substring(3)))
                            {
                                string text = File.ReadAllText(direct + "\\" + input.Substring(3));
                                System.Console.WriteLine(text);
                            }
                            else if (Directory.Exists(direct + "\\" + input.Substring(3)))
                            {
                                var x = new DirectoryInfo(direct + "\\" + input.Substring(3));
                                FileInfo[] files = x.GetFiles();
                                string str = "";
                                foreach (var director in System.IO.Directory.GetDirectories(direct + "\\" + input.Substring(3)))
                                {
                                    var tempDir = new DirectoryInfo(director);
                                    var tempDirName = tempDir.Name;
                                    System.Console.ForegroundColor = ConsoleColor.Magenta;
                                    System.Console.Write(tempDirName + '\t');
                                    //str += tempDirName + "\t";
                                }
                                System.Console.ForegroundColor = ConsoleColor.White;
                                foreach (FileInfo file in files)
                                {
                                    str = str + file.Name + '\t';
                                }
                                System.Console.WriteLine(str);
                            }
                            else
                            {
                                System.Console.WriteLine(input + ": No such file or directory");
                            }
                        }

                    }
                    else if (input == "echo") // определяем создание фйлов/вывод на экран(1)
                    {
                        System.Console.WriteLine();
                    }
                    else if (input.Length > 4 && input.Substring(0, 5) == "mkdir") // создаём директорию
                    {
                        CreateDirectory(direct, input.Substring(6));
                    }
                    else if (input.Length > 4 && input.Substring(0, 5) == "echo ") // определяем создание фйлов/вывод на экран(2)
                    {
                        string[] sp = input.Split(' ');
                        if (sp.Length > 2)
                            if (sp[2] == ">")
                                CreateFile(direct + "\\" + sp[3], sp[1]);
                            else
                                System.Console.WriteLine("no command " + input + " found");
                        else if (sp.Length == 2)
                            System.Console.WriteLine(sp[1]);
                        else if (sp.Length == 1)
                            System.Console.WriteLine();
                        else
                            System.Console.WriteLine("no command " + input + " found");
                    }
                    else if (input == "uptime") // находим текущее время и время работы cmd
                    {
                        string time = DateTime.Now.ToString("h:mm:ss tt");
                        System.Console.WriteLine(time + " " + watch.ElapsedMilliseconds/3600 + " min");
                    }
                    else if (input.Length>4 && input.Substring(0, 5) == "head ")
                    {
                        if (File.Exists(direct + "\\" + input.Substring(5)))
                        {
                            var tempText = File.ReadAllLines(direct + "\\" + input.Substring(5));
                            for (int i=0; i<tempText.Length; i++)
                            {
                                if (i == 10)
                                    break;
                                System.Console.WriteLine(tempText[i]);
                            }
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("no command " + input + " found");
                    }


                }
            }
            catch(Exception e)
            {
                Main();
            }


            watch.Stop();
        }
       
    }
}