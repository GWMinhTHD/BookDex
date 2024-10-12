using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Data;
using StoreManagement.ViewModels.DashboardViewModel;

namespace StoreManagement.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(Roles = "Manager")]
    public class DashboardController: Controller
    {
        private readonly AppDBContext _context;

        public DashboardController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult Index(int year)
        {
            // Get the distinct years from the Orders table
            var availableYears = _context.Order
                .Select(o => o.DateCreated.Year)
                .Distinct()
                .OrderByDescending(y => y)
                .ToList();

            // Default to the current year or the first available year if no year is specified
            if ( year == 0 || !availableYears.Contains(year) )
            {
                year = availableYears.FirstOrDefault(DateTime.Now.Year);
            }

            // Fetch sales data by month for the selected year
            var salesData = _context.Order
                .Where(o => o.DateCreated.Year == year)
                .GroupBy(o => o.DateCreated.Month)
                .Select(g => new SalesByMonthVM
                {
                    Month = g.Key,
                    TotalSales = g.Sum(o => o.Total)
                })
                .ToList();

            // Ensure all 12 months are included in the result
            var fullYearData = Enumerable.Range(1, 12).Select(month => new SalesByMonthVM
            {
                Month = month,
                TotalSales = salesData.FirstOrDefault(s => s.Month == month)?.TotalSales ?? 0
            }).ToList();

            // Pass the available years and selected year to the view
            ViewBag.AvailableYears = availableYears;
            ViewBag.SelectedYear = year;

            return View(fullYearData);
        }
    }
}
