using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

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
        private Utils util = new Utils();
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

                Task<bool> doperson = PersonSynch();
                bool persondone = await doperson;

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

                Task<bool> dophoto = PhotoSynch();
                bool photodone = await dophoto;
               
                Task<bool> dovegetationcensus = VegetationCensusSynch();
                bool vegetationcensusdone = await dovegetationcensus;


                // finish up by saving the details
                if (projectdone && plotdone && treedone && stemmapdone && ecositedone && soildone && smalltreedone && vegetationdone && deformitydone && dwddone && photodone && persondone && vegetationcensusdone && service.IsSuccess )
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
                else 
                {
                    await Application.Current.MainPage.DisplayAlert("Synch did not finish", service.Msg, "Ok");
                    return false;
                }
            }
            catch (Exception ex)
            { 
                LogWriter logger = new LogWriter();
                logger.LogWrite(ex.Message);
                return false;
            }
        }
        public async Task<bool> RunSynch(String _plotid)
        {
            // this is just a plot level synch process
            try
            {
                DateTime basedate = new DateTime(2000, 1, 1, 0, 0, 0);
                string pulldate = "01-01-2000";
                //confirm the date used for the last synch
                if (DateTime.Compare(settings.LastSynched, basedate) < 0)
                {
                    prevdate = "01-01-2000";
                    settings.LastSynched = basedate;
                }

                Task<bool> doperson = PersonSynch();
                bool persondone = await doperson;

                Task<bool> doplot = PlotSynch(_plotid);
                bool plotdone = await doplot;
                string plottype = databasehelper.GetPlotType(_plotid);

                Task<bool> dotree = TreeSynch(_plotid, pulldate, plottype);
                bool treedone = await dotree;

                Task<bool> doecosite = EcositeSynch(_plotid, pulldate);
                bool ecositedone = await doecosite;

                Task<bool> dosoil = SoilSynch(_plotid, pulldate);
                bool soildone = await dosoil;

                Task<bool> dophoto = PhotoSynch();
                bool photodone = await dophoto;
                bool smalltreedone = true;
                bool vegetationdone = true;
                bool dwddone = true;
                bool vegetationcensusdone = true;
                bool deformitydone = true;
                bool stemmapdone = true;
                if (plottype == "B" || plottype == "C")
                {
                    Task<bool> dosmalltree = SmalltreeSynch(_plotid, pulldate);
                    smalltreedone = await dosmalltree;

                    Task<bool> dostemmap = StemmapSynch();
                    stemmapdone = await dostemmap;

                }
                if (plottype == "C")
                {

                    Task<bool> dodeformity = DeformitySynch();
                    deformitydone = await dodeformity;

                    Task<bool> dovegetation = VegetationSynch(_plotid, pulldate);
                    vegetationdone = await dovegetation;

                    Task<bool> dodwd = DWDSynch(_plotid, pulldate);
                    dwddone = await dodwd;

                    Task<bool> dovegetationcensus = VegetationCensusSynch(_plotid, pulldate);
                    vegetationcensusdone = await dovegetationcensus;
                }

                // finish up by saving the details
                if (plotdone && treedone &&  ecositedone && soildone && photodone && persondone && service.IsSuccess && smalltreedone && dwddone && vegetationdone && vegetationcensusdone) {
                  //  settings.LastSynched = DateTime.UtcNow;
                    settings.PROJECT_ROWS_SYNCHED = projectpushes;
                    settings.PLOT_ROWS_SYNCHED = plotpushes;
                    settings.TREE_ROWS_SYNCHED = treepushes;
                    settings.PROJECT_ROWS_PULLED = projectpulls;
                    settings.PLOT_ROWS_PULLED = plotpulls;
                    settings.TREE_ROWS_PULLED = treepulls;

                    databasehelper.UpdateSettings(settings);
                    return true;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Plot Synch did not finish", service.Msg, "Ok");
                    return false;
                }
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
            var updatelist = databasehelper.GetProjectToUpdate(settings.LastSynched);
            // pull updated records
            Task<List<PROJECT>> getupdatetask = manager.GetTasksAsync(filterUpdate);
            List<PROJECT> updaterecords = await getupdatetask;
            foreach (var itm in updaterecords)
            {
                if (!updatelist.Any(item => item.PROJECTID == itm.PROJECTID))
                {
                    databasehelper.UpdateProject(itm);
                }
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


  
            return true;
        }
        public async Task<bool> PlotSynch()
        {
            PlotManager manager = new PlotManager(service); 
            string filterNew = "&filter=CreatedAtServer gt '" + prevdate + "'";
            string filterUpdate = "&filter=LastModifiedAtServer gt '" + prevdate + "'";
            // get the update list of records
            var updatelist = databasehelper.GetPlotToUpdate(settings.LastSynched);

            // pull updated records
            Task<List<PLOT>> getupdatetask = manager.GetTasksAsync(filterUpdate);
            List<PLOT> updaterecords = await getupdatetask;
            foreach (var itm in updaterecords)
            {
                // only update records not being updated from the app
                if (!updatelist.Any(item => item.PLOTID == itm.PLOTID))
                {
                    databasehelper.UpdatePlot(itm);
                }
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
                 plotpulls = plotpulls + 1;
                
            }

            return true;
        }
        public async Task<bool> PlotSynch(String _plotid)
        {
            PlotManager manager = new PlotManager(service);
            string filterNew = "&filter=CreatedAtServer gt '" + prevdate + "' AND PLOTID eq '" + _plotid + "'";
            string filterUpdate = "&filter=LastModifiedAtServer gt '" + prevdate + "' AND PLOTID eq '" + _plotid + "'";

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
                plotpushes = plotpushes + 1;
            }
            //push updates
            var updatelist = databasehelper.GetPlotToUpdate(settings.LastSynched);
            if (updatelist.Count > 0)
            {
                Task pushupdatetask = manager.SaveTasksAsync(updatelist, false);
                await pushupdatetask;
            }

            return true;
        }
        public async Task<bool> TreeSynch()
        {
            TreeManager manager = new TreeManager(service);
            string filterNew = "&filter=CreatedAtServer gt '" + prevdate + "'";
            string filterUpdate = "&filter=LastModifiedAtServer gt '" + prevdate + "'";
            var updatelist = databasehelper.GetTreeToUpdate(settings.LastSynched);

            // pull updated records
            Task<List<TREE>> getupdatetask = manager.GetTasksAsync(filterUpdate);
            List<TREE> updaterecords = await getupdatetask;
            foreach (var itm in updaterecords)
            {
                if (!updatelist.Any(item => item.TREEID == itm.TREEID))
                {
                    databasehelper.UpdateTree(itm);
                }
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
           
            if (updatelist.Count > 0)
            {
                Task pushupdatetask = manager.SaveTasksAsync(updatelist, false);
                await pushupdatetask;
            }

            if (!util.DoPartialSynch)
            {
                // pull new records
                Task<List<TREE>> gettask = manager.GetTasksAsync(filterNew);
                List<TREE> newrecords = await gettask;
                foreach (var itm in newrecords)
                {
                    databasehelper.InsertTree(itm);
                    treepulls = treepulls + 1;
                }
            }

            //push deletes
            //var deletelist = databasehelper.GetTreeToDelete(settings.LastSynched);
            //foreach (var itm in deletelist)
            //{
            //    Task deletetask = manager.DeleteTaskAsync(itm);
            //    await deletetask;
            //}

            return true;
        }
        public async Task<bool> TreeSynch(String _plotid, String _origdate, String plottype)
        {
            TreeManager manager = new TreeManager(service);
            string filterNew = "&filter=CreatedAtServer gt '" + _origdate + "' AND PLOTID eq '" + _plotid + "'";
            string filterUpdate = "&filter=LastModifiedAtServer gt '" + prevdate + "' AND PLOTID eq '" + _plotid + "'";
            string filterTree = "";
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
                treepushes = treepushes + 1;
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
//                filterTree = filterTree + "'" + itm.TREEID + "',";
                treepulls = treepulls + 1;
            }
            // now use the trees to makes a list for synching

            List<TREE> treelist = databasehelper.GetFilteredTreeData(_plotid);
            int counter = 0;
            foreach (var tree in treelist)
            {
                filterTree = filterTree + " TREEID eq '" + tree.TREEID + "' OR";
                counter = counter + 1;
                if (counter % 10 == 0)  // run the filter in 25 items chunks - this is a horrible workaround
                {
                    filterTree = filterTree.Substring(0, filterTree.Length - 3);
                    if (plottype == "B" || plottype == "C") { await StemmapSynch(_origdate, filterTree); }
                    if (plottype == "C") { await DeformitySynch(_origdate, filterTree); }
                    filterTree = "";
                }
            }
   
            if (filterTree.Length >0)  // and then finsih up with whatever data is left over to filter from
            {
                filterTree = filterTree.Substring(0, filterTree.Length - 3);
                if (plottype == "B" || plottype == "C") { await StemmapSynch(_origdate, filterTree); }
                if (plottype == "C") { await DeformitySynch(_origdate, filterTree); }
            }
            return true;
        }

        public async Task<bool> StemmapSynch()
        {
            StemmapManager manager = new StemmapManager(service);
            string filterNew = "&filter=CreatedAtServer gt '" + prevdate + "'";
            string filterUpdate = "&filter=LastModifiedAtServer gt '" + prevdate + "'";
            var updatelist = databasehelper.GetStemmapToUpdate(settings.LastSynched);
            // pull updated records
            Task<List<STEMMAP>> getupdatetask = manager.GetTasksAsync(filterUpdate);
            List<STEMMAP> updaterecords = await getupdatetask;
            foreach (var itm in updaterecords)
            {
                if (!updatelist.Any(item => item.STEMMAPID  == itm.STEMMAPID ))
                {
                    databasehelper.UpdateStemmap(itm);
                }
            }

            // push new records
            var list = databasehelper.GetStemmaptoInsert(settings.LastSynched);
            if (list.Count > 0)
            {
                Task inserttask = manager.SaveTasksAsync(list, true);
                await inserttask;
               
            }
            //push updates
            
            if (updatelist.Count > 0)
            {
                Task pushupdatetask = manager.SaveTasksAsync(updatelist, false);
                await pushupdatetask;
            }
            if (!util.DoPartialSynch)
            {
                // pull new records
                Task<List<STEMMAP>> gettask = manager.GetTasksAsync(filterNew);
                List<STEMMAP> newrecords = await gettask;
                foreach (var itm in newrecords)
                {
                    databasehelper.InsertStemmap(itm);
                }
            }

            //push deletes
            //var deletelist = databasehelper.GetStemmapToDelete(settings.LastSynched);
            //foreach (var itm in deletelist)
            //{
            //    Task deletetask = manager.DeleteTaskAsync(itm);
            //    await deletetask;
            //}
            return true;
        }

        public async Task<bool> StemmapSynch(String _origdate, string infilter)
        {
            StemmapManager manager = new StemmapManager(service);

            string filterNew = "&filter=CreatedAtServer gt '" + _origdate + "' AND (" + infilter + ")";
            string filterUpdate = "&filter=LastModifiedAtServer gt '" + prevdate + "' AND (" + infilter + ")";

            // pull updated records
            Task<List<STEMMAP>> getupdatetask = manager.GetTasksAsync(filterUpdate);
            List<STEMMAP> updaterecords = await getupdatetask;
            foreach (var itm in updaterecords)
            {
                databasehelper.UpdateStemmap(itm);
            }

            // pull new records
            Task<List<STEMMAP>> gettask = manager.GetTasksAsync(filterNew);
            List<STEMMAP> newrecords = await gettask;
            foreach (var itm in newrecords)
            {
               databasehelper.InsertStemmap(itm);
            }
            return true;
        }
        public async Task<bool> EcositeSynch()
        {
            EcositeManager manager = new EcositeManager(service);
            string filterNew = "&filter=CreatedAtServer gt '" + prevdate + "'";
            string filterUpdate = "&filter=LastModifiedAtServer gt '" + prevdate + "'";
            var updatelist = databasehelper.GetEcositeToUpdate(settings.LastSynched);
            // pull updated records
            Task<List<ECOSITE>> getupdatetask = manager.GetTasksAsync(filterUpdate);
            List<ECOSITE> updaterecords = await getupdatetask;
            foreach (var itm in updaterecords)
            {
                if (!updatelist.Any(item => item.ECOSITEID == itm.ECOSITEID))
                {
                    databasehelper.UpdateEcosite(itm);
                }
            }

            // push new records
            var list = databasehelper.GetEcositetoInsert(settings.LastSynched);
            if (list.Count > 0)
            {
                Task inserttask = manager.SaveTasksAsync(list, true);
                await inserttask;

            }
            //push updates
           
            if (updatelist.Count > 0)
            {
                Task pushupdatetask = manager.SaveTasksAsync(updatelist, false);
                await pushupdatetask;
            }
            if (!util.DoPartialSynch)
            {
                // pull new records
                Task<List<ECOSITE>> gettask = manager.GetTasksAsync(filterNew);
                List<ECOSITE> newrecords = await gettask;
                foreach (var itm in newrecords)
                {
                    databasehelper.InsertEcosite(itm);
                }
            }

            return true;
        }
        public async Task<bool> EcositeSynch(String _plotid, String _origdate)
        {
            EcositeManager manager = new EcositeManager(service);
            string filterNew = "&filter=CreatedAtServer gt '" + _origdate + "' AND PLOTID eq '" + _plotid + "'";
            string filterUpdate = "&filter=LastModifiedAtServer gt '" + prevdate + "' AND PLOTID eq '" + _plotid + "'";


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
            return true;
        }
        public async Task<bool> SoilSynch()
        {
            SoilManager manager = new SoilManager(service);
            string filterNew = "&filter=CreatedAtServer gt '" + prevdate + "'";
            string filterUpdate = "&filter=LastModifiedAtServer gt '" + prevdate + "'";
            var updatelist = databasehelper.GetSoilToUpdate(settings.LastSynched);
            // pull updated records
            Task<List<SOIL>> getupdatetask = manager.GetTasksAsync(filterUpdate);
            List<SOIL> updaterecords = await getupdatetask;
            foreach (var itm in updaterecords)
            {
                if (!updatelist.Any(item => item.SOILID == itm.SOILID))
                {
                    databasehelper.UpdateSoil(itm);
                }
            }

            // push new records
            var list = databasehelper.GetSoiltoInsert(settings.LastSynched);
            if (list.Count > 0)
            {
                Task inserttask = manager.SaveTasksAsync(list, true);
                await inserttask;

            }
            //push updates
           
            if (updatelist.Count > 0)
            {
                Task pushupdatetask = manager.SaveTasksAsync(updatelist, false);
                await pushupdatetask;
            }
            if (!util.DoPartialSynch)
            {
                // pull new records
                Task<List<SOIL>> gettask = manager.GetTasksAsync(filterNew);
                List<SOIL> newrecords = await gettask;
                foreach (var itm in newrecords)
                {
                    databasehelper.InsertSoil(itm);
                }
            }

            //push deletes
            //var deletelist = databasehelper.GetSoilToDelete(settings.LastSynched);
            //foreach (var itm in deletelist)
            //{
            //    Task deletetask = manager.DeleteTaskAsync(itm);
            //    await deletetask;
            //}
            return true;
        }

        public async Task<bool> SoilSynch(String _plotid, String _origdate)
        {
            SoilManager manager = new SoilManager(service);
            string filterNew = "&filter=CreatedAtServer gt '" + _origdate + "' AND PLOTID eq '" + _plotid + "'";
            string filterUpdate = "&filter=LastModifiedAtServer gt '" + prevdate + "' AND PLOTID eq '" + _plotid + "'";

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
            return true;
        }

        public async Task<bool> SmalltreeSynch()
        {
            SmalltreeManager manager = new SmalltreeManager(service);
            string filterNew = "&filter=CreatedAtServer gt '" + prevdate + "'";
            string filterUpdate = "&filter=LastModifiedAtServer gt '" + prevdate + "'";
            var updatelist = databasehelper.GetSmalltreeToUpdate(settings.LastSynched);
            // pull updated records
            Task<List<SMALLTREE>> getupdatetask = manager.GetTasksAsync(filterUpdate);
            List<SMALLTREE> updaterecords = await getupdatetask;
            foreach (var itm in updaterecords)
            {
                if (!updatelist.Any(item => item.SMALLTREEID  == itm.SMALLTREEID ))
                {
                    databasehelper.UpdateSmallTree(itm);
                }
            }

            // push new records
            var list = databasehelper.GetSmalltreetoInsert(settings.LastSynched);
            if (list.Count > 0)
            {
                Task inserttask = manager.SaveTasksAsync(list, true);
                await inserttask;

            }
            //push updates
          
            if (updatelist.Count > 0)
            {
                Task pushupdatetask = manager.SaveTasksAsync(updatelist, false);
                await pushupdatetask;
            }
            if (!util.DoPartialSynch)
            {
                // pull new records
                Task<List<SMALLTREE>> gettask = manager.GetTasksAsync(filterNew);
                List<SMALLTREE> newrecords = await gettask;
                foreach (var itm in newrecords)
                {
                    databasehelper.InsertSmallTree(itm);
                }
            }
            return true;
        }
        public async Task<bool> SmalltreeSynch(String _plotid, String _origdate)
        {
            SmalltreeManager manager = new SmalltreeManager(service);
            string filterNew = "&filter=CreatedAtServer gt '" + _origdate + "' AND PLOTID eq '" + _plotid + "'";
            string filterUpdate = "&filter=LastModifiedAtServer gt '" + prevdate + "' AND PLOTID eq '" + _plotid + "'";

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

            return true;
        }

        public async Task<bool> VegetationSynch()
        {
            VegetationManager manager = new VegetationManager(service);
            string filterNew = "&filter=CreatedAtServer gt '" + prevdate + "'";
            string filterUpdate = "&filter=LastModifiedAtServer gt '" + prevdate + "'";
            var updatelist = databasehelper.GetVegetationToUpdate(settings.LastSynched);
            // pull updated records
            Task<List<VEGETATION>> getupdatetask = manager.GetTasksAsync(filterUpdate);
            List<VEGETATION> updaterecords = await getupdatetask;
            foreach (var itm in updaterecords)
            {
                if (!updatelist.Any(item => item.VEGETATIONID  == itm.VEGETATIONID))
                {
                    databasehelper.UpdateVegetation(itm);
                }
            }

            // push new records
            var list = databasehelper.GetVegetationtoInsert(settings.LastSynched);
            if (list.Count > 0)
            {
                Task inserttask = manager.SaveTasksAsync(list, true);
                await inserttask;

            }
            //push updates
           
            if (updatelist.Count > 0)
            {
                Task pushupdatetask = manager.SaveTasksAsync(updatelist, false);
                await pushupdatetask;
            }
            if (!util.DoPartialSynch)
            {
                // pull new records
                Task<List<VEGETATION>> gettask = manager.GetTasksAsync(filterNew);
                List<VEGETATION> newrecords = await gettask;
                foreach (var itm in newrecords)
                {
                    databasehelper.InsertVegetation(itm);
                }
            }

            //push deletes
            //var deletelist = databasehelper.GetVegetationToDelete(settings.LastSynched);
            //foreach (var itm in deletelist)
            //{
            //    Task deletetask = manager.DeleteTaskAsync(itm);
            //    await deletetask;
            //}
            return true;
        }
        public async Task<bool> VegetationSynch(String _plotid, String _origdate)
        {
            VegetationManager manager = new VegetationManager(service);
            string filterNew = "&filter=CreatedAtServer gt '" + _origdate + "' AND PLOTID eq '" + _plotid + "'";
            string filterUpdate = "&filter=LastModifiedAtServer gt '" + prevdate + "' AND PLOTID eq '" + _plotid + "'";

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
            
            return true;
        }
        public async Task<bool> DeformitySynch()
        {
            DeformityManager manager = new DeformityManager(service);
            string filterNew = "&filter=CreatedAtServer gt '" + prevdate + "'";
            string filterUpdate = "&filter=LastModifiedAtServer gt '" + prevdate + "'";
            var updatelist = databasehelper.GetDeformityToUpdate(settings.LastSynched);
            // pull updated records
            Task<List<DEFORMITY>> getupdatetask = manager.GetTasksAsync(filterUpdate);
            List<DEFORMITY> updaterecords = await getupdatetask;
            foreach (var itm in updaterecords)
            {
                if (!updatelist.Any(item => item.DEFORMITYID  == itm.DEFORMITYID))
                {
                    databasehelper.UpdateDeformity(itm);
                }
            }

            // push new records
            var list = databasehelper.GetDeformitytoInsert(settings.LastSynched);
            if (list.Count > 0)
            {
                Task inserttask = manager.SaveTasksAsync(list, true);
                await inserttask;

            }
            //push updates
           
            if (updatelist.Count > 0)
            {
                Task pushupdatetask = manager.SaveTasksAsync(updatelist, false);
                await pushupdatetask;
            }
            if (!util.DoPartialSynch)
            {
                // pull new records
                Task<List<DEFORMITY>> gettask = manager.GetTasksAsync(filterNew);
                List<DEFORMITY> newrecords = await gettask;
                foreach (var itm in newrecords)
                {
                    databasehelper.InsertDeformity(itm);
                }
            }

            //push deletes
            //var deletelist = databasehelper.GetDeformityToDelete(settings.LastSynched);
            //foreach (var itm in deletelist)
            //{
            //    Task deletetask = manager.DeleteTaskAsync(itm);
            //    await deletetask;
            //}
            return true;
        }
        public async Task<bool> DeformitySynch(String _origdate, string infilter)
        {

            string filterNew = "&filter=CreatedAtServer gt '" + _origdate + "' AND (" + infilter + ")";
            string filterUpdate = "&filter=LastModifiedAtServer gt '" + prevdate + "' AND (" + infilter + ")";

            DeformityManager manager = new DeformityManager(service);

            // pull updated records
            Task<List<DEFORMITY>> getupdatetask = manager.GetTasksAsync(filterUpdate);
            List<DEFORMITY> updaterecords = await getupdatetask;
            foreach (var itm in updaterecords)
            {
                databasehelper.UpdateDeformity(itm);
            }

                // pull new records
                Task<List<DEFORMITY>> gettask = manager.GetTasksAsync(filterNew);
                List<DEFORMITY> newrecords = await gettask;
                foreach (var itm in newrecords)
                {
                    databasehelper.InsertDeformity(itm);
                }
            
            return true;
        }

        public async Task<bool> DWDSynch()
        {
            DWDManager manager = new DWDManager(service);
            string filterNew = "&filter=CreatedAtServer gt '" + prevdate + "'";
            string filterUpdate = "&filter=LastModifiedAtServer gt '" + prevdate + "'";
            var updatelist = databasehelper.GetDWDToUpdate(settings.LastSynched);
            // pull updated records
            Task<List<DWD>> getupdatetask = manager.GetTasksAsync(filterUpdate);
            List<DWD> updaterecords = await getupdatetask;
            foreach (var itm in updaterecords)
            {
                if (!updatelist.Any(item => item.DWDID  == itm.DWDID ))
                {
                    databasehelper.UpdateDWD(itm);
                }
            }

            // push new records
            var list = databasehelper.GetDWDtoInsert(settings.LastSynched);
            if (list.Count > 0)
            {
                Task inserttask = manager.SaveTasksAsync(list, true);
                await inserttask;

            }
            //push updates

            if (updatelist.Count > 0)
            {
                Task pushupdatetask = manager.SaveTasksAsync(updatelist, false);
                await pushupdatetask;
            }
            if (!util.DoPartialSynch)
            {
                // pull new records
                Task<List<DWD>> gettask = manager.GetTasksAsync(filterNew);
                List<DWD> newrecords = await gettask;
                foreach (var itm in newrecords)
                {
                    databasehelper.InsertDWD(itm);
                }
            }


            return true;
        }
        public async Task<bool> DWDSynch(String _plotid, String _origdate)
        {
            DWDManager manager = new DWDManager(service);
            string filterNew = "&filter=CreatedAtServer gt '" + _origdate + "' AND PLOTID eq '" + _plotid + "'";
            string filterUpdate = "&filter=LastModifiedAtServer gt '" + prevdate + "' AND PLOTID eq '" + _plotid + "'";

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

            return true;
        }
        public async Task<bool> PhotoSynch()
        {
            PhotoManager manager = new PhotoManager(service);
            string filterNew = "&filter=CreatedAtServer gt '" + prevdate + "'";
            string filterUpdate = "&filter=LastModifiedAtServer gt '" + prevdate + "'";
            var updatelist = databasehelper.GetPhotoToUpdate(settings.LastSynched);
            // pull updated records
            Task<List<PHOTO>> getupdatetask = manager.GetTasksAsync(filterUpdate);
            List<PHOTO> updaterecords = await getupdatetask;
            foreach (var itm in updaterecords)
            {
                if (!updatelist.Any(item => item.PHOTOID  == itm.PHOTOID ))
                {
                    databasehelper.UpdatePhoto(itm);
                }
            }

            // push new records
            var list = databasehelper.GetPhototoInsert(settings.LastSynched);
            if (list.Count > 0)
            {
                Task inserttask = manager.SaveTasksAsync(list, true);
                await inserttask;

            }
            //push updates
           
            if (updatelist.Count > 0)
            {
                Task pushupdatetask = manager.SaveTasksAsync(updatelist, false);
                await pushupdatetask;
            }

            // pull new records
            Task<List<PHOTO>> gettask = manager.GetTasksAsync(filterNew);
            List<PHOTO> newrecords = await gettask;
            foreach (var itm in newrecords)
            {
                databasehelper.InsertPhoto(itm);
            }

  
            return true;
        }
        public async Task<bool> PhotoSynch(String _plotid, String _origdate)
        {
            PhotoManager manager = new PhotoManager(service);
            string filterNew = "&filter=CreatedAtServer gt '" + _origdate + "' AND PLOTID eq '" + _plotid + "'";
            string filterUpdate = "&filter=LastModifiedAtServer gt '" + prevdate + "' AND PLOTID eq '" + _plotid + "'";

            // pull updated records
            Task<List<PHOTO>> getupdatetask = manager.GetTasksAsync(filterUpdate);
            List<PHOTO> updaterecords = await getupdatetask;
            foreach (var itm in updaterecords)
            {
                databasehelper.UpdatePhoto(itm);
            }

            // push new records
            var list = databasehelper.GetPhototoInsert(settings.LastSynched);
            if (list.Count > 0)
            {
                Task inserttask = manager.SaveTasksAsync(list, true);
                await inserttask;

            }
            //push updates
            var updatelist = databasehelper.GetPhotoToUpdate(settings.LastSynched);
            if (updatelist.Count > 0)
            {
                Task pushupdatetask = manager.SaveTasksAsync(updatelist, false);
                await pushupdatetask;
            }

            // pull new records
            Task<List<PHOTO>> gettask = manager.GetTasksAsync(filterNew);
            List<PHOTO> newrecords = await gettask;
            foreach (var itm in newrecords)
            {
                databasehelper.InsertPhoto(itm);
            }

            return true;
        }
        public async Task<bool> PersonSynch()
        {
            PersonManager manager = new PersonManager(service);
            string filterNew = "&filter=CreatedAtServer gt '" + prevdate + "'";
            string filterUpdate = "&filter=LastModifiedAtServer gt '" + prevdate + "'";
            var updatelist = databasehelper.GetPersonToUpdate(settings.LastSynched);
            // pull updated records
            Task<List<PERSON>> getupdatetask = manager.GetTasksAsync(filterUpdate);
            List<PERSON> updaterecords = await getupdatetask;
            foreach (var itm in updaterecords)
            {
                if (!updatelist.Any(item => item.PERSONID  == itm.PERSONID ))
                {
                    databasehelper.UpdatePerson(itm);
                }
            }

            // push new records
            var list = databasehelper.GetPersontoInsert(settings.LastSynched);
            if (list.Count > 0)
            {
                Task inserttask = manager.SaveTasksAsync(list, true);
                await inserttask;

            }
            //push updates
         
            if (updatelist.Count > 0)
            {
                Task pushupdatetask = manager.SaveTasksAsync(updatelist, false);
                await pushupdatetask;
            }

            // pull new records
            Task<List<PERSON>> gettask = manager.GetTasksAsync(filterNew);
            List<PERSON> newrecords = await gettask;
            foreach (var itm in newrecords)
            {
                databasehelper.InsertPerson(itm);
            }

   
            return true;
        }
        public async Task<bool> VegetationCensusSynch()
        {
            VegetationCensusManager manager = new VegetationCensusManager(service);
            string filterNew = "&filter=CreatedAtServer gt '" + prevdate + "'";
            string filterUpdate = "&filter=LastModifiedAtServer gt '" + prevdate + "'";
            var updatelist = databasehelper.GetVegetationCensusToUpdate(settings.LastSynched);
            // pull updated records
            Task<List<VEGETATIONCENSUS>> getupdatetask = manager.GetTasksAsync(filterUpdate);
            List<VEGETATIONCENSUS> updaterecords = await getupdatetask;
            foreach (var itm in updaterecords)
            {
                if (!updatelist.Any(item => item.VEGETATIONCENSUSID  == itm.VEGETATIONCENSUSID ))
                {
                    databasehelper.UpdateVegetation(itm);
                }
            }

            // push new records
            var list = databasehelper.GetVegetationCensustoInsert(settings.LastSynched);
            if (list.Count > 0)
            {
                Task inserttask = manager.SaveTasksAsync(list, true);
                await inserttask;

            }
            //push updates
           
            if (updatelist.Count > 0)
            {
                Task pushupdatetask = manager.SaveTasksAsync(updatelist, false);
                await pushupdatetask;
            }
            if (!util.DoPartialSynch)
            {
                // pull new records
                Task<List<VEGETATIONCENSUS>> gettask = manager.GetTasksAsync(filterNew);
                List<VEGETATIONCENSUS> newrecords = await gettask;
                foreach (var itm in newrecords)
                {
                    databasehelper.InsertVegetationCensus(itm);
                }
            }

            return true;
        }
        public async Task<bool> VegetationCensusSynch(String _plotid, String _origdate)
        {
            VegetationCensusManager manager = new VegetationCensusManager(service);
            string filterNew = "&filter=CreatedAtServer gt '" + _origdate + "' AND PLOTID eq '" + _plotid + "'";
            string filterUpdate = "&filter=LastModifiedAtServer gt '" + prevdate + "' AND PLOTID eq '" + _plotid + "'";

            // pull updated records
            Task<List<VEGETATIONCENSUS>> getupdatetask = manager.GetTasksAsync(filterUpdate);
            List<VEGETATIONCENSUS> updaterecords = await getupdatetask;
            foreach (var itm in updaterecords)
            {
                databasehelper.UpdateVegetation(itm);
            }

            // push new records
            var list = databasehelper.GetVegetationCensustoInsert(settings.LastSynched);
            if (list.Count > 0)
            {
                Task inserttask = manager.SaveTasksAsync(list, true);
                await inserttask;

            }
            //push updates
            var updatelist = databasehelper.GetVegetationCensusToUpdate(settings.LastSynched);
            if (updatelist.Count > 0)
            {
                Task pushupdatetask = manager.SaveTasksAsync(updatelist, false);
                await pushupdatetask;
            }
          
                // pull new records
                Task<List<VEGETATIONCENSUS>> gettask = manager.GetTasksAsync(filterNew);
                List<VEGETATIONCENSUS> newrecords = await gettask;
            foreach (var itm in newrecords)
            {
                databasehelper.InsertVegetationCensus(itm);
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
