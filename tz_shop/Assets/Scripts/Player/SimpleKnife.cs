using UnityEngine;

[CreateAssetMenu(fileName = "SimpleKnife", menuName = "Scriptables/Melee/SimpleKnife")]
public class SimpleKnife : Melee
{
    public override void Hit()
    {
        Debug.Log($"hit distance: {_hitDistance}, damage: {_damage}");
    }
}
