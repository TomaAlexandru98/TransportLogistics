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
        public IActionResult UpdateCustomer([FromRoute]string Id)
        {
            try
            {
                var customerToUpdate = customerService.GetCustomerById(Id);

                var editCustomerViewModel = new UpdateCustomerViewModel()
                {
                    Id = Id,
                    Name = customerToUpdate.Name,
                    PhoneNo = customerToUpdate.ContactDetails.PhoneNo,
                    Email = customerToUpdate.ContactDetails.Email
                };

                return PartialView("_UpdateCustomerPartial", editCustomerViewModel);

            } catch (CustomerNotFoundException notFound)
            {
                logger.LogError("Failed to find the customer entity {@Exception}", notFound.Message);
                logger.LogDebug("Failed to find the customer entity {@ExceptionMessage}", notFound);
                return BadRequest("Failed to find customer");
            }
            catch (Exception e)
            {
                logger.LogError("Failed to update customer entity at Get {@Exception}", e.Message);
                logger.LogDebug("Failed to update customer entity at Get {@ExceptionMessage}", e);
                return BadRequest("Failed to update customer entity");
            }
        }

        [HttpPost]
        public IActionResult UpdateCustomer([FromForm] UpdateCustomerViewModel updatedData)
        {
            if (!ModelState.IsValid || updatedData == null ||
                   updatedData.Email == null ||
                   updatedData.Name == null ||
                   updatedData.PhoneNo == null)
            {
                return PartialView("_UpdateCustomerPartial", new NewCustomerViewModel());
            }

            try
            {
                var customerToUpdate = customerService.GetCustomerById(updatedData.Id);
                customerService.UpdateCustomer(customerToUpdate.Id,
                                                updatedData.Name,
                                                updatedData.PhoneNo,
                                                updatedData.Email);

                return PartialView("_UpdateCustomerPartial", updatedData);
            }
            catch (Exception e)
            {
                logger.LogError("Failed to edit customer entity {@Exception}", e.Message);
                logger.LogDebug("Failed to edit customer entity {@ExceptionMessage}", e);
                return BadRequest("Failed to edit customer entity");
            }
        }



        [HttpGet]
        public IActionResult AddLocation([FromRoute]string Id)
        {

            NewLocationViewModel locationModel = new NewLocationViewModel()
            {
                CustomerId = Id
            };

            return PartialView("_AddAddressPartial", locationModel);
        }

        [HttpPost]
        public IActionResult AddLocation([FromForm] NewLocationViewModel locationData)
        {
            if (!ModelState.IsValid ||
                        locationData.CustomerId == null ||
                        locationData == null ||
                        locationData.Country == null ||
                        locationData.City == null ||
                        locationData.Street == null ||
                        locationData.StreetNumber <= 0 ||
                        locationData.PostalCode == null)
            {
                return PartialView("_AddLocationPartial", locationData);
            }

            try
            {
                var customer = customerService.GetCustomerById(locationData.CustomerId);

                var newLocation = LocationAddress.Create(
                            locationData.Country,
                            locationData.City,
                            locationData.Street,
                            locationData.StreetNumber,
                            locationData.PostalCode);

                customerService.AddLocationToCustomer(customer.Id, newLocation);

                return PartialView("_AddLocationPartial", locationData);
            }
            catch (CustomerNotFoundException notFound)
            {
                logger.LogError("Failed to find the customer entity {@Exception}", notFound.Message);
                logger.LogDebug("Failed to find the customer entity {@ExceptionMessage}", notFound);
                return BadRequest("Failed to find user");
            }
            catch (Exception e)
            {
                logger.LogError("Failed to add a new Location {@Exception}", e.Message);
                logger.LogDebug("Failed to add a new Location {@ExceptionMessage}", e);
                return BadRequest("Failed to create a new Location");
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