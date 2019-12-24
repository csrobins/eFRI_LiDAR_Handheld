using SQLite;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;
using eLiDAR.Models;
using System;
using System.Threading.Tasks;

namespace eLiDAR.Helpers
{
    public class DatabaseHelper
    {

        static SQLiteConnection sqliteconnection;
        public const string DbFileName = "eLiDAR.sqlite";

        public DatabaseHelper()
        {
            sqliteconnection = DependencyService.Get<ISQLite>().GetConnection();

        }
        // Get All project data 
        public List<PROJECT> GetAllProjectData()
        {
            return (from data in sqliteconnection.Table<PROJECT>()
                    select data).ToList();
        }

        //Get Specific Project data
        public PROJECT GetProjectData(String id)
        {
            return sqliteconnection.Table<PROJECT>().FirstOrDefault(t => t.PROJECTID == id);
        }

        //Get Specific Project Title
        public string GetProjectTitle(String id)
        {
            var project = sqliteconnection.Query<PROJECT>("select Name from Project where PROJECTID = '" + id + "'").FirstOrDefault();
            return project.NAME.ToString();
        }
        public string GetPlotTitle(String id)
        {
            var plot = sqliteconnection.Query<PLOT>("select PLOTNUM from Plot where PLOTID = '" + id + "'").FirstOrDefault();
            return plot.PLOTNUM;
        }
        public string GetTreeTitle(String id)
        {
            var tree = sqliteconnection.Query<TREE>("select TREENUM from Tree where TREEID = '" + id + "'").FirstOrDefault();
            return tree.TREENUM.ToString();
        }

        public int GetAzimuth(String id)
        {
            var tree = sqliteconnection.Query<STEMMAP>("select AZIMUTH from StemMap where TREEID = '" + id + "'").FirstOrDefault();
            return tree.AZIMUTH;
        }
        public double GetDistance(String id)
        {
            var tree = sqliteconnection.Query<STEMMAP>("select DISTANCE from StemMap where TREEID = '" + id + "'").FirstOrDefault();
            return tree.DISTANCE ;
        }

        public bool IsStemMapExists(String id)
        {
            var stemmap = sqliteconnection.Query<STEMMAP>("select STEMMAPID from StemMap where TREEID = '" + id + "'").Count();
            if (stemmap > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsEcositeExists(String id)
        {
            var ecosite = sqliteconnection.Query<ECOSITE>("select ECOSITEID from Ecosite where PLOTID = '" + id + "'").Count();
            if (ecosite > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // Delete all Project Data
        public void DeleteAllProjects()
        {
            sqliteconnection.DeleteAll<PROJECT>();
        }

        // Delete Specific Contact
        public void DeleteProject(String id)
        {
            sqliteconnection.Delete<PROJECT>(id);
        }

        // Insert new Project to DB 
        public void InsertProject(PROJECT project)
        {
            sqliteconnection.Insert(project);
        }

        // Update Project Data
        public void UpdateProject(PROJECT project)
        {
            sqliteconnection.Update(project);
        }
        /// <summary>
        /// /PLOT VM's start here
        /// </summary>
        /// <returns></returns>
        // Get All Plot Data 
        public List<PLOT> GetAllPlotData()
        {
            return (from data in sqliteconnection.Table<PLOT>().OrderBy(t => t.PLOTNUM)
                    select data).ToList() ;
        }

        public List<PLOT> GetFilteredPlotData(string projectid)
        {
            return (from data in sqliteconnection.Table<PLOT>().OrderBy(t => t.PLOTNUM).Where(t => t.PROJECTID == projectid) 
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
        /// /TREE VM's start here
        /// </summary>
        /// <returns></returns>
        // Get All Tree Data 
        public List<TREE> GetAllTreeData()
        {
            return (from data in sqliteconnection.Table<TREE>().OrderBy(t => t.TREENUM)
                    select data).ToList();
        }
        
        public List<TREE> GetFilteredTreeData(string plotid)
        {
            return (from data in sqliteconnection.Table<TREE>().OrderBy(t => t.TREENUM).Where(t => t.PLOTID == plotid)
                    select data).ToList();
        }
        public List<TREE> GetFilteredTreeStemList(string plotid)
        {
            string qry = "select TREE.TREEID, PLOTID, TREENUM, SPECIES, ORIGIN, STATUS, DBH, HT, STEMMAP.AZIMUTH, STEMMAP.DISTANCE from TREE LEFT JOIN STEMMAP ON TREE.TREEID = STEMMAP.TREEID where TREE.PLOTID = '" + plotid + "' ORDER BY TREENUM";
            try
            {
                var tree = sqliteconnection.Query<TREE>(qry).ToList();
                return tree;
            }
            catch (Exception e)
            {
                var myerror = e.Message; // erro
                return null;
            }


            
          
        }
        //Get Specific data
        public TREE GetTreeData(string id)
        {
            return sqliteconnection.Table<TREE>().FirstOrDefault(t => t.TREEID == id);
        }

        // Delete all Data
        public void DeleteAllTrees()
        {
            sqliteconnection.DeleteAll<TREE>();
        }

        // Delete Specific
        public void DeleteTree(string id)
        {
            sqliteconnection.Delete<TREE>(id);
        }
        // Insert new to DB 
        public void InsertTree(TREE tree)
        {
            sqliteconnection.Insert(tree);
        }
        // Update Tree Data
        public void UpdateTree(TREE tree)
        {
            sqliteconnection.Update(tree);
        }
        public void DeleteStemmap(string id)
        {
            sqliteconnection.Delete<STEMMAP>(id);
        }
        public void DeleteAllStemmaps()
        {
            sqliteconnection.DeleteAll<STEMMAP>();
        }
        public List<STEMMAP> GetAllStemmapData()
        {
            return (from data in sqliteconnection.Table<STEMMAP>().OrderBy(t => t.TREEID)
                    select data).ToList();
        }
        public List<STEMMAP> GetFilteredStemmapData(string treeid)
        {
            return (from data in sqliteconnection.Table<STEMMAP>().OrderBy(t => t.STEMMAPID).Where(t => t.TREEID == treeid)
                    select data).ToList();
        }
        public STEMMAP GetStemmapData(string id)
        {
            return sqliteconnection.Table<STEMMAP>().FirstOrDefault(t => t.TREEID == id);
        }
        public void InsertStemmap(STEMMAP stemmap)
        {
            sqliteconnection.Insert(stemmap);
        }
        public void UpdateStemmap(STEMMAP stemmap)
        {
            sqliteconnection.Update(stemmap);
        }

        // Ecosite helpers
        // Delete Specific
        public void DeleteEcosite(string id)
        {
            sqliteconnection.Delete<ECOSITE>(id);
        }
        // Insert new to DB 
        public void InsertEcosite(ECOSITE ecosite)
        {
            sqliteconnection.Insert(ecosite);
        }
        // Update Tree Data
        public void UpdateEcosite(ECOSITE ecosite)
        {
            sqliteconnection.Update(ecosite);
        }
 
        public void DeleteAllEcosites()
        {
            sqliteconnection.DeleteAll<ECOSITE>();
        }
        public List<ECOSITE> GetAllEcositeData()
        {
            return (from data in sqliteconnection.Table<ECOSITE>().OrderBy(t => t.ECOSITEID)
                    select data).ToList();
        }
        public List<ECOSITE> GetFilteredEcositeData(string plotid)
        {
            return (from data in sqliteconnection.Table<ECOSITE>().OrderBy(t => t.PLOTID).Where(t => t.PLOTID == plotid)
                    select data).ToList();
        }
        public ECOSITE GetEcositeData(string id)
        {
            return sqliteconnection.Table<ECOSITE>().FirstOrDefault(t => t.PLOTID == id);
        }
   // SOIL Helpers
        public void DeleteSoil(string id)
        {
            sqliteconnection.Delete<SOIL>(id);
        }
        // Insert new to DB 
        public void  InsertSoil(SOIL soil)
        {
            sqliteconnection.Insert(soil);
           
        }
        // Update Tree Data
        public void UpdateSoil(SOIL soil)
        {
            sqliteconnection.Update(soil);
        }

        public void DeleteAllSoil()
        {
            sqliteconnection.DeleteAll<SOIL>();
        }
        public List<SOIL> GetAllSoilData()
        {
            return (from data in sqliteconnection.Table<SOIL>().OrderBy(t => t.FROM)
                    select data).ToList();
        }
        public List<SOIL> GetFilteredSoilData(string plotid)
        {
            return (from data in sqliteconnection.Table<SOIL>().OrderBy(t => t.FROM).Where(t => t.PLOTID == plotid)
                    select data).ToList();
        }
        public SOIL GetSoilData(string id)
        {
            return sqliteconnection.Table<SOIL>().FirstOrDefault(t => t.SOILID == id);
        }

        // Small Tree Helpers
        public void DeleteSmallTree(string id)
        {
            sqliteconnection.Delete<SMALLTREE>(id);
        }
        // Insert new to DB 
        public void InsertSmallTree(SMALLTREE smalltree)
        {
            sqliteconnection.Insert(smalltree);
        }
        // Update Tree Data
        public void UpdateSmallTree(SMALLTREE smalltree)
        {
            sqliteconnection.Update(smalltree);
        }

        public void DeleteAllSmallTree()
        {
            sqliteconnection.DeleteAll<SMALLTREE>();
        }
        public List<SMALLTREE> GetAllSmallTreeData()
        {
            return (from data in sqliteconnection.Table<SMALLTREE>().OrderBy(t => t.SPECIES)
                    select data).ToList();
        }
        public List<SMALLTREE> GetFilteredSmallTreeData(string plotid)
        {
            return (from data in sqliteconnection.Table<SMALLTREE>().OrderBy(t => t.SPECIES).Where(t => t.PLOTID == plotid)
                    select data).ToList();
        }
        public SMALLTREE GetSmallTreeData(string id)
        {
            return sqliteconnection.Table<SMALLTREE>().FirstOrDefault(t => t.SMALLTREEID == id);
        }


    }
}