namespace Domain;

public class AuditLog
{
    public int Id { get; set; }

    public string Action { get; set; } = string.Empty;

    public string Details {  get; set; } = string.Empty;

    public string CreatedBy {  get; set; } = string.Empty;

    public DateTime UpdatedBy { get; set;} = DateTime.UtcNow;
}
