using eLiDAR.Helpers;
using eLiDAR.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eLiDAR.API
{
    // main class to control the API to the REST end point and push and pull data from Azure SQL database
    class SynchManager
    {
        private DatabaseHelper databasehelper;
        private readonly RestService service;
        private SETTINGS settings;
        private string prevdate;
        private string prevsynchdate;
        private int plotpulls;
        private int projectpulls;
        private int treepulls;
        private int plotpushes;
        private int projectpushes;
        private int treepushes;

        public SynchManager()
        {
            service = new RestService(); 
            databasehelper = new DatabaseHelper();
            settings = databasehelper.GetSettingsData();
            prevdate = GetDate();
        }
        // main procedure from which to run the synch process
        // all other sych processes start form here
        //  there is one set of procedires for each of the 10 tables

        public async Task<bool> RunSynch()
        {
            try
            {
                DateTime basedate = new DateTime(2000, 1, 1, 0, 0, 0);
                //confirm the date used for the last synch
                if (DateTime.Compare(settings.LastSynched, basedate) < 0)
                {
                    prevdate = "01-01-2000";
                    settings.LastSynched = basedate;
                }
                // execute the synch on each tabe here
                Task<bool> doproject = ProjectSynch();
                bool projectdone = await doproject;

                Task<bool> doplot = PlotSynch();
                bool plotdone = await doplot;

                Task<bool> dotree = TreeSynch();
                bool treedone = await dotree;

                Task<bool> dostemmap = StemmapSynch();
                bool stemmapdone = await dostemmap;

                Task<bool> doecosite = EcositeSynch();
                bool ecositedone = await doecosite;

                Task<bool> dosoil = SoilSynch();
                bool soildone = await dosoil;

                Task<bool> dosmalltree = SmalltreeSynch();
                bool smalltreedone = await dosmalltree;

                Task<bool> dovegetation = VegetationSynch();
                bool vegetationdone = await dovegetation;

                Task<bool> dodeformity = DeformitySynch();
                bool deformitydone = await dodeformity;

                Task<bool> dodwd = DWDSynch();
                bool dwddone = await dodwd;


                // finish up by saving the details
                if (projectdone && plotdone && treedone && stemmapdone && ecositedone && soildone && smalltreedone && vegetationdone && deformitydone && dwddone)
                {
                    settings.LastSynched = DateTime.UtcNow;
                    settings.PROJECT_ROWS_SYNCHED = projectpushes;
                    settings.PLOT_ROWS_SYNCHED = plotpushes;
                    settings.TREE_ROWS_SYNCHED = treepushes;
                    settings.PROJECT_ROWS_PULLED = projectpulls;
                    settings.PLOT_ROWS_PULLED = plotpulls;
                    settings.TREE_ROWS_PULLED = treepulls;

                    databasehelper.UpdateSettings(settings);
                    return true;
                }
                else { return false; }
            }
            catch (Exception ex)
            { 
                LogWriter logger = new LogWriter();
                logger.LogWrite(ex.Message);
                return false;
            }
        }
        // run the project table synch
        public async Task<bool> ProjectSynch()
        {
            ProjectManager  manager = new ProjectManager(service);
            string filterNew = "&filter=CreatedAtServer gt '" + prevdate + "'";
            string filterUpdate = "&filter=LastModifiedAtServer gt '" + prevdate + "'";

            // pull updated records
            Task<List<PROJECT>> getupdatetask = manager.GetTasksAsync(filterUpdate);
            List<PROJECT> updaterecords = await getupdatetask;
            foreach (var itm in updaterecords)
            {
                databasehelper.UpdateProject(itm);
                // projectpulls += projectpulls;
            }

            // push new records
            var list = databasehelper.GetProjecttoInsert(settings.LastSynched);
            if (list.Count > 0)
            {
                Task inserttask = manager.SaveTasksAsync(list, true);
                await inserttask;
                projectpushes = projectpushes+1; 
            }
            //push updates
            var updatelist = databasehelper.GetProjectToUpdate(settings.LastSynched);
            if (updatelist.Count > 0)
            {
                Task pushupdatetask = manager.SaveTasksAsync(updatelist, false);
                await pushupdatetask;
            }
            // pull new records
            Task<List<PROJECT>> gettask = manager.GetTasksAsync(filterNew);
            List<PROJECT> newrecords = await gettask;
            foreach (var itm in newrecords)
            {
                databasehelper.InsertProject(itm);
                projectpulls = projectpulls+1; 
            }

            //push deletes
            var deletelist = databasehelper.GetProjectToDelete(settings.LastSynched);
            foreach(var itm in deletelist)
            {
                Task deletetask = manager.DeleteTaskAsync(itm);
                await deletetask;
            }
  
            return true;
        }
        public async Task<bool> PlotSynch()
        {
            PlotManager manager = new PlotManager(service); 
            string filterNew = "&filter=CreatedAtServer gt '" + prevdate + "'";
            string filterUpdate = "&filter=LastModifiedAtServer gt '" + prevdate + "'";

            // pull updated records
            Task<List<PLOT>> getupdatetask = manager.GetTasksAsync(filterUpdate);
            List<PLOT> updaterecords = await getupdatetask;
            foreach (var itm in updaterecords)
            {
                databasehelper.UpdatePlot(itm);
            }

            // push new records
            var list = databasehelper.GetPlottoInsert(settings.LastSynched);
            if (list.Count > 0)
            {
                Task inserttask = manager.SaveTasksAsync(list, true);
                await inserttask;
                plotpushes = plotpushes+1;
            }
            //push updates
            var updatelist = databasehelper.GetPlotToUpdate(settings.LastSynched);
            if (updatelist.Count > 0)
            {
                Task pushupdatetask = manager.SaveTasksAsync(updatelist, false);
                await pushupdatetask;
            }
            // pull new records
            Task<List<PLOT>> gettask = manager.GetTasksAsync(filterNew);
            List<PLOT> newrecords = await gettask;
            foreach (var itm in newrecords)
            {
                databasehelper.InsertPlot(itm);
                plotpulls = plotpulls+1;
            }

            //push deletes
            var deletelist = databasehelper.GetPlotToDelete(settings.LastSynched);
            foreach (var itm in deletelist)
            {
                Task deletetask = manager.DeleteTaskAsync(itm);
                await deletetask;
            }

            return true;
        }
        public async Task<bool> TreeSynch()
        {
            TreeManager manager = new TreeManager(service);
            string filterNew = "&filter=CreatedAtServer gt '" + prevdate + "'";
            string filterUpdate = "&filter=LastModifiedAtServer gt '" + prevdate + "'";

            // pull updated records
            Task<List<TREE>> getupdatetask = manager.GetTasksAsync(filterUpdate);
            List<TREE> updaterecords = await getupdatetask;
            foreach (var itm in updaterecords)
            {
                databasehelper.UpdateTree(itm);
            }

            // push new records
            var list = databasehelper.GetTreetoInsert(settings.LastSynched);
            if (list.Count > 0)
            {
                Task inserttask = manager.SaveTasksAsync(list, true);
                await inserttask;
                treepushes = treepushes+1;
            }
            //push updates
            var updatelist = databasehelper.GetTreeToUpdate(settings.LastSynched);
            if (updatelist.Count > 0)
            {
                Task pushupdatetask = manager.SaveTasksAsync(updatelist, false);
                await pushupdatetask;
            }
            // pull new records
            Task<List<TREE>> gettask = manager.GetTasksAsync(filterNew);
            List<TREE> newrecords = await gettask;
            foreach (var itm in newrecords)
            {
                databasehelper.InsertTree(itm);
                treepulls = treepulls+1;
            }

            //push deletes
            var deletelist = databasehelper.GetTreeToDelete(settings.LastSynched);
            foreach (var itm in deletelist)
            {
                Task deletetask = manager.DeleteTaskAsync(itm);
                await deletetask;
            }

            return true;
        }

        public async Task<bool> StemmapSynch()
        {
            StemmapManager manager = new StemmapManager(service);
            string filterNew = "&filter=CreatedAtServer gt '" + prevdate + "'";
            string filterUpdate = "&filter=LastModifiedAtServer gt '" + prevdate + "'";

            // pull updated records
            Task<List<STEMMAP>> getupdatetask = manager.GetTasksAsync(filterUpdate);
            List<STEMMAP> updaterecords = await getupdatetask;
            foreach (var itm in updaterecords)
            {
                databasehelper.UpdateStemmap(itm);
            }

            // push new records
            var list = databasehelper.GetStemmaptoInsert(settings.LastSynched);
            if (list.Count > 0)
            {
                Task inserttask = manager.SaveTasksAsync(list, true);
                await inserttask;
               
            }
            //push updates
            var updatelist = databasehelper.GetStemmapToUpdate(settings.LastSynched);
            if (updatelist.Count > 0)
            {
                Task pushupdatetask = manager.SaveTasksAsync(updatelist, false);
                await pushupdatetask;
            }
            // pull new records
            Task<List<STEMMAP>> gettask = manager.GetTasksAsync(filterNew);
            List<STEMMAP> newrecords = await gettask;
            foreach (var itm in newrecords)
            {
                databasehelper.InsertStemmap(itm);
            }

            //push deletes
            var deletelist = databasehelper.GetStemmapToDelete(settings.LastSynched);
            foreach (var itm in deletelist)
            {
                Task deletetask = manager.DeleteTaskAsync(itm);
                await deletetask;
            }
            return true;
        }
        public async Task<bool> EcositeSynch()
        {
            EcositeManager manager = new EcositeManager(service);
            string filterNew = "&filter=CreatedAtServer gt '" + prevdate + "'";
            string filterUpdate = "&filter=LastModifiedAtServer gt '" + prevdate + "'";

            // pull updated records
            Task<List<ECOSITE>> getupdatetask = manager.GetTasksAsync(filterUpdate);
            List<ECOSITE> updaterecords = await getupdatetask;
            foreach (var itm in updaterecords)
            {
                databasehelper.UpdateEcosite(itm);
            }

            // push new records
            var list = databasehelper.GetEcositetoInsert(settings.LastSynched);
            if (list.Count > 0)
            {
                Task inserttask = manager.SaveTasksAsync(list, true);
                await inserttask;

            }
            //push updates
            var updatelist = databasehelper.GetEcositeToUpdate(settings.LastSynched);
            if (updatelist.Count > 0)
            {
                Task pushupdatetask = manager.SaveTasksAsync(updatelist, false);
                await pushupdatetask;
            }
            // pull new records
            Task<List<ECOSITE>> gettask = manager.GetTasksAsync(filterNew);
            List<ECOSITE> newrecords = await gettask;
            foreach (var itm in newrecords)
            {
                databasehelper.InsertEcosite(itm);
            }

            //push deletes
            var deletelist = databasehelper.GetEcositeToDelete(settings.LastSynched);
            foreach (var itm in deletelist)
            {
                Task deletetask = manager.DeleteTaskAsync(itm);
                await deletetask;
            }
            return true;
        }
        public async Task<bool> SoilSynch()
        {
            SoilManager manager = new SoilManager(service);
            string filterNew = "&filter=CreatedAtServer gt '" + prevdate + "'";
            string filterUpdate = "&filter=LastModifiedAtServer gt '" + prevdate + "'";

            // pull updated records
            Task<List<SOIL>> getupdatetask = manager.GetTasksAsync(filterUpdate);
            List<SOIL> updaterecords = await getupdatetask;
            foreach (var itm in updaterecords)
            {
                databasehelper.UpdateSoil(itm);
            }

            // push new records
            var list = databasehelper.GetSoiltoInsert(settings.LastSynched);
            if (list.Count > 0)
            {
                Task inserttask = manager.SaveTasksAsync(list, true);
                await inserttask;

            }
            //push updates
            var updatelist = databasehelper.GetSoilToUpdate(settings.LastSynched);
            if (updatelist.Count > 0)
            {
                Task pushupdatetask = manager.SaveTasksAsync(updatelist, false);
                await pushupdatetask;
            }
            // pull new records
            Task<List<SOIL>> gettask = manager.GetTasksAsync(filterNew);
            List<SOIL> newrecords = await gettask;
            foreach (var itm in newrecords)
            {
                databasehelper.InsertSoil(itm);
            }

            //push deletes
            var deletelist = databasehelper.GetSoilToDelete(settings.LastSynched);
            foreach (var itm in deletelist)
            {
                Task deletetask = manager.DeleteTaskAsync(itm);
                await deletetask;
            }
            return true;
        }
        public async Task<bool> SmalltreeSynch()
        {
            SmalltreeManager manager = new SmalltreeManager(service);
            string filterNew = "&filter=CreatedAtServer gt '" + prevdate + "'";
            string filterUpdate = "&filter=LastModifiedAtServer gt '" + prevdate + "'";

            // pull updated records
            Task<List<SMALLTREE>> getupdatetask = manager.GetTasksAsync(filterUpdate);
            List<SMALLTREE> updaterecords = await getupdatetask;
            foreach (var itm in updaterecords)
            {
                databasehelper.UpdateSmallTree(itm);
            }

            // push new records
            var list = databasehelper.GetSmalltreetoInsert(settings.LastSynched);
            if (list.Count > 0)
            {
                Task inserttask = manager.SaveTasksAsync(list, true);
                await inserttask;

            }
            //push updates
            var updatelist = databasehelper.GetSmalltreeToUpdate(settings.LastSynched);
            if (updatelist.Count > 0)
            {
                Task pushupdatetask = manager.SaveTasksAsync(updatelist, false);
                await pushupdatetask;
            }
            // pull new records
            Task<List<SMALLTREE>> gettask = manager.GetTasksAsync(filterNew);
            List<SMALLTREE> newrecords = await gettask;
            foreach (var itm in newrecords)
            {
                databasehelper.InsertSmallTree(itm);
            }

            //push deletes
            var deletelist = databasehelper.GetSmalltreeToDelete(settings.LastSynched);
            foreach (var itm in deletelist)
            {
                Task deletetask = manager.DeleteTaskAsync(itm);
                await deletetask;
            }
            return true;
        }

        public async Task<bool> VegetationSynch()
        {
            VegetationManager manager = new VegetationManager(service);
            string filterNew = "&filter=CreatedAtServer gt '" + prevdate + "'";
            string filterUpdate = "&filter=LastModifiedAtServer gt '" + prevdate + "'";

            // pull updated records
            Task<List<VEGETATION>> getupdatetask = manager.GetTasksAsync(filterUpdate);
            List<VEGETATION> updaterecords = await getupdatetask;
            foreach (var itm in updaterecords)
            {
                databasehelper.UpdateVegetation(itm);
            }

            // push new records
            var list = databasehelper.GetVegetationtoInsert(settings.LastSynched);
            if (list.Count > 0)
            {
                Task inserttask = manager.SaveTasksAsync(list, true);
                await inserttask;

            }
            //push updates
            var updatelist = databasehelper.GetVegetationToUpdate(settings.LastSynched);
            if (updatelist.Count > 0)
            {
                Task pushupdatetask = manager.SaveTasksAsync(updatelist, false);
                await pushupdatetask;
            }
            // pull new records
            Task<List<VEGETATION>> gettask = manager.GetTasksAsync(filterNew);
            List<VEGETATION> newrecords = await gettask;
            foreach (var itm in newrecords)
            {
                databasehelper.InsertVegetation(itm);
            }

            //push deletes
            var deletelist = databasehelper.GetVegetationToDelete(settings.LastSynched);
            foreach (var itm in deletelist)
            {
                Task deletetask = manager.DeleteTaskAsync(itm);
                await deletetask;
            }
            return true;
        }
        public async Task<bool> DeformitySynch()
        {
            DeformityManager manager = new DeformityManager(service);
            string filterNew = "&filter=CreatedAtServer gt '" + prevdate + "'";
            string filterUpdate = "&filter=LastModifiedAtServer gt '" + prevdate + "'";

            // pull updated records
            Task<List<DEFORMITY>> getupdatetask = manager.GetTasksAsync(filterUpdate);
            List<DEFORMITY> updaterecords = await getupdatetask;
            foreach (var itm in updaterecords)
            {
                databasehelper.UpdateDeformity(itm);
            }

            // push new records
            var list = databasehelper.GetDeformitytoInsert(settings.LastSynched);
            if (list.Count > 0)
            {
                Task inserttask = manager.SaveTasksAsync(list, true);
                await inserttask;

            }
            //push updates
            var updatelist = databasehelper.GetDeformityToUpdate(settings.LastSynched);
            if (updatelist.Count > 0)
            {
                Task pushupdatetask = manager.SaveTasksAsync(updatelist, false);
                await pushupdatetask;
            }
            // pull new records
            Task<List<DEFORMITY>> gettask = manager.GetTasksAsync(filterNew);
            List<DEFORMITY> newrecords = await gettask;
            foreach (var itm in newrecords)
            {
                databasehelper.InsertDeformity(itm);
            }

            //push deletes
            var deletelist = databasehelper.GetDeformityToDelete(settings.LastSynched);
            foreach (var itm in deletelist)
            {
                Task deletetask = manager.DeleteTaskAsync(itm);
                await deletetask;
            }
            return true;
        }

        public async Task<bool> DWDSynch()
        {
            DWDManager manager = new DWDManager(service);
            string filterNew = "&filter=CreatedAtServer gt '" + prevdate + "'";
            string filterUpdate = "&filter=LastModifiedAtServer gt '" + prevdate + "'";

            // pull updated records
            Task<List<DWD>> getupdatetask = manager.GetTasksAsync(filterUpdate);
            List<DWD> updaterecords = await getupdatetask;
            foreach (var itm in updaterecords)
            {
                databasehelper.UpdateDWD(itm);
            }

            // push new records
            var list = databasehelper.GetDWDtoInsert(settings.LastSynched);
            if (list.Count > 0)
            {
                Task inserttask = manager.SaveTasksAsync(list, true);
                await inserttask;

            }
            //push updates
            var updatelist = databasehelper.GetDWDToUpdate(settings.LastSynched);
            if (updatelist.Count > 0)
            {
                Task pushupdatetask = manager.SaveTasksAsync(updatelist, false);
                await pushupdatetask;
            }
            // pull new records
            Task<List<DWD>> gettask = manager.GetTasksAsync(filterNew);
            List<DWD> newrecords = await gettask;
            foreach (var itm in newrecords)
            {
                databasehelper.InsertDWD(itm);
            }

            //push deletes
            var deletelist = databasehelper.GetDWDToDelete(settings.LastSynched);
            foreach (var itm in deletelist)
            {
                Task deletetask = manager.DeleteTaskAsync(itm);
                await deletetask;
            }
            return true;
        }


        string GetDate()
        {
            DateTime initdate = settings.LastSynched;
            prevsynchdate = initdate.ToString();
            return prevsynchdate.Replace("T", " "); 
        }
    }
}
