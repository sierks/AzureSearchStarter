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
    public class AzureDataSourceController : Controller
    {
        private AzureDataSourceContext db = new AzureDataSourceContext();

        //public const string search_index_name = "dc";

        //private Microsoft.Azure.Search.SearchServiceClient adminService = new Microsoft.Azure.Search.SearchServiceClient("codecamp", new Microsoft.Azure.Search.SearchCredentials(LocalDataSourceController.admin_key));

        //private Microsoft.Azure.Search.SearchIndexClient adminIndex = new Microsoft.Azure.Search.SearchIndexClient("codecamp", search_index_name, new Microsoft.Azure.Search.SearchCredentials(LocalDataSourceController.admin_key));

        //private Microsoft.Azure.Search.SearchIndexClient queryIndex = new Microsoft.Azure.Search.SearchIndexClient("codecamp", search_index_name, new Microsoft.Azure.Search.SearchCredentials(LocalDataSourceController.query_key));


        public ViewResult Index(string sortOrder, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.SuperHeroNameSortParm = String.IsNullOrEmpty(sortOrder) ? "SuperHeroName_desc" : "";
            ViewBag.RealNameSortParm = sortOrder == "RealName" ? "RealName_desc" : "RealName";

            var heroes = from s in db.DCSuperHeroes
                         select s;
            switch (sortOrder)
            {
                case "SuperHeroName_desc":
                    heroes = heroes.OrderByDescending(s => s.SuperHeroName);
                    break;
                case "RealName":
                    heroes = heroes.OrderBy(s => s.RealName);
                    break;
                case "RealName_desc":
                    heroes = heroes.OrderByDescending(s => s.RealName);
                    break;
                default:  // SuperHeroName ascending 
                    heroes = heroes.OrderBy(s => s.SuperHeroName);
                    break;
            }

            return View(heroes.ToList());
        }

        public ViewResult Search(string searchString)
        {
            ViewBag.CurrentFilter = searchString;
            
            string forText = searchString;
            if (string.IsNullOrEmpty(forText))
            {
                forText = "*";
            }

            //get the default Search Parameters
            Microsoft.Azure.Search.Models.SearchParameters sp = new Microsoft.Azure.Search.Models.SearchParameters();

            sp.IncludeTotalResultCount = true;

            sp.SearchMode = Microsoft.Azure.Search.Models.SearchMode.Any;
            //  Microsoft.Azure.Search.Models.SearchMode.All = search for all the queried words
            //  Microsoft.Azure.Search.Models.SearchMode.Any = any of the queried words/phrases



            //sp.HighlightFields = new List<string>();
            //foreach (var field in this.adminService.Indexes.GetWithHttpMessagesAsync(search_index_name).Result.Body.Fields)
            //{
            //    if (field.IsRetrievable && field.IsSearchable)
            //        sp.HighlightFields.Add(field.Name);
            //}
            //sp.HighlightPreTag = "<b>";
            //sp.HighlightPostTag = "</b>";



            //sp.MinimumCoverage = the % of the index to be searched before returning results; defaults to 100%
            //sp.OrderBy = a list of Order By Statements; sorting defaults to search score
            //sp.ScoringProfile = allows you to specify which scoring profile you want to use, otherwise uses the default
            
            //these items can be used for paging
            //  sp.Skip = how many results have already been retrieved/displayed?
            //  sp.Top = how many results should be retrieved/displayed

            //here is the Filter, where you could filter by User, Store, etc.
            //  spUser.Filter += " User eq " + this.User.Identity.Name;

            
            //var async = this.queryIndex.Documents.SearchWithHttpMessagesAsync<AzureSearchStarter.Models.SuperHeroModel>(forText, sp);
            //Microsoft.Rest.Azure.AzureOperationResponse<Microsoft.Azure.Search.Models.DocumentSearchResult<AzureSearchStarter.Models.SuperHeroModel>> result = async.Result;
            //return View(result);

            return View();
        }


        public ActionResult Create()
        {
            return View();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SuperHeroName, RealName, PowersAndAbilities")]SuperHeroModel hero)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    hero.SuperHeroModelID = Guid.NewGuid().ToString();

                    db.DCSuperHeroes.Add(hero);
                    db.SaveChanges();


                    //var batch = Microsoft.Azure.Search.Models.IndexBatch.MergeOrUpload(new[] { hero });
                    //var async = this.adminIndex.Documents.IndexWithHttpMessagesAsync(batch);
                    ////var result = async.Result;


                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(hero);
        }


        public ActionResult Edit(string SuperHeroModelID)
        {
            if (SuperHeroModelID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuperHeroModel hero = db.DCSuperHeroes.Find(SuperHeroModelID);
            if (hero == null)
            {
                return HttpNotFound();
            }
            return View(hero);
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(string SuperHeroModelID)
        {
            if (SuperHeroModelID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var heroToUpdate = db.DCSuperHeroes.Find(SuperHeroModelID);
            if (TryUpdateModel(heroToUpdate, "",
               new string[] { "SuperHeroName", "RealName", "PowersAndAbilities" }))
            {
                try
                {
                    db.SaveChanges();


                    //var batch = Microsoft.Azure.Search.Models.IndexBatch.MergeOrUpload(new[] { heroToUpdate });
                    //var async = this.adminIndex.Documents.IndexWithHttpMessagesAsync(batch);
                    ////var result = async.Result;


                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(heroToUpdate);
        }


        public ActionResult Delete(string SuperHeroModelID, bool? saveChangesError = false)
        {
            if (SuperHeroModelID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            SuperHeroModel hero = db.DCSuperHeroes.Find(SuperHeroModelID);
            if (hero == null)
            {
                return HttpNotFound();
            }
            return View(hero);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string SuperHeroModelID)
        {
            try
            {
                SuperHeroModel hero = db.DCSuperHeroes.Find(SuperHeroModelID);
                db.DCSuperHeroes.Remove(hero);
                db.SaveChanges();


                //var batch = Microsoft.Azure.Search.Models.IndexBatch.Delete(new[] { hero });
                //this.adminIndex.Documents.IndexWithHttpMessagesAsync(batch);

            
            }
            catch (RetryLimitExceededException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { SuperHeroModelID = SuperHeroModelID, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
                //this.adminIndex.Dispose();
                //this.adminService.Dispose();
                //this.queryIndex.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
