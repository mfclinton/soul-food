public abstract class Recipe : GameScriptableObject
{
    public bool TryExecute(CookState cookState)
    {
        if (Validate(cookState))
        {
            Execute();
            return true;
        }
        return false;
    }
    
    public abstract bool Validate(CookState cookState);
    
    public abstract void Execute();
}