#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

#region Additional Namespaces
using WestWindSystem.BLL;
using WestWindSystem.Entities;
#endregion

namespace WebApp.Pages.Samples
{
    public class RegionQueryOneModel : PageModel
    {
        #region Private service fields and class constructor
        private readonly ILogger<IndexModel> _logger;
        private readonly RegionServices _regionServices;

        public RegionQueryOneModel(ILogger<IndexModel> logger, RegionServices regionservices)
        {
            _logger = logger;
            _regionServices = regionservices;
        }

        #endregion

        [TempData]
        public string FeedbackMessage { get; set; }

        // this is bound to the input control via asp-for
        // this is a two way binding out and in
        // data is move out and in FOr You Automatically

        [BindProperty]
        public int regionid { get; set; }

        public Region regionInfo { get; set; }

        public void OnGet()
        {  
        }

        public void OnPost()
        {
            if (regionid > 0)
            {
                regionInfo = _regionServices.Region_GetById(regionid);
                if (regionInfo == null)
                {
                    FeedbackMessage = "Region id is not valid. No such region on file.";
                }
                else
                {
                    FeedbackMessage = $"ID: {regionInfo.RegionID} Description: {regionInfo.RegionDescription}";
                }
            }
            else
            {
                FeedbackMessage = "Required: Region id is a non-zero positive whole number.";
            }
        }
    }
}