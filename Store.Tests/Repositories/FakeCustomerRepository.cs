using Store.Domain.Entities;
using Store.Domain.Repositories;

namespace Store.Tests.Repositories;

public class FakeCustomerRepository : ICustomerRepository
{
    public Customer Get(string document)
    {
        if (document == "12345678901")
            return new Customer("Bruce Wayne", "bat@man.com");

        return null;
    }
}