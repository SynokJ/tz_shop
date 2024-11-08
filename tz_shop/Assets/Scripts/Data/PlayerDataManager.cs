using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    private const string _SHOP_ITEM_JSON_NAME = "ShopItemsData";

    private static int _coinsAmount = 99999;
    private static int _gemsAmount = 99999;

    private void OnDestroy()
    {
        
    }

    public bool TrySpendCoins(int cost)
    {
        if (_coinsAmount - cost < 0) return false;
        _coinsAmount -= cost;
        return true;
    }

    public bool TrySpendGems(int cost)
    {
        if (_gemsAmount - cost < 0) return false;
        _gemsAmount -= cost;
        return true;
    }

    public bool TryGetShopItemData(out ShopItemDataManager dataManager)
    {
        string result = PlayerPrefs.GetString(_SHOP_ITEM_JSON_NAME);
        dataManager = JsonUtility.FromJson<ShopItemDataManager>(result);
        //UnityEngine.Debug.Log("Load Data TryGetShopItem => " + result);
        return dataManager != null;
    }

    public void SaveShopitemData(ShopItemDataManager dataManager)
    {
        string result = JsonUtility.ToJson(dataManager);
        //UnityEngine.Debug.Log("Save Data TryGetShopItem => " + result);
        PlayerPrefs.SetString(_SHOP_ITEM_JSON_NAME, result);
    }
}
