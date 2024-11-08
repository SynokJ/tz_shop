using System;
using UnityEngine;

public abstract class GameItem : ScriptableObject
{
    [SerializeField] protected Sprite _itemSprite;
    public bool isAvailable;
    public bool isTemporary;
    public CustomTimerValue leftTime;

    public abstract GameItem InitItem(Transform snapPoint);
    public abstract void ResetItem();

    protected GameObject GenerateGameItem(Transform snapPoint)
    {
        GameObject gameItem = new UnityEngine.GameObject(this.GetType().ToString());
        gameItem.transform.SetParent(snapPoint, false);

        UnityEngine.SpriteRenderer renderer = gameItem.AddComponent<UnityEngine.SpriteRenderer>();
        renderer.sprite = _itemSprite;
        renderer.sortingOrder = 1;
        return gameItem;
    }
}
