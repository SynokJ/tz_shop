using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private GameObject _blockPanel;
    [SerializeField] private GameObject _timerPanel;
    [SerializeField] private GameObject _activatePanel;

    [SerializeField] private Image _prevImage;

    [SerializeField] private GameObject _coinButton;
    [SerializeField] private GameObject _gemsButton;
    [SerializeField] private GameObject _subscritionButton;

    [SerializeField] private TMPro.TextMeshProUGUI _timerText;
    [SerializeField] private TMPro.TextMeshProUGUI _coinsCost;
    [SerializeField] private TMPro.TextMeshProUGUI _gemsCost;
    [SerializeField] private TMPro.TextMeshProUGUI _subscriptionsCost;

    private ShopItemSO _shopItemSO;
    private ShopItemUI _shopItemUI;
    private ShopItemTimer _shopItemTimer;

    private PlayerDataManager _playerDataManager;

    public delegate void OnShopItemsChanged(ShopItemSO item);
    private OnShopItemsChanged _itemPurchased;
    private OnShopItemsChanged _itemReseted;

    public void InitData(
        ShopItemSO item,
        PlayerDataManager dataManager,
        OnShopItemsChanged itemPurchase,
        OnShopItemsChanged itemReset)
    {
        _shopItemSO = item;
        _playerDataManager = dataManager;
        _itemPurchased = itemPurchase;
        _itemReseted = itemReset;

        InitVisualComponenets();
        UpdateShopItemInfo();

        if (_playerDataManager.TryGetShopItemData(out ShopItemDataManager tempDataManager))
        {
            if (tempDataManager.ContainsTempItem(_shopItemSO.gameItem.GetInstanceID()))
            {
                if (_shopItemTimer == null)
                {
                    _shopItemTimer = new ShopItemTimer(_shopItemSO.gameItem.leftTime, EndTimerAction, ChangeTimerValue, _shopItemSO.gameItem);
                    _playerDataManager.SaveShopitemData(tempDataManager);
                }

                _shopItemUI.SetActiveStatusToBlockedComponents(false);
                _shopItemUI.SetActivateStatusToActivatedComponents(false);
                _shopItemUI.SetActiveStatusToTimerComponents(true);
            }
        }
    }

    private void InitVisualComponenets()
    {
        _shopItemUI = new ShopItemUI(_blockPanel, _activatePanel, _timerPanel, _coinButton, _gemsButton, _subscritionButton);

        _prevImage.sprite = _shopItemSO.prevImage;
        _coinsCost.text = _shopItemSO.coinCost.ToString();
        _gemsCost.text = _shopItemSO.gemCost.ToString();
        _subscriptionsCost.text = $"{_shopItemSO.subHour}:{_shopItemSO.subMin}:{_shopItemSO.subSec}";
    }

    public void OnCoinButtonClicked()
    {
        if (_playerDataManager.TrySpendCoins(_shopItemSO.coinCost))
        {
            _shopItemSO.isBought = true;
            _itemPurchased?.Invoke(_shopItemSO);

            UpdateShopItemInfo();
        }
    }

    public void OnGemsButtonClicked()
    {
        if (_playerDataManager.TrySpendGems(_shopItemSO.gemCost))
        {
            _shopItemSO.isBought = true;
            _itemPurchased?.Invoke(_shopItemSO);

            UpdateShopItemInfo();
        }
    }

    public void OnSubscriptionButtonClicked()
    {
        if (_shopItemTimer != null) return;

        _shopItemSO.isBought = true;
        _shopItemSO.gameItem.isTemporary = true;
        _itemPurchased?.Invoke(_shopItemSO);

        _shopItemUI.SetActiveStatusToTimerComponents(true);
        _shopItemUI.SetActiveStatusToBlockedComponents(false);
        _shopItemUI.SetActivateStatusToActivatedComponents(false);

        int hour = _shopItemSO.subHour;
        int min = _shopItemSO.subMin;
        int sec = _shopItemSO.subSec;
        _shopItemTimer = new ShopItemTimer(hour, min, sec, EndTimerAction, ChangeTimerValue, _shopItemSO.gameItem);
        if(_playerDataManager.TryGetShopItemData(out ShopItemDataManager dataManager))
        {
            dataManager.RegisterBoughtItem(_shopItemSO);
            _playerDataManager.SaveShopitemData(dataManager);
        }
                }

    private void Update()
    {
        _shopItemTimer?.ChangeTimer(Time.deltaTime);
    }

    public void UpdateShopItemInfo()
    {
        if (_shopItemSO.isBought == false)
        {
            _shopItemUI.SetActiveStatusToBlockedComponents(true);
            _shopItemUI.SetActivateStatusToActivatedComponents(false);
            _shopItemUI.SetActiveStatusToTimerComponents(false);
        }
        else
        {
            _shopItemUI.SetActiveStatusToBlockedComponents(false);
            _shopItemUI.SetActivateStatusToActivatedComponents(true);
            _shopItemUI.SetActiveStatusToTimerComponents(false);
        }
    }

    public void ChangeTimerValue()
    {
        _timerText.text = _shopItemTimer.TargetDate.ToLongTimeString();
    }

    public void EndTimerAction()
    {
        _shopItemTimer = null;
        _shopItemSO.isBought = false;
        UpdateShopItemInfo();

        _itemReseted?.Invoke(_shopItemSO);
    }
}
