using System;

namespace WildBeard.Orders.Model
{
    public interface IEntity
    {
        Guid Id { get; set; }

        DateTime CreatedAt { get; set; }

        DateTime? LastModifiedAt { get; set; }
    }
}
