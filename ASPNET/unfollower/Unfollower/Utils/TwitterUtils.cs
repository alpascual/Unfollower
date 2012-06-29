using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TweetSharp;
using System.Net;

namespace Unfollower.Utils
{
    public class TwitterUtils
    {
        public static void SendTweet(   string sMessage, 
                                        string consumerKey, 
                                        string consumerSecret, 
                                        string token, 
                                        string tokenSecret)
        {
            TwitterService service = new TwitterService(consumerKey, consumerSecret);
            service.AuthenticateWith(token, tokenSecret);
           
            TwitterStatus status = service.SendTweet(sMessage);
        }

        public static void SendTweet(string sMessage, int userID)
        {
            TwitterService service = null;
            //Al Pascual
            if (userID > 0)
            {
                service = new TwitterService("yASjbX9Zbu1OD0gk89qExA", "mTDH5xPm6Fu1KS3m1NUVTWKXBXi330TCakspt5Dz060");
                service.AuthenticateWith("14379285-gx8kCMPXCmilKSRcA9K8kVAf0RwFCtbp08AyrpUOQ", "bcZbtH5kW1FQ8P8UAuKhSXimr1T4rp8bWp6bzNF0KSk");
            }
                //MVP
            else
            {
                service = new TwitterService("g0fsdKhJObiZLamY2P7vfg", "ghUKQ9y0LvN08clZq6wwU41a0gMlrv9oGj9zd55292w");
                service.AuthenticateWith("14820993-dEh4f6mD1RNSe1pp8ZmKwJb9g0TGyyGqyHuvZsM9s", "6CFvw63PNUNx4cbj53n5idwlaRUVSz9lKNhr8FM");
            }

            if (userID == 0 || userID == 1)
            {
                TwitterStatus status = service.SendTweet(sMessage);
            }
            else
                service.SendDirectMessage(userID, sMessage);
        }
        public static void SendTweet_Start(string sMessage, int userID)
        {
            // alpascual
            // Pass your credentials to the service
            TwitterService service = new TwitterService("yASjbX9Zbu1OD0gk89qExA", "mTDH5xPm6Fu1KS3m1NUVTWKXBXi330TCakspt5Dz060");
            //OAuthAccessToken access = service.GetAccessTokenWithXAuth("alpascual", ""); // <-- user supplied username and password


            //// Step 1 - Retrieve an OAuth Request Token
            OAuthRequestToken requestToken = service.GetRequestToken();

            //// Step 2 - Redirect to the OAuth Authorization URL
            Uri uri = service.GetAuthorizationUri(requestToken);
            ////Process.Start(uri.ToString());
            //WebClient client = new WebClient();
            //string sResponse = client.DownloadString(uri);

            //// Step 3 - Exchange the Request Token for an Access Token
            string verifier = "3479615"; // <-- This is input into your application by your user
            OAuthAccessToken access = service.GetAccessToken(requestToken, verifier);
           
            // Step 4 - User authenticates using the Access Token
            service.AuthenticateWith(access.Token, access.TokenSecret);
            
            if (userID == 0)
            {
                TwitterStatus status = service.SendTweet(sMessage);
            }
            else
                service.SendDirectMessage(userID, sMessage);
        }

        public static TwitterUser []  GetFollowersFor(string sUsername)
        {
             // Pass your credentials to the service
            TwitterService service = new TwitterService("g0fsdKhJObiZLamY2P7vfg", "ghUKQ9y0LvN08clZq6wwU41a0gMlrv9oGj9zd55292w");

            // Step 1 - Retrieve an OAuth Request Token
            OAuthRequestToken requestToken = service.GetRequestToken();

            // Step 2 - Redirect to the OAuth Authorization URL
            Uri uri = service.GetAuthorizationUri(requestToken);
            //Process.Start(uri.ToString());

            // Step 3 - Exchange the Request Token for an Access Token
            string verifier = "123456"; // <-- This is input into your application by your user
            OAuthAccessToken access = service.GetAccessToken(requestToken, verifier);
            access.TokenSecret = "6CFvw63PNUNx4cbj53n5idwlaRUVSz9lKNhr8FM";
            access.Token = "14820993-dEh4f6mD1RNSe1pp8ZmKwJb9g0TGyyGqyHuvZsM9s";

            // Step 4 - User authenticates using the Access Token
            service.AuthenticateWith(access.Token, access.TokenSecret);
            TwitterCursorList<TwitterUser> followersList = null;
            TwitterCursorList<TwitterUser> list = service.ListFollowersOf(sUsername, -1);
            long? nextCursor = list.NextCursor;

            followersList = new TwitterCursorList<TwitterUser>(list);

            while ((nextCursor ?? 0) != 0)
            {
                TwitterCursorList<TwitterUser> tempFollowersList = service.ListFollowersOf(sUsername, (long)nextCursor);
                if (tempFollowersList.Count <= 0)
                    break;
               
               followersList.AddRange(tempFollowersList);

               nextCursor = tempFollowersList.NextCursor;
            }

            TwitterUser[] array = new TwitterUser[followersList.Count];
            followersList.CopyTo(array);
            return array;
        }

        
    }
}