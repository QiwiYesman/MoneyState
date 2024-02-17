namespace MoneyState.Model.Containers;

public abstract class EntityContainerBase
{
    public abstract void Read();
    public abstract void Delete(int id);
}