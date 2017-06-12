using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Moq;
using System.Linq;

namespace PluralsightCustomerServiceTests
{
    [TestClass]
    public class When_creating_multiple_customers2
    {
        //shows how to verify that a method was called an explicit number of times
        [TestMethod]
        public void the_customer_repository_should_be_called_once_per_customer()
        {
            //Arrange
            var listOfCustomerDtos = new List<CustomerToCreateDto>
                    {
                        new CustomerToCreateDto
                            {
                                FirstName = "Sam", LastName = "Sampson"
                            },
                        new CustomerToCreateDto
                            {
                                FirstName = "Bob", LastName = "Builder"
                            },
                        new CustomerToCreateDto
                            {
                                FirstName = "Doug", LastName = "Digger"
                            }
                    };

            var mockCustomerRepository = new Mock<ICustomerRepository>();

            var customerService = new CustomerService(mockCustomerRepository.Object);

            //mockCustomerRepository.Setup(x => x.Save(It.IsAny<Customer>()));

            //Act
            customerService.Create(listOfCustomerDtos);

            //Assert
            mockCustomerRepository.Verify(x => x.Save(It.IsAny<Customer>()), Times.Exactly(3));
        }

        [TestMethod]
        public void CustomerGetsSaved()
        {
            Customer customer = new Customer( "Doug",  "Digger" );
            var mockCustomerRepository = new Mock<ICustomerRepository>();

            var customerService = new CustomerService(mockCustomerRepository.Object);
            customerService.SaveACustomer(customer);
            mockCustomerRepository.Verify(x => x.Save(It.IsAny<Customer>()) );
        }


        [TestMethod]
        public void CustomerFromRepo()
        {
            Customer customer = new Customer("Doug", "Digger");
            var mockCustomerRepository = new Mock<ICustomerRepository>();
            mockCustomerRepository.Setup(m => m.Customers).Returns(new Customer[] {
               new Customer("Frank", "Furter") ,
               new Customer( "Ham", "Burger") 
              }.AsQueryable());
            var customerService = new CustomerService(mockCustomerRepository.Object);
            customerService.SaveACustomer(customer);
            customer.FirstName = "Big Bird";

            
            Customer cus =customerService.GetACustomer(customer);
            int n = customerService.GetAnInt(42);
             mockCustomerRepository.Verify(x => x.Get(It.IsAny<Customer>()));
        }

    }
}
