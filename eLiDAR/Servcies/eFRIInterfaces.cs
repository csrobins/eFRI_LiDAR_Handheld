using System;
using System.Collections.Generic;
using eLiDAR.Models;
using eLiDAR.Helpers;

namespace eLiDAR.Servcies
{
       public interface IProjectRepository
    {
        List<PROJECT> GetAllProjectData();
        //Get Specific data
        PROJECT GetProjectData(string PROJECTID);
        // Delete all Data
        void DeleteAllProjects();
        // Delete Specific
        void DeleteProject(string PROJECTID);
        // Insert new to DB 
        void InsertProject(PROJECT project);
        // Update Data
        void UpdateProject(PROJECT project);
    }
    public interface IPlotRepository
    {
        List<PLOT> GetAllData();
        //Get Specific data
        PLOT GetPlotData(string PLOTID);
        // Delete all Data
        void DeleteAllPlots();
        // Delete Specific
        void DeletePlot(string PLOTID);
        // Insert new to DB 
        void InsertPlot (PLOT plot);
        // Update Data
        void UpdatePlot(PLOT plot);
    }
    public class ProjectRepository : IProjectRepository
    {
        DatabaseHelper _databaseHelper;
        public ProjectRepository()
        {
            _databaseHelper = new DatabaseHelper();
        }
        public void DeleteProject(string ID)
        {
            _databaseHelper.DeleteProject(ID);
        }
        public void DeleteAllProjects()
        {
            _databaseHelper.DeleteAllProjects();
        }

        public List<PROJECT> GetAllProjectData()
        {
            return _databaseHelper.GetAllProjectData();
        }

        public PROJECT GetProjectData(string PROJECTID)
        {
            return _databaseHelper.GetProjectData(PROJECTID);
        }

        public void InsertProject(PROJECT project)
        {
            project.PROJECTID = Guid.NewGuid().ToString();
            _databaseHelper.InsertProject(project);
        }

        public void UpdateProject(PROJECT project)
        {
            _databaseHelper.UpdateProject(project);
        }
    }

    public class PlotRepository : IPlotRepository
    {
        DatabaseHelper _databaseHelper;
        public PlotRepository()
        {
            _databaseHelper = new DatabaseHelper();
        }
        public void DeletePlots(string ID)
        {
            _databaseHelper.DeletePlot(ID);
        }
        public void DeleteAllPlots()
        {
            _databaseHelper.DeleteAllPlots();
        }

        public List<PLOT> GetAllData()
        {
            return _databaseHelper.GetAllPlotData();
        }

        public PLOT GetPlotData(string PlotID)
        {
            return _databaseHelper.GetPlotData(PlotID);
        }

        public void InsertPlot(PLOT Plot)
        {
            Plot.PLOTID = Guid.NewGuid().ToString();
            _databaseHelper.InsertPlot(Plot);
        }

        public void UpdatePlot(PLOT Plot)
        {
            _databaseHelper.UpdatePlot(Plot);
        }
    }

}
