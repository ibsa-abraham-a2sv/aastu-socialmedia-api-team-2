namespace Domain.Common.Models;

public abstract class EntityBase {
    public EntityBase(Guid id) => Id = id;

    public EntityBase() {}

    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
