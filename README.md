# Gimmal Records SPO Connector AuthUtil
This console application uses an SPO add-in client ID and client secret to authenticate with SPO. This application demonstrates the [authorization code flow](https://docs.microsoft.com/en-us/sharepoint/dev/sp-add-ins/authorization-code-oauth-flow-for-sharepoint-add-ins) for a [provider-hosted](https://docs.microsoft.com/en-us/sharepoint/dev/sp-add-ins/sharepoint-add-ins#provider-hosted-sharepoint-add-ins) [low-trust](https://docs.microsoft.com/en-us/sharepoint/dev/sp-add-ins/creating-sharepoint-add-ins-that-use-low-trust-authorization) SPO add-in.

# Source Code
There are two code files in the project:
- Program.cs
  - Authored by Gimmal
  - Obtains app-only access token from Azure ACS
  - Uses access token to build a CSOM ClientContext
  - Calls SPO to test if the add-in can authenticate successfully
- TokenHelper.cs
  - Authored by Microsoft
  - Creating a new SharePoint add-in project in Visual Studio will produce this file
    - See [Get started creating provider-hosted SharePoint Add-ins](https://docs.microsoft.com/en-us/sharepoint/dev/sp-add-ins/get-started-creating-provider-hosted-sharepoint-add-ins) for more information
  - This file was copied from a new project using the latest version of Visual Studio 2019

# Configuring the application to run
Edit the appSettings section of the SPOC AuthUtil.exe.config file to authenticate with SPO. You should be using whatever values you provided when [registering your add-in](https://docs.microsoft.com/en-us/sharepoint/dev/sp-add-ins/register-sharepoint-add-ins#to-register-by-using-appregnewaspx) with Azure ACS and SPO from the appregnew.aspx page. The following settings must be configured:
- ClientId – The client ID of your SPO add-in
- ClientSecret – The client secret of your SPO add-in
- SPOSiteUrl – The URL to your SPO site

# Running the application
Double click the SPOC AuthUtil.exe file to launch the application. The console will output the results and require a keypress to exit the program. Assuming the application has been configured correctly, as described above, there are two outcomes expected when the application runs:
- Success
  - The console will display “Success!” if the application can authenticate with SPO and execute a CSOM query
- Unauthorized
  - The console will display an exception message and stack trace if there is a 401 Unauthorized exception from SPO
