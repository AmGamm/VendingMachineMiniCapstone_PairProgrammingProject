using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Capstone.Classes
{
    public class VendingMachine
    {
        private int vendingBalance = 0;
        public int VendingBalance { get { return vendingBalance; } }
        List<Item> itemList = new List<Item>();
        private string logPath = Path.Combine(Environment.CurrentDirectory, "Vending_Machine_Log.txt");

        public double TotalSales { get; set; }
        
        List<Item> saleList = new List<Item>();


        public VendingMachine() //populates itemList with items after adding properties to them
        {
            string directory = Environment.CurrentDirectory;
            string itemFile = "vendingmachine.csv";
            string itemFullPath = Path.Combine(directory, itemFile);

            using (StreamReader reader = new StreamReader(itemFullPath))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] itemArray = line.Split('|');
                    double moneyParsing = double.Parse(itemArray[2]);
                    double centsAmount = moneyParsing * 100;
                    Item tempItem = new Item();
                    tempItem.Slot = itemArray[0];
                    tempItem.Description = itemArray[1];
                    tempItem.PriceInCents = (int)centsAmount;
                    tempItem.Inventory = 5;
                    itemList.Add(tempItem);
                }
            }
        }
        public string[] DisplayItems()
        {
            string[] itemArray = new string[itemList.Count];

            int count = 0;
            foreach (Item item in itemList)//loops through itemList and inserts slot, description, and $ to array to be displayed
            {
                int remainder = (item.PriceInCents % 100);
                int quotient = item.PriceInCents / 100;
                string result = quotient + "." + remainder.ToString("D2");
                string tempstring = item.Slot + " " + item.Description + " " + "$" + result.ToString() + "| " +item.Inventory;
                itemArray[count] = tempstring;
                count++;
            }
            return itemArray;

        }

        public void FeedMoney(int inputMoney)//changed to void because we made check balance method
        {
            vendingBalance += (inputMoney * 100);
            //need to add to log
            using (StreamWriter sw = new StreamWriter(logPath, true))
            {
                sw.WriteLine($"{DateTime.Now} FEED MONEY: \t{inputMoney.ToString("C2")}\t${CheckBalance()}");

            }
            
        }

        public bool DoesExist(string inputSlotString)
        {
            bool result = false;
            foreach (Item item in itemList)
            {
                if (inputSlotString == item.Slot)
                {

                    result = true;
                    return result;

                }

            }
            return result;
        }

        public bool IsAvailable(string inputSlotString)
        {
            bool result = false;
            foreach (Item item in itemList)
            {
                if (inputSlotString == item.Slot)
                {
                    if (item.Inventory > 0)
                    {
                        result = true;
                        return result;
                    }
                }

            }
            return result;
        }

        public bool IsEnoughMoney(string inputProduct, int fedMoney)  //in old code, we were changing bool to false if it didn't find right item on first loop.
        {
            bool isItEnough = false;  //changed bool to equal false, only update if it is true.  
            foreach (Item item in itemList)
            {
               
                if (inputProduct == item.Slot && fedMoney >= item.PriceInCents)
                {
                    isItEnough =  true;  //changes bool when it finds the correct item then breaks out of loop.
                    break;
                }
            }
            return isItEnough;
        }

        public string DispenseItem(string inputSlotString)
        {
            string productName = "";
            foreach (Item item in itemList)//loop to reduce inventory by 1 and adjust vendingmachine balance
            {
                if (inputSlotString == item.Slot)
                {
                    item.Inventory--;
                    vendingBalance -= item.PriceInCents;
                    productName = item.Slot+": "+item.Description;
                    
                    //total sales for report
                    int totalRemainder = (item.PriceInCents % 100);
                    int totalQuotient = item.PriceInCents / 100;
                    string totalResult = totalQuotient + "." + totalRemainder.ToString("D2");
                    double totalResultValue = double.Parse(totalResult);
                    TotalSales += totalResultValue;
                    item.TotalItemSold++;

                    using (StreamWriter sw = new StreamWriter(logPath, true))
                    {
                        int remainder = (item.PriceInCents % 100);
                        int quotient = item.PriceInCents / 100;
                        string result = quotient + "." + remainder.ToString("D2");
                        
                        sw.WriteLine($"{DateTime.Now} {item.Description} {item.Slot}\t${result}\t${CheckBalance()}");

                    }
                }
            }
            //add to log

            string message = "";
            if (inputSlotString.Contains("A"))
            {
                message = $"{productName} - Crunch Crunch, Yum!";
            }
            if (inputSlotString.Contains("B"))
            {
                message = $"{productName} -Munch Munch, Yum!";
            }
            if (inputSlotString.Contains("C"))
            {
                message = $"{productName} -Glug Glug, Yum!";
            }
            if (inputSlotString.Contains("D"))
            {
                message = $"{productName} -Chew Chew, Yum!";
            }

            return message;
        }

        public string CheckBalance()
        {
            string balance = "";
            int remainder = (vendingBalance % 100);
            int quotient = vendingBalance / 100;
            balance = quotient + "." + remainder.ToString("D2");
            return balance;
        }

        public int[] GiveChange()
        {
            string beforeBalance = CheckBalance();
            int[] changeArray = new int[3];
            while (vendingBalance > 0)//loop to give change by giving quarters first then dimes then nickles and reducing vending balance to 0
            {
                if (vendingBalance >= 25)
                {
                    vendingBalance -= 25;
                    changeArray[0]++;
                }
                else if ((vendingBalance >= 10) && (vendingBalance < 25))
                {
                    vendingBalance -= 10;
                    changeArray[1]++;
                }
                else if ((vendingBalance >= 5) && (vendingBalance < 10))
                {
                    vendingBalance -= 5;
                    changeArray[2]++;
                }
            }

            //adjust log
            using (StreamWriter sw = new StreamWriter(logPath, true))
            {
                
                sw.WriteLine($"{DateTime.Now} GIVE CHANGE: \t${beforeBalance}\t${CheckBalance()}");

            }

            return changeArray;
        }

        public string[] DisplaySalesReport()
        {
            string[] salesArray = new string[itemList.Count];

            int count = 0;
            foreach (Item item in itemList)//loops through itemList and inserts slot, description, and $ to array to be displayed
            {
                int remainder = (item.PriceInCents % 100);
                int quotient = item.PriceInCents / 100;
                string result = quotient + "." + remainder.ToString("D2");
                string tempstring = item.Description + "| " + item.TotalItemSold;
                salesArray[count] = tempstring;
                count++;
            }
            return salesArray;

        }

    }

}
