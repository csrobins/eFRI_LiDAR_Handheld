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
        List<PLOTLIST> GetFilteredDataFull(string PROJECTID);
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
        List<PROJECT> GetProjectList();
        String GetPlotType(string plotid);
        List<PERSON> GetPersonList(string projectid);

    }
    public interface ITreeRepository
    {
        List<TREE> GetAllData();
        List<TREE> GetFilteredData(string fk);
        List<TREELIST> GetFilteredDataFull(string fk);

        List<TREE> GetFilteredTreeStemData(string fk);
        List<TREELIST> GetFilteredTreeStemDataFull(string fk);

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

        String GetPlotType(string plotid);

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
    public interface ISoilRepository
    {
        List<SOIL> GetAllData();
        List<SOIL> GetFilteredData(string fk);

        //Get Specific data
        SOIL GetSoilData(string SOILID);
        // Delete all Data
        void DeleteAllSoil();
        // Delete Specific
        void DeleteSoil(string SOILID);
        // Insert new to DB 
        void InsertSoil(SOIL soil, string fk);
        // Update Data
        void UpdateSoil(SOIL soil);
        String GetTitle(string plotid);
    

    }
    public interface ISmallTreeRepository
    {
        List<SMALLTREE> GetAllData();
        List<SMALLTREE> GetFilteredData(string fk);

        //Get Specific data
        SMALLTREE GetSmallTreeData(string SMALLTREEID);
        // Delete all Data
        void DeleteAllSmallTree();
        // Delete Specific
        void DeleteSmallTree(string SMALLTREEID);
        // Insert new to DB 
        void InsertSmallTree(SMALLTREE smalltree, string fk);
        // Update Data
        void UpdateSmallTree(SMALLTREE smalltree);
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
        void DeleteVegetation(string VEGETATIONID);
        // Insert new to DB 
        void InsertVegetation(VEGETATION vegetation, string fk);
        // Update Data
        void UpdateVegetation(VEGETATION vegetation);
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
        void DeleteDeformity(string DEFORMITYID);
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
        void DeleteDWD(string DWDID);
        // Insert new to DB 
        void InsertDWD(DWD dwd, string fk);
        // Update Data
        void UpdateDWD(DWD dwd);
        String GetTitle(string plotid);
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
        public List<PERSON> GetPersonList(string id)
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
        public List<TREELIST> GetFilteredDataFull(string fk)
        {
            return _databaseHelper.GetFilteredTreeDataFull(fk);
        }

        public List<TREE> GetFilteredTreeStemData(string fk)
        {
            return _databaseHelper.GetFilteredTreeStemList(fk);
        }
        public List<TREELIST> GetFilteredTreeStemDataFull(string fk)
        {
            return _databaseHelper.GetFilteredTreeStemListFull(fk);
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

    public class SoilRepository : ISoilRepository
    {
        DatabaseHelper _databaseHelper;
        public SoilRepository()
        {
            _databaseHelper = new DatabaseHelper();
        }
        public void DeleteSoil(string ID)
        {
            _databaseHelper.DeleteSoil(ID);
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
        }

        public void UpdateSoil(SOIL soil)
        {
            _databaseHelper.UpdateSoil(soil);
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
        public void DeleteSmallTree(string ID)
        {
            _databaseHelper.DeleteSmallTree(ID);
        }
        public void DeleteAllSmallTree()
        {
            _databaseHelper.DeleteAllSmallTree();
        }

        public List<SMALLTREE> GetAllData()
        {
            return _databaseHelper.GetAllSmallTreeData();
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
        }

        public void UpdateSmallTree(SMALLTREE smalltree)
        {
            _databaseHelper.UpdateSmallTree(smalltree);
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
        public void DeleteVegetation(string ID)
        {
            _databaseHelper.DeleteVegetation(ID);
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
        }

        public void UpdateVegetation(VEGETATION vegetation)
        {
            _databaseHelper.UpdateVegetation(vegetation);
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
        public void DeleteDeformity(string ID)
        {
            _databaseHelper.DeleteDeformity(ID);
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
        }

        public void UpdateDeformity(DEFORMITY deformity)
        {
            _databaseHelper.UpdateDeformity(deformity);
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
        public void DeleteDWD(string ID)
        {
            _databaseHelper.DeleteDWD(ID);
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
        }

        public void UpdateDWD(DWD dwd)
        {
            _databaseHelper.UpdateDWD(dwd);
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
