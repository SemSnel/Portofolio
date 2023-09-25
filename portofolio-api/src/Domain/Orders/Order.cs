using SemSnel.Portofolio.Domain.Common.Entities;

namespace SemSnel.Portofolio.Domain.Orders;

public sealed class Order : AggregateRoot<Guid>, IAuditableEntity
{
    public Guid? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public Guid? LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public Guid? DeletedBy { get; set; }
    public DateTime? DeletedOn { get; set; }
    public bool IsDeleted { get; set; }
    
    public IEnumerable<Guid> OrderItemsIds { get; init; } = Enumerable.Empty<Guid>();
    public IEnumerable<OrderItem> OrderItems { get; init; } = Enumerable.Empty<OrderItem>();
}

public sealed class OrderItem : Entity<Guid>
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public decimal Tax { get; set; }
    public decimal Total { get; set; }
}

