# eFRI LiDAR Handheld - Ontario Forest Inventory Field Data Collection 

RELEASE NOTES: v black walnut build 17 (May 31, 2022)

1. Various upates to the validator.
2. Updates to the synching to be simpler and more reliable
3. Addtiions for 2022 field season (like the Samll Trees)
4. Available now in UWP, iOS (13) and Android (10)

Known and Unresolved Issues (Jul 13th 2021)
1. Nulls do not work in numeric fields (we may need to consider a -1 to represent null.
2. Need an Ocular Distance in Tree Hts area.
3. Might need an additional field in the DWD screen?

RELEASE NOTES: v black spruce build 14

1. Various upates to the validator.
2. Fix to allow for crown wdiths on some dead trees.

RELEASE NOTES: v black spruce build 13

1. Added the ability to synch a single plot of data (in the Settings screen) - this is useful when editing plot data long after the plot was collected.

RELEASE NOTES: v black spruce build 12

1. Added several more validations to the plot screen.
2. Fixed the tree validator from Build 11.

RELEASE NOTES: v black spruce build 11 (DO NOT USE BUILD 11)

1. Added several more layers to the soils horizon list.
2. Resolved the issue with not using 0 after the decimal on several key fields.
3. Fixed saving of Tree Ages on initial add tree screen.
4. Update the boreal species list.
5. Made the Maturity class rationale a pick-list.
6. Added checks on Stand Information.
7. Allow N for Broken top.
8. Disable Crown Width for Stem Map on Dead trees.
9. Several other small issues.

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
