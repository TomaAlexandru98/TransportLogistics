using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc.Ajax;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public OrdersController(OrderService orderService, ILogger<OrderService> logger, CustomerService customerService)
        {
            this.orderservice = orderService;
            this.logger = logger;
            this.customerService = customerService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult OrdersTable()
        {
            var ordersView = new OrderViewModel()
            {
                Orders = orderservice.GetAllOrders()
            };

            return PartialView("_OrdersTablePartial", ordersView);
        }


        [HttpGet]
        public IActionResult NewOrder(string RecipientId)
        {
            try
            {
                //load partial view cu datele populate pe parte de server / cu JSON
                var customers = customerService.GetAllCustomers();
               // var locations = customerService.GetCustomerAddresses(RecipientId);
                List<SelectListItem> customerNames = new List<SelectListItem>();

                foreach (var customer in customers)
                {
                    customerNames.Add(new SelectListItem(customer.Name, customer.Id.ToString()));
                }

                NewOrderViewModel newOrderViewModel = new NewOrderViewModel()
                {
                    CustomerList = customerNames,
                     
                    Price = 200
                };

                return PartialView("_NewOrderPartial", newOrderViewModel);
            }
            catch (Exception e)
            {
                logger.LogError("Failed to create a new Order {@Exception}", e.Message);
                logger.LogDebug("Failed to create a new Order {@ExceptionMessage}", e);
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult NewOrder([FromForm]NewOrderViewModel orderData)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    
                    //orderservice.CreateOrder(orderData.RecipientId,orderData.Location1,orderData.Location2,orderData.Price);
                    //return RedirectToAction("Index");
                    return PartialView("_NewOrderPartial", orderData);
                }

                return PartialView("_NewOrderPartial", orderData);
            }
            catch (Exception e)
            {
                logger.LogError("Failed to create a new Order {@Exception}", e.Message);
                logger.LogDebug("Failed to create a new Order {@ExceptionMessage}", e);
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public JsonResult GetCustomerLocations(string id)
        {

            var locationList = customerService.GetCustomerAddresses(id);

            if (locationList.Count()>0)
            {
               
                return Json(new { data = locationList });
            }
            else
            {
                return Json(new { data = "" });
            }

        }
       
    }
}