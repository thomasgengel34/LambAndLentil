﻿using System.Collections.Generic;

namespace PluralSight.Moq.Code.Demo02
{
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public void Create(IEnumerable<CustomerToCreateDto> customersToCreate)
         {
             foreach (var customerToCreateDto in customersToCreate)
             {
                 _customerRepository.Save(
                     new Customer(
                         customerToCreateDto.FirstName, 
                         customerToCreateDto.LastName)
                     );
             }
         }

        public Customer Get(Customer customer)
        {
            return new Customer("Abe", "Lincoln");
        }

        public void SaveACustomer(Customer customer)
        {
            _customerRepository.Save(customer);
        }
    }
}