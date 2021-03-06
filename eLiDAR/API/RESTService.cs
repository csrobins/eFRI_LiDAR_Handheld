﻿using eLiDAR.Models;
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

    }

    public class RestService : IRestService
    {
        HttpClient _client;

      //  public List<PROJECT> Items { get; private set; }

        public RestService()
        {
            _client = new HttpClient();
        }

        public async Task<List<PROJECT>> GetCurrentProjectListAsync(string table,string filter)
        {
            List<PROJECT> Items = new List<PROJECT>();
             var uri = new Uri(string.Format(Constants.APIGetUrl, table, filter));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<PROJECT>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
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
                    var uri = new Uri(string.Format(Constants.APIPostUrl, table));
                    response = await _client.PostAsync(uri, content);
                }
                else
                {
                    var uri = new Uri(string.Format(Constants.APIPutUrl, table));
                    response = await _client.PutAsync(uri, content);
                }
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\Items successfully saved.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
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
            var uri = new Uri(string.Format(Constants.APIGetUrl, table, filter));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<PLOT>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
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
                    var uri = new Uri(string.Format(Constants.APIPostUrl, table));
                    response = await _client.PostAsync(uri, content);
                }
                else
                {
                    var uri = new Uri(string.Format(Constants.APIPutUrl, table));
                    response = await _client.PutAsync(uri, content);
                }
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\Items successfully saved.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }

        public async Task<List<TREE>> GetCurrentTreeListAsync(string table, string filter)
        {
        List<TREE> Items = new List<TREE>();
        var uri = new Uri(string.Format(Constants.APIGetUrl, table, filter));
        try
        {
            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Items = JsonConvert.DeserializeObject<List<TREE>>(content);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
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
                    var uri = new Uri(string.Format(Constants.APIPostUrl, table));
                    response = await _client.PostAsync(uri, content);
                }
                else
                {
                    var uri = new Uri(string.Format(Constants.APIPutUrl, table));
                    response = await _client.PutAsync(uri, content);
                }
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\Items successfully saved.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }

        public async Task<List<STEMMAP>> GetCurrentSTEMMAPListAsync(string table, string filter)
        {
            List<STEMMAP> Items = new List<STEMMAP>();
            var uri = new Uri(string.Format(Constants.APIGetUrl, table, filter));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<STEMMAP>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
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
                    var uri = new Uri(string.Format(Constants.APIPostUrl, table));
                    response = await _client.PostAsync(uri, content);
                }
                else
                {
                    var uri = new Uri(string.Format(Constants.APIPutUrl, table));
                    response = await _client.PutAsync(uri, content);
                }
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\Items successfully saved.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }

        public async Task<List<ECOSITE>> GetCurrentECOSITEListAsync(string table, string filter)
        {
            List<ECOSITE> Items = new List<ECOSITE>();
            var uri = new Uri(string.Format(Constants.APIGetUrl, table, filter));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<ECOSITE>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
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
                    var uri = new Uri(string.Format(Constants.APIPostUrl, table));
                    response = await _client.PostAsync(uri, content);
                }
                else
                {
                    var uri = new Uri(string.Format(Constants.APIPutUrl, table));
                    response = await _client.PutAsync(uri, content);
                }
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\Items successfully saved.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }

        public async Task<List<SOIL>> GetCurrentSOILListAsync(string table, string filter)
        {
            List<SOIL> Items = new List<SOIL>();
            var uri = new Uri(string.Format(Constants.APIGetUrl, table, filter));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<SOIL>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
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
                    var uri = new Uri(string.Format(Constants.APIPostUrl, table));
                    response = await _client.PostAsync(uri, content);
                }
                else
                {
                    var uri = new Uri(string.Format(Constants.APIPutUrl, table));
                    response = await _client.PutAsync(uri, content);
                }
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\Items successfully saved.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }

        public async Task<List<SMALLTREE>> GetCurrentSMALLTREEListAsync(string table, string filter)
        {
            List<SMALLTREE> Items = new List<SMALLTREE>();
            var uri = new Uri(string.Format(Constants.APIGetUrl, table, filter));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<SMALLTREE>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
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
                    var uri = new Uri(string.Format(Constants.APIPostUrl, table));
                    response = await _client.PostAsync(uri, content);
                }
                else
                {
                    var uri = new Uri(string.Format(Constants.APIPutUrl, table));
                    response = await _client.PutAsync(uri, content);
                }
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\Items successfully saved.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }

        public async Task<List<VEGETATION>> GetCurrentVEGETATIONListAsync(string table, string filter)
        {
            List<VEGETATION> Items = new List<VEGETATION>();
            var uri = new Uri(string.Format(Constants.APIGetUrl, table, filter));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<VEGETATION>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
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
                    var uri = new Uri(string.Format(Constants.APIPostUrl, table));
                    response = await _client.PostAsync(uri, content);
                }
                else
                {
                    var uri = new Uri(string.Format(Constants.APIPutUrl, table));
                    response = await _client.PutAsync(uri, content);
                }
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\Items successfully saved.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }

        public async Task<List<DEFORMITY>> GetCurrentDEFORMITYListAsync(string table, string filter)
        {
            List<DEFORMITY> Items = new List<DEFORMITY>();
            var uri = new Uri(string.Format(Constants.APIGetUrl, table, filter));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<DEFORMITY>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
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
                    var uri = new Uri(string.Format(Constants.APIPostUrl, table));
                    response = await _client.PostAsync(uri, content);
                }
                else
                {
                    var uri = new Uri(string.Format(Constants.APIPutUrl, table));
                    response = await _client.PutAsync(uri, content);
                }
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\Items successfully saved.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }

        public async Task<List<DWD>> GetCurrentDWDListAsync(string table, string filter)
        {
            List<DWD> Items = new List<DWD>();
            var uri = new Uri(string.Format(Constants.APIGetUrl, table, filter));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<DWD>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
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
                    var uri = new Uri(string.Format(Constants.APIPostUrl, table));
                    response = await _client.PostAsync(uri, content);
                }
                else
                {
                    var uri = new Uri(string.Format(Constants.APIPutUrl, table));
                    response = await _client.PutAsync(uri, content);
                }
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\Items successfully saved.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }
    }
}