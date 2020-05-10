using FoodLavkaAdmin.Models;
using FoodLavkaAdmin.Models.ModelStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodLavkaAdmin.Services
{
    public class DbService
    {

        private IServiceProvider _scopeFactory;

        public DbService(IServiceProvider scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        #region Order
        public async Task<int> CreateOrder(Order order)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ModelContext>();

                db.Orders.Add(order);
                return await db.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public async Task<int> UpdateOrder(Order order)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ModelContext>();
                var updatedOrder = db.Orders.Find(order.Id);
                if (order == null)
                    throw new NullReferenceException();
                else 
                {
                    updatedOrder.Address = order.Address;
                    updatedOrder.ClientId = order.ClientId;
                    updatedOrder.Description = order.Description;
                    updatedOrder.Price = order.Price;
                    updatedOrder.Date = order.Date;
                };
                db.Entry(updatedOrder).State = EntityState.Modified;

                return await Task.Run(() => db.SaveChanges()).ConfigureAwait(false);
            }
        }

        public async Task<List<Order>> FindAllOrders()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ModelContext>();
                return await Task.Run(() => db.Orders.ToList()).ConfigureAwait(false);
            }
        }

        public async Task<int> DeleteOrder(Order order)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ModelContext>();
                db.Orders.Remove(order);

                return await Task.Run(() => db.SaveChanges()).ConfigureAwait(false);
            }
        }

        public async Task<Order> GetOrderById(int id)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ModelContext>();
                return await Task.Run(() => db.Orders.Find(id)).ConfigureAwait(false);
            }
        }
        #endregion

        #region Client
        public async Task<int> CreateClient(Client client)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ModelContext>();
                db.Clients.Add(client);

                return await db.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public async Task<int> UpdateClient(Client client)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ModelContext>();
                var updatedClient = db.Clients.Find(client.Id);
                if (client == null)
                    throw new NullReferenceException();
                else
                {
                    updatedClient.Name = client.Name;
                    updatedClient.PhoneNumber = client.PhoneNumber;
                };
                db.Entry(updatedClient).State = EntityState.Modified;

                return await db.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public async Task<List<Client>> FindAllClients()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ModelContext>();
                return await Task.Run(() => db.Clients.ToList()).ConfigureAwait(false);
            }
        }

        public async Task<int> DeleteClient(Client client)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ModelContext>();
                db.Clients.Remove(client);

                return await db.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public async Task<Client> GetClientById(int id)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ModelContext>();
                return await Task.Run(() => db.Clients.Find(id)).ConfigureAwait(false);
            }
        }
        #endregion

        #region private methods
        //private TResult _Request<TResult>()
        //{
        //    using (var scope = _scopeFactory.CreateScope())
        //    {
        //        var db = scope.ServiceProvider.GetRequiredService<ModelContext>();
        //        return await Task.Run(() => db.Clients.Find(id)).ConfigureAwait(false);
        //    }
        //}
        #endregion
    }
}
