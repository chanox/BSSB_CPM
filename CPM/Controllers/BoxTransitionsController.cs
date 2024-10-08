using Microsoft.AspNetCore.Mvc;

namespace CPM_Project.Controllers
{
    public class BoxTransitionsController : Controller
    {

        public IActionResult Overview()
        {
            return View();
        }

        public IActionResult ColumnsFromUp()
        {
            return View();
        }

        public IActionResult ColumnsCustom()
        {
            return View();
        }

        public IActionResult PanelsZoom()
        {
            return View();
        }

        public IActionResult RowsFromDown()
        {
            return View();
        }

        public IActionResult RowsFromRight()
        {
            return View();
        }
    }
}