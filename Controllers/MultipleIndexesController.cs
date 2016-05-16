using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AzureSearchStarter.DataAccess;
using AzureSearchStarter.Models;
using System.Data.Entity.Infrastructure;

namespace AzureSearchStarter.Controllers
{
    public class MultipleIndexesController : Controller
    {
        //private Microsoft.Azure.Search.SearchIndexClient localDataSourceIndex = new Microsoft.Azure.Search.SearchIndexClient("codecamp", LocalDataSourceController.search_index_name, new Microsoft.Azure.Search.SearchCredentials(LocalDataSourceController.query_key));
        //private Microsoft.Azure.Search.SearchIndexClient azureDataSourceIndex = new Microsoft.Azure.Search.SearchIndexClient("codecamp", AzureDataSourceController.search_index_name, new Microsoft.Azure.Search.SearchCredentials(LocalDataSourceController.query_key));

        public ViewResult Index(string searchString)
        {
            ViewBag.CurrentFilter = searchString;

            var sortedList = new SortedList<double, Microsoft.Azure.Search.Models.SearchResult<Models.SuperHeroModel>>();

            //if (!string.IsNullOrEmpty(searchString))
            //{
            //    string forText = searchString;
            //    if (string.IsNullOrEmpty(forText))
            //    {
            //        forText = "*";
            //    }

            //    //get the default Search Parameters
            //    Microsoft.Azure.Search.Models.SearchParameters sp = new Microsoft.Azure.Search.Models.SearchParameters();

            //    sp.IncludeTotalResultCount = true;

            //    sp.SearchMode = Microsoft.Azure.Search.Models.SearchMode.Any;

            //    sp.HighlightFields = new List<string>();
            //    sp.HighlightFields.Add("SuperHeroName");
            //    sp.HighlightFields.Add("RealName");
            //    sp.HighlightFields.Add("PowersAndAbilities");

            //    sp.HighlightPreTag = "<b>";
            //    sp.HighlightPostTag = "</b>";

            //    //initiate the query for both indexes...
            //    var localtask = this.localDataSourceIndex.Documents.SearchWithHttpMessagesAsync<AzureSearchStarter.Models.SuperHeroModel>(forText, sp);
            //    var azuretask = this.azureDataSourceIndex.Documents.SearchWithHttpMessagesAsync<AzureSearchStarter.Models.SuperHeroModel>(forText, sp);

            //    //wait til them all/both finish
            //    System.Threading.Tasks.Task.WaitAll(localtask, azuretask);

            //    //initially load up the local one first
            //    this.parseSearchResults(sortedList, localtask.Result);
            //    this.parseSearchResults(sortedList, azuretask.Result);
            //}

            return View(sortedList);
        }

        private void parseSearchResults(SortedList<double, Microsoft.Azure.Search.Models.SearchResult<Models.SuperHeroModel>> sortedList, 
                                        Microsoft.Rest.Azure.AzureOperationResponse<Microsoft.Azure.Search.Models.DocumentSearchResult<AzureSearchStarter.Models.SuperHeroModel>> result)
        {
            if (result != null && result.Body != null && result.Body.Count > 0 && result.Body.Results != null && result.Body.Results.Count > 0)
            {
                foreach (var item in result.Body.Results)
                {
                    var scoreKey = item.Score;
                    while (sortedList.ContainsKey(scoreKey))
                    {
                        scoreKey += 0.000001;
                    }
                    sortedList.Add(scoreKey, item);
                }
            }
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //this.localDataSourceIndex.Dispose();
                //this.azureDataSourceIndex.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
