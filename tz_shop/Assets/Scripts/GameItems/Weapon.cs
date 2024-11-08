public abstract class Weapon : GameItem
{
    [UnityEngine.SerializeField] private UnityEngine.Sprite _bulletSprite;
    [UnityEngine.SerializeField, UnityEngine.Range(3, 10)] private int _bulletAmount;
    [UnityEngine.SerializeField, UnityEngine.Range(10, 50)] protected float _bulletSpeed;

    private UnityEngine.GameObject _weapon;
    protected ObjectPool _pool;

    public override GameItem InitItem(UnityEngine.Transform snapPoint)
    {
        _weapon = GenerateGameItem(snapPoint);
        _pool = new ObjectPool(_bulletSprite, _bulletAmount, _weapon.transform);
        return this;
    }

    public override void ResetItem()
    {
        _pool.CleanPool();
        UnityEngine.GameObject.Destroy(_weapon);
    }

    public abstract void Shoot(UnityEngine.Vector2 dir);
}
