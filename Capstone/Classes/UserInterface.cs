using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Capstone.Classes
{
    public class UserInterface
    {
        VendingMachine vendingMachine;


        public UserInterface(VendingMachine vendingMachine)
        {
            this.vendingMachine = vendingMachine;
        }


        public void RunInterface()
        {
            while (true)
            {
                IntroPic();
                MainMenu();

            }

        }
        public void MainMenu()
        {
            Console.WriteLine();
            Console.WriteLine("(1) Display Vending Machine Items");
            Console.WriteLine("(2) Purchase");

            try
            {
                int menuChoice = int.Parse(Console.ReadLine());
                if (menuChoice == 1)
                {
                    Console.WriteLine();
                    string[] displayArray = vendingMachine.DisplayItems();
                    foreach (string item in displayArray)
                    {
                        Console.WriteLine(item);
                    }
                    MainMenu();
                }
                else if (menuChoice == 2)
                {

                    PurchaseMenu();
                }
                else if (menuChoice == 411)
                {
                    SalesReport();
                    Console.WriteLine("*****************************");
                    Console.WriteLine("Sales Report Printed");
                    Console.WriteLine("*****************************");
                    MainMenu();
                }

                else
                {
                    MainMenu();
                }
            }
            catch (System.FormatException e)
            {

                Console.WriteLine("*****************************");
                Console.WriteLine("Please Select A Menu Option.");
                Console.WriteLine("*****************************");
                MainMenu();
            }

        }

        public void PurchaseMenu()
        {
            Console.WriteLine();
            Console.WriteLine("(1) Feed Money");
            Console.WriteLine("(2) Select Product");
            Console.WriteLine("(3) Finish Transaction");


            int purchaseMenuChoice = int.Parse(Console.ReadLine());
            if (purchaseMenuChoice == 1)
            {
                Console.WriteLine();
                Console.Write("Please Insert Cash ($1, $2, $5, $10): $");
                try
                {
                    int inputMoney = int.Parse(Console.ReadLine());
                    if (inputMoney < 0)
                    {
                        Console.WriteLine("Balance Cannot Be Below $0.00"); // added if statement here to keep user from inputing negative money
                    }
                    else
                    {
                        vendingMachine.FeedMoney(inputMoney);
                        Console.WriteLine();
                        Console.WriteLine("Current Balance: $" + vendingMachine.CheckBalance());//changed to check balance because of bug with totalMoney variable
                        PurchaseMenu();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine();
                    Console.WriteLine("**********************************");
                    Console.WriteLine("Please Enter Whole Dollar Amount.");
                    Console.WriteLine("**********************************");
                    PurchaseMenu();
                }

            }
            if (purchaseMenuChoice == 2)
            {
                Console.WriteLine();
                Console.Write("Please Select Product: ");


                string inputProduct = Console.ReadLine();
                inputProduct = inputProduct.ToUpper(); //allows user input to be case insensitive
                int balance = vendingMachine.VendingBalance;
                bool doesExist = vendingMachine.DoesExist(inputProduct);
                bool isAvailable = vendingMachine.IsAvailable(inputProduct);//changed these to be in variables to make it cleaner
                bool isEnough = vendingMachine.IsEnoughMoney(inputProduct, balance);

                if (doesExist && isAvailable && isEnough)
                {
                    Console.WriteLine();
                    Console.WriteLine("-----------------------------------------");
                    Console.WriteLine(vendingMachine.DispenseItem(inputProduct));
                    Console.WriteLine("-----------------------------------------");
                    Console.WriteLine("Remaining Balance: $" + vendingMachine.CheckBalance());
                    Console.WriteLine();
                }
                else if (!doesExist)
                {
                    Console.WriteLine();
                    Console.WriteLine("Sorry That Item Doesn't Exist");
                }
                else if (!isAvailable)
                {
                    Console.WriteLine();
                    Console.WriteLine("Sorry That Item Is Out of Stock. Please Select Another Item");
                }
                else if (!isEnough)
                {
                    Console.WriteLine();
                    Console.WriteLine("Insuffienct Funds. Please Insert More $");
                }

                PurchaseMenu();

            }
            if (purchaseMenuChoice == 3)
            {

                Console.WriteLine();
                Console.WriteLine($"Your Change Is: $" + vendingMachine.CheckBalance());
                int[] changeArray = vendingMachine.GiveChange();

                Console.WriteLine("_________________________________");
                System.Threading.Thread.Sleep(3000);
                RunInterface();
            }
            else
            {
                PurchaseMenu();

            }




        }
        public void IntroPic()
        {


            Console.WriteLine("_____________________________________________");
            System.Threading.Thread.Sleep(100);
            Console.WriteLine("|############################################|");
            System.Threading.Thread.Sleep(100);
            Console.WriteLine("|#|                           |##############|");
            System.Threading.Thread.Sleep(100);
            Console.WriteLine("|#|  =====  ..--''`  |~~``|   |##|````````|##|");
            System.Threading.Thread.Sleep(100);
            Console.WriteLine("|#|  |   |  \\     |  :    |   |##| Exact  |##|");
            System.Threading.Thread.Sleep(100);
            Console.WriteLine("|#|  |___|   /___ |  | ___|   |##| Change |##|");
            System.Threading.Thread.Sleep(100);
            Console.WriteLine("|#|  /=__\\  ./.__\\   |/,__\\   |##| Only   |##|");
            System.Threading.Thread.Sleep(100);
            Console.WriteLine("|#|  \\__//   \\__//    \\__//   |##|________|##|");
            System.Threading.Thread.Sleep(100);
            Console.WriteLine("|#|===========================|##############|");
            System.Threading.Thread.Sleep(100);
            Console.WriteLine("|#|```````````````````````````|##############|");
            System.Threading.Thread.Sleep(100);
            Console.WriteLine("|#| =.._      +++     //////  |##############|");
            System.Threading.Thread.Sleep(100);
            Console.WriteLine("|#| \\/  \\     | |     \\   \\ \\ |#|`````````|##|");
            System.Threading.Thread.Sleep(100);
            Console.WriteLine("|#|  \\___\\    |_|     /___ /  |#| _______ |##|");
            System.Threading.Thread.Sleep(100);
            Console.WriteLine("|#|  / __\\  /|_|\\   // __\\    |#| |1|2|3| |##|");
            System.Threading.Thread.Sleep(100);
            Console.WriteLine("|#|  \\__//-  \\|_//   -\\__//   |#| |4|5|6| |##|");
            System.Threading.Thread.Sleep(100);
            Console.WriteLine("|#|===========================|#| |7|8|9| |##|");
            System.Threading.Thread.Sleep(100);
            Console.WriteLine("|#|```````````````````````````|#| ``````` |##|");
            System.Threading.Thread.Sleep(100);
            Console.WriteLine("|#| ..--    ______   .--._.   |#|[=======]|##|");
            System.Threading.Thread.Sleep(100);
            Console.WriteLine("|#| \\   \\   |    |   |    |   |#|  _   _  |##|");
            System.Threading.Thread.Sleep(100);
            Console.WriteLine("|#|  \\___\\  : ___:   | ___|   |#| ||| ( ) |##|");
            System.Threading.Thread.Sleep(100);
            Console.WriteLine("|#|  / __\\  |/ __\\   // __\\   |#| |||  `  |##|");
            System.Threading.Thread.Sleep(100);
            Console.WriteLine("|#|  \\__//   \\__//  /_\\__//   |#|  ~      |##|");
            System.Threading.Thread.Sleep(100);
            Console.WriteLine("|#|===========================|#|_________|##|");
            System.Threading.Thread.Sleep(100);
            Console.WriteLine("|#|```````````````````````````|##############|");
            System.Threading.Thread.Sleep(100);
            Console.WriteLine("|############################################|");
            System.Threading.Thread.Sleep(100);
            Console.WriteLine("|#|||||||||||||||||||||||||||||####```````###|");
            System.Threading.Thread.Sleep(100);
            Console.WriteLine("|#||||||||||||PUSH|||||||||||||####\\|||||/###|");
            System.Threading.Thread.Sleep(100);
            Console.WriteLine("|############################################|");
            System.Threading.Thread.Sleep(100);
            Console.WriteLine("\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\////////////////////////////|");
            System.Threading.Thread.Sleep(100);
            Console.WriteLine("|________________________________ | CR98 |___|");

        }

        public void SalesReport()
        {
            Console.WriteLine();
            string[] displayArray = vendingMachine.DisplaySalesReport();
            string salesPath = Path.Combine(Environment.CurrentDirectory, "Sales_Report.txt");

            foreach (string item in displayArray)
            {
                using (StreamWriter sw = new StreamWriter(salesPath, true))
                {
                    sw.WriteLine(item);
                }

            }
            using (StreamWriter sw = new StreamWriter(salesPath, true))
            {
                sw.WriteLine($"**Total Sales** {vendingMachine.TotalSales.ToString("C2")}.  Report run:  {DateTime.Now}\n\n");
                sw.WriteLine("-------------------------------------------------------------------------");
            }
        }

    }
}
