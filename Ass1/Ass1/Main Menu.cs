using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ass1
{
    class Main_Menu
    {
        private User[] _userGroup;

        protected static int origRow;
        protected static int origCol;

        internal User[] UserGroup { get => _userGroup; set => _userGroup = value; }
        public List<Account> accountList = new List<Account>();

        public void InitialAccount()
        {
            DirectoryInfo di = new DirectoryInfo(@"/Users/Memoriaa/Desktop/IT/.Net Application/Ass1/Ass1/Ass1/bin/Debug");
            foreach (var fi in di.GetFiles())
            {
                if (int.TryParse(fi.Name, out int i))
                {
                    string path = @fi.Name;
                    string readText = File.ReadAllText(path);
                    using (StreamReader sr = new StreamReader(path))
                    {
                        List<String> allArray = new List<string>();
                        while (sr.Peek() >= 0)
                        {
                            string str;
                            string[] strArray;
                            str = sr.ReadLine();
                            strArray = str.Split(':');
                            allArray.Add(strArray[1]);
                        }
                        Account account = new Account();
                        account.AccountNumber = Convert.ToInt32(allArray[0]);
                        account.AccountBalance = Convert.ToDouble(allArray[1]);
                        account.FirstName = allArray[2];
                        account.LastName = allArray[3];
                        account.Address = allArray[4];
                        account.PhoneNumber = allArray[5];
                        account.Email = allArray[6];
                        if (accountList.Contains(account) == false)
                        {
                            accountList.Add(account);
                        }
                    }
                }
            }
        }
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
        
        #region MainMenu Layout
        public void showMainMenu()
        {
            do
            {
                origRow = Console.CursorTop;
                origCol = Console.CursorLeft;

                WriteAt("╔", 30, 5);
                //Draw the left line, from top to bottom
                for (int i = 6; i < 19; i++)
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
                for (int i = 6; i < 19; i++)
                {
                    WriteAt("║", 80, i);
                }
                WriteAt("╚", 30, 19);
                //Draw the bottom line
                for (int i = 31; i < 80; i++)
                {
                    WriteAt("─", i, 19);
                }
                WriteAt("╝", 80, 19);
                WriteAt("WELCOME TO SIMPLE BANKING SYSTEM", 41, 7);
                //Draw the middle line
                for (int i = 31; i < 79; i++)
                {
                    WriteAt("▓", i, 9);
                }
                WriteAt("1.Creat a new account", 40, 11);
                WriteAt("2.Search for an account", 40, 12);
                WriteAt("3.Deposit", 40, 13);
                WriteAt("4.Withdraw", 40, 14);
                WriteAt("5.A/C Statement", 40, 15);
                WriteAt("6.Delete account", 40, 16);
                WriteAt("7.Exit", 40, 17);

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("\t\t\t\tPlease Choose : ");
                Console.SetCursorPosition(48, 21);
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        CreateAccount();
                        break;
                    case "2":
                        Console.Clear();
                        SearchForAccount();
                        break;
                    case "3":
                        Console.Clear();
                        Desposit();
                        break;
                    case "4":
                        Console.Clear();
                        Withdraw();
                        break;
                    case "5":
                        Console.Clear();
                        AccountStatement();
                        break;
                    case "6":
                        Console.Clear();
                        DeleteAccount();
                        break;
                    case "7":
                        Console.Clear();
                        System.Environment.Exit(0);
                        break;
                    /* case "8":
                         Console.Clear();
                         PrintAllAccount();
                         break;*/
                    default:
                        Console.WriteLine();
                        Console.WriteLine("\t\t\t\t\t\tInvalid Input!");
                        Continue();
                        break;
                }
            }
            while (true);
        }
        #endregion


        public void CreateAccount()
        {
            #region CreateAccount Layout
            Account account = new Account();
            WriteAt("CREATE A NEW ACCOUNT\n", 45, 11);
            WriteAt("ENTER DAETAILS\n", 47, 13);
            WriteAt("First Name:", 47, 15);
            Console.SetCursorPosition(59, 15);
            account.FirstName = Console.ReadLine();
            WriteAt("Last Name:", 47, 16);
            account.LastName = Console.ReadLine();
            WriteAt("Address:", 47, 17);
            account.Address = Console.ReadLine();
            WriteAt("Phone:", 47, 18);
            account.PhoneNumber = Console.ReadLine();
            #endregion

            #region Check the PhoneNumber Format
            while (true)
            {
                if (account.PhoneNumber.Length > 10)
                {
                    Console.WriteLine();
                    Console.WriteLine("\t\t\t\tPhone length should no more than 10 characters.");
                    Continue();
                    Console.Clear();
                    WriteAt("CREATE A NEW ACCOUNT\n", 45, 11);
                    WriteAt("ENTER DAETAILS\n", 47, 13);
                    WriteAt("First Name:" + account.FirstName, 47, 15);
                    WriteAt("Last Name:" + account.LastName, 47, 16);
                    WriteAt("Address:" + account.Address, 47, 17);
                    WriteAt("Phone:", 47, 18);
                    Console.SetCursorPosition(53, 18);
                    account.PhoneNumber = Console.ReadLine();
                }
                else
                {
                    break;
                }
            }
            #endregion

            WriteAt("Email:", 47, 19);
            account.Email = Console.ReadLine();

            #region Check the Email format
            while (true)
            {
                string temp = "@";
                string temp2 = "uts.edu.au";
                string temp3 = "gmail.com";
                string temp4 = "outlook.com";
                bool @Contained = account.Email.Contains(temp);
                bool eduContained = account.Email.Contains(temp2);
                bool gmailContained = account.Email.Contains(temp3);
                bool outlookContained = account.Email.Contains(temp4);
                if ((@Contained && (eduContained || gmailContained || outlookContained))== false)
                {
                    Console.WriteLine();
                    Console.WriteLine("\t\t\t\tYou enter the wrong email address, please try again");
                    Continue();
                    Console.Clear();
                    WriteAt("CREATE A NEW ACCOUNT\n", 45, 11);
                    WriteAt("ENTER DAETAILS\n", 47, 13);
                    WriteAt("First Name:" + account.FirstName, 47, 15);
                    WriteAt("Last Name:" + account.LastName, 47, 16);
                    WriteAt("Address:" + account.Address, 47, 17);
                    WriteAt("Phone:" + account.PhoneNumber, 47, 18);
                    WriteAt("Email:", 47, 19);
                    Console.SetCursorPosition(53, 19);
                    account.Email = Console.ReadLine();
                }
                else
                {
                    break;
                }
            }
            #endregion

            Random random = new Random();
            int n = random.Next(100001, 9999999);
            account.AccountNumber = n;
            account.AccountBalance = 0;
            Console.WriteLine("\t\t\t\t\tRegister Successfully!\nPlease wait a second...");
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("\t\t\t\t\tShow your Inforamtion below...");
            Console.WriteLine();
            Console.WriteLine("\t\t\t\t\t\tAccount No:" + account.AccountNumber);
            Console.WriteLine("\t\t\t\t\t\tAccount Balance: " + account.AccountBalance);
            Console.WriteLine("\t\t\t\t\t\tFirst Name: " + account.FirstName);
            Console.WriteLine("\t\t\t\t\t\tLast Name: " + account.LastName);
            Console.WriteLine("\t\t\t\t\t\tPhone:" + account.PhoneNumber);
            Console.WriteLine("\t\t\t\t\t\tEmail:" + account.Email);
            Console.WriteLine();
            Console.WriteLine("\t\t\t\t\tIs the information correct (Y/N)?");

            Console.SetCursorPosition(59, 16);
            string yesORno = Console.ReadKey().Key.ToString();

            switch (yesORno)
            {
                case "Y":
                    accountList.Add(account);
                    string accountNumber = Convert.ToString(account.AccountNumber);
                    string path = @accountNumber;

                    if (!File.Exists(path))
                    {
                        string newText = "Account No:" + account.AccountNumber + Environment.NewLine +
                                         "Account Balance:" + account.AccountBalance + Environment.NewLine +
                                         "First Name:" + account.FirstName + Environment.NewLine +
                                         "Last Name:" + account.LastName + Environment.NewLine +
                                         "Address:" + account.Address + Environment.NewLine +
                                         "Phone:" + account.PhoneNumber + Environment.NewLine +
                                         "Email:" + account.Email + Environment.NewLine;
                        File.WriteAllText(path, newText);
                    }
                    Console.WriteLine("\n\t\t\t\t\tAccount Created! Details will be provides via email.");
                    Console.WriteLine();
                    Console.WriteLine("\t\t\t\t\t\tAccount number is " + account.AccountNumber);
                    break;
                case "N":
                    break;
            }
            Continue();
        }

        public void SearchForAccount()
        {
            WriteAt("Please enter details", 45, 11);
            Console.WriteLine();
            WriteAt("Acount Number: ", 45, 13);
            string accountNumber = Console.ReadLine();
            string path = @accountNumber;
            try
            {
                AccountNumberLengthCheck(accountNumber);
                //IsInt(accountNumber);
            }
            catch (ArgumentOutOfRangeException a)
            {
                Console.WriteLine("\t\t\t\t\t" + a.Message);
                Console.WriteLine("\t\t\t\t\tRetry (Y/N)");
                Console.SetCursorPosition(48, 20);
                string yesORno = Console.ReadKey(false).Key.ToString();

                switch (yesORno)
                {
                    case "Y":
                        Console.Clear();
                        Withdraw();
                        break;
                    default:
                        Console.Clear();
                        return;


                }
            }
            try
            {
                if (File.Exists(path))
                {
                    Console.WriteLine();
                    Console.WriteLine("\t\t\t\t\t\tAccount found");
                    Console.WriteLine();
                    string readText = File.ReadAllText(path);
                    Console.Write(readText);
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("\t\t\t\t\t\tAccount not found!");
                }
                Console.WriteLine();
                Console.WriteLine("\t\t\t\t\tCheck another account? (Y/N)");
                Console.SetCursorPosition(59, 26);
                string yesORno = Console.ReadKey().Key.ToString();
                switch (yesORno)
                {

                    case "Y":
                        Console.Clear();
                        SearchForAccount();
                        break;
                    case "N":
                        Continue();
                        break;
                    default:
                        Continue();
                        break;
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Desposit()
        {
            WriteAt("Please enter desposit details\n", 47, 13);
            WriteAt("Account Number: ", 47, 15);

            string accNumber = Console.ReadLine();
            try
            {
                AccountNumberLengthCheck(accNumber);
                //IsInt(accNumber);
            }
            catch (ArgumentOutOfRangeException a)
            {
                Console.WriteLine(a.Message);
                Console.WriteLine("\nRetry (Y/N)");
                string yesORno = Console.ReadKey(false).Key.ToString();

                switch (yesORno)
                {
                    case "Y":
                        Console.Clear();
                        Desposit();
                        break;
                    default:
                        Console.Clear();
                        return;

                }
            }
            Console.WriteLine();
            Console.WriteLine("\t\t\t\tPlease enter deposit ammount:");
            Console.SetCursorPosition(61, 17);
            string input = Console.ReadLine();
            try
            {
                if (input is string)
                {

                }
            }
            catch
            {

                throw;
            }
            int amount = Convert.ToInt32(input);
            CheckAccountNumber myAccountNo = new CheckAccountNumber(Convert.ToInt32(accNumber));
            bool accountExist = !accountList.Exists(x => x.AccountNumber == Convert.ToInt32(accNumber));
            if (accountExist == true)
            {
                Console.WriteLine();
                Console.WriteLine("\t\t\t\t\tSorry, Account does not exist!");
                Console.WriteLine("\t\t\t\t\tRetry? (Y/N)");
                Console.SetCursorPosition(61, 20);
                string yesORno = Console.ReadKey().Key.ToString();

                switch (yesORno)
                {
                    case "Y":
                        Console.Clear();
                        Desposit();
                        break;
                    case "N":
                        break;

                }
            }
            foreach (Account s in accountList.FindAll(new Predicate<Account>(myAccountNo.IsSame)))
            {

                if (s.AccountDesposit(amount) > 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("\t\t\t\t\tDeposit successful!");
                    Console.WriteLine("\t\t\t\t\tCurrent Balance : {0}", s.AccountBalance);
                    PathRewrite(s);
                }
                else Console.WriteLine("\t\t\t\t\tDeposit input cannot lower than 0");
            }
            Continue();
        }

        public void Withdraw()
        {
            WriteAt("Please enter withdraw details\n", 47, 13);
            WriteAt("Account Number: ", 47, 15);
            string accNumber = Console.ReadLine();
            try
            {
                AccountNumberLengthCheck(accNumber);
                //IsInt(accNumber);
            }
            catch (ArgumentOutOfRangeException a)
            {
                Console.WriteLine("\t\t\t\t\t" + a.Message);
                Console.WriteLine("\t\t\t\t\tRetry (Y/N)");
                Console.SetCursorPosition(48, 20);
                string yesORno = Console.ReadKey(false).Key.ToString();

                switch (yesORno)
                {
                    case "Y":
                        Console.Clear();
                        Withdraw();
                        break;
                    default:
                        Console.Clear();
                        return;


                }
            }
            Console.WriteLine("\t\t\t\tPlease enter withdraw amount: ");
            Console.SetCursorPosition(61, 16);
            double amount = Convert.ToDouble(Console.ReadLine());
            CheckAccountNumber myAccountNo = new CheckAccountNumber(Convert.ToInt32(accNumber));
            bool accountExist = !accountList.Exists(x => x.AccountNumber == Convert.ToInt32(accNumber));
            if (accountExist == true)
            {
                Console.WriteLine();
                Console.WriteLine("\t\t\t\t\tSorry, Account does not exist!");
                Console.WriteLine("\t\t\t\t\tRetry? (Y/N)");
                Console.SetCursorPosition(61, 20);
                string yesORno = Console.ReadKey().Key.ToString();

                switch (yesORno)
                {
                    case "Y":
                        Console.Clear();
                        Withdraw();
                        break;
                    case "N":
                        break;
                }
            }
            foreach (Account s in accountList.FindAll(new Predicate<Account>(myAccountNo.IsSame)))
            {

                if (s.AccountWithdraw(amount) > 0)
                {
                    Console.WriteLine("\t\t\t\t\t\tWithdraw successful!");
                    Console.WriteLine("\t\t\t\t\t\tCurrent Balance : {0}", s.AccountBalance);
                    PathRewrite(s);
                }
                else Console.WriteLine("\t\t\t\t\t\tYour amount is wrong.");
            }
            Continue();
        }

        public void AccountStatement()
        {
            WriteAt("Please enter desposit details\n", 47, 13);
            WriteAt("Account Number: ", 47, 15);
            string accNumber = Console.ReadLine();
            try
            {
                AccountNumberLengthCheck(accNumber);
                //IsInt(accNumber);
            }
            catch (ArgumentOutOfRangeException a)
            {
                Console.WriteLine("\t\t\t\t\t" + a.Message);
                Console.WriteLine("\t\t\t\t\tRetry (Y/N)");
                Console.SetCursorPosition(48, 20);
                string yesORno = Console.ReadKey(false).Key.ToString();

                switch (yesORno)
                {
                    case "Y":
                        Console.Clear();
                        AccountStatement();
                        break;
                    default:
                        Console.Clear();
                        return;

                }
            }
            CheckAccountNumber myAccountNo = new CheckAccountNumber(Convert.ToInt32(accNumber));
            try
            {
                bool accountExist = !accountList.Exists(x => x.AccountNumber == Convert.ToInt32(accNumber));
                if (accountExist == true)
                {
                    Console.WriteLine();
                    Console.WriteLine("\t\t\t\t\tSorry, Account does not exist!");
                    Console.WriteLine("\t\t\t\t\tRetry? (Y/N)");
                    Console.SetCursorPosition(61, 20);
                    string yesORno = Console.ReadKey().Key.ToString();

                    switch (yesORno)
                    {
                        case "Y":
                            Console.Clear();
                            AccountStatement();
                            break;
                        case "N":

                            break;
                    }
                }
                foreach (Account s in accountList.FindAll(new Predicate<Account>(myAccountNo.IsSame)))
                {

                    Console.WriteLine("\t\t\t\t\t\tAccount Statement.");
                    string accountNumber = Convert.ToString(s.AccountNumber);
                    string path = @accountNumber;
                    string readText = File.ReadAllText(path);
                    Console.WriteLine(readText);
                    Console.WriteLine("\t\t\t\t\t\tEmail Statement? (Y/N)");
                    Console.SetCursorPosition(72, 25);
                    string yesORno = Console.ReadKey(false).Key.ToString();

                    switch (yesORno)
                    {
                        case "Y":
                            Console.Clear();
                            sendEmail(s.Email, s);
                            break;
                        default:
                            Console.Clear();
                            return;

                    }
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message + "\nMeans that this account has been deleted...");

            }
            Continue();
        }

        public class CheckAccountNumber
        {
            private int _accountNo;
            public CheckAccountNumber(int No)
            { this._accountNo = No; }
            public bool IsSame(Account s)
            { return (s.AccountNumber == _accountNo) ? true : false; }
        }

        public void DeleteAccount()
        {
            WriteAt("Please enter delete details\n", 47, 13);
            WriteAt("Account Number: ", 47, 15);
            string accNumber = Console.ReadLine();

            try
            {
                AccountNumberLengthCheck(accNumber);
               // IsInt(accNumber);
            }
            catch (ArgumentOutOfRangeException a)
            {
                Console.WriteLine();
                Console.WriteLine("\t\t\t\t\t\t" + a.Message);
                Console.WriteLine("\t\t\t\t\t\tRetry? (Y/N)");
                Console.SetCursorPosition(61, 20);
                string yesORno = Console.ReadKey(false).Key.ToString();

                switch (yesORno)
                {
                    case "Y":
                        Console.Clear();
                        DeleteAccount();
                        break;
                    default:
                        Console.Clear();
                        return;

                }
            }


            CheckAccountNumber myAccountNo = new CheckAccountNumber(Convert.ToInt32(accNumber));
            bool accountExist = !accountList.Exists(x => x.AccountNumber == Convert.ToInt32(accNumber));
            if (accountExist == true)
            {
                Console.WriteLine();
                Console.WriteLine("\t\t\t\t\tSorry, Account does not exist!");
                Console.WriteLine("\t\t\t\t\tRetry? (Y/N)");
                Console.SetCursorPosition(61, 20);
                string yesORno = Console.ReadKey().Key.ToString();

                switch (yesORno)
                {
                    case "Y":
                        Console.Clear();
                        DeleteAccount();
                        break;
                    case "N":

                        break;
                }
            }
            foreach (Account s in accountList.FindAll(new Predicate<Account>(myAccountNo.IsSame)))
            {

                try
                {
                    Console.WriteLine("\t\t\t\t\t\tAccount detail show below.");
                    string accountNumber = Convert.ToString(s.AccountNumber);
                    string path = @accountNumber;
                    string readText = File.ReadAllText(path);
                    Console.WriteLine(readText);
                    Console.WriteLine("\t\t\t\t\t\tDelete?(Y/N)");
                    Console.SetCursorPosition(61, 25);
                    string yesORno = Console.ReadKey().Key.ToString();

                    switch (yesORno)
                    {
                        case "Y":
                            try
                            {
                                if (File.Exists(path))
                                {
                                    File.Delete(path);
                                    Console.WriteLine();
                                    Console.WriteLine("\t\t\t\t\t\tAccount Deleted!");
                                    accountList.Remove(s);
                                }
                                else Console.WriteLine("Not find...");
                            }
                            catch (IOException ioExp)
                            {
                                Console.WriteLine(ioExp.Message);
                            }
                            break;
                        case "N":
                            break;
                        default:
                            Console.WriteLine("Not vaild");
                            continue;
                    }
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                }

            }
            Continue();
        }
        public void PathRewrite(Account account)
        {
            string accountNumber = Convert.ToString(account.AccountNumber);
            string path = @accountNumber;

            string newText = "Account No:" + account.AccountNumber + Environment.NewLine +
                                 "Account Balance:" + account.AccountBalance + Environment.NewLine +
                                 "First Name:" + account.FirstName + Environment.NewLine +
                                 "Last Name:" + account.LastName + Environment.NewLine +
                                 "Address:" + account.Address + Environment.NewLine +
                                 "Phone:" + account.PhoneNumber + Environment.NewLine +
                                 "Email:" + account.Email + Environment.NewLine;
            File.WriteAllText(path, newText);
        }
        public void AccountNumberLengthCheck(string account_number)
        {
            if (account_number.Length > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(account_number), "accout number length should no more than 10 characters...");
            }

        }


        /// <summary>
        /// Check input context is int or not
        /// </summary>
        /// <param name="input"></param>
        public void IsInt(string input)
        {
            if (input is string)
            {
                throw new ArgumentOutOfRangeException(nameof(input), "AccountNumber must be number");
            }
        }
        

        #region Email Function
        public void sendEmail(string to, Account account)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("memoriaa94@gmail.com", "Bank Notice");
                mailMessage.To.Add(to);
                mailMessage.Priority = MailPriority.Normal;
                string body = Convert.ToString(account.AccountNumber);
                mailMessage.Body = "Your Account number is: "+body;
                mailMessage.Subject = "Bank System Auto Email";
                mailMessage.IsBodyHtml = true;
                SmtpClient MySmtp = new SmtpClient("smtp.gmail.com", 587);
                MySmtp.Credentials = new System.Net.NetworkCredential("memoriaa94", "Penguin-94");
                MySmtp.EnableSsl = true;
                MySmtp.Send(mailMessage);
                MySmtp = null;
                mailMessage.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fail to Send：" + ex.Message);
            }
        }
        #endregion
        public void Continue()
        {
            Console.WriteLine("\n\t\t\t\t\tPlease press any key to continue");
            Console.ReadKey(false);
            Console.Clear();
        }

    }
}

