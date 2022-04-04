using Microsoft.SharePoint.Client;
using System;
using System.Configuration;

namespace Gimmal.Records.SPOC.AuthUtil
{
    class Program
    {
        //All configurable parameters should be set in the app.config file for this console application
        static void Main()
        {
            //URL of the site (SPWeb) where the add-in (app) is installed and trusted
            string siteUrl = ConfigurationManager.AppSettings.Get("SPOSiteUrl");
            
            try
            {
                //Azure ACS issues an access token for this app principal using the authorization code flow
                OAuthTokenResponse atr = GetSpoAuthResponse(siteUrl);

                //Make a request to SPO using the access token
                TestSpoAccessToken(siteUrl, atr.AccessToken);

                Console.WriteLine("Success!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey(true);
        }

        private static OAuthTokenResponse GetSpoAuthResponse(string siteUrl)
        {
            Uri siteUri = new Uri(siteUrl, UriKind.Absolute);

            string targetHost = siteUri.Authority;

            string targetRealm = TokenHelper.GetRealmFromTargetUrl(siteUri);

            //Our application primarily uses the add-in-only authorization policy
            return TokenHelper.GetAppOnlyAccessToken(TokenHelper.SharePointPrincipal, targetHost, targetRealm);
        }

        private static void TestSpoAccessToken(string siteUrl, string accessToken)
        {
            using (ClientContext context = TokenHelper.GetClientContextWithAccessToken(siteUrl, accessToken))
            {
                context.Load(context.Web, web => web.Title);

                //If there are any SPO authentication problems, then this method will throw an exception...
                context.ExecuteQuery();
            }
        }
    }
}
