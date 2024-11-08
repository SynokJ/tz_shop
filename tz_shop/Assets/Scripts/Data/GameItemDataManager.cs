using System;
using System.Collections.Generic;
using UnityEngine;

public class GameItemDataManager : MonoBehaviour
{
    [SerializeField] private PlayerDataManager _playerDataManager;
    [SerializeField] private GameItem[] _gameItems;

    private List<GameItem> _availableGameItems = new List<GameItem>();
    private int _itemId = 0;

    private void Awake()
    {
        InitAvailableGameItems();
    }

    public void InitAvailableGameItems()
    {
        _availableGameItems.Clear();

        if (_playerDataManager.TryGetShopItemData(out ShopItemDataManager dataManager))
        {
            int size = _gameItems.Length;
            for (int i = 0; i < size; ++i)
            {
                if (!dataManager.ContainsShopItem(_gameItems[i].GetInstanceID()))
                {
                    _gameItems[i].isTemporary = dataManager.ContainsTempItem(_gameItems[i].GetInstanceID());
                    _gameItems[i].isAvailable = false;
                    if (dataManager.TryGetTiemById(_gameItems[i].GetInstanceID(), out CustomTimerValue timer))
                        _gameItems[i].leftTime = timer;
                    continue;
                }
                _gameItems[i].isAvailable = true;
                _availableGameItems.Add(_gameItems[i]);
            }
        }
    }

    public bool TryGetCurrentItem(out GameItem result)
    {
        result = null;
        if (_availableGameItems == null || _availableGameItems.Count == 0) return false;
        result = _availableGameItems[_itemId];
        return true;
    }

    public void SwitchToNextItem(CustomTimerValue time)
    {
        RegisterLeftTime(time);
        _itemId++;
        if (_itemId >= _availableGameItems.Count)
            _itemId = 0;
    }

    public void SwitchToPrevItem(CustomTimerValue time)
    {
        RegisterLeftTime(time);
        _itemId--;
        if (_itemId < 0)
            _itemId = _availableGameItems.Count - 1;
    }

    public void RegisterLeftTime(CustomTimerValue time)
    {
        if (_availableGameItems.Count > 0 && _itemId < _availableGameItems.Count && _itemId > 0)
            _availableGameItems[_itemId].leftTime = time;
    }
}
