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
                        .AsEnumerable();
        }

        public bool RemoveOrder(Order orderToRemove)
        {
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
