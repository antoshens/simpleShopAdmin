using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FoodLavkaAdmin.Models;
using FoodLavkaAdmin.Services;
using System;

namespace FoodLavkaAdmin.Controllers
{
    public class ClientController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DbService _dbService;

        public ClientController(ILogger<HomeController> logger, DbService dbService)
        {
            _logger = logger;
            _dbService = dbService;
        }

        [HttpGet]
        public async Task<IActionResult> Client()
        {
            var clients = new List<Client>();

            await Task.Run(() =>
            {
                clients = _dbService.FindAllClients().Result;
            });

            return View("Client", clients);
        }

        public async Task<IActionResult> ClientGrid()
        {
            var clients = new List<Client>();

            await Task.Run(() =>
            {
                clients = _dbService.FindAllClients().Result;
            });

            return PartialView("_ClientGrid", clients);
        }

        [HttpGet]
        public IActionResult GetClientActionForm(Client client)
        {
            var model = client.Id != -1 ? client : new Client();

            return PartialView("_ClientForm", model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient(Client client)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("ClientGrid");

            try
            {
                await Task.Run(() =>
                {
                    client.Id = 0;
                    var result = _dbService.CreateClient(client).Result;
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return RedirectToAction("ClientGrid");
            }

            return RedirectToAction("ClientGrid");
        }

        [HttpPost]
        public async Task<IActionResult> EditClient(Client client)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("ClientGrid");

            try
            {
                await Task.Run(() =>
                {
                    var result = _dbService.UpdateClient(client).Result;
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return RedirectToAction("ClientGrid");
            }

            return RedirectToAction("ClientGrid");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteClient(Client client)
        {
            try
            {
                await Task.Run(() =>
                {
                    var result = _dbService.DeleteClient(client).Result;
                });

                return RedirectToAction("ClientGrid");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return RedirectToAction("ClientGrid");
            }
        }
    }
}