using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using eLiDAR.Models;


namespace eLiDAR.API
{
	public class ProjectManager
	{
		IRestService restService;
		private static string tablename = "project";

		public ProjectManager(IRestService service)
		{
			restService = service;
		}

		public Task<List<PROJECT>> GetTasksAsync( string filter)
		{
			return restService.GetCurrentProjectListAsync(tablename, filter);
		}

		public Task SaveTasksAsync(List<PROJECT> items, bool isNewItem = false)
		{
			return restService.PushProjectAsync(items, isNewItem);
		}

		public Task DeleteTaskAsync(PROJECT item)
		{
			return restService.DeleteAsync(tablename,item.PROJECTID);
		}
	}
	public class PlotManager
	{
		IRestService restService;
		private static string tablename = "plot";

		public PlotManager(IRestService service)
		{
			restService = service;
		}

		public Task<List<PLOT>> GetTasksAsync(string filter)
		{
			return restService.GetCurrentPlotListAsync(tablename, filter);
		}

		public Task SaveTasksAsync(List<PLOT> items, bool isNewItem = false)
		{
			return restService.PushPlotAsync(items, isNewItem);
		}

		public Task DeleteTaskAsync(PLOT item)
		{
			return restService.DeleteAsync(tablename, item.PLOTID);
		}
	}
	public class TreeManager
	{
		IRestService restService;
		private static string tablename = "tree";

		public TreeManager(IRestService service)
		{
			restService = service;
		}

		public Task<List<TREE>> GetTasksAsync(string filter)
		{
			return restService.GetCurrentTreeListAsync(tablename, filter);
		}

		public Task SaveTasksAsync(List<TREE> items, bool isNewItem = false)
		{
			return restService.PushTreeAsync(items, isNewItem);
		}

		public Task DeleteTaskAsync(TREE item)
		{
			return restService.DeleteAsync(tablename, item.TREEID);
		}
	}
	public class StemmapManager
	{
		IRestService restService;
		private static string tablename = "stemmap";

		public StemmapManager(IRestService service)
		{
			restService = service;
		}

		public Task<List<STEMMAP>> GetTasksAsync(string filter)
		{
			return restService.GetCurrentSTEMMAPListAsync(tablename, filter);
		}

		public Task SaveTasksAsync(List<STEMMAP> items, bool isNewItem = false)
		{
			return restService.PushSTEMMAPAsync(items, isNewItem);
		}

		public Task DeleteTaskAsync(STEMMAP item)
		{
			return restService.DeleteAsync(tablename, item.STEMMAPID);
		}
	}
	public class EcositeManager
	{
		IRestService restService;
		private static string tablename = "ecosite";

		public EcositeManager(IRestService service)
		{
			restService = service;
		}

		public Task<List<ECOSITE>> GetTasksAsync(string filter)
		{
			return restService.GetCurrentECOSITEListAsync(tablename, filter);
		}

		public Task SaveTasksAsync(List<ECOSITE> items, bool isNewItem = false)
		{
			return restService.PushECOSITEAsync(items, isNewItem);
		}

		public Task DeleteTaskAsync(ECOSITE item)
		{
			return restService.DeleteAsync(tablename, item.ECOSITEID);
		}
	}
	public class SoilManager
	{
		IRestService restService;
		private static string tablename = "soil";

		public SoilManager(IRestService service)
		{
			restService = service;
		}

		public Task<List<SOIL>> GetTasksAsync(string filter)
		{
			return restService.GetCurrentSOILListAsync(tablename, filter);
		}

		public Task SaveTasksAsync(List<SOIL> items, bool isNewItem = false)
		{
			return restService.PushSOILAsync(items, isNewItem);
		}

		public Task DeleteTaskAsync(SOIL item)
		{
			return restService.DeleteAsync(tablename, item.SOILID);
		}
	}
	public class SmalltreeManager
	{
		IRestService restService;
		private static string tablename = "smalltree";

		public SmalltreeManager(IRestService service)
		{
			restService = service;
		}

		public Task<List<SMALLTREE>> GetTasksAsync(string filter)
		{
			return restService.GetCurrentSMALLTREEListAsync(tablename, filter);
		}

		public Task SaveTasksAsync(List<SMALLTREE> items, bool isNewItem = false)
		{
			return restService.PushSMALLTREEAsync(items, isNewItem);
		}

		public Task DeleteTaskAsync(SMALLTREE item)
		{
			return restService.DeleteAsync(tablename, item.SMALLTREEID);
		}
	}
	public class VegetationManager
	{
		IRestService restService;
		private static string tablename = "vegetation";

		public VegetationManager(IRestService service)
		{
			restService = service;
		}

		public Task<List<VEGETATION>> GetTasksAsync(string filter)
		{
			return restService.GetCurrentVEGETATIONListAsync(tablename, filter);
		}

		public Task SaveTasksAsync(List<VEGETATION> items, bool isNewItem = false)
		{
			return restService.PushVEGETATIONAsync(items, isNewItem);
		}

		public Task DeleteTaskAsync(VEGETATION item)
		{
			return restService.DeleteAsync(tablename, item.VEGETATIONID);
		}
	}
	public class DeformityManager
	{
		IRestService restService;
		private static string tablename = "deformity";

		public DeformityManager(IRestService service)
		{
			restService = service;
		}

		public Task<List<DEFORMITY>> GetTasksAsync(string filter)
		{
			return restService.GetCurrentDEFORMITYListAsync(tablename, filter);
		}

		public Task SaveTasksAsync(List<DEFORMITY> items, bool isNewItem = false)
		{
			return restService.PushDEFORMITYAsync(items, isNewItem);
		}

		public Task DeleteTaskAsync(DEFORMITY item)
		{
			return restService.DeleteAsync(tablename, item.DEFORMITYID);
		}
	}
	public class DWDManager
	{
		IRestService restService;
		private static string tablename = "dwd";

		public DWDManager(IRestService service)
		{
			restService = service;
		}

		public Task<List<DWD>> GetTasksAsync(string filter)
		{
			return restService.GetCurrentDWDListAsync(tablename, filter);
		}

		public Task SaveTasksAsync(List<DWD> items, bool isNewItem = false)
		{
			return restService.PushDWDAsync(items, isNewItem);
		}

		public Task DeleteTaskAsync(DWD item)
		{
			return restService.DeleteAsync(tablename, item.DWDID);
		}
	}
	public class PhotoManager
	{
		IRestService restService;
		private static string tablename = "photo";

		public PhotoManager(IRestService service)
		{
			restService = service;
		}

		public Task<List<PHOTO>> GetTasksAsync(string filter)
		{
			return restService.GetCurrentPhotoListAsync(tablename, filter);
		}

		public Task SaveTasksAsync(List<PHOTO> items, bool isNewItem = false)
		{
			return restService.PushPhotoAsync(items, isNewItem);
		}

		public Task DeleteTaskAsync(PHOTO item)
		{
			return restService.DeleteAsync(tablename, item.PHOTOID);
		}
	}
	public class PersonManager
	{
		IRestService restService;
		private static string tablename = "person";

		public PersonManager(IRestService service)
		{
			restService = service;
		}

		public Task<List<PERSON>> GetTasksAsync(string filter)
		{
			return restService.GetCurrentPersonListAsync(tablename, filter);
		}

		public Task SaveTasksAsync(List<PERSON> items, bool isNewItem = false)
		{
			return restService.PushPersonAsync(items, isNewItem);
		}

		public Task DeleteTaskAsync(PERSON item)
		{
			return restService.DeleteAsync(tablename, item.PERSONID);
		}
	}
	public class VegetationCensusManager
	{
		IRestService restService;
		private static string tablename = "vegetationcensus";

		public VegetationCensusManager(IRestService service)
		{
			restService = service;
		}

		public Task<List<VEGETATIONCENSUS>> GetTasksAsync(string filter)
		{
			return restService.GetCurrentVegetationCensusListAsync(tablename, filter);
		}

		public Task SaveTasksAsync(List<VEGETATIONCENSUS> items, bool isNewItem = false)
		{
			return restService.PushVegetationCensusAsync(items, isNewItem);
		}

		public Task DeleteTaskAsync(VEGETATIONCENSUS item)
		{
			return restService.DeleteAsync(tablename, item.VEGETATIONCENSUSID);
		}
	}
}

