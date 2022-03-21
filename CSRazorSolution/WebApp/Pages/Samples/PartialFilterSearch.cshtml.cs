#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

#region Additional Namespaces
using WestWindSystem.BLL;
using WestWindSystem.Entities;
using WebApp.Helpers; // this is where the Paginator is coded
#endregion

namespace WebApp.Pages.Samples
{
    public class PartialFilterSearchModel : PageModel
    {

        #region Private service fields and class constructor
        private readonly ILogger<IndexModel> _logger;
        private readonly TerritoryServices _territoryServices;
        private readonly RegionServices _regionServices;

        public PartialFilterSearchModel(ILogger<IndexModel> logger, TerritoryServices territoryservices, RegionServices regionServices)
        {
            _logger = logger;
            _territoryServices = territoryservices;
            _regionServices = regionServices;
        }

        #endregion

        [TempData]
        public string Feedback { get; set; }

        [BindProperty(SupportsGet = true)]
        public string searcharg { get; set; }

        public List<Territory> TerritoryInfo { get; set; } = new();

        [BindProperty]
        public List<Region> RegionList { get; set; } = new();

        #region Paginator
        //my desired page size
        private const int PAGE_SIZE = 5;
        //be able to hold an instance of the Paginator
        public Paginator Pager { get; set; }
        #endregion

        public void OnGet(int? currentPage)
        {
            //using the Paginator with your query

            //OnGet will have a parameter (Request query string) that receives the current page number. On the intial load of the page, this value will be null.


            // obtain the data list for the Region dropdownlist (select tag)
            RegionList = _regionServices.Region_List();

            if (!string.IsNullOrWhiteSpace(searcharg))
            {
                //setting up for using the Paginator only needs to be done if a query is executing
                //determine the current page number
                int pageNumber = currentPage.HasValue ? currentPage.Value : 1;

                //setup the current state of hte paginator (sizing)
                PageState current = new(pageNumber, PAGE_SIZE);

                //temporary local integer to hold the results of the query's total collection size
                //this will be needed by the Paginator during the paginator's execution
                int totalcount;

                //we need to pass paging data into our query so that efficiencies in the system will ONLY return the amount of records to actually be displayed on the browser page.

                TerritoryInfo = _territoryServices.GetByPartialDescription(searcharg, pageNumber,PAGE_SIZE, out totalcount);

                //create the needed Paginator instance
                Pager = new Paginator(totalcount, current);
            }
        }

        public IActionResult OnPostFetch()
        {
            if (string.IsNullOrWhiteSpace(searcharg))
            {
                Feedback = "Required: Search argument is empty.";
            }
            return RedirectToPage(new { searcharg = searcharg });
        }

        public IActionResult OnPostClear()
        {
            Feedback = "";
            //searcharg = null
            ModelState.Clear();
            return RedirectToPage(new { searcharg = (string?)null });
        }
    }
}
