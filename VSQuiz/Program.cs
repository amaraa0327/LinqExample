using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VSQuiz.DomainClasses;

namespace VSQuiz
{
    class Program
    {
        #region [Util(common) functions]
        public static string PrintTask1(Header header)
        {
            StringBuilder detail = new StringBuilder();
            ((List<LineItem>)header.Lines).ForEach(line =>
            {
                detail.Append(PrintLineItem(line));
                detail.Append("\n\r");
            });
            detail.Append("-----------------------------------------------");

            return string.Format(
                    "HeaderID = {0}\n\rClientName = {1}\n\r{2}",
                    header.HeaderID, header.Client.ClientName, detail);
        }
        public static string PrintTask2(Header header)
        {
            StringBuilder detail = new StringBuilder();
            var sortedlines = from line in header.Lines orderby line.Charge descending select line;
            foreach (LineItem line in sortedlines)
            {
                detail.Append(PrintLineItem(line));
                detail.Append("\n\r");
            }
            detail.Append("-----------------------------------------------");

            return string.Format(
                    "HeaderID = {0}\n\rClientName = {1}\n\r{2}",
                    header.HeaderID, header.Client.ClientName, detail);
        }
        public static string PrintLineItem(LineItem line)
        {
            return string.Format(" {0} | {1} | {2} | {3}", line.LineNo, line.OrderDate, line.Item.ItemName, line.Charge);
        }
        #endregion

        static void Main(string[] args)
        {
            #region [Domain class' initialization]
            Item item1 = new Item() { ItemName = "Item 1" };
            Item item2 = new Item() { ItemName = "Item 2" };
            Item item3 = new Item() { ItemName = "Item 3" };
            Item item4 = new Item() { ItemName = "Item 4" };
            Item item5 = new Item() { ItemName = "Item 5" };

            Client client1 = new Client() { ClientName = "Client 1" };
            Client client2 = new Client() { ClientName = "Client 2" };
            Client client3 = new Client() { ClientName = "Client 3" };
            #endregion

            #region [Order initialization]
            List<Header> allHeaders = new List<Header>(){
                new Header()
                {
                    HeaderID = "249e1bd0-0b6c-4dce-8d72-e16f20b1b49c",
                    Client = client1,
                    Lines = new List<LineItem>() {
                        new LineItem(){ LineNo = 1, Item = item1, OrderDate = new DateTime(2017, 2, 21), Charge = 100},
                        new LineItem(){ LineNo = 2, Item = item2, OrderDate = new DateTime(2017, 2, 22), Charge = 200},
                        new LineItem(){ LineNo = 3, Item = item3, OrderDate = new DateTime(2017, 2, 23), Charge = 300},
                    }
                },
                new Header()
                {
                    HeaderID = "ea9560cc-e38b-481e-9630-3ca03e5b46a2",
                    Client = client2,
                    Lines = new List<LineItem>() {
                        new LineItem(){ LineNo = 1, Item = item3, OrderDate = new DateTime(2017, 2, 23), Charge = 300},
                        new LineItem(){ LineNo = 2, Item = item4, OrderDate = new DateTime(2017, 2, 24), Charge = 400},
                        new LineItem(){ LineNo = 3, Item = item5, OrderDate = new DateTime(2017, 2, 25), Charge = 500 }
                    }
                },
                new Header()
                {
                    HeaderID = "afaf2070-8fb7-492d-b7c2-b2ca7ed80844",
                    Client = client3,
                    Lines = new List<LineItem>() {
                        new LineItem(){ LineNo = 1, Item = item2, OrderDate = new DateTime(2017, 2, 22), Charge = 200},
                        new LineItem(){ LineNo = 2, Item = item4, OrderDate = new DateTime(2017, 2, 24), Charge = 400}
                    }
                }
            };
            #endregion

            //all order list is allHeaders
            #region [Task 1]
            Console.WriteLine("*** Task 1: Print orders\n\r-----------------------------------------------");
            allHeaders.ForEach(header => Console.WriteLine(PrintTask1(header)));
            #endregion

            #region [Task 2]
            Console.WriteLine("*** Task 2: Print orders sorted descending by client name and line items sorted descending by Charges\n\r-----------------------------------------------");
            var sortedlist = from header in allHeaders orderby header.Client.ClientName descending select header;
            foreach (Header header in sortedlist)
            {
                Console.WriteLine(PrintTask2(header));
            }
            #endregion

            #region [Task 3]
            Console.WriteLine("*** Task3: Print lines ordered by date (ascending)\n\r-----------------------------------------------");
            var sortedlines3 = from line in allHeaders.SelectMany(hdr => hdr.Lines) orderby line.OrderDate select line;
            foreach (LineItem line in sortedlines3)
            {
                Console.WriteLine(PrintLineItem(line));
            }
            Console.WriteLine("-----------------------------------------------");
            #endregion

            #region [Task 4]
            Console.WriteLine("*** Task 4: Print lines sorted by LineNumber where charge is greater than or equal 300.\n\r-----------------------------------------------");
            var sortedlines4 = from line in allHeaders.SelectMany(hdr => hdr.Lines) orderby line.LineNo where line.Charge >= 300 select line;
            foreach (LineItem line in sortedlines4)
            {
                Console.WriteLine(PrintLineItem(line));
            }
            Console.WriteLine("-----------------------------------------------");
            #endregion

            #region [Task 5]
            Console.WriteLine("*** Task 5: Print count of line items per date.\n\r-----------------------------------------------");
            var sortedlines5 = from line in allHeaders.SelectMany(hdr => hdr.Lines) group line by line.OrderDate into gl select gl;
            foreach (IGrouping<DateTime, LineItem> linegroup in sortedlines5)
            {
                Console.WriteLine(string.Format("{0} | {1}", linegroup.Key, linegroup.Count()));
            }
            Console.WriteLine("-----------------------------------------------");

            Console.WriteLine("*** Task 5: Print line items total charges per date.\n\r-----------------------------------------------");
            foreach (IGrouping<DateTime, LineItem> linegroup in sortedlines5)
            {
                Console.WriteLine(string.Format("{0} | {1}", linegroup.Key, linegroup.Sum(lg => lg.Charge)));
            }
            Console.WriteLine("-----------------------------------------------");
            #endregion

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }
}
