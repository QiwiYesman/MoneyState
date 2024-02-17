using SQLite;

namespace MoneyState.Model.Entities;

public class EntityBase
{
    [PrimaryKey, AutoIncrement] 
    public int Id { get; set; } = 0;

    public EntityBase()
    {
        
    }
}