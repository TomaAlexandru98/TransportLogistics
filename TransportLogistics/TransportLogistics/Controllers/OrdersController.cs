using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TransportLogistics.ApplicationLogic.Services;
using TransportLogistics.Model;
using TransportLogistics.ViewModels.Orders;

namespace TransportLogistics.Controllers
{
    public class OrdersController : Controller
    {
        private readonly OrderService orderservice;
        private readonly ILogger<OrderService> logger;
        private readonly CustomerService customerService;
        public OrdersController(OrderService orderservice, ILogger<OrderService> logger, CustomerService customerService)
        {
            this.orderservice = orderservice;
            this.logger = logger;
            this.customerService = customerService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult OrdersTable()
        {
            return PartialView("_OrdersTable");
        }

        [HttpGet]
        public IActionResult NewOrder()
        {
           var customers =  customerService.GetAllCustomers().ToList();
            List<string> customerNames = new List<string>();
            foreach(var customer in customers)
            {
                customerNames.Add(customer.Name);
            }
            var newOrderViewModel = new NewOrderViewModel()
            {

                CustomerNames = customerNames.AsEnumerable()
            };
            return PartialView("_NewOrderPartial", newOrderViewModel);
        }

        [HttpPost]
        public IActionResult NewOrder([FromForm]NewOrderViewModel orderData)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    orderservice.CreateOrder(orderData.DeliveryAddress,orderData.PickUpAddress,orderData.Recipient,orderData.Price);
                    //return RedirectToAction("Index");
                    return PartialView("_NewOrderPartial", orderData);
                }
                return View(orderData);

            }
            catch (Exception e)
            {
                logger.LogError("Failed to create a new Order {@Exception}", e.Message);
                logger.LogDebug("Failed to create a new Order {@ExceptionMessage}", e);
                return BadRequest(e.Message);
            }
        }
    }
}