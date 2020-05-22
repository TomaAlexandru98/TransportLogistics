using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc.Ajax;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Logging;
using TransportLogistics.ApplicationLogic.Services;
using TransportLogistics.Model;
using TransportLogistics.ViewModels.Orders;

namespace TransportLogistics.Controllers
{
    public class OrdersController : Controller
    {
        private readonly OrderService orderService;
        private readonly ILogger<OrderService> logger;
        private readonly CustomerService customerService;
        public OrdersController(OrderService orderService, ILogger<OrderService> logger, CustomerService customerService)
        {
            this.orderService = orderService;
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
                Orders = orderService.GetAllOrders()
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

        private SelectListItem CreateListItem(LocationAddress location)
        {
            string dropdownText = $"{ location.PostalCode }, { location.Street}";
            SelectListItem selectLocation = new SelectListItem(dropdownText, location.Id.ToString());
            return selectLocation;
        }

        [HttpGet]
        public IActionResult NewOrder(string id)
        {
            List<SelectListItem> pickupLocations = new List<SelectListItem>();
            List<SelectListItem> deliveryLocations = new List<SelectListItem>();
            string senderId = null;

            try
            {
                if (id != null)
                {
                    if (customerService.IsCustomer(id))
                    {
                        senderId = id;
                        var locations = customerService.GetCustomerAddresses(id);
                        foreach (var location in locations)
                        {
                            pickupLocations.Add(CreateListItem(location));
                        }
                    }
                    else
                    {
                        var pickup = customerService.GetLocationAddress(id);
                        var customer = customerService.GetCustomerByLocation(pickup);
                        senderId = customer.Id.ToString();

                        foreach (var location in customer.LocationAddresses)
                        {
                            if (location.Id != pickup.Id)
                            {
                                deliveryLocations.Add(CreateListItem(location));
                            }
                        }

                        pickupLocations.Add(CreateListItem(pickup));
                        pickupLocations.AddRange(deliveryLocations);
                    }
                }

                NewOrderViewModel newOrderViewModel = new NewOrderViewModel()
                {
                    CustomerList = GetCustomerList(),
                    PickupLocation = pickupLocations,
                    DeliveryLocation = deliveryLocations,
                    SenderId = senderId
                };

                return PartialView("_NewOrderPartial", newOrderViewModel);
            }
            catch (Exception e)
            {
                logger.LogError("Failed to load information for Order {@Exception}", e.Message);
                logger.LogDebug("Failed to load information for Order {@ExceptionMessage}", e);
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
                    var sender = customerService.GetCustomerById(orderData.SenderId);
                    var recipient = customerService.CreateNewCustomer(orderData.RecipientName,
                                orderData.RecipientEmail,
                                orderData.RecipientPhoneNo);

                    orderService.CreateOrder(recipient,
                        sender,
                        orderData.PickupLocationId,
                        orderData.DeliveryLocationId,
                        orderData.Price);
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

            orderService.Remove(removeData.Id);

            return PartialView("_RemoveOrderPartial", removeData);
        }

        [HttpGet]
        public IActionResult Update(string id)
        {
            var order = orderService.GetById(id);
            var senderId = order.Sender.Id;
            
            var locations = customerService.GetCustomerAddresses(senderId.ToString());

            List<SelectListItem> customerLocations = new List<SelectListItem>();
            foreach (var location in locations)
            {
                customerLocations.Add(CreateListItem(location));
            }

            UpdateOrderViewModel newOrderViewModel = new UpdateOrderViewModel()
            {
                Id = order.Id.ToString(),
                CustomerList = GetCustomerList(),
                PickupLocation = customerLocations,
                DeliveryLocation = customerLocations,
                DeliveryLocationId = order.DeliveryAddress.Id.ToString(),
                PickupLocationId = order.PickUpAddress.Id.ToString(),
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
                
                orderService.Update(viewModel.Id,
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