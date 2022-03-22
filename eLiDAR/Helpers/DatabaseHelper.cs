using SQLite;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;
using eLiDAR.Models;
using System;
using System.Threading.Tasks;
using eLiDAR;

namespace eLiDAR.Helpers
{
    public class DatabaseHelper
    {
        private LogWriter logger;
        static SQLiteConnection sqliteconnection;
        public const string DbFileName = "eLiDAR.sqlite";
    
        public DatabaseHelper()
        {
            logger = new LogWriter(); 
            sqliteconnection = DependencyService.Get<ISQLite>().GetConnection();

        }
        // Get All project data 
        public List<PROJECT> GetAllProjectData()
        {
            return (from data in sqliteconnection.Table<PROJECT>().Where(t => t.IsDeleted == "N").OrderBy(t => t.NAME )  
                    select data).ToList();
        }

        public List<PERSON> GetAllPersonData()
        {
            return (from data in sqliteconnection.Table<PERSON>()
                    select data).ToList();
        }


        //Get Specific Project data
        public PROJECT GetProjectData(String id)
        {
            return sqliteconnection.Table<PROJECT>().FirstOrDefault(t => t.PROJECTID == id);
        }

        //Get Specific Person data
        public PERSON GetPersonData(String id)
        {
            return sqliteconnection.Table<PERSON>().FirstOrDefault(t => t.PERSONID == id);
        }
        public PHOTO GetPhotoData(String id)
        {
            return sqliteconnection.Table<PHOTO>().FirstOrDefault(t => t.PHOTOID == id);
        }

        //Get Specific Project Title
        public string GetProjectTitle(String id)
        {
            var project = sqliteconnection.Query<PROJECT>("select Name from Project where PROJECTID = '" + id + "'").FirstOrDefault();
            return project.NAME.ToString();
        }
        public string GetPlotTitle(String id)
        {
            var plot = sqliteconnection.Query<PLOT>("select VSNPLOTNAME from Plot where PLOTID = '" + id + "'").FirstOrDefault();
            return plot.VSNPLOTNAME;
        }
        public string GetPlotType(String id)
        {
            var plot = sqliteconnection.Query<PLOT>("select VSNPLOTTYPECODE from Plot where PLOTID = '" + id + "'").FirstOrDefault();
            return plot.VSNPLOTTYPECODE;
        }

        public string GetTreeTitle(String id)
        {
            var tree = sqliteconnection.Query<TREE>("select TREENUMBER from Tree where TREEID = '" + id + "'").FirstOrDefault();
            return tree.TREENUMBER.ToString();
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

        public bool RequiresCrownWidth(String id)
        {
            var tree = sqliteconnection.Query<TREE>("select * from TREE where TREEID = '" + id + "'").FirstOrDefault ();
 //           if ((tree.TREESTATUSCODE == "D" || tree.TREESTATUSCODE == "DV") || tree.BROKENTOP == "Y"  || tree.DECAYCLASS == 4 || tree.DECAYCLASS == 5)
            if ((tree.TREESTATUSCODE == "D" || tree.TREESTATUSCODE == "DV") && (tree.DECAYCLASS == 4 || tree.DECAYCLASS == 5))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool IsStemMapExists(String id)
        {
            var stemmap = sqliteconnection.Query<STEMMAP>("select STEMMAPID from STEMMAP where TREEID = '" + id + "'").Count();
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
        public void DeleteAllPersons()
        {
            sqliteconnection.DeleteAll<PERSON>();
        }
        // Delete Specific Contact
        public void DeletePerson(String id)
        {
            sqliteconnection.Delete<PERSON>(id);
        }
        public void DeletePhoto(String id)
        {
            sqliteconnection.Delete<PHOTO>(id);
        }

        // Delete Specific Contact
        public void DeleteProject(String id)
        {
            sqliteconnection.Delete<PROJECT>(id);
        }

        // Insert new Project to DB 
        public void InsertProject(PROJECT project)
        {
            try
            {
                sqliteconnection.Insert(project);
            }
            catch (Exception ex) 
                { logger.LogWrite(ex.Message);   }

        }

        public void InsertPerson(PERSON person)
        {
            try
            {
                sqliteconnection.Insert(person);
            }
            catch (Exception ex)
            { logger.LogWrite(ex.Message); }
        }
        public void InsertPhoto(PHOTO photo)
        {
            try
            {
                sqliteconnection.Insert(photo);
            }
            catch (Exception ex)
            { logger.LogWrite(ex.Message); }
        }
        // Update Project Data
        public void UpdatePerson(PERSON person)
        {
            sqliteconnection.Update(person);
        }
        public void UpdatePhoto(PHOTO photo)
        {
            sqliteconnection.Update(photo);
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
            return (from data in sqliteconnection.Table<PLOT>().OrderBy(t => t.VSNPLOTNAME)
                    select data).ToList() ;
        }
        public List<PERSON> GetFilteredPersonData(string projectid = null)
        {
            if (projectid != null) 
            { 
                return (from data in sqliteconnection.Query<PERSON>("select * from PERSON where PROJECTID = '" + projectid + "' and IsDeleted = 'N' ORDER BY LASTNAME")
                    select data).ToList();
            }
            else
            {
                return (from data in sqliteconnection.Query<PERSON>("select * from PERSON where IsDeleted = 'N' ORDER BY LASTNAME")
                        select data).ToList();
            }

        }
        public List<PHOTO> GetFilteredPhotoData(string plotid)
        {
            return (from data in sqliteconnection.Query<PHOTO>("select * from PHOTO where PLOTID = '" + plotid + "' and IsDeleted = 'N' ORDER BY PHOTOTYPE, FRAMENUMBER, PHOTONUMBER, AZIMUTH")
                    select data).ToList();
        }
        public USER GetUser(string username)
        {
            return sqliteconnection.Table<USER>().FirstOrDefault(t => t.USERNAME == username);
        }
        public List<PLOT> GetFilteredPlotData(string projectid)
        {
          //  return (from data in sqliteconnection.Table<PLOT>().OrderBy(t => t.VSNPLOTNAME).Where(t => t.PROJECTID == projectid) 
            //        select data).ToList();
            return (from data in sqliteconnection.Query<PLOT>("select * from PLOT where PROJECTID = '" + projectid + "' and IsDeleted = 'N' ORDER BY VSNPLOTNAME")
                    select data).ToList();
        }
        public List<PLOTLIST> GetFilteredPlotDataFull(string projectid)
        {
        //    return (from data in sqliteconnection.Query<PLOTLIST>("Select * from PLOT").OrderBy(t => t.VSNPLOTNAME).Where(t => t.PROJECTID == projectid)
         //           select data).ToList();
            return (from data in sqliteconnection.Query<PLOTLIST>("select * from PLOT where PROJECTID = '" + projectid + "' and IsDeleted = 'N' ORDER BY VSNPLOTNAME")
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
            try
            {
                sqliteconnection.Insert(plot);
            }
            catch (Exception ex)
            { logger.LogWrite(ex.Message); }
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
            return (from data in sqliteconnection.Table<TREE>().OrderBy(t => t.TREENUMBER)
                    select data).ToList();
        }
        
        public List<TREE> GetFilteredTreeData(string plotid)
        {
//            return (from data in sqliteconnection.Table<TREE>().OrderBy(t => t.TREENUMBER).Where(t => t.PLOTID == plotid)
 //                   select data).ToList();
            return (from data in sqliteconnection.Query<TREE>("select * from TREE where PLOTID = '" + plotid + "' and IsDeleted = 'N' ORDER BY TREENUMBER")
                    select data).ToList();
        }
        public List<TREELIST> GetFilteredTreeDataFull(string plotid)
        {
            return (from data in sqliteconnection.Query<TREELIST>("select TREE.*, VSNPLOTTYPECODE from TREE INNER JOIN PLOT ON TREE.PLOTID = PLOT.PLOTID").OrderBy(t => t.TREENUMBER).Where(t => t.PLOTID == plotid && t.IsDeleted == "N")
                    select data).ToList();
      //      return (from data in sqliteconnection.Query<TREELIST>("select * from TREE where PLOTID = '" + plotid + "' and IsDeleted = 'N' ORDER BY TREENUMBER")
      //              select data).ToList();
        }
        public List<SMALLTREELIST> GetFilteredSmallTreeDataFull(string plotid)
        {
               return (from data in sqliteconnection.Query<SMALLTREELIST>("select SMALLTREE.* from SMALLTREE").OrderBy(t => t.SpeciesName).Where(t => t.PLOTID == plotid && t.IsDeleted == "N")
                        select data).ToList();
        }

        public List<TREE> GetFilteredTreeStemList(string plotid)
        {
//            string qry = "select TREE.TREEID, PLOTID, TREENUMBER, SPECIESCODE, TREEORIGINCODE, TREESTATUSCODE, DBH, DIRECTTOTALHEIGHT, STEMMAP.AZIMUTH, STEMMAP.DISTANCE from TREE LEFT JOIN STEMMAP ON TREE.TREEID = STEMMAP.TREEID where TREE.PLOTID = '" + plotid + "' ORDER BY TREENUMBER";
            string qry = "select TREE.TREEID, PLOTID, TREENUMBER, SPECIESCODE, TREEORIGINCODE, TREESTATUSCODE, DBH, DIRECTTOTALHEIGHT,CORESTATUSCODE, STEMMAP.AZIMUTH, STEMMAP.DISTANCE from TREE LEFT JOIN STEMMAP ON TREE.TREEID = STEMMAP.TREEID where TREE.PLOTID = '" + plotid + "' and TREE.IsDeleted = 'N' ORDER BY TREENUMBER";

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
        public List<TREELIST> GetFilteredTreeStemListFull(string plotid, bool sort = false)
        {
            //            string qry = "select VSNPLOTTYPECODE, TREE.TREEID, TREE.PLOTID, TREENUMBER, SPECIESCODE, TREE.TREEORIGINCODE, TREESTATUSCODE, DBH, DIRECTTOTALHEIGHT, STEMMAP.AZIMUTH, STEMMAP.DISTANCE from TREE INNER JOIN PLOT ON TREE.PLOTID = PLOT.PLOTID LEFT JOIN STEMMAP ON TREE.TREEID = STEMMAP.TREEID where TREE.PLOTID = '" + plotid + "' ORDER BY TREENUMBER";
            string qry;
            if (!sort)
            {
                qry = "select VSNPLOTTYPECODE, TREE.TREEID, TREE.PLOTID, TREENUMBER, SPECIESCODE, TREE.TREEORIGINCODE, TREESTATUSCODE, DBH, CROWNCLASSCODE, OCULARTOTALHEIGHT, DIRECTTOTALHEIGHT, ERRORCOUNT, CASE WHEN OCULARTOTALHEIGHT > 0 THEN OCULARTOTALHEIGHT ELSE DIRECTTOTALHEIGHT END HEIGHT, CORESTATUSCODE, STEMMAP.AZIMUTH, STEMMAP.DISTANCE from TREE INNER JOIN PLOT ON TREE.PLOTID = PLOT.PLOTID LEFT JOIN STEMMAP ON TREE.TREEID = STEMMAP.TREEID where TREE.PLOTID = '" + plotid + "' and TREE.IsDeleted = 'N' ORDER BY TREENUMBER";
            }
            else
            {  
                qry = "select VSNPLOTTYPECODE, TREE.TREEID, TREE.PLOTID, TREENUMBER, SPECIESCODE, TREE.TREEORIGINCODE, TREESTATUSCODE, DBH, CROWNCLASSCODE,OCULARTOTALHEIGHT, DIRECTTOTALHEIGHT,  ERRORCOUNT, CASE WHEN OCULARTOTALHEIGHT > 0 THEN OCULARTOTALHEIGHT ELSE DIRECTTOTALHEIGHT END HEIGHT, CORESTATUSCODE, STEMMAP.AZIMUTH, STEMMAP.DISTANCE from TREE INNER JOIN PLOT ON TREE.PLOTID = PLOT.PLOTID LEFT JOIN STEMMAP ON TREE.TREEID = STEMMAP.TREEID where TREE.PLOTID = '" + plotid + "' and TREE.IsDeleted = 'N' ORDER BY SPECIESCODE, DBH DESC";
            }
            try
            {
                var tree = sqliteconnection.Query<TREELIST>(qry).ToList();
                return tree;
            }
            catch (Exception e)
            {
                var myerror = e.Message; // erro
                return null;
            }
        }
        public bool IsTreeNumUnique(TREE _tree)
        {
            // for examinginif a unique tree number is being saved
            string qry;
            if (_tree.TREEID != null)
            {
                qry = "select count(TREEID) from TREE where PLOTID = '" + _tree.PLOTID + "' and IsDeleted = 'N' and TREENUMBER = " + _tree.TREENUMBER + " and TREEID <> '" + _tree.TREEID + "'";
            }
            else
            {
                qry = "select count(TREEID) from TREE where PLOTID = '" + _tree.PLOTID + "' and IsDeleted = 'N' and TREENUMBER = " + _tree.TREENUMBER;
            }
            try
            {
                var treenum = sqliteconnection.ExecuteScalar<int>(qry);
                if (treenum > 0) { return false; }
                else { return true; }
            }
            catch (Exception e)
            {
                var myerror = e.Message; // erro
                return false;
            }
        }
        public bool IsPhotoTableEmpty(string _plotid)
        {
            // for examingin if photo table is empty
            string qry;
            qry = "select count(PHOTOID) from PHOTO where PLOTID = '" + _plotid + "'";
            try
            {
                var treenum = sqliteconnection.ExecuteScalar<int>(qry);
                if (treenum > 0) { return false; }
                else { return true; }
            }
            catch (Exception e)
            {
                var myerror = e.Message; // erro
                return false;
            }
        }

        public bool IsPlotNumUnique(PLOT _plot)
        {
            // for examinginif a unique tree number is being saved
            string qry;
            if (_plot.PLOTID != null)
            {
                qry = "select count(PLOTID) from PLOT where MEASURETYPECODE = '" + _plot.MEASURETYPECODE + "' and IsDeleted = 'N' and PROJECTID = '" + _plot.PROJECTID + "' and VSNPLOTNAME = '" + _plot.VSNPLOTNAME + "' and PLOTID <> '" + _plot.PLOTID + "'";
            }
            else
            {
                qry = "select count(PLOTID) from PLOT where MEASURETYPECODE = '" + _plot.MEASURETYPECODE + "' and IsDeleted = 'N' and PROJECTID = '" + _plot.PROJECTID + "' and VSNPLOTNAME = '" + _plot.VSNPLOTNAME + "'";
            }

            try
            {
                var num = sqliteconnection.ExecuteScalar<int>(qry);
                if (num > 0) { return false; }
                else { return true; }
            }
            catch (Exception e)
            {
                var myerror = e.Message; // erro
                return false;
            }
        }
        public bool IsDWDNumUnique(DWD _dwd)
        {
            // for examinginif a unique tree number is being saved
            string qry;
            if (_dwd.DWDID != null)
            {
                qry = "select count(DWDID) from DWD where PLOTID = '" + _dwd.PLOTID + "'  and IsDeleted = 'N' and IS_ACCUM != 'Y' and DWDNUM = '" + _dwd.DWDNUM + "' and DWDID <> '" + _dwd.DWDID + "'";
            }
            else
            {
                qry = "select count(DWDID) from DWD where PLOTID = '" + _dwd.PLOTID + "' and IsDeleted = 'N' and IS_ACCUM != 'Y' and DWDNUM = '" + _dwd.DWDNUM + "'";
            }

            try
            {
                var num = sqliteconnection.ExecuteScalar<int>(qry);
                if (num > 0) { return false; }
                else { return true; }
            }
            catch (Exception e)
            {
                var myerror = e.Message; // erro
                return false;
            }
        }
        public bool IsSoilNumUnique(SOIL  _soil)
        {
            // for examinginif a unique tree number is being saved
            string qry;
            if (_soil.SOILID != null)
            {
                qry = "select count(SOILID) from SOIL where PLOTID = '" + _soil.PLOTID + "' and IsDeleted = 'N'  and HORIZONNUMBER = " + _soil.HORIZONNUMBER + " and SOILID <> '" + _soil.SOILID + "'";
            }
            else
            {
                qry = "select count(SOILID) from SOIL where PLOTID = '" + _soil.PLOTID + "' and IsDeleted = 'N' and HORIZONNUMBER = " + _soil.HORIZONNUMBER;
            }

            try
            {
                var num = sqliteconnection.ExecuteScalar<int>(qry);
                if (num > 0) { return false; }
                else { return true; }
            }
            catch (Exception e)
            {
                var myerror = e.Message; // erro
                return false;
            }
        }
        public bool IsVegUnique(VEGETATION _vegetation)
        {
            // for examinginif a unique veg species is being saved
            string qry;
            if (_vegetation.VEGETATIONID != null)
            {
                qry = "select count(VEGETATIONID) from VEGETATION where PLOTID = '" + _vegetation.PLOTID + "'  and IsDeleted = 'N'  and VSNSPECIESCODE = '" + _vegetation.VSNSPECIESCODE + "' and VEGETATIONID <> '" + _vegetation.VEGETATIONID + "'";
            }
            else
            {
                qry = "select count(VEGETATIONID) from VEGETATION where PLOTID = '" + _vegetation.PLOTID + "' and IsDeleted = 'N' and VSNSPECIESCODE = '" + _vegetation.VSNSPECIESCODE + "'";
            }

            try
            {
                var num = sqliteconnection.ExecuteScalar<int>(qry);
                if (num > 0) { return false; }
                else { return true; }
            }
            catch (Exception e)
            {
                var myerror = e.Message; // erro
                return false;
            }
        }
        public bool IsSpecimenUnique(VEGETATION _vegetation)
        {
            // for examinginif a unique veg specimen number is being saved
            string qry;
            if (_vegetation.SPECIMENNUMBER == 0) { return true; }
            if (_vegetation.VEGETATIONID != null)
            {
                qry = "select count(VEGETATIONID) from VEGETATION where PLOTID = '" + _vegetation.PLOTID + "'  and IsDeleted = 'N'  and SPECIMENNUMBER = " + _vegetation.SPECIMENNUMBER + " and VEGETATIONID <> '" + _vegetation.VEGETATIONID + "'";
            }
            else
            {
                qry = "select count(VEGETATIONID) from VEGETATION where PLOTID = '" + _vegetation.PLOTID + "' and IsDeleted = 'N' and SPECIMENNUMBER = " + _vegetation.SPECIMENNUMBER;
            }

            try
            {
                var num = sqliteconnection.ExecuteScalar<int>(qry);
                if (num > 0) { return false; }
                else { return true; }
            }
            catch (Exception e)
            {
                var myerror = e.Message; // erro
                return false;
            }
        }
        public bool IsSpecimenUnique(VEGETATIONCENSUS _vegetation)
        {
            // for examinginif a unique veg specimen number is being saved
            string qry;
            if (_vegetation.SPECIMENNUMBER == 0 ) { return true; }
            if (_vegetation.VEGETATIONCENSUSID != null)
            {
                qry = "select count(VEGETATIONCENSUSID) from VEGETATIONCENSUS where PLOTID = '" + _vegetation.PLOTID + "'  and IsDeleted = 'N'  and SPECIMENNUMBER = " + _vegetation.SPECIMENNUMBER + " and VEGETATIONCENSUSID <> '" + _vegetation.VEGETATIONCENSUSID + "'";
            }
            else
            {
                qry = "select count(VEGETATIONCENSUSID) from VEGETATIONCENSUS where PLOTID = '" + _vegetation.PLOTID + "' and IsDeleted = 'N' and SPECIMENNUMBER = " + _vegetation.SPECIMENNUMBER;
            }

            try
            {
                var num = sqliteconnection.ExecuteScalar<int>(qry);
                if (num > 0) { return false; }
                else { return true; }
            }
            catch (Exception e)
            {
                var myerror = e.Message; // erro
                return false;
            }
        }
        public bool IsVegUnique(VEGETATIONCENSUS _vegetation)
        {
            // for examinginif a unique veg census species is being saved
            string qry;
            if (_vegetation.VEGETATIONCENSUSID != null)
            {
                qry = "select count(VEGETATIONCENSUSID) from VEGETATIONCENSUS where PLOTID = '" + _vegetation.PLOTID + "' and IsDeleted = 'N' and VSNSPECIESCODE = '" + _vegetation.VSNSPECIESCODE + "' and VEGETATIONCENSUSID <> '" + _vegetation.VEGETATIONCENSUSID + "'";
            }
            else
            {
                qry = "select count(VEGETATIONCENSUSID) from VEGETATIONCENSUS where PLOTID = '" + _vegetation.PLOTID + "' and IsDeleted = 'N'  and VSNSPECIESCODE = '" + _vegetation.VSNSPECIESCODE + "'";
            }
            try
            {
                var num = sqliteconnection.ExecuteScalar<int>(qry);
                if (num > 0) { return false; }
                else { return true; }
            }
            catch (Exception e)
            {
                var myerror = e.Message; // erro
                return false;
            }
        }
        public bool IsSmallTreeUnique(SMALLTREE _smalltree)
        {
            // for examinginif a unique veg census species is being saved
            string qry;
            if (_smalltree.SMALLTREEID != null)
            {
                qry = "select count(SMALLTREEID) from SMALLTREE where PLOTID = '" + _smalltree.PLOTID + "' and IsDeleted = 'N' and SPECIESCODE = " + _smalltree.SPECIESCODE + " and SMALLTREEID <> '" + _smalltree.SMALLTREEID + "'";
            }
            else
            {
                qry = "select count(SMALLTREEID) from SMALLTREE where PLOTID = '" + _smalltree.PLOTID + "' and IsDeleted = 'N' and SPECIESCODE = " + _smalltree.SPECIESCODE;
            }
            try
            {
                var num = sqliteconnection.ExecuteScalar<int>(qry);
                if (num > 0) { return false; }
                else { return true; }
            }
            catch (Exception e)
            {
                var myerror = e.Message; // erro
                return false;
            }
        }

        //Get Specific data
        public TREE GetTreeData(string id)
        {
            return sqliteconnection.Table<TREE>().FirstOrDefault(t => t.TREEID == id);
        }
        public int GetNextTreeNumber(string id)
        {
            string qry;
            qry = "select max(TREENUMBER) from TREE where PLOTID = '" + id + "' and IsDeleted = 'N'";
            try
            {
                var num = sqliteconnection.ExecuteScalar<int>(qry);
                if (num > 0) { return num +1; }
                else { return 1; }
            }
            catch (Exception e)
            {
                var myerror = e.Message; // erro
                return 1;
            }
        }
        public int GetNextSoilNumber(string id)
        {
            string qry;
            qry = "select max(HORIZONNUMBER) from SOIL where PLOTID = '" + id + "' and IsDeleted = 'N'";
            try
            {
                var num = sqliteconnection.ExecuteScalar<int>(qry);
                if (num > 0) { return num + 1; }
                else { return 1; }
            }
            catch (Exception e)
            {
                var myerror = e.Message; // erro
                return 1;
            }
        }
        public int GetNextDWDNumber(string id)
        {
            string qry;
            qry = "select max(DWDNUM) from DWD where PLOTID = '" + id + "' and IsDeleted = 'N'";
            try
            {
                var num = sqliteconnection.ExecuteScalar<int>(qry);
                if (num > 0) { return num + 1; }
                else { return 1; }
            }
            catch (Exception e)
            {
                var myerror = e.Message; // erro
                return 1;
            }
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
            try
            {
                sqliteconnection.Insert(tree);
            }
            catch (Exception ex)
            { logger.LogWrite(ex.Message); }
        }
        // Update Tree Data
        public void UpdateTree(TREE tree)
        {
            try
            {
                sqliteconnection.Update(tree);
            }
            catch (Exception ex)
            { logger.LogWrite(ex.Message); }
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
      //      return (from data in sqliteconnection.Table<STEMMAP>().OrderBy(t => t.STEMMAPID).Where(t => t.TREEID == treeid)
       //             select data).ToList();
            return (from data in sqliteconnection.Query<STEMMAP>("select * from STEMMAP where TREEID = '" + treeid + "' and IsDeleted = 'N'")
                    select data).ToList();
        }
        public STEMMAP GetStemmapData(string id)
        {
            return sqliteconnection.Table<STEMMAP>().FirstOrDefault(t => t.TREEID == id);
        }
        public void InsertStemmap(STEMMAP stemmap)
        {
            try
            {
                sqliteconnection.Insert(stemmap);
            }
            catch (Exception ex)
            { logger.LogWrite(ex.Message); }
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
            try
            {
                sqliteconnection.Insert(ecosite);
            }
            catch (Exception ex)
            { logger.LogWrite(ex.Message); }
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
     //       return (from data in sqliteconnection.Table<ECOSITE>().OrderBy(t => t.PLOTID).Where(t => t.PLOTID == plotid)
     //               select data).ToList();
            return (from data in sqliteconnection.Query<ECOSITE>("select * from ECOSITE where PLOTID = '" + plotid + "' and IsDeleted = 'N'")
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
            try
            {
                sqliteconnection.Insert(soil);
            }
            catch (Exception ex)
            { logger.LogWrite(ex.Message); }

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
            return (from data in sqliteconnection.Table<SOIL>().OrderBy(t => t.DEPTHFROM)
                    select data).ToList();
        }
        public List<SOIL> GetFilteredSoilData(string plotid)
        {
//            return (from data in sqliteconnection.Table<SOIL>().OrderBy(t => t.HORIZONNUMBER).Where(t => t.PLOTID == plotid)
 //                   select data).ToList();
            return (from data in sqliteconnection.Query<SOIL>("select * from SOIL where PLOTID = '" + plotid + "' and IsDeleted = 'N' ORDER BY HORIZONNUMBER")
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
            try
            {
                sqliteconnection.Insert(smalltree);
            }
            catch (Exception ex)
            { logger.LogWrite(ex.Message); }
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
            return (from data in sqliteconnection.Table<SMALLTREE>().OrderBy(t => t.SPECIESCODE)
                    select data).ToList();
        }
        public List<SMALLTREE> GetFilteredSmallTreeData(string plotid)
        {
//            return (from data in sqliteconnection.Table<SMALLTREE>().OrderBy(t => t.SPECIESCODE).Where(t => t.PLOTID == plotid)
 //                   select data).ToList();
            return (from data in sqliteconnection.Query<SMALLTREE>("select * from SMALLTREE where PLOTID = '" + plotid + "' and IsDeleted = 'N' ORDER BY SPECIESCODE")
                    select data).ToList();
        }
        public SMALLTREE GetSmallTreeData(string id)
        {
            return sqliteconnection.Table<SMALLTREE>().FirstOrDefault(t => t.SMALLTREEID == id);
        }

        // Vegetation Helpers
        public void DeleteVegetation(string id)
        {
            sqliteconnection.Delete<VEGETATION>(id);
        }
        public void DeleteVegetationCensus(string id)
        {
            sqliteconnection.Delete<VEGETATIONCENSUS>(id);
        }
        public void InsertVegetationCensus(VEGETATIONCENSUS vegetation)
        {
            try
            {
                sqliteconnection.Insert(vegetation);
            }
            catch (Exception ex)
            { logger.LogWrite(ex.Message); }
        }
        // Insert new to DB 
        public void InsertVegetation(VEGETATION vegetation)
        {
            try
            {
                sqliteconnection.Insert(vegetation);
            }
            catch (Exception ex)
            { logger.LogWrite(ex.Message); }
        }
        // Update Data
        public void UpdateVegetation(VEGETATION vegetation)
        {
            sqliteconnection.Update(vegetation);
        }
        public void UpdateVegetation(VEGETATIONCENSUS vegetation)
        {
            sqliteconnection.Update(vegetation);
        }
        public void DeleteAllVegetation()
        {
            sqliteconnection.DeleteAll<VEGETATION>();
        }
        public void DeleteAllVegetationCensus()
        {
            sqliteconnection.DeleteAll<VEGETATIONCENSUS>();
        }
        public List<VEGETATION> GetAllVegetationData()
        {
            return (from data in sqliteconnection.Table<VEGETATION>().OrderBy(t => t.VSNSPECIESCODE)
                    select data).ToList();
        }
        public List<VEGETATIONCENSUS> GetAllVegetationCensusData()
        {
            return (from data in sqliteconnection.Table<VEGETATIONCENSUS>().OrderBy(t => t.VSNSPECIESCODE)
                    select data).ToList();
        }
        public List<VEGETATION> GetFilteredVegetationData(string plotid)
        {
//            return (from data in sqliteconnection.Table<VEGETATION>().OrderBy(t => t.VSNSPECIESCODE).Where(t => t.PLOTID == plotid)
//                    select data).ToList();
            return (from data in sqliteconnection.Query<VEGETATION>("select * from VEGETATION where PLOTID = '" + plotid + "' and IsDeleted = 'N' ORDER BY VSNSPECIESCODE")
                    select data).ToList();
        }
        public List<VEGETATIONCENSUS> GetFilteredVegetationCensusData(string plotid)
        {
//            return (from data in sqliteconnection.Table<VEGETATIONCENSUS>().OrderBy(t => t.VSNSPECIESCODE).Where(t => t.PLOTID == plotid)
 //                   select data).ToList();
            return (from data in sqliteconnection.Query<VEGETATIONCENSUS>("select * from VEGETATIONCENSUS where PLOTID = '" + plotid + "' and IsDeleted = 'N' ORDER BY VSNSPECIESCODE")
                    select data).ToList();

        }
        public VEGETATION GetVegetationData(string id)
        {
            return sqliteconnection.Table<VEGETATION>().FirstOrDefault(t => t.VEGETATIONID == id);
        }
        public VEGETATIONCENSUS GetVegetationCensusData(string id)
        {
            return sqliteconnection.Table<VEGETATIONCENSUS>().FirstOrDefault(t => t.VEGETATIONCENSUSID == id);
        }
        // Deformity Helpers

        public void DeleteDeformity(string id)
        {
            sqliteconnection.Delete<DEFORMITY>(id);
        }
        // Insert new to DB 
        public void InsertDeformity(DEFORMITY deformity)
        {
            try
            {
                sqliteconnection.Insert(deformity);
            }
            catch (Exception ex)
            { logger.LogWrite(ex.Message); }
        }
        // Update Data
        public void UpdateDeformity(DEFORMITY deformity)
        {
            sqliteconnection.Update(deformity );
        }
        public void DeleteAllDeformity()
        {
            sqliteconnection.DeleteAll<DEFORMITY>();
        }
        public List<DEFORMITY> GetAllDeformityData()
        {
            return (from data in sqliteconnection.Table<DEFORMITY>().OrderBy(t => t.HEIGHTFROM)
                    select data).ToList();
        }
        public List<DEFORMITY> GetFilteredDeformityData(string id)
        {
//            return (from data in sqliteconnection.Table<DEFORMITY>().OrderBy(t => t.HEIGHTFROM).Where(t => t.TREEID == id)
 //                   select data).ToList();
            return (from data in sqliteconnection.Query<DEFORMITY>("select * from DEFORMITY where TREEID = '" + id + "' and IsDeleted = 'N' ORDER BY HEIGHTFROM")
                    select data).ToList();
        }
        public DEFORMITY  GetDeformityData(string id)
        {
            return sqliteconnection.Table<DEFORMITY >().FirstOrDefault(t => t.DEFORMITYID == id);
        }

        //DWD Helpers
        public void DeleteDWD(string id)
        {
            sqliteconnection.Delete<DWD>(id);
        }
        // Insert new to DB 
        public void InsertDWD(DWD dwd)
        {
            try
            {
                sqliteconnection.Insert(dwd);
            }
            catch (Exception ex)
            { logger.LogWrite(ex.Message); }
        }
        // Update Data
        public void UpdateDWD(DWD dwd)
        {
            sqliteconnection.Update(dwd);
        }
        public void DeleteAllDWD()
        {
            sqliteconnection.DeleteAll<DWD>();
        }
        public List<DWD> GetAllDWDData()
        {
            return (from data in sqliteconnection.Table<DWD>().OrderBy(t => t.DWDNUM)
                    select data).ToList();
        }
        public List<DWD> GetFilteredDWDData(string plotid)
        {
//            return (from data in sqliteconnection.Table<DWD>().OrderBy(t => t.LINENUMBER).OrderBy(t => t.DWDNUM ).Where(t => t.PLOTID == plotid)
  //                  select data).ToList();
            return (from data in sqliteconnection.Query<DWD>("select * from DWD where PLOTID = '" + plotid + "' and IsDeleted = 'N' ORDER BY LINENUMBER,DWDNUM")
                    select data).ToList();
        }
        public DWD GetDWDData(string id)
        {
            return sqliteconnection.Table<DWD>().FirstOrDefault(t => t.DWDID == id);
        }
        public string GetLastSynchDate()
        {
            var settings = sqliteconnection.Table<SETTINGS>().FirstOrDefault();
            return settings.LastSynched.ToString() ; 
        }
        public List<PROJECT> GetProjecttoInsert(DateTime fromdate)
        {
            return sqliteconnection.Table<PROJECT>().Where(t => t.Created > fromdate).ToList() ;
        }
        public List<PROJECT> GetProjectToUpdate(DateTime fromdate)
        {
            return sqliteconnection.Table<PROJECT>().Where(t => t.LastModified > fromdate).ToList();
        }
        public List<PROJECT> GetProjectToDelete(DateTime fromdate)
        {
            return sqliteconnection.Table<PROJECT>().Where(t => t.LastModified > fromdate).Where(t => t.IsDeleted =="Y").ToList();
        }
        public void UpdateSettings(SETTINGS settings)
        {
            sqliteconnection.Update(settings);
        }
        public SETTINGS GetSettingsData()
        {
            return sqliteconnection.Table<SETTINGS>().Where(t => t.PLOT_ROWS_SYNCHED >=0).FirstOrDefault();
        }
        public List<PLOT> GetPlottoInsert(DateTime fromdate)
        {
            return sqliteconnection.Table<PLOT>().Where(t => t.Created > fromdate).ToList();
        }
        public List<PLOT> GetPlotToUpdate(DateTime fromdate)
        {
            return sqliteconnection.Table<PLOT>().Where(t => t.LastModified > fromdate).ToList();
        }
        public List<PLOT> GetPlotToDelete(DateTime fromdate)
        {
            return sqliteconnection.Table<PLOT>().Where(t => t.LastModified > fromdate).Where(t => t.IsDeleted == "Y").ToList();
        }
        public List<TREE> GetTreetoInsert(DateTime fromdate)
        {
            return sqliteconnection.Table<TREE>().Where(t => t.Created > fromdate).ToList();
        }
        public List<TREE> GetTreeToUpdate(DateTime fromdate)
        {
            return sqliteconnection.Table<TREE>().Where(t => t.LastModified > fromdate).ToList();
        }
        public List<TREE> GetTreeToDelete(DateTime fromdate)
        {
            return sqliteconnection.Table<TREE>().Where(t => t.LastModified > fromdate).Where(t => t.IsDeleted == "Y").ToList();
        }
        public List<STEMMAP> GetStemmaptoInsert(DateTime fromdate)
        {
            return sqliteconnection.Table<STEMMAP>().Where(t => t.Created > fromdate).ToList();
        }
        public List<STEMMAP> GetStemmapToUpdate(DateTime fromdate)
        {
            return sqliteconnection.Table<STEMMAP>().Where(t => t.LastModified > fromdate).ToList();
        }
        public List<STEMMAP> GetStemmapToDelete(DateTime fromdate)
        {
            return sqliteconnection.Table<STEMMAP>().Where(t => t.LastModified > fromdate).Where(t => t.IsDeleted == "Y").ToList();
        }
        public List<ECOSITE> GetEcositetoInsert(DateTime fromdate)
        {
            return sqliteconnection.Table<ECOSITE>().Where(t => t.Created > fromdate).ToList();
        }
        public List<ECOSITE> GetEcositeToUpdate(DateTime fromdate)
        {
            return sqliteconnection.Table<ECOSITE>().Where(t => t.LastModified > fromdate).ToList();
        }
        public List<ECOSITE> GetEcositeToDelete(DateTime fromdate)
        {
            return sqliteconnection.Table<ECOSITE>().Where(t => t.LastModified > fromdate).Where(t => t.IsDeleted == "Y").ToList();
        }
        public List<SOIL> GetSoiltoInsert(DateTime fromdate)
        {
            return sqliteconnection.Table<SOIL>().Where(t => t.Created > fromdate).ToList();
        }
        public List<SOIL> GetSoilToUpdate(DateTime fromdate)
        {
            return sqliteconnection.Table<SOIL>().Where(t => t.LastModified > fromdate).ToList();
        }
        public List<SOIL> GetSoilToDelete(DateTime fromdate)
        {
            return sqliteconnection.Table<SOIL>().Where(t => t.LastModified > fromdate).Where(t => t.IsDeleted == "Y").ToList();
        }
        public List<SMALLTREE> GetSmalltreetoInsert(DateTime fromdate)
        {
            return sqliteconnection.Table<SMALLTREE>().Where(t => t.Created > fromdate).ToList();
        }
        public List<SMALLTREE> GetSmalltreeToUpdate(DateTime fromdate)
        {
            return sqliteconnection.Table<SMALLTREE>().Where(t => t.LastModified > fromdate).ToList();
        }
        public List<SMALLTREE> GetSmalltreeToDelete(DateTime fromdate)
        {
            return sqliteconnection.Table<SMALLTREE>().Where(t => t.LastModified > fromdate).Where(t => t.IsDeleted == "Y").ToList();
        }
        public List<VEGETATION> GetVegetationtoInsert(DateTime fromdate)
        {
            return sqliteconnection.Table<VEGETATION>().Where(t => t.Created > fromdate).ToList();
        }
        public List<VEGETATION> GetVegetationToUpdate(DateTime fromdate)
        {
            return sqliteconnection.Table<VEGETATION>().Where(t => t.LastModified > fromdate).ToList();
        }
        public List<VEGETATION> GetVegetationToDelete(DateTime fromdate)
        {
            return sqliteconnection.Table<VEGETATION>().Where(t => t.LastModified > fromdate).Where(t => t.IsDeleted == "Y").ToList();
        }
        public List<DEFORMITY> GetDeformitytoInsert(DateTime fromdate)
        {
            return sqliteconnection.Table<DEFORMITY>().Where(t => t.Created > fromdate).ToList();
        }
        public List<DEFORMITY> GetDeformityToUpdate(DateTime fromdate)
        {
            return sqliteconnection.Table<DEFORMITY>().Where(t => t.LastModified > fromdate).ToList();
        }
        public List<DEFORMITY> GetDeformityToDelete(DateTime fromdate)
        {
            return sqliteconnection.Table<DEFORMITY>().Where(t => t.LastModified > fromdate).Where(t => t.IsDeleted == "Y").ToList();
        }
        public List<DWD> GetDWDtoInsert(DateTime fromdate)
        {
            return sqliteconnection.Table<DWD>().Where(t => t.Created > fromdate).ToList();
        }
        public List<DWD> GetDWDToUpdate(DateTime fromdate)
        {
            return sqliteconnection.Table<DWD>().Where(t => t.LastModified > fromdate).ToList();
        }
        public List<DWD> GetDWDToDelete(DateTime fromdate)
        {
            return sqliteconnection.Table<DWD>().Where(t => t.LastModified > fromdate).Where(t => t.IsDeleted == "Y").ToList();
        }
        public List<PHOTO> GetPhototoInsert(DateTime fromdate)
        {
            return sqliteconnection.Table<PHOTO>().Where(t => t.Created > fromdate).ToList();
        }
        public List<PHOTO> GetPhotoToUpdate(DateTime fromdate)
        {
            return sqliteconnection.Table<PHOTO>().Where(t => t.LastModified > fromdate).ToList();
        }
        public List<PHOTO> GetPhotoToDelete(DateTime fromdate)
        {
            return sqliteconnection.Table<PHOTO>().Where(t => t.LastModified > fromdate).Where(t => t.IsDeleted == "Y").ToList();
        }
        public List<PERSON> GetPersontoInsert(DateTime fromdate)
        {
            return sqliteconnection.Table<PERSON>().Where(t => t.Created > fromdate).ToList();
        }
        public List<PERSON> GetPersonToUpdate(DateTime fromdate)
        {
            return sqliteconnection.Table<PERSON>().Where(t => t.LastModified > fromdate).ToList();
        }
        public List<PERSON> GetPersonToDelete(DateTime fromdate)
        {
            return sqliteconnection.Table<PERSON>().Where(t => t.LastModified > fromdate).Where(t => t.IsDeleted == "Y").ToList();
        }
        public List<VEGETATIONCENSUS> GetVegetationCensustoInsert(DateTime fromdate)
        {
            return sqliteconnection.Table<VEGETATIONCENSUS>().Where(t => t.Created > fromdate).ToList();
        }
        public List<VEGETATIONCENSUS> GetVegetationCensusToUpdate(DateTime fromdate)
        {
            return sqliteconnection.Table<VEGETATIONCENSUS>().Where(t => t.LastModified > fromdate).ToList();
        }
        public List<VEGETATIONCENSUS> GetVegetationCensusToDelete(DateTime fromdate)
        {
            return sqliteconnection.Table<VEGETATIONCENSUS>().Where(t => t.LastModified > fromdate).Where(t => t.IsDeleted == "Y").ToList();
        }
    }


}