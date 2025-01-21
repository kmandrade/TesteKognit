namespace Domain.Entities;

public class BaseEntity
{
    public int Id { get; set; }
    public bool? Active { get; set; }
    public DateTime? DateCreated { get; set; }
    public DateTime? DateModified { get; set; }
}

