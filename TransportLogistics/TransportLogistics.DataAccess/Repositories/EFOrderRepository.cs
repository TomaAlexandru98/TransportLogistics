using System;
using System.Collections.Generic;
using System.Text;
using TransportLogistics.Model;
using TransportLogistics.Data.Abstractions;
using TransportLogistics.DataAccess.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TransportLogistics.DataAccess.Repositories
{
    class EFOrderRepository : EFBaseRepository<Order>, IOrderRepository
    {
        public EFOrderRepository(TransportLogisticsDbContext context) : base(context)
        {

        }


        public new Order GetById(Guid orderId)
        {
            return dbContext.Orders
                        .Include(o => o.PickUpAddress)
                        .Include(o => o.DeliveryAddress)
                        .Include(o => o.Recipient)
                        .Include(o => o.Recipient.ContactDetails)
                        .Include(o => o.Sender)
                        .ThenInclude(o => o.LocationAddresses)
                        .Where(o => o.Id == orderId)
                        .FirstOrDefault();
        }


        public new IEnumerable<Order> GetAll()
        {
            return dbContext.Orders
                        .Include(o => o.PickUpAddress)
                        .Include(o => o.DeliveryAddress)
                        .Include(o => o.Sender)
                        .Include(o => o.Sender.ContactDetails)
                        .Include(o => o.Recipient)
                        .Include(o => o.Recipient.ContactDetails)
                        .OrderByDescending(o => o.CreationTime)
                        .AsEnumerable();
        }

        public bool RemoveOrdersFromCustomer(Guid customerId)
        {
            var orders = dbContext.Orders
                .Where(o => o.Sender.Id == customerId)
                .AsEnumerable();

            if (orders.Count() == 0)
            {
                return false;
            }

            foreach (var order in orders)
            {
                RemoveOrder(order.Id);
            }

            dbContext.SaveChanges();
            return true;
        }

        public bool RemoveOrder(Guid orderId)
        {
            var orderToRemove = GetById(orderId);

            if (orderToRemove != null)
            {
                dbContext.Remove(orderToRemove.Recipient.ContactDetails);
                dbContext.Remove(orderToRemove.Recipient);
                dbContext.Remove(orderToRemove);
                dbContext.SaveChanges();

                return true;
            }
            return false;
        }
    }
}
