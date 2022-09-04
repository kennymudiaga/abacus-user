namespace AbacusUser.Domain.Models.Users;

public class UserOrg
{
    /// <summary>
    /// Constructor required for frameworks (mongo, EF)
    /// </summary>
    protected UserOrg() 
    {
        Roles = new List<string>();
    }
    public UserOrg(string orgId, bool isOwner)
        : this()
    {
        if (string.IsNullOrEmpty(orgId)) throw new ArgumentException("orgId must be a non-empty string.", nameof(orgId));
        OrgId = orgId;
        DateAdded = DateTime.UtcNow;
        if (isOwner)
        {
            Roles.Add("owner");
        }
    }
    public string? OrgId { get; set; }
    public List<string> Roles { get; set; }
    public bool IsOwner { get; set; }
    public DateTime DateAdded { get; set; }
    public DateTime? DateLastModified { get; set; }
}
