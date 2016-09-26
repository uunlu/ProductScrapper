using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Services.Description;

namespace ConsoleApplication.TaskHandler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Start processing...");

                //var msgs =  DB.Query<Message>().Where(x => x.Status == MessageStatus.Pending).ToList();

                //Console.WriteLine($"Will process {msgs.Count()} messages.");

                //foreach (var m in msgs)
                //{
                //    // your logic for handling the message based on it's type. If you have just one message, e.g. called "ScrapTheWebMessage" then you will place your scrapping logic here

                //    // update the message status that you have processed it
                //    m.Status = MessageStatus.Complete;
                //    DB.Store(m);
                //    DB.SaveChanges();
                //}

                Console.WriteLine("Completed processing. Waiting and trying again.");
                Thread.Sleep(1000);
            }
        }
    }
}
