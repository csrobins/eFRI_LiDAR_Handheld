# eFRI LiDAR Handheld - Ontario Forest Inventory Field Data Collection 

Xamarin project for an Ontario eFRI LiDAR plot for field data collection

The Next Generation eFRI Ground Data Collection App is the development of a mobile field application for eFRI Ground Calibration Plots.  The application is being developed in Xamarin.Forms as an open source app that will be widely available for anyone to take the core development and expand upon it, or deploy it for an eFRI field data collection program.  The app is being deployed in Android and iOS and could also be deployed in UWP if the need arises.  The app uses SQlite on device for all data collection disconnected.  When connected, the app pushes and puls data via JSON and a REST API to an Azure SQL database.

eLiDAR will live in a public repository on Github at https://github.com/csrobins/eFRI_LiDAR_Handheld.  From here, anyone can download the full code and related components, or they can choose to modify and continue to develop the application further with their own code and solutions.  The current version is available here now.  In the repository, you will find all the code for:
1.	The Xamarin.Forms solution (created in Visual Studio 2019), which contains all of the C# code for the eLiDAR common project, the eLidar.Android project and the eLidar.iOS project.
2.	The SQLite database (on device) is included in the db folder.
3.	The SQL to recreate the Azure SQL database is in the API folder.
