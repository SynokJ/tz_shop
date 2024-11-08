using UnityEngine;

[CreateAssetMenu(fileName = "ShopItem", menuName = "Scriptables/ShopItem")]
public class ShopItemSO : ScriptableObject
{
    public int coinCost;
    public int gemCost;
    public int subHour;
    public int subMin;
    public int subSec;
    public bool isBought;
    public Sprite prevImage;
    public GameItem gameItem;
}
