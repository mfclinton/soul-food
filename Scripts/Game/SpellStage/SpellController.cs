using System;
using UnityEngine;
using System.Linq;

public class SpellController : MonoBehaviour
{
    // Events
    public event Action<Spell> OnSpellExecuted;
    
    private void Awake()
    {
        ComboController comboController = FindAnyObjectByType<ComboController>();
        comboController.OnComboCompleted += OnComboCompleted;
    }

    private void OnComboCompleted(ComboState comboState)
    {
        Spell spell = GetFirstValidSpell(comboState);
        if (spell == null)
            return;
        
        spell.Execute();
        OnSpellExecuted?.Invoke(spell);
    }

    #region Helper Methods

    public Spell GetFirstValidSpell(ComboState comboState)
    {
        return DataManager.Instance.spellsList.FirstOrDefault(spell => spell.Validate(comboState));
    }
    
    public Spell[] GetValidSpells(ComboState comboState)
    {
        return DataManager.Instance.spellsList.Where(spell => spell.Validate(comboState)).ToArray();
    }

    #endregion
}
