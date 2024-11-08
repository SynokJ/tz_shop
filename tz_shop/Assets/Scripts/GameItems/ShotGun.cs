[UnityEngine.CreateAssetMenu(fileName = "ShotGunSO", menuName = "Scriptables/Weapon/ShotGunSO")]
public class ShotGun : Weapon
{
    public override void Shoot(UnityEngine.Vector2 dir)
    {
        UnityEngine.GameObject bullet = _pool.InstantiateFromPool();
        if (bullet.TryGetComponent(out UnityEngine.Rigidbody2D rb))
        {
            rb.velocity = UnityEngine.Vector2.zero;
            rb.AddForce(dir * _bulletSpeed, UnityEngine.ForceMode2D.Impulse);
        }
    }
}
