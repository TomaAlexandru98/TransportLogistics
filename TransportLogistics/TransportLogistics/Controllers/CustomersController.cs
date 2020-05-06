using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TransportLogistics.ApplicationLogic.Services;
using TransportLogistics.Model;
using TransportLogistics.Models.Customers;

namespace TransportLogistics.Controllers
{
    public class CustomersController : Controller
    {
        private readonly CustomerService customerService;

        public CustomersController(CustomerService customerService)
        {
            this.customerService = customerService;
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
                return BadRequest(e.Message);
            }
        }
    }
}