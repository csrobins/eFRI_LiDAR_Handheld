# eFRI LiDAR Handheld - Ontario Forest Inventory Field Data Collection 

RELEASE NOTES: v black spruce build 10

1. Updated vegetation table for synching of all QUAD fields..
2. Added soil depth and soil chemistry suffixes to the ecosite choices. 
3. Minor changes in the Soil screen.

RELEASE NOTES: v black spruce build 9

1. Updated validation of stem maps for dead trees.
2. Fixed coarse Grade in Soil Structure. 
3. Fixed saving of StemQualityCode on initial tree screen.
4. Allowed for Ag soil horizon.
5. Added null values to many of the pickers throughout the app, to allow them to be reset by the user.
6. Allowed for LFH as a soil horizon.
7. Showing both Direct and Ocular tree heights in the tree list.
8. Added more detail to the Soil List.
9. Fixed layout of the Vegetation screen.

Xamarin project for an Ontario eFRI LiDAR plot for field data collection

The Next Generation eFRI Ground Data Collection App is the development of a mobile field application for eFRI Ground Calibration Plots.  The application is being developed in Xamarin.Forms as an open source app that will be widely available for anyone to take the core development and expand upon it, or deploy it for an eFRI field data collection program.  The app is being deployed in Android and iOS and could also be deployed in UWP if the need arises.  The app uses SQlite on device for all data collection disconnected.  When connected, the app pushes and puls data via JSON and a REST API to an Azure SQL database.

eLiDAR will live in a public repository on Github at https://github.com/csrobins/eFRI_LiDAR_Handheld.  From here, anyone can download the full code and related components, or they can choose to modify and continue to develop the application further with their own code and solutions.  The current version is available here now.  In the repository, you will find all the code for:
1.	The Xamarin.Forms solution (created in Visual Studio 2019), which contains all of the C# code for the eLiDAR common project, the eLidar.Android project and the eLidar.iOS project.
2.	The SQLite database (on device) is included in the db folder.
3.	The SQL to recreate the Azure SQL database is in the API folder.
