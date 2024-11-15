using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleFormsC_
{
    public partial class это_точно_cmd : Form
    {
        String direct = Directory.GetCurrentDirectory();
        String absDirect = Directory.GetCurrentDirectory();
        String dir = "localhost:~";
        Stopwatch watch = new Stopwatch();

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
        public это_точно_cmd()
        {
            watch.Start();

            PictureBox pictureBox = new PictureBox();
            InitializeComponent();
            imgItsOk.Load(Directory.GetCurrentDirectory() + @"\imgs\eyes-black.gif");
            imgItsOk.SizeMode = PictureBoxSizeMode.StretchImage;
            
           
            lblCurDir.Text = dir;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void exit_Click(object sender, EventArgs e)
        {
            watch.Stop();
            Close();
        }

        private void output_Click(object sender, EventArgs e)
        {

        }

        private void imgItsOk_Click(object sender, EventArgs e)
        {
            
        }

        private void upperTextBoxChecker_Tick(object sender, EventArgs e)
        {
            if (upperTextBox.Text == "a")
            {
                lowerTextBox.Text = "abrocos";
            }
            else
            {
                lowerTextBox.Text = upperTextBox.Text;
            }
            upperTextBoxChecker.Stop();
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void это_точно_cmd_Load(object sender, EventArgs e)
        {

        }

        private void lblDir_Click(object sender, EventArgs e)
        {

        }

        private void checking(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                compiling();
                upperTextBox.Text = null;
            }
        }

        public void compiling()
        {
            lowerTextBox.Text = null;
            String input = upperTextBox.Text.Substring(0, upperTextBox.Text.Length-2);
            //System.Console.Write(dir + "# ");
            // input = System.Console.ReadLine();
            upperTextBox.Text = null;
            
            if (input.Length < 2) // таких команд точно нет
                lowerTextBox.Text = "too short!";
            else if (input == "exit")
            {
                watch.Stop();
                Close();
            }
            else if (input.Length > 1 && input.Substring(0, 2) == "cd") // меняет текущую директорию
            {
                if (input == "cd")
                {
                    string[] tempDir = dir.Split('\\');
                    string[] tempDirect = direct.Split('\\');
                    string[] tempAbsDirect = absDirect.Split('\\');
                    if ((tempAbsDirect.Length) >= tempDirect.Length)
                    {
                        dir = "localhost:~";
                        direct = absDirect;
                        lblCurDir.Text = dir;
                    }
                    else
                    {
                        // string[] tempDir = dir.Split('\\');
                        // string[] tempDirect = direct.Split("\\");
                        dir = tempDir[0];
                        direct = tempDirect[0];
                        for (int i = 1; i < (tempDir.Length - 1); i++)
                        {
                            dir += "\\" + tempDir[i];
                        }
                        for (int i = 1; i < (tempDirect.Length - 1); i++)
                        {
                            direct += "\\" + tempDirect[i];
                        }
                        lblCurDir.Text = dir;
                    }
                }

                else if (input.Length>3 && Directory.Exists(direct + "\\" + input.Substring(3)) && input.Substring(3, 1) != " ")
                {
                    dir += "\\" + input.Substring(3);
                    direct += "\\" + input.Substring(3);
                    lblCurDir.Text = dir;
                }
                else
                {
                    lowerTextBox.Text = "\"cd: can't do to \" + input.Substring(3) + \": No such file or directory\"";
                }
            }
            else if (input == "clear") // очищает консоль
            {
                // watch.Restart();
                lowerTextBox.Text = null;
            }
            else if ((input.Length == 2 && input.Substring(0, 2) == "ls") | (input.Length >= 3 && input.Substring(0, 3) == "ls ")) // проверка на вывод данных
            {
                if (input.Length < 4) // что помельче
                {
                    var x = new DirectoryInfo(direct);
                    FileInfo[] files = x.GetFiles();
                    string str = "";
                    lowerTextBox.ForeColor = Color.Magenta;
                    foreach (var director in System.IO.Directory.GetDirectories(direct))
                    {
                        var tempDir = new DirectoryInfo(director);
                        var tempDirName = tempDir.Name;
                        //System.Console.ForegroundColor = ConsoleColor.Magenta;
                        //System.Console.Write(tempDirName + '\t');
                        
                        lowerTextBox.Text += tempDirName + '\t';
                        //str += tempDirName + "\t";
                    }
                    lowerTextBox.ForeColor = Color.White;
                    //System.Console.ForegroundColor = ConsoleColor.White;
                    foreach (FileInfo file in files)
                    {
                        str = str + file.Name + '\t';
                    }
                    //System.Console.WriteLine(str);
                    lowerTextBox.Text += str;
                }
                else // вывод из другого файла
                {
                    if (File.Exists(direct + '\\' + input.Substring(3)))
                    {
                        string text = File.ReadAllText(direct + '\\' + input.Substring(3));
                        //System.Console.WriteLine(text);
                        lowerTextBox.Text = text;
                    }
                    else if (Directory.Exists(direct + '\\' + input.Substring(3)))
                    {
                        var x = new DirectoryInfo(direct + '\\' + input.Substring(3));
                        FileInfo[] files = x.GetFiles();
                        string str = "";
                        lowerTextBox.ForeColor = Color.Magenta;
                        foreach (var director in System.IO.Directory.GetDirectories(direct + '\\' + input.Substring(3)))
                        {
                            var tempDir = new DirectoryInfo(director);
                            var tempDirName = tempDir.Name;
                            // System.Console.ForegroundColor = ConsoleColor.Magenta;
                            lowerTextBox.Text += tempDirName + '\t';
                           // System.Console.Write(tempDirName + '\t');
                            //str += tempDirName + "\t";
                        }
                        //System.Console.ForegroundColor = ConsoleColor.White;
                        lowerTextBox.ForeColor = Color.White;
                        foreach (FileInfo file in files)
                        {
                            str = str + file.Name + '\t';
                        }
                        //System.Console.WriteLine(str);
                        lowerTextBox.Text += str;
                    }
                    else
                    {
                        //System.Console.WriteLine(input + ": No such file or directory");
                        lowerTextBox.Text += input + ": No such file or directory";
                    }
                }

            }
            else if (input == "echo") // определяем создание фйлов/вывод на экран(1)
            {
                //System.Console.WriteLine();
                lowerTextBox.Text = null;
            }
            else if (input.Length > 4 && input.Substring(0, 5) == "mkdir") // создаём директорию
            {
                if (input.Length < 6)
                    lowerTextBox.Text = "no such command";
                else
                    Directory.CreateDirectory(direct + '\\' + input.Substring(6));
            }
            else if (input.Length > 4 && input.Substring(0, 5) == "echo ") // определяем создание фйлов/вывод на экран(2)
            {
                string[] sp = input.Split(' ');
                if (sp.Length > 2)
                    if (sp[2] == ">")
                        CreateFile(direct + "\\" + sp[3], sp[1]);
                    else
                        lowerTextBox.Text = "no command " + input + " found";
                        //System.Console.WriteLine("no command " + input + " found");
                else if (sp.Length == 2)
                    lowerTextBox.Text = sp[1];
                //System.Console.WriteLine(sp[1]);

                else if (sp.Length == 1)
                    lowerTextBox.Text = null;
                    //System.Console.WriteLine();
                else
                    lowerTextBox.Text = "no command " + input + " found";
                   // System.Console.WriteLine("no command " + input + " found");
            }
            else if (input == "uptime") // находим текущее время и время работы cmd
            {
                string time = DateTime.Now.ToString("h:mm:ss tt");
                lowerTextBox.Text = time + " " + watch.ElapsedMilliseconds/60000 + " min";
                //        System.Console.WriteLine(time + " " + watch.ElapsedMilliseconds / 3600 + " min");
            }
            else if (input.Length > 4 && input.Substring(0, 5) == "head ")
            {
                if (File.Exists(direct + "\\" + input.Substring(5)))
                {
                    var tempText = File.ReadAllLines(direct + "\\" + input.Substring(5));
                    for (int i = 0; i < tempText.Length; i++)
                    {
                        if (i == 10)
                            break;
                        System.Console.WriteLine(tempText[i]);
                    }
                }
            }
            else if (input.Length>4 && input.Substring(0, 3) == "chi")
            {
                if (File.Exists(direct + '\\' + input.Substring(4)))
                    imgItsOk.Load(direct + '\\' + input.Substring(4));
                else
                    lowerTextBox.Text = "c: " + input.Substring(4) + " no such file or directory";
            }
            else
            {
                lowerTextBox.Text = "no command " + input + " found";
                //System.Console.WriteLine("no command " + input + " found");
            }



        }
    }
}
