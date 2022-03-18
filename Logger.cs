using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Send_Email
{
    public static class Logger
    {
        public static void ErrorLog(string message)
        {
            string logPath = ConfigurationManager.AppSettings["logPath"];

            using (StreamWriter writer = new StreamWriter(logPath, true))
            {
                writer.WriteLine($"{DateTime.Now} " +
                    $"\nStatus: Error" +
                    $"\nError: {message}" +
                    $"\n------------------------------------");
            }
        }

        public static void SuccessLog(string sender, string receiver, string subject, string body)
        {
            string logPath = ConfigurationManager.AppSettings["logPath"];

            using (StreamWriter writer = new StreamWriter(logPath, true))
            {
                writer.WriteLine($"Sender: {sender} " +
                                 $"\nReceiver: {receiver} " +
                                 $"\n\nSubject: {subject} " +
                                 $"\nBody: {body} " +
                                 $"\n\nDate: {DateTime.Now} " +
                                 $"\nStatus: Success! " +
                                 $"\n------------------------------------");
            }
        }
    }
}
