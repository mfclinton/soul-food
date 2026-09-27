using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIDebuggerSpellController : MonoBehaviour
{
    [SerializeField] private Transform spellUIRoot;
    [SerializeField] private GameObject spellRowPrefab;
    [SerializeField] private TextMeshProUGUI spellNameText;
    [SerializeField] private Image spellImagePrefab;
    
    private void Awake()
    {
        InitializeComboSpellsUI();
    }

    private void InitializeComboSpellsUI()
    {
        foreach (Spell spell in DataManager.Instance.spellsList)
        {
            if (!spell is ComboSpell)
                return;
            
            // Add a new row
            GameObject row = Instantiate(spellRowPrefab, spellUIRoot);
            
            // Add Header
            string spellName = spell.dataName;
            TextMeshProUGUI header = Instantiate(spellNameText, row.transform);
            header.text = spellName;
            
            // Add required combo actions
            ComboAction[] requiredComboActions = (spell as ComboSpell).RequiredComboActions;
            foreach (ComboAction comboAction in requiredComboActions)
            {
                Sprite icon = DataManager.Instance.comboActionDataDictionary[comboAction].icon;
                
                // Instantiate combo images
                Image comboActionImage = Instantiate(spellImagePrefab, row.transform);
                comboActionImage.sprite = icon;
            }
            
            // Add Spell Symbol
            Image spellImage = Instantiate(spellImagePrefab, row.transform);
            spellImage.sprite = spell.icon;
        }
    }
}
