using System;
using System.Collections.Generic;
using eLiDAR.Models;
using eLiDAR.Helpers;
using System.Threading.Tasks;

namespace eLiDAR.Services
{

    public interface IPersonRepository
    {
        List<PERSON> GetAllPersonData();
        //Get Specific data
        PERSON GetPersonData(string PERSONID);
        // Delete all Data
        void DeleteAllPersons();
        // Delete Specific
        void DeletePerson(PERSON _person);
        // Insert new to DB 
        void InsertPerson(PERSON person);
        // Update Data
        void UpdatePerson(PERSON person);
        String GetProjectTitle(string projectid);
        List<PERSON> GetFilteredData(string id);
        
    }
    public interface IPhotoRepository
    {
    
        PHOTO GetPhotoData(string PHOTOID);
        // Delete all Data
   
        void DeletePhoto(PHOTO _photo);
        // Insert new to DB 
        void InsertPhoto(PHOTO _photo);
        // Update Data
        void UpdatePhoto(PHOTO _photo);
        String GetPlotTitle(string plotid);
        List<PHOTO> GetFilteredData(string id);
        bool IsPhotoTableEmpty(string plotid);
    }
    public interface IProjectRepository
    {
        List<PROJECT> GetAllProjectData();
        //Get Specific data
        PROJECT GetProjectData(string PROJECTID);
        // Delete all Data
        void DeleteAllProjects();
        // Delete Specific
        void DeleteProject(PROJECT table);
        // Insert new to DB 
        void InsertProject(PROJECT project);
        // Update Data
        void UpdateProject(PROJECT project);
    }
    public interface IPlotRepository
    {
        List<PLOT> GetAllData();
        List<PLOT> GetFilteredData(string PROJECTID);
        List<PLOTLIST> GetFilteredDataFull(string PROJECTID);
        //Get Specific data
        PLOT GetPlotData(string PLOTID);
        // Delete all Data
        void DeleteAllPlots();
        // Delete Specific
        void DeletePlot(PLOT table);
        // Insert new to DB 
        void InsertPlot (PLOT plot, string fk);
        // Update Data
        void UpdatePlot(PLOT plot);

        String GetProjectTitle(string projectid);
        List<PROJECT> GetProjectList();
        String GetPlotType(string plotid);
        List<PERSON> GetPersonList(string projectid);
        bool IsUniquePlot(PLOT _plot);
        bool AllowDelete(PLOT _plot);
        int TreeErrorCount(string _plotid);
    }
    public interface ITreeRepository
    {
        List<TREE> GetAllData();
        List<TREE> GetFilteredData(string fk);
        List<TREELIST> GetFilteredDataFull(string fk);

        List<TREE> GetFilteredTreeStemData(string fk);
        List<TREELIST> GetFilteredTreeStemDataFull(string fk, bool sort = false);

        //Get Specific data
        TREE GetTreeData(string TREEID);
        // Delete all Data
        void DeleteAllTrees();
        // Delete Specific
        void DeleteTree(TREE table);
        // Insert new to DB 
        void InsertTree(TREE tree, string fk);
        // Update Data
        void UpdateTree(TREE tree);
        String GetPlotTitle(string plotid);
        int GetAzimuth(string treeid);
        double GetDistance(string treeid);

        String GetPlotType(string plotid);
        bool AllowDelete(TREE _tree);
        int GetNextNumber(string plotid);

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
        void DeleteTree(STEMMAP table);
        // Insert new to DB 
        void InsertTree(STEMMAP stemmap, string fk);
        // Update Data
        void UpdateTree(STEMMAP stemmap);
        String GetTitle(string treeid);
        bool IsStemMapExists(string treeid);
        bool IsRequiresCrownWidth(string treeid);
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
        void DeleteEcosite(ECOSITE table);
        // Insert new to DB 
        void InsertEcosite(ECOSITE ecosite, string fk);
        // Update Data
        void UpdateEcosite(ECOSITE ecosite);
        String GetTitle(string plotid);
        bool IsEcositeExists(string plotid);
        List<PERSON> GetPersonList(string id = null);
        
    }
    public interface ISoilRepository
    {
        List<SOIL> GetAllData();
        List<SOIL> GetFilteredData(string fk);

        //Get Specific data
        SOIL GetSoilData(string SOILID);
        // Delete all Data
        void DeleteAllSoil();
        // Delete Specific
        void DeleteSoil(SOIL table);
        // Insert new to DB 
        void InsertSoil(SOIL soil, string fk);
        // Update Data
        void UpdateSoil(SOIL soil);
        String GetTitle(string plotid);
        int GetNextNumber(string plotid);

    }
    public interface ISmallTreeRepository
    {
        List<SMALLTREE> GetAllData();
        List<SMALLTREE> GetFilteredData(string fk);
        List<SMALLTREELIST> GetFilteredDataFull(string fk);
        //Get Specific data
        SMALLTREE GetSmallTreeData(string SMALLTREEID);
        // Delete all Data
        void DeleteAllSmallTree();
        // Delete Specific
        void DeleteSmallTree(SMALLTREE table);
        // Insert new to DB 
        void InsertSmallTree(SMALLTREE smalltree, string fk);
        // Update Data
        void UpdateSmallTree(SMALLTREE smalltree);
        String GetTitle(string plotid);
    }

    public interface ISmallTreeTallyRepository
    {
        List<SMALLTREETALLY> GetAllData();
        List<SMALLTREETALLY> GetFilteredData(string fk);
        List<SMALLTREETALLYLIST> GetFilteredDataFull(string fk); 
        //Get Specific data
        SMALLTREETALLY GetSmallTreeTallyData(string SMALLTREETALLYID);
        // Delete all Data
        void DeleteAllSmallTreeTally();
        // Delete Specific
        void DeleteSmallTreeTally(SMALLTREETALLY table);
        // Insert new to DB 
        void InsertSmallTreeTally(SMALLTREETALLY smalltreetally, string fk);
        // Update Data
        void UpdateSmallTreeTally(SMALLTREETALLY smalltreetally);
        String GetTitle(string plotid);
    }

    public interface IVegetationRepository
    {
        List<VEGETATION> GetAllData();
        List<VEGETATION> GetFilteredData(string fk);

        //Get Specific data
        VEGETATION GetVegetationData(string VEGETATIONID);
        // Delete all Data
        void DeleteAllVegetation();
        // Delete Specific
        void DeleteVegetation(VEGETATION table);
        // Insert new to DB 
        void InsertVegetation(VEGETATION vegetation, string fk);
        // Update Data
        void UpdateVegetation(VEGETATION vegetation);
        String GetTitle(string plotid);
    }
    public interface IVegetationCensusRepository
    {
        List<VEGETATIONCENSUS> GetAllData();
        List<VEGETATIONCENSUS> GetFilteredData(string fk);

        //Get Specific data
        VEGETATIONCENSUS GetVegetationData(string VEGETATIONCENSUSID);
        // Delete all Data
        void DeleteAllVegetation();
        // Delete Specific
        void DeleteVegetation(VEGETATIONCENSUS table);
        // Insert new to DB 
        void InsertVegetation(VEGETATIONCENSUS vegetation, string fk);
        // Update Data
        void UpdateVegetation(VEGETATIONCENSUS vegetation);
        String GetTitle(string plotid);
    }
    public interface IDeformityRepository
    {
        List<DEFORMITY> GetAllData();
        List<DEFORMITY> GetFilteredData(string fk);

        //Get Specific data
        DEFORMITY GetDeformityData(string DEFORMITYID);
        // Delete all Data
        void DeleteAllDeformity();
        // Delete Specific
        void DeleteDeformity(DEFORMITY table);
        // Insert new to DB 
        void InsertDeformity(DEFORMITY deformity, string fk);
        // Update Data
        void UpdateDeformity(DEFORMITY deformity);
        String GetTitle(string plotid);
    }
    public interface IDWDRepository
    {
        List<DWD> GetAllData();
        List<DWD> GetFilteredData(string fk);

        //Get Specific data
        DWD GetDWDData(string DWDID);
        // Delete all Data
        void DeleteAllDWD();
        // Delete Specific
        void DeleteDWD(DWD table);
        // Insert new to DB 
        void InsertDWD(DWD dwd, string fk);
        // Update Data
        void UpdateDWD(DWD dwd);
        String GetTitle(string plotid);
        int GetNextNumber(string plotid);
    }


    public class ProjectRepository : IProjectRepository
    {
        DatabaseHelper _databaseHelper;
        public ProjectRepository()
        {
            _databaseHelper = new DatabaseHelper();
        }
        public void DeleteProject(PROJECT _table)
        {
            //            _databaseHelper.DeleteProject(ID);
            _table.LastModified = System.DateTime.UtcNow;
            _table.IsDeleted = "Y";
            _databaseHelper.UpdateProject(_table);
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
    public class PersonRepository : IPersonRepository
    {
        DatabaseHelper _databaseHelper;
        public PersonRepository()
        {
            _databaseHelper = new DatabaseHelper();
        }
        public void DeletePerson(PERSON _person)
        {
            //            _databaseHelper.DeletePerson(ID);
            _person.LastModified = System.DateTime.UtcNow;
            _person.IsDeleted = "Y";
            _databaseHelper.UpdatePerson(_person);
        }
        public void DeleteAllPersons()
        {
            _databaseHelper.DeleteAllPersons();
        }

        public List<PERSON> GetAllPersonData()
        {
            return _databaseHelper.GetAllPersonData();
        }

        public PERSON GetPersonData(string PERSONID)
        {
            return _databaseHelper.GetPersonData(PERSONID);
        }

        public void InsertPerson(PERSON person)
        {
            person.PERSONID = Guid.NewGuid().ToString();

            _databaseHelper.InsertPerson(person);
        }
        public void UpdatePerson(PERSON person)
        {
            _databaseHelper.UpdatePerson(person);
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
        public List<PERSON> GetFilteredData(string id)
        {
            return _databaseHelper.GetFilteredPersonData(id);
        }
    }
    public class PhotoRepository : IPhotoRepository
    {
        DatabaseHelper _databaseHelper;
        public PhotoRepository()
        {
            _databaseHelper = new DatabaseHelper();
        }
        public void DeletePhoto(PHOTO _photo)
        {
            _photo.IsDeleted = "Y";
            _photo.LastModified = System.DateTime.UtcNow;
            _databaseHelper.UpdatePhoto(_photo);
            _databaseHelper.SetPlotSynch(_photo.PLOTID);
        }
        
        public PHOTO GetPhotoData(string PHOTOID)
        {
            return _databaseHelper.GetPhotoData(PHOTOID);
        }

        public void InsertPhoto(PHOTO photo)
        {
            photo.PHOTOID = Guid.NewGuid().ToString();

            _databaseHelper.InsertPhoto(photo);
            _databaseHelper.SetPlotSynch(photo.PLOTID);
        }
        public void UpdatePhoto(PHOTO photo)
        {
            _databaseHelper.UpdatePhoto(photo);
            _databaseHelper.SetPlotSynch(photo.PLOTID);
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
        public List<PHOTO> GetFilteredData(string id)
        {
            return _databaseHelper.GetFilteredPhotoData(id);
        }
        public bool IsPhotoTableEmpty(string plotid)
        {
            return _databaseHelper.IsPhotoTableEmpty(plotid);  
        }
    }
    public class PlotRepository : IPlotRepository
    {
        DatabaseHelper _databaseHelper;
        public PlotRepository()
        {
            _databaseHelper = new DatabaseHelper();
        }
        public void DeletePlot(PLOT _table)
        {
            //          _databaseHelper.DeletePlot(ID);
            _table.LastModified = System.DateTime.UtcNow;
            _table.IsDeleted = "Y";
            _databaseHelper.UpdatePlot(_table);
        }
        public void DeleteAllPlots()
        {
            _databaseHelper.DeleteAllPlots();
        }

        public List<PLOT> GetAllData()
        {
            return _databaseHelper.GetAllPlotData();
        }
        public List<PROJECT> GetProjectList()
        {
            return _databaseHelper.GetAllProjectData();
        }
        public List<PLOT> GetFilteredData(string id)
        {
            return _databaseHelper.GetFilteredPlotData(id);
        }
        public List<PLOTLIST> GetFilteredDataFull(string id)
        {
            return _databaseHelper.GetFilteredPlotDataFull(id);
        }
        public List<PERSON> GetPersonList(string id = null)
        {
            return _databaseHelper.GetFilteredPersonData(id);
        }
        public PLOT GetPlotData(string PlotID)
        {
            return _databaseHelper.GetPlotData(PlotID);
        }

        public void InsertPlot(PLOT Plot, string fk)
        {
            Plot.PLOTID = Guid.NewGuid().ToString();
            Plot.PROJECTID = fk;
            Plot.SynchRequired = System.DateTime.UtcNow;
            _databaseHelper.InsertPlot(Plot);
        }

        public void UpdatePlot(PLOT Plot)
        {
            Plot.SynchRequired = System.DateTime.UtcNow;
            _databaseHelper.UpdatePlot(Plot);
            
        }
        public bool AllowDelete(PLOT _plot)
        {
            //to check if there are related records and prevent deletion
            if (_databaseHelper.GetFilteredTreeData(_plot.PLOTID).Count > 0) { return false; }
            else if (_databaseHelper.GetFilteredSmallTreeData(_plot.PLOTID).Count > 0) { return false; }
            else if (_databaseHelper.GetFilteredSmallTreeTallyData(_plot.PLOTID).Count > 0) { return false; }
            else if (_databaseHelper.GetFilteredPhotoData(_plot.PLOTID).Count > 0) { return false; }
            else if (_databaseHelper.GetFilteredDWDData(_plot.PLOTID).Count > 0) { return false; }
            else if (_databaseHelper.GetFilteredEcositeData(_plot.PLOTID).Count > 0) { return false; }
            else if (_databaseHelper.GetFilteredSoilData(_plot.PLOTID).Count > 0) { return false; }
            else if (_databaseHelper.GetFilteredVegetationData(_plot.PLOTID).Count > 0) { return false; }
            else if (_databaseHelper.GetFilteredVegetationCensusData(_plot.PLOTID).Count > 0) { return false; }
            else { return true; }
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
        public String GetPlotType(string plotid)
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

        public bool IsUniquePlot(PLOT _plot)
        {
            return _databaseHelper.IsPlotNumUnique(_plot);  
        }

        public int TreeErrorCount(string _plotid)
        {
            return _databaseHelper.TreeErrorCount(_plotid); 
        }


    }

    public class TreeRepository : ITreeRepository
    {
        DatabaseHelper _databaseHelper;
        public TreeRepository()
        {
            _databaseHelper = new DatabaseHelper();
        }
        public void DeleteTree(TREE _table)
        {
            //           _databaseHelper.DeleteTree(ID);
            _table.LastModified = System.DateTime.UtcNow;  
            _table.IsDeleted = "Y";
            _databaseHelper.UpdateTree(_table);
            _databaseHelper.SetPlotSynch(_table.PLOTID);

        }
        public int GetNextNumber(string plotid)
        {
           return _databaseHelper.GetNextTreeNumber(plotid);
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
        public List<TREELIST> GetFilteredDataFull(string fk)
        {
            return _databaseHelper.GetFilteredTreeDataFull(fk);
        }

        public List<TREE> GetFilteredTreeStemData(string fk)
        {
            return _databaseHelper.GetFilteredTreeStemList(fk);
        }
        public List<TREELIST> GetFilteredTreeStemDataFull(string fk, bool sort = false)
        {
            return _databaseHelper.GetFilteredTreeStemListFull(fk, sort);
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
            _databaseHelper.SetPlotSynch(Tree.PLOTID );
        }
        public bool AllowDelete(TREE _tree)
        {
         //to check if there are related records and prevent deletion
            if (_databaseHelper.GetFilteredStemmapData(_tree.TREEID).Count > 0) { return false; }
            else if (_databaseHelper.GetFilteredDeformityData(_tree.TREEID).Count > 0) { return false; }
            else { return true; }
        }

        public void UpdateTree(TREE Tree)
        {
            _databaseHelper.UpdateTree(Tree);
            _databaseHelper.SetPlotSynch(Tree.PLOTID );
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
        public String GetPlotType(string plotid)
        {
            if (plotid == "")
            {
                return "";
            }
            else
            {
                return _databaseHelper.GetPlotType(plotid);
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
        public void DeleteTree(STEMMAP _table)
        {
//            _databaseHelper.DeleteStemmap(ID);
            _table.IsDeleted = "Y";
            _table.LastModified = System.DateTime.UtcNow;
            _databaseHelper.UpdateStemmap(_table);
            _databaseHelper.SetPlotSynch(null,_table.TREEID);

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
            _databaseHelper.SetPlotSynch(null, Stemmap.TREEID);
        }

        public void UpdateTree(STEMMAP Stemmap)
        {
            _databaseHelper.UpdateStemmap(Stemmap);
            _databaseHelper.SetPlotSynch(null, Stemmap.TREEID);
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
        public bool IsRequiresCrownWidth(string treeid) {
            return _databaseHelper.RequiresCrownWidth(treeid);
        }

    }

    public class EcositeRepository : IEcositeRepository
    {
        DatabaseHelper _databaseHelper;
        public EcositeRepository()
        {
            _databaseHelper = new DatabaseHelper();
        }
        public void DeleteEcosite(ECOSITE _table)
        {
//            _databaseHelper.DeleteEcosite(ID);
            _table.IsDeleted = "Y";
            _databaseHelper.UpdateEcosite(_table);
            _databaseHelper.SetPlotSynch(_table.PLOTID);

        }
        public void DeleteAllEcosites()
        {
            _databaseHelper.DeleteAllEcosites();
        }

        public List<ECOSITE> GetAllData()
        {
            return _databaseHelper.GetAllEcositeData();
        }
        public List<PERSON> GetPersonList(string id = null)
        {
            return _databaseHelper.GetFilteredPersonData(id);
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
            _databaseHelper.SetPlotSynch(ecosite.PLOTID);
        }

        public void UpdateEcosite(ECOSITE ecosite)
        {
            _databaseHelper.UpdateEcosite(ecosite);
            _databaseHelper.SetPlotSynch(ecosite.PLOTID);
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

    public class SoilRepository : ISoilRepository
    {
        DatabaseHelper _databaseHelper;
        public SoilRepository()
        {
            _databaseHelper = new DatabaseHelper();
        }
        public int GetNextNumber(string plotid)
        {
            return _databaseHelper.GetNextSoilNumber(plotid);
        }
        public void DeleteSoil(SOIL _table)
        {
            _table.IsDeleted = "Y";
            _table.LastModified = System.DateTime.UtcNow;
            _databaseHelper.UpdateSoil(_table);
            _databaseHelper.SetPlotSynch(_table.PLOTID);

        }
        public void DeleteAllSoil()
        {
            _databaseHelper.DeleteAllSoil();
        }

        public List<SOIL> GetAllData()
        {
            return _databaseHelper.GetAllSoilData();
        }

        public List<SOIL> GetFilteredData(string fk)
        {
            return _databaseHelper.GetFilteredSoilData(fk);
        }

        public SOIL GetSoilData(string SoilID)
        {
            return _databaseHelper.GetSoilData(SoilID);
        }

        public void InsertSoil(SOIL soil, string fk)
        {
            soil.SOILID  = Guid.NewGuid().ToString();
            soil.PLOTID = fk;
            _databaseHelper.InsertSoil(soil);
            _databaseHelper.SetPlotSynch(soil.PLOTID);
        }

        public void UpdateSoil(SOIL soil)
        {
            _databaseHelper.UpdateSoil(soil);
            _databaseHelper.SetPlotSynch(soil.PLOTID);
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
    }

    public class SmallTreeRepository : ISmallTreeRepository
    {
        DatabaseHelper _databaseHelper;
        public SmallTreeRepository()
        {
            _databaseHelper = new DatabaseHelper();
        }
        public void DeleteSmallTree(SMALLTREE _table)
        {
            //            _databaseHelper.DeleteSmallTree(ID);
            _table.LastModified = System.DateTime.UtcNow;
            _table.IsDeleted = "Y";
            _databaseHelper.UpdateSmallTree(_table);
            _databaseHelper.SetPlotSynch(_table.PLOTID);
        }
        public void DeleteAllSmallTree()
        {
            _databaseHelper.DeleteAllSmallTree();
        }

        public List<SMALLTREE> GetAllData()
        {
            return _databaseHelper.GetAllSmallTreeData();
        }
        public List<SMALLTREELIST> GetFilteredDataFull(string fk)
        {
            return _databaseHelper.GetFilteredSmallTreeDataFull(fk);
        }
        public List<SMALLTREE> GetFilteredData(string fk)
        {
            return _databaseHelper.GetFilteredSmallTreeData(fk);
        }

        public SMALLTREE GetSmallTreeData(string SmallTreeID)
        {
            return _databaseHelper.GetSmallTreeData(SmallTreeID);
        }

        public void InsertSmallTree(SMALLTREE smalltree, string fk)
        {
            smalltree.SMALLTREEID = Guid.NewGuid().ToString();
            smalltree.PLOTID = fk;
            _databaseHelper.InsertSmallTree(smalltree);
            _databaseHelper.SetPlotSynch(smalltree.PLOTID);
        }

        public void UpdateSmallTree(SMALLTREE smalltree)
        {
            _databaseHelper.UpdateSmallTree(smalltree);
            _databaseHelper.SetPlotSynch(smalltree.PLOTID);
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
    }

    public class SmallTreeTallyRepository : ISmallTreeTallyRepository
    {
        DatabaseHelper _databaseHelper;
        public SmallTreeTallyRepository()
        {
            _databaseHelper = new DatabaseHelper();
        }
        public void DeleteSmallTreeTally(SMALLTREETALLY _table)
        {

            _table.LastModified = System.DateTime.UtcNow;
            _table.IsDeleted = "Y";
            _databaseHelper.UpdateSmallTreeTally(_table);
            _databaseHelper.SetPlotSynch(_table.PLOTID);
        }
        public void DeleteAllSmallTreeTally()
        {
            _databaseHelper.DeleteAllSmallTreeTally();
        }

        public List<SMALLTREETALLY> GetAllData()
        {
            return _databaseHelper.GetAllSmallTreeTallyData();
        }
        public List<SMALLTREETALLYLIST> GetFilteredDataFull(string fk)
        {
            return _databaseHelper.GetFilteredSmallTreeTallyDataFull(fk);
        }
        public List<SMALLTREETALLY> GetFilteredData(string fk)
        {
            return _databaseHelper.GetFilteredSmallTreeTallyData(fk);
        }

        public SMALLTREETALLY GetSmallTreeTallyData(string SmallTreeTallyID)
        {
            return _databaseHelper.GetSmallTreeTallyData(SmallTreeTallyID);
        }

        public void InsertSmallTreeTally(SMALLTREETALLY smalltreetally, string fk)
        {
            smalltreetally.SMALLTREETALLYID = Guid.NewGuid().ToString();
            smalltreetally.PLOTID = fk;
            _databaseHelper.InsertSmallTreeTally(smalltreetally);
            _databaseHelper.SetPlotSynch(smalltreetally.PLOTID);
        }

        public void UpdateSmallTreeTally(SMALLTREETALLY smalltreetally)
        {
            _databaseHelper.UpdateSmallTreeTally(smalltreetally);
            _databaseHelper.SetPlotSynch(smalltreetally.PLOTID);
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

    }





    public class VegetationRepository : IVegetationRepository
    {
        DatabaseHelper _databaseHelper;
        public VegetationRepository()
        {
            _databaseHelper = new DatabaseHelper();
        }
        public void DeleteVegetation(VEGETATION _table)
        {
            _table.IsDeleted = "Y";
            _table.LastModified = System.DateTime.UtcNow;
            _databaseHelper.UpdateVegetation(_table);
            _databaseHelper.SetPlotSynch(_table.PLOTID);

            //        _databaseHelper.DeleteVegetation(ID);
        }
        public void DeleteAllVegetation()
        {
            _databaseHelper.DeleteAllVegetation();
        }
        public List<VEGETATION> GetAllData()
        {
            return _databaseHelper.GetAllVegetationData();
        }

        public List<VEGETATION> GetFilteredData(string fk)
        {
            return _databaseHelper.GetFilteredVegetationData(fk);
        }

        public VEGETATION GetVegetationData(string VegetationID)
        {
            return _databaseHelper.GetVegetationData(VegetationID);
        }

        public void InsertVegetation(VEGETATION vegetation, string fk)
        {
            vegetation.VEGETATIONID = Guid.NewGuid().ToString();
            vegetation.PLOTID = fk;
            _databaseHelper.InsertVegetation(vegetation);
            _databaseHelper.SetPlotSynch(vegetation.PLOTID);
        }

        public void UpdateVegetation(VEGETATION vegetation)
        {
            _databaseHelper.UpdateVegetation(vegetation);
            _databaseHelper.SetPlotSynch(vegetation.PLOTID);
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

    }
    public class VegetationCensusRepository : IVegetationCensusRepository
    {
        DatabaseHelper _databaseHelper;
        public VegetationCensusRepository()
        {
            _databaseHelper = new DatabaseHelper();
        }
        public void DeleteVegetation(VEGETATIONCENSUS _table)
        {
//            _databaseHelper.DeleteVegetationCensus(ID);
            _table.IsDeleted = "Y";
            _table.LastModified = System.DateTime.UtcNow;
            _databaseHelper.UpdateVegetation(_table);
            _databaseHelper.SetPlotSynch(_table.PLOTID);

        }
        public void DeleteAllVegetation()
        {
            _databaseHelper.DeleteAllVegetationCensus();
        }
        public List<VEGETATIONCENSUS> GetAllData()
        {
            return _databaseHelper.GetAllVegetationCensusData();
        }

        public List<VEGETATIONCENSUS> GetFilteredData(string fk)
        {
            return _databaseHelper.GetFilteredVegetationCensusData(fk);
        }

        public VEGETATIONCENSUS GetVegetationData(string VegetationID)
        {
            return _databaseHelper.GetVegetationCensusData(VegetationID);
        }

        public void InsertVegetation(VEGETATIONCENSUS vegetationcensus, string fk)
        {
            vegetationcensus.VEGETATIONCENSUSID = Guid.NewGuid().ToString();
            vegetationcensus.PLOTID = fk;
            _databaseHelper.InsertVegetationCensus(vegetationcensus);
            _databaseHelper.SetPlotSynch(vegetationcensus.PLOTID);
        }

        public void UpdateVegetation(VEGETATIONCENSUS vegetation)
        {
            _databaseHelper.UpdateVegetation(vegetation);
            _databaseHelper.SetPlotSynch(vegetation.PLOTID);
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

    }
    public class DeformityRepository : IDeformityRepository
    {
        DatabaseHelper _databaseHelper;
        public DeformityRepository()
        {
            _databaseHelper = new DatabaseHelper();
        }
        public void DeleteDeformity(DEFORMITY _table)
        {
 //           _databaseHelper.DeleteDeformity(ID);
            _table.IsDeleted = "Y";
            _databaseHelper.UpdateDeformity(_table);
            _databaseHelper.SetPlotSynch(null, _table.TREEID);
        }
        public void DeleteAllDeformity()
        {
            _databaseHelper.DeleteAllDeformity();
        }
        public List<DEFORMITY> GetAllData()
        {
            return _databaseHelper.GetAllDeformityData();
        }

        public List<DEFORMITY> GetFilteredData(string fk)
        {
            return _databaseHelper.GetFilteredDeformityData(fk);
        }

        public DEFORMITY GetDeformityData(string DeformityID)
        {
            return _databaseHelper.GetDeformityData(DeformityID);
        }

        public void InsertDeformity(DEFORMITY deformity, string fk)
        {
            deformity.DEFORMITYID  = Guid.NewGuid().ToString();
            deformity.TREEID  = fk;
            _databaseHelper.InsertDeformity(deformity);
            _databaseHelper.SetPlotSynch(null, deformity.TREEID);
        }

        public void UpdateDeformity(DEFORMITY deformity)
        {
            _databaseHelper.UpdateDeformity(deformity);
            _databaseHelper.SetPlotSynch(null, deformity.TREEID);
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

    }

    public class DWDRepository : IDWDRepository
    {
        DatabaseHelper _databaseHelper;
        public DWDRepository()
        {
            _databaseHelper = new DatabaseHelper();
        }
        public int GetNextNumber(string plotid)
        {
            return _databaseHelper.GetNextDWDNumber(plotid);
        }
        public void DeleteDWD(DWD _table)
        {
//            _databaseHelper.DeleteDWD(ID);
            _table.IsDeleted = "Y";
            _databaseHelper.UpdateDWD(_table);
            _databaseHelper.SetPlotSynch(_table.PLOTID );
        }
        public void DeleteAllDWD()
        {
            _databaseHelper.DeleteAllDWD();
        }
        public List<DWD> GetAllData()
        {
            return _databaseHelper.GetAllDWDData();
        }

        public List<DWD> GetFilteredData(string fk)
        {
            return _databaseHelper.GetFilteredDWDData(fk);
        }

        public DWD GetDWDData(string DWDID)
        {
            return _databaseHelper.GetDWDData(DWDID);
        }

        public void InsertDWD(DWD dwd, string fk)
        {
            dwd.DWDID = Guid.NewGuid().ToString();
            dwd.PLOTID = fk;
            _databaseHelper.InsertDWD(dwd);
            _databaseHelper.SetPlotSynch(dwd.PLOTID);
        }

        public void UpdateDWD(DWD dwd)
        {
            _databaseHelper.UpdateDWD(dwd);
            _databaseHelper.SetPlotSynch(dwd.PLOTID);
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

    }


}
