using UnityEngine;

[CreateAssetMenu(fileName = "Medicine", menuName = "Scriptables/Consumables/Medicine")]
public class Medicine : Consumabels
{
    [SerializeField] private HealType _type;

    public override void UseConsumable()
    {
        switch (_type)
        {
            case HealType.decrease:
                Debug.Log($"HP is removed ({_healthValue}) => {Time.time}");
                break;
            case HealType.increase:
                Debug.Log($"HP is added ({_healthValue}) => {Time.time}");
                break;
        }
    }
}

public enum HealType
{
    increase = 0,
    decrease = 1
}
