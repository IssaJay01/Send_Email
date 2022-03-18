using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

namespace SendEmail
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Who Are You Sending This Email To? ");
            string to = Console.ReadLine();

            Console.Write("\nWhat is the subject of your email? ");
            string subject = Console.ReadLine();

            Console.WriteLine("\nFinally, please enter the contents your email below and press [ENTER] when finished.");
            string body = Console.ReadLine();

            SendEmail(to, subject, body);
        }//static void Main

        public static void SendEmail(string to, string subject, string bodyText)
        {
            //credentials
            string from = ConfigurationManager.AppSettings["emailAddress"];
            string password = ConfigurationManager.AppSettings["password"];
            string username = ConfigurationManager.AppSettings["username"];

            MimeMessage message = new MimeMessage();

            message.From.Add(new MailboxAddress(username, from));
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;

            message.Body = new TextPart("plain")
            {
                Text = bodyText
            };

            SmtpClient client = new SmtpClient();
            //1st attempt
            try
            {
                client.Connect("smtp.gmail.com", 466, true); //465 is port gmail 587 is outlook(i think)
                client.Authenticate(from, password);
                client.Send(message);

                Console.WriteLine($"\nEmail successfully sent. {DateTime.Now.ToString("dddd, dd MMMM yyyy")}");
                Send_Email.Logger.SuccessLog(from, to, subject, bodyText);
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nEmail was not successfully sent, please check error logs for more information.");
                Send_Email.Logger.ErrorLog(ex.Message);

                //2nd attempt
                try
                {
                    client.Connect("smtp.gmail.com", 466, true);
                    client.Authenticate(from, password);
                    client.Send(message);

                    Console.WriteLine($"\nEmail successfully sent. {DateTime.Now.ToString("dddd, dd MMMM yyyy")}");
                    Send_Email.Logger.SuccessLog(from, to, subject, bodyText);
                }
                catch (Exception ex_1)
                {
                    Console.WriteLine("\nEmail was not successfully sent, please check error logs for more information.");
                    Send_Email.Logger.ErrorLog(ex_1.Message);

                    //3rd and final attempt
                    try
                    {
                        client.Connect("smtp.gmail.com", 465, true);
                        client.Authenticate(from, password);
                        client.Send(message);

                        Console.WriteLine($"\nEmail successfully sent. {DateTime.Now.ToString("dddd, dd MMMM yyyy")}");
                        Send_Email.Logger.SuccessLog(from, to, subject, bodyText);
                    }
                    catch (Exception ex_2)
                    {
                        Console.WriteLine("\nEmail was not successfully sent, please check error logs for more information.");
                        Send_Email.Logger.ErrorLog(ex_2.Message);
                    }
                }
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
            Console.ReadLine();
        }//public static void SendMail
    }//class Program
}//namespace



/*
Console.Write("Password: ");
ConsoleColor originalBGColor = Console.BackgroundColor;
ConsoleColor originalFGColor = Console.ForegroundColor;
Console.BackgroundColor = ConsoleColor.Gray;
Console.ForegroundColor = ConsoleColor.Gray;
string password = Console.ReadLine();

Console.BackgroundColor = originalBGColor;
Console.ForegroundColor = originalFGColor;
*/