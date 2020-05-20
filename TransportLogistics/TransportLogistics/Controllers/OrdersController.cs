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


        private List<SelectListItem> GetCustomerList()
        {
            var customers = customerService.GetAllCustomers();
            List<SelectListItem> customerNames = new List<SelectListItem>();

            foreach (var customer in customers)
            {
                if (customer.LocationAddresses.Count > 1)
                {
                    customerNames.Add(new SelectListItem(customer.Name, customer.Id.ToString()));
                }
            }
            return customerNames;
        }

        [HttpGet]
        public IActionResult NewOrder(string id)
        {
            List<SelectListItem> customerLocations = null;

            if (id != null)
            {
                var locations = customerService.GetCustomerAddresses(id);
                customerLocations = new List<SelectListItem>();
                foreach (var location in locations)
                {
                    customerLocations.Add(new SelectListItem(location.PostalCode, location.Id.ToString()));
                }
            }

            NewOrderViewModel newOrderViewModel = new NewOrderViewModel()
            {
                CustomerList = GetCustomerList(),
                PickupLocation = customerLocations,
                DeliveryLocation = customerLocations
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
                    var recipient = customerService.GetCustomerById(orderData.RecipientId);

                    orderservice.CreateOrder(recipient, 
                        orderData.PickupLocationId, 
                        orderData.DeliveryLocationId,
                        orderData.Price);

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
        public IActionResult Remove([FromRoute]string Id)
        {

            RemoveOrderViewModel removeViewModel = new RemoveOrderViewModel()
            {
                Id = Id
            };

            return PartialView("_RemoveOrderPartial", removeViewModel);
        }

        [HttpPost]
        public IActionResult Remove(RemoveOrderViewModel removeData)
        {

            orderservice.Remove(removeData.Id);


            return PartialView("_RemoveOrderPartial", removeData);
        }

        [HttpGet]
        public IActionResult Update(string id)
        {
            List<SelectListItem> customerLocations = null;

            var order = orderservice.GetById(id);
            var customerId = order.Recipient.Id;
            var locations = customerService.GetCustomerAddresses(customerId.ToString());
            customerLocations = new List<SelectListItem>();
            foreach (var location in locations)
            {
                customerLocations.Add(new SelectListItem(location.PostalCode, location.Id.ToString()));
            }

            UpdateOrderViewModel newOrderViewModel = new UpdateOrderViewModel()
            {
                Id = order.Id.ToString(),
                CustomerList = GetCustomerList(),
                PickupLocation = customerLocations,
                DeliveryLocation = customerLocations,
                Price = order.Price,
                
            };

            return PartialView("_Update", newOrderViewModel);
        }

        [HttpPost]
        public IActionResult Update([FromForm]UpdateOrderViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_Update", viewModel);
            }

            try
            {
                
                orderservice.Update(viewModel.Id,
                                      viewModel.PickupLocationId,
                                      viewModel.DeliveryLocationId,
                                      viewModel.Price);
                return PartialView("_Update", viewModel);
            }
            catch (Exception e)
            {
                logger.LogError("Failed to update order {@Exception}", e.Message);
                logger.LogDebug("Failed to update order {ExceptionMessage}", e);
                return BadRequest(e.Message);
            }
        }
        /*
        [HttpGet]
        public JsonResult GetCustomerLocations(string id)
        {
            var locationList = customerService.GetCustomerAddresses(id);
            return Json(new { data = locationList });
        }
        */
    }
}