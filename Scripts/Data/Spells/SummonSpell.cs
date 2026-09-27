

using UnityEngine;

[CreateAssetMenu(fileName = "NewSummonSpell", menuName = "Spells/SummonSpell")]
public class SummonSpell : ComboSpell
{
    [SerializeField] private MonsterData monsterData;
    
    public override bool Validate(ComboState comboState)
    {
        return comboState.CompareComboBuffer(requiredComboActions);
    }

    public override void Execute()
    {
        MonsterController.Instance.SummonMonster(monsterData);
    }
}