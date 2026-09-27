using UnityEngine;

public abstract class ComboSpell : Spell
{
    [SerializeField] protected ComboAction[] requiredComboActions;
    public ComboAction[] RequiredComboActions => requiredComboActions;
}
