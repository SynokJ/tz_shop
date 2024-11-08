using System.Collections.Generic;

public class ObjectPool
{

    private Queue<UnityEngine.GameObject> _bullets = new Queue<UnityEngine.GameObject>();
    private UnityEngine.Transform _originTr;

    public ObjectPool(UnityEngine.Sprite bulletSprite, int amount, UnityEngine.Transform originTr)
    {
        _originTr = originTr;

        for (int i = 0; i < amount; ++i)
        {
            UnityEngine.GameObject bulletPref = new UnityEngine.GameObject("Bullet " + i);
            GenerateRenderer(bulletPref, bulletSprite);
            GenerateRigidbody2D(bulletPref);

            bulletPref.SetActive(false);
            _bullets.Enqueue(bulletPref);
        }
    }

    ~ObjectPool()
    {
        CleanPool();
    }

    public void CleanPool()
    {
        foreach (var item in _bullets)
            UnityEngine.GameObject.Destroy(item.gameObject);
        _bullets.Clear();
    }

    public UnityEngine.GameObject InstantiateFromPool()
    {
        UnityEngine.GameObject obj = _bullets.Dequeue();
        obj.SetActive(true);
        obj.transform.position = _originTr.position;

        _bullets.Enqueue(obj);
        return obj;
    }

    private void GenerateRenderer(UnityEngine.GameObject bulletPref, UnityEngine.Sprite _bulletSprite)
    {
        UnityEngine.SpriteRenderer renderer = bulletPref.AddComponent<UnityEngine.SpriteRenderer>();
        renderer.sprite = _bulletSprite;
    }

    private void GenerateRigidbody2D(UnityEngine.GameObject bulletPref)
    {
        UnityEngine.Rigidbody2D rb = bulletPref.AddComponent<UnityEngine.Rigidbody2D>();
        rb.gravityScale = 0.0f;
    }
}
