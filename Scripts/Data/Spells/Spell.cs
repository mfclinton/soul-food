using System;
using UnityEngine;

public abstract class Spell : GameScriptableObject
{
    public bool TryExecute(ComboState comboState)
    {
        if (Validate(comboState))
        {
            Execute();
            return true;
        }
        return false;
    }
    
    public abstract bool Validate(ComboState comboState);
    
    public abstract void Execute();
}
