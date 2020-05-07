using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime;
using TransportLogistics.ApplicationLogic.Exceptions;
using TransportLogistics.ApplicationLogic.Services;
using TransportLogistics.Model;
using TransportLogistics.Models.Customers;

namespace TransportLogistics.Controllers
{
    public class CustomersController : Controller
    {
        private readonly CustomerService customerService;
        private readonly ILogger<CustomerService> logger;

        public CustomersController(CustomerService customerService, ILogger<CustomerService> logger)
        {
            this.customerService = customerService;
            this.logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                List<CustomerViewModel> customerViews = new List<CustomerViewModel>();

                foreach (var customerIndex in customerService.GetAllCustomers())
                {
                    var customer = customerService.GetCustomerById(customerIndex.Id);

                    List<LocationViewModel> locationViews = new List<LocationViewModel>();

                    IEnumerable<LocationAddress> locationAddresses = customerService?.GetCustomerAddresses(customer.Id.ToString());

                    if (locationAddresses != null)
                    {
                        foreach (var locationAddress in locationAddresses)
                        {
                            locationViews.Add(new LocationViewModel()
                            {
                                City = locationAddress.City,
                                Country = locationAddress.Country,
                                PostalCode = locationAddress.PostalCode,
                                Street = locationAddress.Street,
                                StreetNumber = locationAddress.StreetNumber
                            });
                        }
                    }

                    customerViews.Add(new CustomerViewModel()
                    {
                        Id = customer.Id.ToString(),
                        Name = customer.Name,
                        PhoneNo = customer.ContactDetails.PhoneNo,
                        Email = customer.ContactDetails.Email,
                        LocationViews = locationViews
                    });
                }

                CustomerListViewModel customerViewModel = new CustomerListViewModel()
                {
                    CustomerViews = customerViews
                };

                return View(customerViewModel);
            }
            catch (Exception e)
            {
                logger.LogError("Failed to create a new Customer {@Exception}", e.Message);
                logger.LogDebug("Failed to create a new Customer {@ExceptionMessage}", e);
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public IActionResult CreateCustomer()
        {
            return PartialView("_NewCustomerPartial", new NewCustomerViewModel());
        }

        [HttpGet]
        public IActionResult UpdateCustomer(string customerId)
        {
            var customerToUpdate = customerService.GetCustomerById(customerId);

            var customerViewModel = new UpdateCustomerViewModel()
            {
                Id = customerId,
                PhoneNo = customerToUpdate.ContactDetails.PhoneNo,
                Email = customerToUpdate.ContactDetails.Email
            };

            return PartialView("_UpdateCustomerPartial", customerViewModel);
        }

        [HttpPost]
        public IActionResult CreateCustomer([FromForm] NewCustomerViewModel customerData)
        {
            
            if (!ModelState.IsValid || customerData == null ||
                    customerData.Email == null ||
                    customerData.Name == null ||
                    customerData.PhoneNo == null)
            {
                return PartialView("_NewCustomerPartial", new NewCustomerViewModel());
            }

            try
            {
                customerService.CreateNewCustomer(customerData.Name, 
                                    customerData.PhoneNo, 
                                    customerData.Email);

                return PartialView("_NewCustomerPartial", customerData);
            }
            catch (Exception e)
            {
                logger.LogError("Failed to create a new Customer {@Exception}", e.Message);
                logger.LogDebug("Failed to create a new Customer {@ExceptionMessage}", e);
                return BadRequest("Failed to create a new Customer");
            }
        }


        [HttpGet]
        public IActionResult Remove([FromRoute]string Id)
        {
                
            RemoveCustomerViewModel removeViewModel = new RemoveCustomerViewModel()
            {
                Id = Id
            };

            return PartialView("_RemoveCustomerPartial", removeViewModel);       
        }

        [HttpPost]
        public IActionResult Remove(RemoveCustomerViewModel removeData)
        {
            try
            {
                customerService.RemoveCustomerById(removeData.Id);
            }
            catch (CustomerNotFoundException notFound)
            {
                logger.LogError("Failed to find the customer entity {@Exception}", notFound.Message);
                logger.LogDebug("Failed to find the customer entity {@ExceptionMessage}", notFound);
            } 
            catch (Exception e)
            {
                logger.LogError("Failed to remove the customer entity {@Exception}", e.Message);
                logger.LogDebug("Failed to remove customer entity {@ExceptionMessage}", e);
            }

            return PartialView("_RemoveCustomerPartial", removeData);
        }
    }
}