using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PopulateSpellDisplay : MonoBehaviour
{
    // Root UI element to add rows to
    [SerializeField] private Transform spellUIRoot;
    // Prefab for the row
    [SerializeField] private GameObject spellRowPrefab;
    // Prefab for the arrow
    [SerializeField] private GameObject spellImagePrefab;


    private void Awake()
    {
        PopulateSpells();
    }

    private void PopulateSpells()
    {
        // Get what the combos are
        var spellsList = DataManager.Instance.spellsList;
        foreach(Spell spell in spellsList)
        {
            if (spell is not ComboSpell)
                return;

            GameObject row = Instantiate(spellRowPrefab, spellUIRoot);

            // Set the icon corresponding to the spell. The row has a GameObject "PanelIcon" with an Image component
            Image spellImage = row.transform.Find("PanelIcon").GetComponent<Image>();
            spellImage.sprite = spell.icon;

            // Get the required combo actions
            ComboAction[] requiredComboActions = (spell as ComboSpell).RequiredComboActions;
            // For each combo, add an icon to the row
            foreach(ComboAction comboAction in requiredComboActions)
            {
                Sprite icon = DataManager.Instance.comboActionDataDictionary[comboAction].icon;

                // The row has a GameObject "PanelCombo" which expects the spellImagePrefabs to be added to it
                GameObject comboActionImage = Instantiate(spellImagePrefab, row.transform.Find("PanelCombo"));
                // comboActionImage has an Image component
                comboActionImage.GetComponent<Image>().sprite = icon;
            }
        }
    }
}
