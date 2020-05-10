using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodLavkaAdmin.Models;
using FoodLavkaAdmin.Services;
using FoodLavkaAdmin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;

namespace FoodLavkaAdmin.Controllers
{
    public class StatisticController : Controller
    {
        private readonly DbService _dbService;
        private readonly string[] _months;

        public StatisticController(DbService dbService)
        {
            _dbService = dbService;
            _months = new[] {
                "Январь",
                "Февраль",
                "Март",
                "Апрель",
                "Май",
                "Июнь",
                "Июль",
                "Август",
                "Сентябрь",
                "Октябрь",
                "Ноябрь",
                "Декабрь"
            };
        }

        private void SetCollectionsForSelects(IEnumerable<Order> orders)
        {
            ViewBag.Months = _months;
            ViewBag.Years = orders.Select(ord => ord.Date.Year).Distinct();
        }

        [HttpGet]
        public async Task<IActionResult> Statistic()
        {
            var orders = new List<Order>();

            await Task.Run(() =>
            {
                orders = _dbService.FindAllOrders().Result;
            });

            SetCollectionsForSelects(orders);

            return View("Statistic");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateChartData([FromQuery(Name = "month")] string month,
            [FromQuery(Name = "year")] string year)
        {
            int selectedYear = 0;
            if (int.TryParse(year, out selectedYear))
                selectedYear = DateTime.Now.Year;

            var orders = new List<Order>();
            await Task.Run(() =>
            {
                orders = _dbService.FindAllOrders().Result;
            });

            var chartData = new List<StatInfoViewModel>();
            if (month == null)
            {
                var groupedOrdrs = orders.Where(ord => ord.Date.Year == selectedYear)
                    .OrderBy(_ord => _ord.Date)
                    .GroupBy(_grp => _grp.Date.Month);

                foreach (var grp in groupedOrdrs)
                {
                    chartData.Add(new StatInfoViewModel
                    {
                        OrdrDay = _months[grp.First().Date.Month - 1],
                        TimePrice = grp.Select(gr => gr).Sum(_ord => _ord.Price)
                    });
                }
            }
            else
            {
                int monthIndex = _months.IndexOf(month) + 1;
                var filteredOrders = orders.Where(ord => ord.Date.Year == selectedYear && ord.Date.Month == monthIndex);

                foreach (var ordr in filteredOrders)
                {
                    chartData.Add(new StatInfoViewModel
                    {
                        OrdrDay = ordr.Date.ToShortDateString(),
                        TimePrice = ordr.Price
                    });
                }
            }

            return Json(chartData);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateClientStats([FromQuery(Name = "month")] string month,
            [FromQuery(Name = "year")] string year)
        {
            int selectedYear = 0;
            if (int.TryParse(year, out selectedYear))
                selectedYear = DateTime.Now.Year;

            int monthIndex = _months.IndexOf(month) + 1;

            var orders = new List<Order>();
            await Task.Run(() =>
            {
                orders = _dbService.FindAllOrders().Result;
            });

            SetCollectionsForSelects(orders);
            ViewBag.SelectedPeriod = new Dictionary<string, string>()
            {
                {"month", month },
                {"year", year }
            };

            var filteredOrders = orders.Where(ord => ord.Date.Year == selectedYear && ord.Date.Month == monthIndex);
            var clientData = new List<StatInfoViewModel>();

            await Task.Run(() =>
            {
                IEnumerable<IGrouping<int, Order>> groupedOrdrs;

                if (month != null)
                {
                    groupedOrdrs = orders.Where(ord => ord.Date.Year == selectedYear && ord.Date.Month == monthIndex)
                    .OrderBy(_ord => _ord.Date)
                    .GroupBy(_grp => _grp.ClientId);
                }
                else
                {
                    groupedOrdrs = orders.Where(ord => ord.Date.Year == selectedYear)
                    .OrderBy(_ord => _ord.Date)
                    .GroupBy(_grp => _grp.ClientId);
                }

                foreach (var grp in groupedOrdrs)
                {
                    int clientId = grp.Select(p => p.ClientId).First();

                    clientData.Add(new StatInfoViewModel
                    {
                        Client = _dbService.GetClientById(clientId).Result,
                        ClientSumPrice = grp.Select(gr => gr).Sum(_ord => _ord.Price)
                    });
                }
            });

            return PartialView("_ClientStats", clientData);
        }
    }
}