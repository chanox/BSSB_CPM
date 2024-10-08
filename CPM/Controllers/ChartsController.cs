using Microsoft.AspNetCore.Mvc;

namespace CPM_Project.Controllers
{
    public class ChartsController : Controller
    {

        public IActionResult ChartJs()
        {
            return View();
        }

        public IActionResult FlotCharts()
        {
            return View();
        }

        public IActionResult InlineGraphs()
        {
            return View();
        }

        public IActionResult Chartist()
        {
            return View();
        }

        public IActionResult C3Charts()
        {
            return View();
        }
    }
}