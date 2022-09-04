using AbacusUser.Domain.Commands;

namespace AbacusUser.Domain.Models.Users;

public class UserProfile : DbEntity
{
    /// <summary>
    /// Protected constructor for Db tools (Mongo or EF)
    /// </summary>
    protected UserProfile() : base() { }
    public UserProfile(SignUpCommand model)
    {
        FirstName = model.FirstName?.Trim().ToLower();
        LastName = model.LastName?.Trim().ToLower();
        Email = model.Email?.Trim().ToLower();
        OtherNames = model.OtherNames?.Trim().ToLower();
        Username = model.Username?.Trim()?.ToLower();
        Phone = model.Phone;
        DateCreated = DateTime.UtcNow;
        DateOfBirth = model.DateOfBirth.GetValueOrDefault().Date;
        Gender = model.Gender?.ToLower();
        Id = Guid.NewGuid().ToString();
    }
    public string? FirstName { get; protected set; }
    public string? LastName { get; protected set; }
    public string? Username { get; protected set; }
    public string? PasswordHash { get; protected set; }
    public string? OtherNames { get; protected set; }
    public string? Email { get; protected set; }
    public string? Phone { get; protected set; }
    public List<UserOrg>? UserOrgs { get; protected set; }
    public int AccessFailedCount { get; set; }
    public bool IsAccountLocked { get; set; }
    public DateTime? LockoutExpiry { get; set; }
    public DateTime DateCreated { get; protected set; }
    public DateTime? LastPasswordChange { get; protected set; }
    public string? PasswordToken { get; set; }
    public DateTime? PasswordTokenExpiry { get; set; }
    public string? CreatorId { get; set; }
    public string? Gender { get; set; }
    public DateTime? LastLogin { get; set; }

    public string Name => $"{FirstName}{MiddleNameOrSpace}{LastName}";
    private string MiddleNameOrSpace => string.IsNullOrWhiteSpace(OtherNames) ? " " : $" {OtherNames} ";

    public DateTime DateOfBirth { get; set; }

    public string GeneratePasswordToken(DateTime expiry)
    {
        var token = new Random().Next(100001, 1000000).ToString();
        PasswordToken = SaltToken(Username, token);
        PasswordTokenExpiry = expiry;
        return token;
    }

    public bool IsPasswordTokenExpired() =>
        (!string.IsNullOrEmpty(PasswordToken)) &&
        PasswordTokenExpiry.HasValue &&
        DateTime.UtcNow > PasswordTokenExpiry;
    public void SetPassword(string passwordHash)
    {
        PasswordHash = passwordHash;
        PasswordToken = null;
        PasswordTokenExpiry = null;
        LastPasswordChange = DateTime.UtcNow;
    }

    public void AddOrg(UserOrg org)
    {
        if (string.IsNullOrWhiteSpace(org?.OrgId)) throw new ArgumentException("Invalid organization or organization ID");
        if (UserOrgs == null) UserOrgs = new List<UserOrg>();
        else if (UserOrgs.Any(o => string.Equals(o.OrgId, org.OrgId, StringComparison.OrdinalIgnoreCase)))
        {
            throw new ApplicationException("User is already in this organization.");
        }
        UserOrgs.Add(org);
    }

    public void AddOrg(string orgId, bool isOwner) => AddOrg(new(orgId, isOwner));

    public void RemoveOrg(string orgId)
    {
        if (string.IsNullOrWhiteSpace(orgId)) throw new ArgumentException($"{nameof(orgId)} must be a non-empty string.");
        var org = UserOrgs?.FirstOrDefault(org => org.OrgId == orgId);
        if (UserOrgs == null || org == null) throw new ApplicationException("User does not belong to the organization!");
        UserOrgs.Remove(org);
    }

    public string SaltToken(string token) => SaltToken(Username, token);

    public static string SaltToken(string? username, string token)
    {
        return $"{username}|{token}";
    }
}
