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
        public string GetPlotType(String id)
        {
            var plot = sqliteconnection.Query<PLOT>("select PLOT_TYPE from Plot where PLOTID = '" + id + "'").FirstOrDefault();
            return plot.PLOT_TYPE;
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
            try
            {
                sqliteconnection.Insert(project);
            }
            catch (Exception ex) 
                { logger.LogWrite(ex.Message);   }

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
        public List<PLOTLIST> GetFilteredPlotDataFull(string projectid)
        {
            return (from data in sqliteconnection.Query<PLOTLIST>("Select * from PLOT").OrderBy(t => t.PLOTNUM).Where(t => t.PROJECTID == projectid)
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
            return (from data in sqliteconnection.Table<TREE>().OrderBy(t => t.TREENUM)
                    select data).ToList();
        }
        
        public List<TREE> GetFilteredTreeData(string plotid)
        {
            return (from data in sqliteconnection.Table<TREE>().OrderBy(t => t.TREENUM).Where(t => t.PLOTID == plotid)
                    select data).ToList();
        }
        public List<TREELIST> GetFilteredTreeDataFull(string plotid)
        {
            return (from data in sqliteconnection.Query<TREELIST>("select TREE.*, PLOT_TYPE from TREE INNER JOIN PLOT ON TREE.PLOTID = PLOT.PLOTID").OrderBy(t => t.TREENUM).Where(t => t.PLOTID == plotid)
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
        public List<TREELIST> GetFilteredTreeStemListFull(string plotid)
        {
            string qry = "select PLOT_TYPE, TREE.TREEID, TREE.PLOTID, TREENUM, SPECIES, TREE.ORIGIN, STATUS, DBH, HT, STEMMAP.AZIMUTH, STEMMAP.DISTANCE from TREE INNER JOIN PLOT ON TREE.PLOTID = PLOT.PLOTID LEFT JOIN STEMMAP ON TREE.TREEID = STEMMAP.TREEID where TREE.PLOTID = '" + plotid + "' ORDER BY TREENUM";
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
                qry = "select count(TREEID) from TREE where PLOTID = '" + _tree.PLOTID + "' and TREENUM = " + _tree.TREENUM + " and TREEID <> '" + _tree.TREEID + "'";
            }
            else
            {
                qry = "select count(TREEID) from TREE where PLOTID = '" + _tree.PLOTID + "' and TREENUM = " + _tree.TREENUM ;
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
        public bool IsPlotNumUnique(PLOT _plot)
        {
            // for examinginif a unique tree number is being saved
            string qry;
            if (_plot.PLOTID != null)
            {
                qry = "select count(PLOTID) from PLOT where MEASUREMENT_TYPE = '" + _plot.MEASUREMENT_TYPE +"' and PROJECTID = '" + _plot.PROJECTID + "' and PLOTNUM = '" + _plot.PLOTNUM + "' and PLOTID <> '" + _plot.PLOTID + "'";
            }
            else
            {
                qry = "select count(PLOTID) from PLOT where MEASUREMENT_TYPE = '" + _plot.MEASUREMENT_TYPE + "' and PROJECTID = '" + _plot.PROJECTID + "' and PLOTNUM = '" + _plot.PLOTNUM + "'";
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
                qry = "select count(DWDID) from DWD where PLOTID = '" + _dwd.PLOTID + "' and DWDNUM = '" + _dwd.DWDNUM + "' and LINE = " + _dwd.LINE + " and PLOTID <> '" + _dwd.PLOTID + "'";
            }
            else
            {
                qry = "select count(DWDID) from DWD where PLOTID = '" + _dwd.PLOTID + "' and DWDNUM = '" + _dwd.DWDNUM + "' and LINE = " + _dwd.LINE;
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
                qry = "select count(SOILID) from SOIL where PLOTID = '" + _soil.PLOTID + "' and LAYER = " + _soil.LAYER + " and PLOTID <> '" + _soil.PLOTID + "'";
            }
            else
            {
                qry = "select count(SOILID) from SOIL where PLOTID = '" + _soil.PLOTID + "' and LAYER = " + _soil.LAYER;
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
            return (from data in sqliteconnection.Table<SOIL>().OrderBy(t => t.FROM)
                    select data).ToList();
        }
        public List<SOIL> GetFilteredSoilData(string plotid)
        {
            return (from data in sqliteconnection.Table<SOIL>().OrderBy(t => t.LAYER).Where(t => t.PLOTID == plotid)
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

        // Vegetation Helpers
        public void DeleteVegetation(string id)
        {
            sqliteconnection.Delete<VEGETATION>(id);
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
        public void DeleteAllVegetation()
        {
            sqliteconnection.DeleteAll<VEGETATION>();
        }
        public List<VEGETATION> GetAllVegetationData()
        {
            return (from data in sqliteconnection.Table<VEGETATION>().OrderBy(t => t.SPECIES)
                    select data).ToList();
        }
        public List<VEGETATION> GetFilteredVegetationData(string plotid)
        {
            return (from data in sqliteconnection.Table<VEGETATION>().OrderBy(t => t.SPECIES).Where(t => t.PLOTID == plotid)
                    select data).ToList();
        }
        public VEGETATION GetVegetationData(string id)
        {
            return sqliteconnection.Table<VEGETATION>().FirstOrDefault(t => t.VEGETATIONID == id);
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
            return (from data in sqliteconnection.Table<DEFORMITY>().OrderBy(t => t.HT_FROM)
                    select data).ToList();
        }
        public List<DEFORMITY> GetFilteredDeformityData(string id)
        {
            return (from data in sqliteconnection.Table<DEFORMITY>().OrderBy(t => t.HT_FROM ).Where(t => t.TREEID == id)
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
            return (from data in sqliteconnection.Table<DWD>().OrderBy(t => t.LINE).OrderBy(t => t.DWDNUM ).Where(t => t.PLOTID == plotid)
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
    }


}