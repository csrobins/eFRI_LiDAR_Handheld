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
        List<PLOT> GetFilteredData(string PROJECTID);

        //Get Specific data
        PLOT GetPlotData(string PLOTID);
        // Delete all Data
        void DeleteAllPlots();
        // Delete Specific
        void DeletePlot(string PLOTID);
        // Insert new to DB 
        void InsertPlot (PLOT plot, string fk);
        // Update Data
        void UpdatePlot(PLOT plot);

        String GetProjectTitle(string projectid);
    
    }
    public interface ITreeRepository
    {
        List<TREE> GetAllData();
        List<TREE> GetFilteredData(string fk);

        //Get Specific data
        TREE GetTreeData(string TREEID);
        // Delete all Data
        void DeleteAllTrees();
        // Delete Specific
        void DeleteTree(string TREEID);
        // Insert new to DB 
        void InsertTree(TREE tree, string fk);
        // Update Data
        void UpdateTree(TREE tree);
        String GetPlotTitle(string plotid);
        int GetAzimuth(string treeid);
        double GetDistance(string treeid);

    }
    public interface IStemMapRepository
    {
        List<STEMMAP> GetAllData();
        List<STEMMAP> GetFilteredData(string fk);

        //Get Specific data
        STEMMAP GetTreeData(string TREEID);
        // Delete all Data
        void DeleteAllTrees();
        // Delete Specific
        void DeleteTree(string STEMMAPID);
        // Insert new to DB 
        void InsertTree(STEMMAP stemmap, string fk);
        // Update Data
        void UpdateTree(STEMMAP stemmap);
        String GetTitle(string treeid);
        bool IsStemMapExists(string treeid);

    }

    public interface IEcositeRepository
    {
        List<ECOSITE> GetAllData();
        List<ECOSITE> GetFilteredData(string fk);

        //Get Specific data
        ECOSITE GetEcositeData(string ECOSITEID);
        // Delete all Data
        void DeleteAllEcosites();
        // Delete Specific
        void DeleteEcosite(string ECOSITEID);
        // Insert new to DB 
        void InsertEcosite(ECOSITE ecosite, string fk);
        // Update Data
        void UpdateEcosite(ECOSITE ecosite);
        String GetTitle(string plotid);
        bool IsEcositeExists(string plotid);

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
        public void DeletePlot(string ID)
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
        public List<PLOT> GetFilteredData(string id)
        {
            return _databaseHelper.GetFilteredPlotData(id);
        }

        public PLOT GetPlotData(string PlotID)
        {
            return _databaseHelper.GetPlotData(PlotID);
        }

        public void InsertPlot(PLOT Plot, string fk)
        {
            Plot.PLOTID = Guid.NewGuid().ToString();
            Plot.PROJECTID = fk;
            _databaseHelper.InsertPlot(Plot);
        }

        public void UpdatePlot(PLOT Plot)
        {
            _databaseHelper.UpdatePlot(Plot);
        }

        public String GetProjectTitle(string projectid)
        {
            if (projectid == "")
            {
                return "";
            }
            else
            {
                return _databaseHelper.GetProjectTitle(projectid);
            }
        }

    }

    public class TreeRepository : ITreeRepository
    {
        DatabaseHelper _databaseHelper;
        public TreeRepository()
        {
            _databaseHelper = new DatabaseHelper();
        }
        public void DeleteTree(string ID)
        {
            _databaseHelper.DeleteTree(ID);
        }
        public void DeleteAllTrees()
        {
            _databaseHelper.DeleteAllTrees();
        }

        public List<TREE> GetAllData()
        {
            return _databaseHelper.GetAllTreeData();
        }

        public List<TREE> GetFilteredData(string fk)
        {
            return _databaseHelper.GetFilteredTreeData(fk);
        }

        public TREE GetTreeData(string TreeID)
        {
            return _databaseHelper.GetTreeData(TreeID);
        }

        public void InsertTree(TREE Tree, string fk)
        {
            Tree.TREEID = Guid.NewGuid().ToString();
            Tree.PLOTID = fk;
            _databaseHelper.InsertTree(Tree);
        }

        public void UpdateTree(TREE Tree)
        {
            _databaseHelper.UpdateTree(Tree);
        }
        public String GetPlotTitle(string plotid)
        {
            if (plotid == "")
            {
                return "";
            }
            else
            {
                return _databaseHelper.GetPlotTitle(plotid);
            }
        }
        public int GetAzimuth(string treeid)
        {
            if (treeid == "")
            {
                return 0;
            }
            else
            {
                return _databaseHelper.GetAzimuth(treeid);
            }
        }
        public double GetDistance(string treeid)
        {
            if (treeid == "")
            {
                return 0;
            }
            else
            {
                return _databaseHelper.GetDistance(treeid);
            }
        }
    }

    public class StemMapRepository : IStemMapRepository
    {
        DatabaseHelper _databaseHelper;
        public StemMapRepository()
        {
            _databaseHelper = new DatabaseHelper();
        }
        public void DeleteTree(string ID)
        {
            _databaseHelper.DeleteStemmap(ID);
        }
        public void DeleteAllTrees()
        {
            _databaseHelper.DeleteAllStemmaps();
        }

        public List<STEMMAP> GetAllData()
        {
            return _databaseHelper.GetAllStemmapData();
        }

        public List<STEMMAP> GetFilteredData(string fk)
        {
            return _databaseHelper.GetFilteredStemmapData(fk);
        }

        public STEMMAP GetTreeData(string TreeID)
        {
            return _databaseHelper.GetStemmapData(TreeID);
        }

        public void InsertTree(STEMMAP Stemmap, string fk)
        {
            Stemmap.STEMMAPID  = Guid.NewGuid().ToString();
            Stemmap.TREEID  = fk;
            _databaseHelper.InsertStemmap(Stemmap);
        }

        public void UpdateTree(STEMMAP Stemmap)
        {
            _databaseHelper.UpdateStemmap(Stemmap);
        }
        public String GetTitle(string treeid)
        {
            if (treeid == "")
            {
                return "";
            }
            else
            {
                return _databaseHelper.GetTreeTitle(treeid);
            }
        }
        public bool IsStemMapExists(string treeid)
        {
            return _databaseHelper.IsStemMapExists(treeid);
        }
    }

    public class EcositeRepository : IEcositeRepository
    {
        DatabaseHelper _databaseHelper;
        public EcositeRepository()
        {
            _databaseHelper = new DatabaseHelper();
        }
        public void DeleteEcosite(string ID)
        {
            _databaseHelper.DeleteEcosite(ID);
        }
        public void DeleteAllEcosites()
        {
            _databaseHelper.DeleteAllEcosites();
        }

        public List<ECOSITE> GetAllData()
        {
            return _databaseHelper.GetAllEcositeData();
        }

        public List<ECOSITE> GetFilteredData(string fk)
        {
            return _databaseHelper.GetFilteredEcositeData(fk);
        }

        public ECOSITE GetEcositeData(string PlotID)
        {
            return _databaseHelper.GetEcositeData(PlotID);
        }

        public void InsertEcosite(ECOSITE ecosite, string fk)
        {
            ecosite.ECOSITEID  = Guid.NewGuid().ToString();
            ecosite.PLOTID  = fk;
            _databaseHelper.InsertEcosite(ecosite);
        }

        public void UpdateEcosite(ECOSITE ecosite)
        {
            _databaseHelper.UpdateEcosite(ecosite);
        }
        public String GetTitle(string plotid)
        {
            if (plotid == "")
            {
                return "";
            }
            else
            {
                return _databaseHelper.GetPlotTitle(plotid);
            }
        }
        public bool IsEcositeExists(string plotid)
        {
            return _databaseHelper.IsEcositeExists(plotid);
        }
    }


}
