using eLiDAR.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace eLiDAR.API
{
	public interface IRestService
	{
		Task<List<PROJECT>> GetCurrentProjectListAsync(string table, string filter);
		Task PushProjectAsync(List<PROJECT> items, bool isNewItem);
		Task DeleteAsync(string table, string id);
        Task<List<PLOT>> GetCurrentPlotListAsync(string table, string filter);
        Task PushPlotAsync(List<PLOT> items, bool isNewItem);
        Task<List<TREE>> GetCurrentTreeListAsync(string table, string filter);
        Task PushTreeAsync(List<TREE> items, bool isNewItem);
        Task<List<STEMMAP>> GetCurrentSTEMMAPListAsync(string table, string filter);
        Task PushSTEMMAPAsync(List<STEMMAP> items, bool isNewItem);
        Task<List<ECOSITE>> GetCurrentECOSITEListAsync(string table, string filter);
        Task PushECOSITEAsync(List<ECOSITE> items, bool isNewItem);
        Task<List<SOIL>> GetCurrentSOILListAsync(string table, string filter);
        Task PushSOILAsync(List<SOIL> items, bool isNewItem);
        Task<List<SMALLTREE>> GetCurrentSMALLTREEListAsync(string table, string filter);
        
        Task PushSMALLTREEAsync(List<SMALLTREE> items, bool isNewItem);
        Task<List<VEGETATION>> GetCurrentVEGETATIONListAsync(string table, string filter);
        
        Task PushVEGETATIONAsync(List<VEGETATION> items, bool isNewItem);
        Task<List<DEFORMITY>> GetCurrentDEFORMITYListAsync(string table, string filter);
        Task PushDEFORMITYAsync(List<DEFORMITY> items, bool isNewItem);
        Task<List<DWD>> GetCurrentDWDListAsync(string table, string filter);
        Task PushDWDAsync(List<DWD> items, bool isNewItem);
        Task<List<PHOTO>> GetCurrentPhotoListAsync(string table, string filter);
        Task PushPhotoAsync(List<PHOTO> items, bool isNewItem);
        Task<List<PERSON>> GetCurrentPersonListAsync(string table, string filter);
        Task PushPersonAsync(List<PERSON> items, bool isNewItem);
        Task<List<VEGETATIONCENSUS>> GetCurrentVegetationCensusListAsync(string table, string filter);
        Task PushVegetationCensusAsync(List<VEGETATIONCENSUS> items, bool isNewItem);
        
        Task<List<SMALLTREETALLY>> GetCurrentSMALLTREETALLYListAsync(string table, string filter);
        Task PushSMALLTREETALLYAsync(List<SMALLTREETALLY> items, bool isNewItem);

    }

    public class RestService : IRestService
    {
        HttpClient _client;
        private bool _isSuccess = true;
        private string _msg;
       // private Constants cont = new Constants();
        private Utilities.Utils util = new Utilities.Utils();
        private string APIGetUrl;
        private string APIPutUrl;
        private string APIPostUrl;

        //  public List<PROJECT> Items { get; private set; }

        public RestService()
        {
            _client = new HttpClient();
            //  ADD IN HEADERS TO THE HTTP REQUEST - CONVERT THIS TO A HASH TO PROTECT IT
            _client.DefaultRequestHeaders.Add(Constants.Azuresubscriptionkey, util.KEY);
            APIGetUrl = util.GetURI;
            APIPutUrl = util.PutURI;
            APIPostUrl = util.PostURI;
        }
        public bool IsSuccess
        {
            get { return _isSuccess; }
            set { _isSuccess = value; }
        }
        public string Msg
        {
            get { return _msg; }
            set { _msg = value; }

        }

        public async Task<List<PROJECT>> GetCurrentProjectListAsync(string table,string filter)
        {
            List<PROJECT> Items = new List<PROJECT>();
            var uri = new Uri(string.Format(APIGetUrl, table, filter));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<PROJECT>>(content);
                }
                else
                {
                    IsSuccess = false;
                   // Msg = "Project dataex.Message;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                IsSuccess = false;
                Msg = ex.Message; 
            }
            return Items;
        }

        public async Task PushProjectAsync(List<PROJECT> items, bool isNewItem = false)
        {
            try
            {
                var table = "project";
                var json = JsonConvert.SerializeObject(items);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
              
                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    var uri = new Uri(string.Format(APIPostUrl, table));
                    response = await _client.PostAsync(uri, content);
                }
                else
                {
                    var uri = new Uri(string.Format(APIPutUrl, table));
                    response = await _client.PutAsync(uri, content);
                }
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\Items successfully saved.");
                }
                else
                {
                    IsSuccess = false;
                    Msg = "Project data did not push";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                IsSuccess = false;
                Msg = ex.Message;

            }
        }

        public async Task DeleteAsync(string table, string id)
        {
            var uri = new Uri(string.Format(Constants.APIDeleteUrl, table, id));

            try
            {
                var response = await _client.DeleteAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tItems successfully deleted.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }

        public async Task<List<PLOT>> GetCurrentPlotListAsync(string table, string filter)
        {
            List<PLOT> Items = new List<PLOT>();
            var uri = new Uri(string.Format(APIGetUrl, table, filter));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<PLOT>>(content);
                }
                else
                {
                    IsSuccess = false;
                    Msg = "Plot data did not serialize";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                IsSuccess = false;
                Msg = ex.Message;

            }
            return Items;
        }
 

        public async Task PushPlotAsync(List<PLOT> items, bool isNewItem)
        {
            try
            {
                var table = "plot";
                var json = JsonConvert.SerializeObject(items);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    var uri = new Uri(string.Format(APIPostUrl, table));
                    response = await _client.PostAsync(uri, content);
                }
                else
                {
                    var uri = new Uri(string.Format(APIPutUrl, table));
                    response = await _client.PutAsync(uri, content);
                }
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\Items successfully saved.");
                }
                else
                {
                    IsSuccess = false;
                    Msg = "Plot data did not serialize";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                IsSuccess = false;
                Msg = ex.Message;

            }
        }

        public async Task<List<TREE>> GetCurrentTreeListAsync(string table, string filter)
        {
        List<TREE> Items = new List<TREE>();
        var uri = new Uri(string.Format(APIGetUrl, table, filter));
        try
        {
            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Items = JsonConvert.DeserializeObject<List<TREE>>(content);
            }
            else
                {
                    IsSuccess = false;
                    Msg = "Tree data did not serialize";
                }
            }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
            IsSuccess = false;
            Msg = ex.Message;

            }
            return Items;
    }

        public async Task PushTreeAsync(List<TREE> items, bool isNewItem)
        {
            try
            {
                var table = "tree";
                var json = JsonConvert.SerializeObject(items);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    var uri = new Uri(string.Format(APIPostUrl, table));
                    response = await _client.PostAsync(uri, content);
                }
                else
                {
                    var uri = new Uri(string.Format(APIPutUrl, table));
                    response = await _client.PutAsync(uri, content);
                }
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\Items successfully saved.");
                }
                else
                {
                    IsSuccess = false;
                    Msg = "Tree data did not push";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                IsSuccess = false;
                Msg = ex.Message;

            }
        }

        public async Task<List<STEMMAP>> GetCurrentSTEMMAPListAsync(string table, string filter)
        {
            List<STEMMAP> Items = new List<STEMMAP>();
            var uri = new Uri(string.Format(APIGetUrl, table, filter));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<STEMMAP>>(content);
                }
                else
                {
                    IsSuccess = false;
                    Msg = "Stem map data did not serialize";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                IsSuccess = false;
                Msg = "Stem map data did not serialize";
            }
            return Items;
        }

        public async Task PushSTEMMAPAsync(List<STEMMAP> items, bool isNewItem)
        {
            try
            {
                var table = "stemmap";
                var json = JsonConvert.SerializeObject(items);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    var uri = new Uri(string.Format(APIPostUrl, table));
                    response = await _client.PostAsync(uri, content);
                }
                else
                {
                    var uri = new Uri(string.Format(APIPutUrl, table));
                    response = await _client.PutAsync(uri, content);
                }
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\Items successfully saved.");
                }
                else
                {
                    IsSuccess = false;
                    Msg = "Stem map data did not push";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                IsSuccess = false;
                Msg = "Stem map data did not push";
            }
        }

        public async Task<List<ECOSITE>> GetCurrentECOSITEListAsync(string table, string filter)
        {
            List<ECOSITE> Items = new List<ECOSITE>();
            var uri = new Uri(string.Format(APIGetUrl, table, filter));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<ECOSITE>>(content);
                }
                else
                {
                    IsSuccess = false;
                    Msg = "Ecosite data did not serialize";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                IsSuccess = false;
                Msg = "Ecosite data did not serialize";
            }
            return Items;
        }

        public async Task PushECOSITEAsync(List<ECOSITE> items, bool isNewItem)
        {
            try
            {
                var table = "ecosite";
                var json = JsonConvert.SerializeObject(items);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    var uri = new Uri(string.Format(APIPostUrl, table));
                    response = await _client.PostAsync(uri, content);
                }
                else
                {
                    var uri = new Uri(string.Format(APIPutUrl, table));
                    response = await _client.PutAsync(uri, content);
                }
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\Items successfully saved.");
                }
                else
                {
                    IsSuccess = false;
                    Msg = "Ecosite data did not push";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                IsSuccess = false;
                Msg = ex.Message;
            }
        }

        public async Task<List<SOIL>> GetCurrentSOILListAsync(string table, string filter)
        {
            List<SOIL> Items = new List<SOIL>();
            var uri = new Uri(string.Format(APIGetUrl, table, filter));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<SOIL>>(content);
                }
                else
                {
                    IsSuccess = false;
                    Msg = "Soil data did not serialize";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                IsSuccess = false;
                Msg = ex.Message;
            }
            return Items;
        }

        public async Task PushSOILAsync(List<SOIL> items, bool isNewItem)
        {
            try
            {
                var table = "soil";
                var json = JsonConvert.SerializeObject(items);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    var uri = new Uri(string.Format(APIPostUrl, table));
                    response = await _client.PostAsync(uri, content);
                }
                else
                {
                    var uri = new Uri(string.Format(APIPutUrl, table));
                    response = await _client.PutAsync(uri, content);
                }
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\Items successfully saved.");
                }
                else
                {
                    IsSuccess = false;
                    Msg = "Soil data did not push";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                IsSuccess = false;
                Msg = ex.Message;
            }
        }

        public async Task<List<SMALLTREE>> GetCurrentSMALLTREEListAsync(string table, string filter)
        {
            List<SMALLTREE> Items = new List<SMALLTREE>();
            var uri = new Uri(string.Format(APIGetUrl, table, filter));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<SMALLTREE>>(content);
                }
                else
                {
                    IsSuccess = false;
                    Msg = "Small tree data did not serialize";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                IsSuccess = false;
                Msg = ex.Message;
            }
            return Items;
        }

        public async Task PushSMALLTREEAsync(List<SMALLTREE> items, bool isNewItem)
        {
            try
            {
                var table = "smalltree";
                var json = JsonConvert.SerializeObject(items);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    var uri = new Uri(string.Format(APIPostUrl, table));
                    response = await _client.PostAsync(uri, content);
                }
                else
                {
                    var uri = new Uri(string.Format(APIPutUrl, table));
                    response = await _client.PutAsync(uri, content);
                }
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\Items successfully saved.");
                }
                else
                {
                    IsSuccess = false;
                    Msg = "Small tree data did not push";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                IsSuccess = false;
                Msg = ex.Message;
            }
        }

        // create functions for SmallTreeTally based off of the above
        public async Task<List<SMALLTREETALLY>> GetCurrentSMALLTREETALLYListAsync(string table, string filter)
        {
            List<SMALLTREETALLY> Items = new List<SMALLTREETALLY>();
            var uri = new Uri(string.Format(APIGetUrl, table, filter));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<SMALLTREETALLY>>(content);
                }
                else
                {
                    IsSuccess = false;
                    Msg = "Small tree tally data did not serialize";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                IsSuccess = false;
                Msg = ex.Message;
            }
            return Items;
        }

        public async Task PushSMALLTREETALLYAsync(List<SMALLTREETALLY> items, bool isNewItem)
        {
            try
            {
                var table = "smalltreetally";
                var json = JsonConvert.SerializeObject(items);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    var uri = new Uri(string.Format(APIPostUrl, table));
                    response = await _client.PostAsync(uri, content);
                }
                else
                {
                    var uri = new Uri(string.Format(APIPutUrl, table));
                    response = await _client.PutAsync(uri, content);
                }
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\Items successfully saved.");
                }
                else
                {
                    IsSuccess = false;
                    Msg = "Small tree tally data did not push";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                IsSuccess = false;
                Msg = ex.Message;
            }
        }



        public async Task<List<VEGETATION>> GetCurrentVEGETATIONListAsync(string table, string filter)
        {
            List<VEGETATION> Items = new List<VEGETATION>();
            var uri = new Uri(string.Format(APIGetUrl, table, filter));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<VEGETATION>>(content);
                }
                else
                {
                    IsSuccess = false;
                    Msg = "Veg data did not serialize";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                IsSuccess = false;
                Msg = ex.Message;
            }
            return Items;
        }

        public async Task PushVEGETATIONAsync(List<VEGETATION> items, bool isNewItem)
        {
            try
            {
                var table = "vegetation";
                var json = JsonConvert.SerializeObject(items);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    var uri = new Uri(string.Format(APIPostUrl, table));
                    response = await _client.PostAsync(uri, content);
                }
                else
                {
                    var uri = new Uri(string.Format(APIPutUrl, table));
                    response = await _client.PutAsync(uri, content);
                }
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\Items successfully saved.");
                }
                else
                {
                    IsSuccess = false;
                    Msg = "Veg data did not push";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                IsSuccess = false;
                Msg = ex.Message;
            }
        }

        public async Task<List<DEFORMITY>> GetCurrentDEFORMITYListAsync(string table, string filter)
        {
            List<DEFORMITY> Items = new List<DEFORMITY>();
            var uri = new Uri(string.Format(APIGetUrl, table, filter));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<DEFORMITY>>(content);
                }
                else
                {
                    IsSuccess = false;
                    Msg = "Deformity data did not serialize";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                IsSuccess = false;
                Msg = ex.Message;
            }
            return Items;
        }

        public async Task PushDEFORMITYAsync(List<DEFORMITY> items, bool isNewItem)
        {
            try
            {
                var table = "deformity";
                var json = JsonConvert.SerializeObject(items);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    var uri = new Uri(string.Format(APIPostUrl, table));
                    response = await _client.PostAsync(uri, content);
                }
                else
                {
                    var uri = new Uri(string.Format(APIPutUrl, table));
                    response = await _client.PutAsync(uri, content);
                }
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\Items successfully saved.");
                }
                else
                {
                    IsSuccess = false;
                    Msg = "Deformity data did not push";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                IsSuccess = false;
                Msg = ex.Message ;
            }
        }

        public async Task<List<DWD>> GetCurrentDWDListAsync(string table, string filter)
        {
            List<DWD> Items = new List<DWD>();
            var uri = new Uri(string.Format(APIGetUrl, table, filter));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<DWD>>(content);
                }
                else
                {
                    IsSuccess = false;
                    Msg = "DWD data did not serialize";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                IsSuccess = false;
                Msg = ex.Message;
            }
            return Items;
        }

        public async Task PushDWDAsync(List<DWD> items, bool isNewItem)
        {
            try
            {
                var table = "dwd";
                var json = JsonConvert.SerializeObject(items);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    var uri = new Uri(string.Format(APIPostUrl, table));
                    response = await _client.PostAsync(uri, content);
                }
                else
                {
                    var uri = new Uri(string.Format(APIPutUrl, table));
                    response = await _client.PutAsync(uri, content);
                }
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\Items successfully saved.");
                }
                else
                {
                    IsSuccess = false;
                    Msg = "DWD data did not push";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                IsSuccess = false;
                Msg = ex.Message;
            }
        }
        public async Task<List<PERSON>> GetCurrentPersonListAsync(string table, string filter)
        {
            List<PERSON> Items = new List<PERSON>();
            var uri = new Uri(string.Format(APIGetUrl, table, filter));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<PERSON>>(content);
                }
                else
                {
                    IsSuccess = false;
                    Msg = "Person data did not serialize";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
              
                    IsSuccess = false;
                    Msg = ex.Message ;
                
            }
            return Items;
        }

        public async Task PushPersonAsync(List<PERSON> items, bool isNewItem)
        {
            try
            {
                var table = "person";
                var json = JsonConvert.SerializeObject(items);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    var uri = new Uri(string.Format(APIPostUrl, table));
                    response = await _client.PostAsync(uri, content);
                }
                else
                {
                    var uri = new Uri(string.Format(APIPutUrl, table));
                    response = await _client.PutAsync(uri, content);
                }
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\Items successfully saved.");
                }
                else
                {
                    IsSuccess = false;
                    Msg = "Person data did not push";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                IsSuccess = false;
                Msg = ex.Message;
            }
        }
        public async Task<List<PHOTO>> GetCurrentPhotoListAsync(string table, string filter)
        {
            List<PHOTO> Items = new List<PHOTO>();
            var uri = new Uri(string.Format(APIGetUrl, table, filter));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<PHOTO>>(content);
                }
                else
                {
                    IsSuccess = false;
                    Msg = "Photo data did not serialize";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                IsSuccess = false;
                Msg = ex.Message;
            }
            return Items;
        }

        public async Task PushPhotoAsync(List<PHOTO> items, bool isNewItem)
        {
            try
            {
                var table = "photo";
                var json = JsonConvert.SerializeObject(items);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    var uri = new Uri(string.Format(APIPostUrl, table));
                    response = await _client.PostAsync(uri, content);
                }
                else
                {
                    var uri = new Uri(string.Format(APIPutUrl, table));
                    response = await _client.PutAsync(uri, content);
                }
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\Items successfully saved.");
                }
                else
                {
                    IsSuccess = false;
                    Msg = "Photo data did not push";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                IsSuccess = false;
                Msg =ex.Message;
            }
        }
        public async Task<List<VEGETATIONCENSUS>> GetCurrentVegetationCensusListAsync(string table, string filter)
        {
            List<VEGETATIONCENSUS> Items = new List<VEGETATIONCENSUS>();
            var uri = new Uri(string.Format(APIGetUrl, table, filter));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<VEGETATIONCENSUS>>(content);
                }
                else
                {
                    IsSuccess = false;
                    Msg = "Veg census data did not serialize";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                IsSuccess = false;
                Msg = ex.Message;
            
        }
            return Items;
        }

        public async Task PushVegetationCensusAsync(List<VEGETATIONCENSUS> items, bool isNewItem)
        {
            try
            {
                var table = "vegetationcensus";
                var json = JsonConvert.SerializeObject(items);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    var uri = new Uri(string.Format(APIPostUrl, table));
                    response = await _client.PostAsync(uri, content);
                }
                else
                {
                    var uri = new Uri(string.Format(APIPutUrl, table));
                    response = await _client.PutAsync(uri, content);
                }
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\Items successfully saved.");
                }
                else
                {
                    IsSuccess = false;
                    Msg = "Veg census data did not push";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                IsSuccess = false;
                Msg = ex.Message;
            }
        }
    }

}