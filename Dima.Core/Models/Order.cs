using Dima.Core.Enums;

namespace Dima.Core.Models;

public class Order
{
    public long Id { get; set; }
    public string Number { get; set; } = Guid.NewGuid().ToString("N")[..8]; //Pegar somente numeros e de 0 a 8 primeiros

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public EOrderStatus Status { get; set; } = EOrderStatus.WaitingPayment;

    public string? ExternalReference { get; set; } //Numero do pedido no gateway de pagamento
    public EPaymentGateway Gateway { get; set; } = EPaymentGateway.Stripe;


    public long ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public long? VoucherId { get; set; }
    public Voucher? Voucher { get; set; }

    public string UserId { get; set; } = string.Empty;

    public decimal Total => Product.Price - (Voucher?.Amount ?? 0);
}
