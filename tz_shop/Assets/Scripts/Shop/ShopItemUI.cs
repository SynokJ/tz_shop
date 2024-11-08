public class ShopItemUI
{
    private UnityEngine.GameObject _blockPanel;
    private UnityEngine.GameObject _activatePanel;
    private UnityEngine.GameObject _timerPanel;

    private UnityEngine.GameObject _coinButton;
    private UnityEngine.GameObject _gemsButton;
    private UnityEngine.GameObject _subscritionButton;

    public ShopItemUI(
        UnityEngine.GameObject blockPanel,
        UnityEngine.GameObject activatePanel,
        UnityEngine.GameObject timerpanel,
        UnityEngine.GameObject coinsButton,
        UnityEngine.GameObject gemsButton,
        UnityEngine.GameObject subButton
    )
    {
        _blockPanel = blockPanel;
        _activatePanel = activatePanel;
        _timerPanel = timerpanel;
        _coinButton = coinsButton;
        _gemsButton = gemsButton;
        _subscritionButton = subButton;
    }

    ~ShopItemUI()
    {
        _blockPanel = null;
        _activatePanel = null;
        _timerPanel = null;
        _coinButton = null;
        _gemsButton = null;
        _subscritionButton = null;
    }

    public void SetActiveStatusToBlockedComponents(bool status)
    {
        _blockPanel.SetActive(status);

        _coinButton.SetActive(status);
        _gemsButton.SetActive(status);
        _subscritionButton.SetActive(status);
    }

    public void SetActivateStatusToActivatedComponents(bool status)
        => _activatePanel.SetActive(status);

    public void SetActiveStatusToTimerComponents(bool status)
        => _timerPanel.SetActive(status);

}
