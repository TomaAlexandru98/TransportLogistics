using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using TransportLogistics.ApplicationLogic.Exceptions;
using TransportLogistics.ApplicationLogic.Services;
using TransportLogistics.Model;
using TransportLogistics.Models.Customers;
using TransportLogistics.ViewModels.Customers;

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

        private CustomerListViewModel LoadCustomerViews()
        {

            CustomerListViewModel customerViewModel = null;
            try
            {
                customerViewModel = new CustomerListViewModel() {
                   CustomerViews = customerService.GetAllCustomers()
                };
                
            } catch (Exception e)
            {
                logger.LogError("Failed to load Customer entities {@Exception}", e.Message);
                logger.LogDebug("Failed to load Customer entities {@ExceptionMessage}", e);
            }

            return customerViewModel;
        }

        public IActionResult Index()
        {
            var customerViewModel = LoadCustomerViews();

            if (customerViewModel == null)
            {
                return BadRequest("Failed to load Customer entities");
            }

            return View(customerViewModel);
        }


        public IActionResult CustomerTable()
        {
            var customerViewModel = LoadCustomerViews();

            if (customerViewModel == null)
            {
                return BadRequest("Failed to load Customer entities");
            }

            return PartialView("_CustomerTable", customerViewModel);
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

            return PartialView("_AddLocationPartial", locationModel);
        }

        [HttpPost]
        public IActionResult AddLocation([FromForm] NewLocationViewModel locationData)
        {
            if (!ModelState.IsValid || locationData == null)
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
        public IActionResult EditLocation([FromRoute] string Id)
        {
            try
            {
                var locationToUpdate = customerService.GetLocationAddress(Id);

                var editLocationViewModel = new EditLocationViewModel()
                {
                    Id = Id,
                    Country = locationToUpdate.Country,
                    City = locationToUpdate.City,
                    Street = locationToUpdate.Street,
                    StreetNumber = locationToUpdate.StreetNumber,
                    PostalCode = locationToUpdate.PostalCode                   
                };

                return PartialView("_EditLocationPartial", editLocationViewModel);

            }
            catch (Exception e)
            {
                logger.LogError("Failed to update location entity at Get {@Exception}", e.Message);
                logger.LogDebug("Failed to update location entity at Get {@ExceptionMessage}", e);
                return BadRequest("Failed to update location entity");
            }
        }


        [HttpPost]
        public IActionResult EditLocation([FromForm] EditLocationViewModel updatedLocation)
        {
            if (!ModelState.IsValid || updatedLocation == null)
            {
                return PartialView("_EditLocationPartial", new EditLocationViewModel());
            }

            try
            {
                var locationToUpdate = customerService.GetLocationAddress(updatedLocation.Id);

                customerService.UpdateLocationAddress(locationToUpdate.Id,
                                updatedLocation.Country,
                                updatedLocation.City,
                                updatedLocation.Street,
                                updatedLocation.StreetNumber,
                                updatedLocation.PostalCode);

                return PartialView("_EditLocationPartial", updatedLocation);
            }
            catch (Exception e)
            {
                logger.LogError("Failed to edit location entity {@Exception}", e.Message);
                logger.LogDebug("Failed to edit location entity {@ExceptionMessage}", e);
                return BadRequest("Failed to edit location entity");
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