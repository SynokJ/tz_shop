using UnityEngine;

public abstract class Consumabels : GameItem
{
    [SerializeField] protected int _healthValue;

    private GameObject _consumables;

    public override GameItem InitItem(Transform snapPoint)
    {
        _consumables = GenerateGameItem(snapPoint);
        return this;
    }

    public override void ResetItem()
    {
        UnityEngine.GameObject.Destroy(_consumables);
    }

    public abstract void UseConsumable();
}
