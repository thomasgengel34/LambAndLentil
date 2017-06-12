using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralsightCustomerServiceTests
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

        public void SaveACustomer(Customer customer)
        {
            _customerRepository.Save(customer);
        }

        public Customer GetACustomer(Customer customer)
        { 
            return  _customerRepository.Get(customer);
        }


        public int GetAnInt(int id)
        {
            return _customerRepository.Get(id);
        }
    }



}
