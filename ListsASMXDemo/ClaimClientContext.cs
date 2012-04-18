﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace ListsASMXDemo
{
        public static class ClaimClientContext
        {
            /// <summary> 
            /// Displays a pop up to login the user. An authentication Cookie is returned if the user is sucessfully authenticated. 
            /// </summary> 
            /// <param name="targetSiteUrl"></param> 
            /// <param name="popUpWidth"></param> 
            /// <param name="popUpHeight"></param> 
            /// <returns></returns> 
            public static CookieCollection GetAuthenticatedCookies(string targetSiteUrl, int popUpWidth, int popUpHeight)
            {
                CookieCollection authCookie = null;
                using (ClaimsWebAuth webAuth = new ClaimsWebAuth(targetSiteUrl, popUpWidth, popUpHeight))
                {
                    authCookie = webAuth.Show();
                }
                return authCookie;
            }

            /// <summary> 
            /// Override for for displaying pop. Default width and height values are used for the pop up window. 
            /// </summary> 
            /// <param name="targetSiteUrl"></param> 
            /// <returns></returns> 
            public static SharePointLists.Lists GetAuthenticatedContext(string targetSiteUrl)
            {
                return (GetAuthenticatedContext(targetSiteUrl, 0, 0));
            }

            /// <summary> 
            /// This method will return a ClientContext object with the authentication cookie set. 
            /// The ClientContext should be disposed of as any other IDisposable 
            /// </summary> 
            /// <param name="targetSiteUrl"></param> 
            /// <returns></returns> 
            public static SharePointLists.Lists GetAuthenticatedContext(string targetSiteUrl, int popUpWidth, int popUpHeight)
            {
                CookieCollection cookies = null;
                cookies = ClaimClientContext.GetAuthenticatedCookies(targetSiteUrl, popUpWidth, popUpHeight);
                if (cookies == null) return null;

                SharePointLists.Lists contexto = new SharePointLists.Lists();

                try
                {
                    contexto.CookieContainer = new CookieContainer();
                    foreach (Cookie cookie in cookies)
                    {
                        contexto.CookieContainer.Add(cookie);
                    }
                }
                catch
                {
                    if (contexto != null) contexto.Dispose();
                    throw;
                }

                return contexto;
            }
        }
}
