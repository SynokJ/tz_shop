using UnityEngine;

public class ShopItemTimer
{
    public CustomTimerValue TargetDate { get => _targetDate; }
    private CustomTimerValue _targetDate;

    public delegate void EndTimerAction();
    private EndTimerAction _endTimerAction;

    public delegate void TimerAction();
    private TimerAction _timerAction;

    private GameItem _gameItem;

    public ShopItemTimer(int hour, int min, int sec, EndTimerAction timerFinished, TimerAction timerAction, GameItem item)
    {
        _gameItem = item;
        _timerAction = timerAction;
        _gameItem.isTemporary = true;
        _endTimerAction = timerFinished;

        _targetDate = new CustomTimerValue(hour, min, sec);
    }

    public ShopItemTimer(CustomTimerValue timer, EndTimerAction timerFinished, TimerAction timerAction, GameItem item)
    {
        _gameItem = item;
        _targetDate = timer;
        _timerAction = timerAction;
        _endTimerAction = timerFinished;
    }

    ~ShopItemTimer()
    {
        _endTimerAction = null;
        _gameItem.leftTime = _targetDate;
    }

    public void ChangeTimer(float deltaTime)
    {
        if (_targetDate.TryAddSeconds(deltaTime))
        {
            _timerAction?.Invoke();
        }
        else
        {
            _endTimerAction?.Invoke();
            _gameItem.isTemporary = false;
        }
    }
}
