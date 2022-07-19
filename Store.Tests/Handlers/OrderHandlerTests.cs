using Store.Domain.Commands;
using Store.Domain.Handlers;
using Store.Domain.Repositories;
using Store.Tests.Repositories;

namespace Store.Tests.Handlers;

[TestClass]
public class OrderHandlerTests
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IDeliveryFeeRepository _deliveryFeeRepository;
    private readonly IDiscountRepository _discountRepository;
    private readonly IProductRepository _productRepository;
    private readonly IOrderRepository _orderRepository;

    public OrderHandlerTests()
    {
        _customerRepository = new FakeCustomerRepository();
        _deliveryFeeRepository = new FakeDeliveryFeeRepository();
        _discountRepository = new FakeDiscountRepository();
        _productRepository = new FakeProductRepository();
        _orderRepository = new FakeOrderRepository();
    }

    private OrderHandler _criarOrderHandler(CreateOrderCommand command)
    {
        var handler = new OrderHandler(_customerRepository, _deliveryFeeRepository, _discountRepository,
            _productRepository, _orderRepository);

        handler.Handle(command);

        return handler;
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void Dado_um_cliente_inexistente_o_pedido_nao_deve_ser_gerado()
    {
        var command = new CreateOrderCommand();
        command.Custumer = "00011122233";
        command.ZipCode = "12345678";
        command.PromoCode = "12345678";
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

        var handler = _criarOrderHandler(command);

        Assert.IsFalse(handler.Valid);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void Dado_um_cep_invalido_o_pedido_deve_ser_gerado_normalmente()
    {
        var command = new CreateOrderCommand();
        command.Custumer = "12345678901";
        command.ZipCode = "11122233";
        command.PromoCode = "12345678";
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

        var handler = _criarOrderHandler(command);

        Assert.IsTrue(handler.Valid);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void Dado_um_promocode_inexistente_o_pedido_deve_ser_gerado_normalmente()
    {
        var command = new CreateOrderCommand();
        command.Custumer = "12345678901";
        command.ZipCode = "11122233";
        command.PromoCode = "00011122";
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

        var handler = _criarOrderHandler(command);

        Assert.IsTrue(handler.Valid);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void Dado_um_pedido_sem_itens_o_mesmo_nao_deve_ser_gerado()
    {
        var command = new CreateOrderCommand();
        command.Custumer = "";
        command.ZipCode = "12345678";
        command.PromoCode = "12345678";

        var handler = _criarOrderHandler(command);

        Assert.IsFalse(handler.Valid);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void Dado_um_comando_invalido_o_pedido_nao_deve_ser_gerado()
    {
        var command = new CreateOrderCommand();
        command.Custumer = "";
        command.ZipCode = "12345678";
        command.PromoCode = "12345678";
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

        var handler = _criarOrderHandler(command);

        Assert.IsFalse(handler.Valid);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void Dado_um_comando_valido_o_pedido_deve_ser_gerado()
    {
        var command = new CreateOrderCommand();
        command.Custumer = "12345678901";
        command.ZipCode = "12345678";
        command.PromoCode = "12345678";
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

        var handler = _criarOrderHandler(command);

        Assert.IsTrue(handler.Valid);
    }
}