using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
                    var customer = customerService.GetCustomerByGuid(customerIndex.Id);

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
            var viewModel = new NewCustomerViewModel();
            return PartialView("_NewCustomerPartial");//, viewModel);
        }

        [HttpPost]
        public IActionResult CreateCustomer([FromForm] NewCustomerViewModel customerData)
        {
            //var creationResultViewModel = new NewCustomerViewModel();
            
            if (!ModelState.IsValid || customerData == null ||
                    customerData.Email == null ||
                    customerData.Name == null ||
                    customerData.PhoneNo == null)
            {
                return RedirectToAction("Index");
                //return PartialView("_NewCustomerPartial", creationResultViewModel);
            }

            try
            {
                customerService.CreateNewCustomer(customerData.Name, 
                                    customerData.PhoneNo, 
                                    customerData.Email);
                return RedirectToAction("Index");

                //return PartialView("_NewCustomerPartial", creationResultViewModel);
            }
            catch (Exception e)
            {
                logger.LogError("Failed to create a new Customer {@Exception}", e.Message);
                logger.LogDebug("Failed to create a new Customer {@ExceptionMessage}", e);
                return BadRequest("Failed to create a new Customer");
            }
             
        }

    }
}