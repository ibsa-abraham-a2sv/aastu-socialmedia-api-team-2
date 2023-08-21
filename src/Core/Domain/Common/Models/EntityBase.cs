namespace Domain.Common.Models;

public abstract class EntityBase {
    protected EntityBase(Guid id) => Id = id;

    protected EntityBase() {}

    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
