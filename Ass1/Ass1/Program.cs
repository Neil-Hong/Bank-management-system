using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ass1
{
    class Program
    {
        protected static int origRow;
        protected static int origCol;

        protected static void WriteAt(string s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(origCol + x, origRow + y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }
        static void Main(string[] args)
        {
            Console.Clear(); //Clean the screen      
            LoginScreen();
            Console.ReadKey();
        }

        /// <summary>
        ///Draw the screen layout of login page 
        /// </summary>
        public static void LoginScreen()
        {
            #region Load Login.txt file
            List<User> usersGroup = new List<User>();
            string path = @"login.txt";
            using (StreamReader sr = new StreamReader(path))
            {
                while (sr.Peek() >= 0)
                {
                    string str;
                    string[] strArray;
                    str = sr.ReadLine();
                    strArray = str.Split('|');
                    User user = new User
                    {
                        UserName = strArray[0],
                        PassWord = strArray[1]
                    };
                    usersGroup.Add(user);
                }
            }
            #endregion

            while (true)
            {
                origRow = Console.CursorTop;
                origCol = Console.CursorLeft;

                WriteAt("╔", 30, 5);
                //Draw the left line, from top to bottom
                for (int i = 6; i < 16; i++)
                {
                    WriteAt("║", 30, i);
                }
                //Draw the top line
                for (int i = 31; i < 80; i++)
                {
                    WriteAt("─", i, 5);
                }
                WriteAt("╗", 80, 5);
                //Draw the right line, from top to bottom
                for (int i = 6; i < 16; i++)
                {
                    WriteAt("║", 80, i);
                }
                WriteAt("╚", 30, 16);
                //Draw the bottom line
                for (int i = 31; i < 80; i++)
                {
                    WriteAt("─", i, 16);
                }
                WriteAt("╝", 80, 16);
                WriteAt("WELCOME TO SIMPLE BANKING SYSTEM", 41, 6);
                WriteAt("LOGIN TO START", 49, 7);
                //Draw the middle line
                for (int i = 31; i < 79; i++)
                {
                    WriteAt("▓", i, 9);
                }
                WriteAt("UserName: ", 44, 12);
                WriteAt("PassWord: ", 44, 13);

                Console.SetCursorPosition(54, 12);
                string username = Console.ReadLine();
                Console.SetCursorPosition(54, 13);
                string password = ReadPassword(); ;
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
               
                #region Check accounts validation
                for (int i = 0; i < usersGroup.Count; i++)
                {
                    if ((usersGroup[i].UserName == username) && (usersGroup[i].PassWord == password))
                    {
                        Console.WriteLine("\t\t\t\t\t\tValid credential!...");
                        Main_Menu bank = new Main_Menu();
                        bank.Continue();

                        //bank.ShowAllUsers();
                        bank.InitialAccount();

                        bank.showMainMenu();

                    }
                    else if ((usersGroup[i].UserName != username) && (usersGroup[i].PassWord == password))
                    {
                        Console.WriteLine("\n\t\t\t\tYour account does not exist, please try again.");
                        Console.ReadKey(false);
                        Console.Clear();
                        break;
                    }
                    else if ((usersGroup[i].UserName == username) && (usersGroup[i].PassWord != password))
                    {
                        Console.WriteLine("\n\t\t\t\tThe password is not right, please try again. \n\t\t\t\t\tPassword you entered is : " + password + "\n\t\t\t\t\tPress any key to continue...");
                        Console.ReadKey(false);
                        Console.Clear();
                        break;
                    }
                    else if ((usersGroup[i].UserName != username) && (usersGroup[i].PassWord != password))
                    {
                        Console.WriteLine("\n\t\t\t\tYour account does not exist, please try again.");
                        Console.ReadKey(false);
                        Console.Clear();
                        break;
                    }
                }
                #endregion

            }
        }

        #region Display “*” instead of the actual characters 
        public static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    password += info.KeyChar;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        password = password.Substring(0, password.Length - 1);
                        int pos = Console.CursorLeft;
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                }
                info = Console.ReadKey(true);
            }
            #endregion
            Console.WriteLine();
            return password;
        }
    }
}
