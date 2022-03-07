using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Samples
{
    public class BasicDataManagementModel : PageModel
    {
        // create bound properties that are directly tied to a control on the form
        // bound properties have the data automatically transfered from the control into the property
        [BindProperty]
        public int Num { get; set; }

        [BindProperty]
        public string FavouriteCourse { get; set; }

        [BindProperty]
        public string Comments { get; set; }

        // Annotation TempData is required IF you are processing multiple requests (OnPost followed by OnGet) to retain data within the property.
       // [TempData]
        public string FeedBack { get; set; }

        public void OnGet()
        {
            // executes for a Get request
            // the first time the page is requested an automatic Get request is set
            // excellent "event" to use to do any initialization to your web page display as the page is shown for the first time.
        }

        public void OnPost()
        {
            // process the OnPost request of the form (method="post")
            // the returned datatype can be void or an ActionResult
            // OnPost request is the request form a <button> that has not indicated a specific processing post using the asp-page-handler.
            // Logic that you wish to accomplish should be isolated to the actions desired for the button.
            // 
            FeedBack = $"Number {Num}, Course {FavouriteCourse} Comments {Comments}";
        }

        public void OnPostA()
        {
            // process the OnPost request of the form (method="post")
            // this method is called due to the helper-tag on the form button
            // the 'string' used on the helper-tag asp-page-handle="string" is
            // add to the OnPost method name
            FeedBack = $"Button A was pressed";
        }

        public void OnPostB()
        {
            // process the OnPost request of the form (method="post")
            // this method is called due to the helper-tag on the form button
            // the 'string' used on the helper-tag asp-page-handle="string" is
            // add to the OnPost method name
            FeedBack = $"Button B was pressed";
        }
    }
}
