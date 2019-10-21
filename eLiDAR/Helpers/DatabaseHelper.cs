using SQLite;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;
using eLiDAR.Models;
using System;

namespace eLiDAR.Helpers {
    public class DatabaseHelper {
        
        static SQLiteConnection sqliteconnection;
        public const string DbFileName = "eLiDAR.sqlite";

        public DatabaseHelper() {
            sqliteconnection = DependencyService.Get<ISQLite>().GetConnection();
           // sqliteconnection.CreateTable<ContactInfo>();
        }
        // Get All project data 
        public List<PROJECT> GetAllProjectData()
        {
            return (from data in sqliteconnection.Table<PROJECT>()
                    select data).ToList();
        }

		//Get Specific Project data
        public PROJECT GetProjectData(String id) {
            return sqliteconnection.Table<PROJECT>().FirstOrDefault(t => t.PROJECTID == id);
        }

        // Delete all Project Data
        public void DeleteAllProjects() {
            sqliteconnection.DeleteAll<PROJECT>();
        }

        // Delete Specific Contact
        public void DeleteProject(String id) {
            sqliteconnection.Delete<PROJECT>(id);
        }

        // Insert new Project to DB 
        public void InsertProject(PROJECT project) {
            sqliteconnection.Insert(project);
        }

        // Update Project Data
        public void UpdateProject(PROJECT project) {
            sqliteconnection.Update(project);
        }
/// <summary>
/// /PLOT VM's start here
/// </summary>
/// <returns></returns>
        // Get All Plot Data 
        public List<PLOT> GetAllPlotData()
        {
            return (from data in sqliteconnection.Table<PLOT>()
                    select data).ToList();
        }

        //Get Specific Plot data
        public PLOT GetPlotData(string id)
        {
            return sqliteconnection.Table<PLOT>().FirstOrDefault(t => t.PLOTID == id);
        }

        // Delete all Plot  Data
        public void DeleteAllPlots()
        {
            sqliteconnection.DeleteAll<PLOT>();
        }

        // Delete Specific
        public void DeletePlot(string id)
        {
            sqliteconnection.Delete<PLOT>(id);
        }
        // Insert new to DB 
        public void InsertPlot(PLOT plot)
        {
            sqliteconnection.Insert(plot);
        }
        // Update Plot Data
        public void UpdatePlot(PLOT plot)
        {
            sqliteconnection.Update(plot);
        }
    }
}