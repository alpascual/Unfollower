using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Security.Authentication;
using JdSoft.Apple.Apns.Notifications;

namespace Unfollower.Utils
{
    public class AppleNotification
    {


        public static void SendNotification(string sDeviceToken, string sMessage)
        {
            //Variables you may need to edit:
            //---------------------------------

            //True if you are using sandbox certificate, or false if using production
            bool sandbox = true;

            //Put your device token in here
            //string testDeviceToken = "fe58fc8f527c363d1b775dca133e04bff24dc5032d08836992395cc56bfa62ef";
            string testDeviceToken = sDeviceToken;

            //Put your PKCS12 .p12 or .pfx filename here.
            // Assumes it is in the same directory as your app
            string p12File = "apn_developer_identity.p12";

            //This is the password that you protected your p12File 
            //  If you did not use a password, set it as null or an empty string
            string p12FilePassword = "";
            
            //Actual Code starts below:
            //--------------------------------

            string p12Filename = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, p12File);

            NotificationService service = new NotificationService(sandbox, p12Filename, p12FilePassword, 1);

            service.SendRetries = 5; //5 retries before generating notificationfailed event
            service.ReconnectDelay = 5000; //5 seconds

            service.Error += new NotificationService.OnError(service_Error);
            service.NotificationTooLong += new NotificationService.OnNotificationTooLong(service_NotificationTooLong);

            service.BadDeviceToken += new NotificationService.OnBadDeviceToken(service_BadDeviceToken);
            service.NotificationFailed += new NotificationService.OnNotificationFailed(service_NotificationFailed);
            service.NotificationSuccess += new NotificationService.OnNotificationSuccess(service_NotificationSuccess);
            service.Connecting += new NotificationService.OnConnecting(service_Connecting);
            service.Connected += new NotificationService.OnConnected(service_Connected);
            service.Disconnected += new NotificationService.OnDisconnected(service_Disconnected);

            //The notifications will be sent like this:
          
            //Create a new notification to send
            Notification alertNotification = new Notification(testDeviceToken);

            alertNotification.Payload.Alert.Body = sMessage;
            alertNotification.Payload.Sound = "default";
            alertNotification.Payload.Badge = 1;

            //Queue the notification to be sent
            if (service.QueueNotification(alertNotification))
                Console.WriteLine("Notification Queued!");
            else
                Console.WriteLine("Notification Failed to be Queued!");


            Console.WriteLine("Cleaning Up...");

            //First, close the service.  
            //This ensures any queued notifications get sent befor the connections are closed
            service.Close();

            //Clean up
            service.Dispose();

            Console.WriteLine("Done!");
            Console.WriteLine("Press enter to exit...");
            Console.ReadLine();
        }

        static void service_BadDeviceToken(object sender, BadDeviceTokenException ex)
        {
            Console.WriteLine("Bad Device Token: {0}", ex.Message);
        }

        static void service_Disconnected(object sender)
        {
            Console.WriteLine("Disconnected...");
        }

        static void service_Connected(object sender)
        {
            Console.WriteLine("Connected...");
        }

        static void service_Connecting(object sender)
        {
            Console.WriteLine("Connecting...");
        }

        static void service_NotificationTooLong(object sender, NotificationLengthException ex)
        {
            Console.WriteLine(string.Format("Notification Too Long: {0}", ex.Notification.ToString()));
        }

        static void service_NotificationSuccess(object sender, Notification notification)
        {
            Console.WriteLine(string.Format("Notification Success: {0}", notification.ToString()));
        }

        static void service_NotificationFailed(object sender, Notification notification)
        {
            Console.WriteLine(string.Format("Notification Failed: {0}", notification.ToString()));
        }

        static void service_Error(object sender, Exception ex)
        {
            Console.WriteLine(string.Format("Error: {0}", ex.Message));
        }




    //    public void SendMessage()
    //    {
    //        // Create a TCP/IP client socket. 
    //        using (TcpClient client = new TcpClient())
    //        {
    //            client.Connect("gateway.sandbox.push.apple.com", 2195);
    //            using (NetworkStream networkStream = client.GetStream())
    //            {
    //                Console.WriteLine("Client connected.");
    //                X509Certificate clientCertificate = new X509Certificate(@"certfile.p12", passwordHere);
    //                X509CertificateCollection clientCertificateCollection = new X509CertificateCollection(new X509Certificate[1] { clientCertificate });
    //                // Create an SSL stream that will close the client's stream.  
    //                SslStream sslStream = new SslStream(
    //                    client.GetStream(),
    //                    false,
    //                    new RemoteCertificateValidationCallback(ValidateServerCertificate),
    //                    null
    //                    );
    //                try
    //                {
    //                    sslStream.AuthenticateAsClient("gateway.sandbox.push.apple.com");
    //                }
    //                catch (AuthenticationException e)
    //                {
    //                    Console.WriteLine("Exception: {0}", e.Message);
    //                    if (e.InnerException != null)
    //                    {
    //                        Console.WriteLine("Inner exception: {0}", e.InnerException.Message);
    //                    }
    //                    Console.WriteLine("Authentication failed - closing the connection.");
    //                    client.Close();
    //                    return;
    //                }
    //            }
    //        }
    //    }

    //    public static byte[] GeneratePayload(byte[] deviceToken, string message, string sound)
    //    {
    //        MemoryStream memoryStream = new MemoryStream();
    //        // Command
    //        memoryStream.WriteByte(0);
    //        byte[] tokenLength = BitConverter.GetBytes((Int16)32);
    //        Array.Reverse(tokenLength);
    //        // device token length
    //        memoryStream.Write(tokenLength, 0, 2);
    //        // Token        
    //        memoryStream.Write(deviceToken, 0, 32);
    //        // String length        
    //        string apnMessage = string.Format("{{\"aps\":{{\"alert\":{{\"body\":\"{0}\",\"action-loc-key\":null}},\"sound\":\"{1}\"}}}}",
    //            message,
    //            sound);
    //        byte[] apnMessageLength = BitConverter.GetBytes((Int16)apnMessage.Length);
    //        Array.Reverse(apnMessageLength);
    //        // message length
    //        memoryStream.Write(apnMessageLength, 0, 2);
    //        // Write the message
    //        memoryStream.Write(System.Text.ASCIIEncoding.ASCII.GetBytes(apnMessage), 0, apnMessage.Length);
    //        return memoryStream.ToArray();
    //    } // End of GeneratePayload
    }
}