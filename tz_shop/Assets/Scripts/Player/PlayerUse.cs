using System;

public class PlayerUse
{
    private UnityEngine.Transform _playerTr;
    private UnityEngine.Transform _snapPointTr;

    private GameItem _currentItem;
    private ShopItemTimer _timer;
    private PlayerDataManager _playerDataManager;
    private TMPro.TextMeshProUGUI _timerText;

    public delegate void OnGameItemChanged();
    private OnGameItemChanged _onGameItemChanged;

    public PlayerUse(
        UnityEngine.Transform playerTr,
        UnityEngine.Transform snapPointTr,
        PlayerDataManager dataManager,
        OnGameItemChanged itemChange,
        TMPro.TextMeshProUGUI timerText)
    {
        _timerText = timerText;
        _playerTr = playerTr;
        _snapPointTr = snapPointTr;
        _onGameItemChanged = itemChange;
        _playerDataManager = dataManager;
    }

    ~PlayerUse()
    {
        _playerTr = null;
        _currentItem = null;
        _playerDataManager = null;
    }

    public void SetCurrentItem(GameItem currentItem)
    {
        if (currentItem == null) return;

        _currentItem?.ResetItem();
        _currentItem = currentItem;
        _currentItem.InitItem(_snapPointTr);

        if (_currentItem.isTemporary)
            _timer = new ShopItemTimer(_currentItem.leftTime, CurrentItemTimeIsSpend, UpdateItemTimer, _currentItem);
        else
            _timerText.text = "infinity";
    }

    public void UseItem()
    {
        if (_currentItem == null) return;

        if (_currentItem is Weapon weapon)
        {
            UnityEngine.Vector2 dir = GetUseDirection();
            weapon.Shoot(dir);
        }
        else if (_currentItem is Consumabels consumabels)
            consumabels.UseConsumable();
        else if (_currentItem is Melee melee)
            melee.Hit();
    }

    public void TryUpdateItemTimer()
    {
        if (_currentItem == null) return;
        if (!_currentItem.isTemporary) return;
        _timer.ChangeTimer(UnityEngine.Time.deltaTime);
    }

    private UnityEngine.Vector2 GetUseDirection()
        => _playerTr.localScale.x < 0 ? UnityEngine.Vector2.left : UnityEngine.Vector2.right;

    public void CurrentItemTimeIsSpend()
    {
        if (_playerDataManager.TryGetShopItemData(out ShopItemDataManager dataManager))
        {
            _currentItem?.ResetItem();
            dataManager.RemoveBoughtItemById(_currentItem.GetInstanceID());
            _playerDataManager.SaveShopitemData(dataManager);

            _currentItem = null;
            _onGameItemChanged?.Invoke();
        }
    }

    public void UpdateItemTimer()
    {
        _timerText.text = _timer.TargetDate.ToLongTimeString();
    //    UnityEngine.Debug.LogAssertion(_timer.TargetDate.ToLongTimeString());
    }

    public CustomTimerValue GetLeftTime()
        => _timer == null ? new CustomTimerValue(0.0f, 0.0f, 0.0f) : _timer.TargetDate;
}
