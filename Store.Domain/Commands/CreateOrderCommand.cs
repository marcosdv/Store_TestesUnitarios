using Flunt.Notifications;
using Flunt.Validations;
using Store.Domain.Commands.Interfaces;

namespace Store.Domain.Commands;

public class CreateOrderCommand : Notifiable, ICommand
{
    public CreateOrderCommand()
    {
        Items = new List<CreateOrderItemCommand>();
    }

    public CreateOrderCommand(string custumer, string zipCode, string promoCode, IList<CreateOrderItemCommand> items)
    {
        Custumer = custumer;
        ZipCode = zipCode;
        PromoCode = promoCode;
        Items = items;
    }

    public string Custumer { get; set; }
    public string ZipCode { get; set; }
    public string PromoCode { get; set; }
    public IList<CreateOrderItemCommand> Items { get; set; }

    public void Validate()
    {
        AddNotifications(new Contract()
            .Requires()
            .HasLen(Custumer, 11, "Custumer", "Cliente inválido")
            .HasLen(ZipCode, 8, "ZipCode", "CEP inválido")
        );
    }
}