using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralsightCustomerServiceTests
{

    public interface ICustomerRepository
    {
        IEnumerable<Customer>  Customers { get; set; }
        void Save(int number);
        void Save(Customer customer); 
        Customer Get(Customer customer);
        int Get(int number);
    }




    public class CustomerRepository : ICustomerRepository
    {
        public IEnumerable<Customer> Customers { get  ; set ; }

        public void Save(Customer customer)
        {
            string foo = "Bar";
            Debug.WriteLine(foo);
        }

        public Customer Get(Customer customer)
        {
            return  new Customer("Abe", "Lincoln");
        }

        public void Save(int number)
        {
            Debug.WriteLine("foo  one"  );
        }

        public int Get(int number)
        {
            return 7; 
        }
    }
}
