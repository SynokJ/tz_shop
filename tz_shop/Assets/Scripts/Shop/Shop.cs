using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private const float _LIMIT_WIDTH_OFFSET = 800.0f;

    [SerializeField] private Canvas _canvas;
    [SerializeField] private ShopItem _shopItemPref;
    [SerializeField] private ShopItemSO[] _shopitemSOs;
    [SerializeField] private PlayerDataManager _playerDataManager;

    private ShopItemDataManager _shopItemDataManager;
    private List<ShopItem> _shopItems = new List<ShopItem>();

    private void Start()
    {
        if (_playerDataManager.TryGetShopItemData(out ShopItemDataManager dataManager))
            _shopItemDataManager = dataManager;
        else
            _shopItemDataManager = new ShopItemDataManager();

        int size = _shopitemSOs.Length;
        float heightOffset = 0.0f;
        float widthOffset = 0.0f;

        for (int i = 0; i < size; ++i)
            GenerateItem(ref widthOffset, ref heightOffset, i);
    }

    private void GenerateItem(ref float widthOffset, ref float heightOffset, int i)
    {

        GameObject tempItem = Instantiate(_shopItemPref.gameObject);
        RectTransform targetTr = tempItem.GetComponent<RectTransform>();
        targetTr.SetParent(_canvas.GetComponent<RectTransform>(), false);

        float posX = widthOffset * targetTr.rect.width * 2.0f;
        float posY = heightOffset * targetTr.rect.height * 2.0f;

        targetTr.position += Vector3.right * posX + Vector3.down * posY;
        widthOffset++;
        if (posX > _LIMIT_WIDTH_OFFSET)
        {
            widthOffset = 0.0f;
            heightOffset++;
        }

        if (tempItem.TryGetComponent(out ShopItem item))
        {
            _shopitemSOs[i].isBought = _shopItemDataManager.ContainsShopItem(_shopitemSOs[i].gameItem.GetInstanceID());
            _shopitemSOs[i].gameItem.isTemporary = _shopItemDataManager.ContainsTempItem(_shopitemSOs[i].gameItem.GetInstanceID());
            item.InitData(_shopitemSOs[i], _playerDataManager, OnPurchaseCompleted, OnPurchaseReset);
            _shopItems.Add(item);
        }
    }

    public void ResetPurchases()
    {
        _shopItemDataManager = new ShopItemDataManager();
        int size = _shopitemSOs.Length;
        for (int i = 0; i < size; ++i)
        {
            _shopitemSOs[i].isBought = _shopItemDataManager.ContainsShopItem(_shopitemSOs[i].gameItem.GetInstanceID());
            _shopitemSOs[i].gameItem.isTemporary = false;
        }

        for (int i = 0; i < size; ++i)
        {
            _shopItems[i].EndTimerAction();
            _shopItems[i].UpdateShopItemInfo();
        }

        _playerDataManager.SaveShopitemData(_shopItemDataManager);
    }

    public void OnPurchaseCompleted(ShopItemSO item)
    {
        _shopItemDataManager.RegisterBoughtItem(item);
        _playerDataManager.SaveShopitemData(_shopItemDataManager);
    }

    public void OnPurchaseReset(ShopItemSO item)
    {
        _shopItemDataManager.RemoveBoughtItem(item);
    }
}
