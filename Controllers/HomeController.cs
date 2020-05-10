using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FoodLavkaAdmin.Models;
using FoodLavkaAdmin.Services;
using FoodLavkaAdmin.ViewModels;
using System;

namespace FoodLavkaAdmin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DbService _dbService;
        private IEnumerable<Client> _clients;

        public HomeController(ILogger<HomeController> logger, DbService dbService)
        {
            _logger = logger;
            _dbService = dbService;
            _clients = new List<Client>();
        }

        [HttpGet]
        public async Task<IActionResult> Order()
        {
            var ordersExt = new List<OrderViewModel>();

            await Task.Run(() =>
            {
                var orders = _dbService.FindAllOrders().Result;

                foreach (Order order in orders)
                {
                    ordersExt.Add(new OrderViewModel
                    {
                        Id = order.Id,
                        ClientId = order.ClientId,
                        Address = order.Address,
                        Description = order.Description,
                        Price = order.Price,
                        Client = _dbService.GetClientById(order.ClientId).Result,
                        Date = order.Date,
                        DateString = order.Date.ToShortDateString()
                    });
                }
            });

            ViewBag.SelectedClient = new Client();

            await Task.Run(() =>
            {
                _clients = _dbService.FindAllClients().Result;
                ViewBag.Clients = _clients;
            });

            return View("Order", ordersExt);
        }

        public async Task<IActionResult> OrderGrid()
        {
            var ordersExt = new List<OrderViewModel>();

            await Task.Run(() =>
            {
                var orders = _dbService.FindAllOrders().Result;

                foreach (Order order in orders)
                {
                    ordersExt.Add(new OrderViewModel
                    {
                        Id = order.Id,
                        ClientId = order.ClientId,
                        Address = order.Address,
                        Description = order.Description,
                        Price = order.Price,
                        Client = _dbService.GetClientById(order.ClientId).Result,
                        Date = order.Date,
                        DateString = order.Date.ToShortDateString()
                    });
                }
            });

            return PartialView("_OrderGrid", ordersExt);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderActionForm(OrderViewModel order)
        {
            var model = new OrderViewModel {
                Date = DateTime.Now,
                DateString = DateTime.Now.ToShortDateString()
            };

            if(order.Id != -1)
            {
                order.Date = DateTime.Parse(order.DateString);
                model = order;
            }

            ViewBag.SelectedClient = new Client();

            await Task.Run(() =>
            {
                ViewBag.Clients = _dbService.FindAllClients().Result;
                var selectedOrder = _dbService.GetOrderById(order.Id).Result;
                if (selectedOrder != null)
                {
                    var selectedClient = _dbService.GetClientById(selectedOrder.ClientId).Result;
                    ViewBag.SelectedClient = selectedClient;
                }
            });

            return PartialView("_OrderForm", model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromForm]OrderViewModel orderView)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("OrderGrid");

            try
            {
                var order = new Order
                {
                    Id = 0,
                    ClientId = orderView.ClientId,
                    Address = orderView.Address,
                    Price = orderView.Price,
                    Description = orderView.Description,
                    Date = DateTime.Parse(orderView.DateString)
                };

                await Task.Run(() =>
                {
                    var result = _dbService.CreateOrder(order).Result;
                });
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                return RedirectToAction("OrderGrid");
            }

            return RedirectToAction("OrderGrid");
        }

        [HttpPost]
        public async Task<IActionResult> EditOrder(OrderViewModel orderExt)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("OrderGrid");

            try
            {
                var order = new Order
                {
                    Id = orderExt.Id,
                    ClientId = orderExt.ClientId,
                    Address = orderExt.Address,
                    Description = orderExt.Description,
                    Price = orderExt.Price,
                    Date = orderExt.Date
                };

                await Task.Run(() =>
                {
                    var result = _dbService.UpdateOrder(order).Result;
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return RedirectToAction("OrderGrid");
            }

            return RedirectToAction("OrderGrid");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteOrder(OrderViewModel orderExt)
        {
            try
            {
                var order = new Order
                {
                    Id = orderExt.Id,
                    ClientId = orderExt.ClientId,
                    Address = orderExt.Address,
                    Description = orderExt.Description,
                    Price = orderExt.Price,
                    Date = orderExt.Date
                };

                await Task.Run(() =>
                {
                    var result = _dbService.DeleteOrder(order).Result;
                });

                return RedirectToAction("OrderGrid");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return RedirectToAction("OrderGrid");
            }
        }
    }
}
