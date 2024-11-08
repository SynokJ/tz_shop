using UnityEngine;

public abstract class Melee : GameItem
{
    [SerializeField] protected float _damage;
    [SerializeField] protected float _hitDistance;

    protected GameObject _melee;

    public override GameItem InitItem(Transform snapPoint)
    {
        _melee = GenerateGameItem(snapPoint);
        return this;
    }

    public override void ResetItem()
    {
        Destroy(_melee);
    }

    public abstract void Hit();
}
