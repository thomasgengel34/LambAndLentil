namespace PluralSight.Moq.Code.Demo02
{
    public interface ICustomerRepository
    {
        void Save(Customer customer); 
        Customer Get(Customer customer);
    }
}