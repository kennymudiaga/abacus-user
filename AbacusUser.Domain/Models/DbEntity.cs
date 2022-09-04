namespace AbacusUser.Domain.Models;

public abstract class DbEntity
{
    protected DbEntity()
    {
        Id = Guid.NewGuid().ToString();
    }
    public string Id { get; set; }
}
