using UnityEngine;

public enum DefaultActions
{
    // Movement
    MoveUp,
    MoveDown,
    MoveLeft,
    MoveRight,
    
    // Other
    Interact,
    OnStageComplete
}

// Override for Default Action to get value from vector
public static class DefaultActionsExtensions
{
    public static DefaultActions GetDefaultAction(this Vector2 direction)
    {
        if (direction.y > 0)
            return DefaultActions.MoveUp;
        if (direction.y < 0)
            return DefaultActions.MoveDown;
        if (direction.x < 0)
            return DefaultActions.MoveLeft;
        if (direction.x > 0)
            return DefaultActions.MoveRight;
        
        return DefaultActions.Interact;
    }
}
