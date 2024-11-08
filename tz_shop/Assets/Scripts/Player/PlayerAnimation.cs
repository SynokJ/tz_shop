public class PlayerAnimation
{
    private const string _IDLE_NAME = "on_run";
    private UnityEngine.Animator _playerAnimator;

    public PlayerAnimation(UnityEngine.Animator playerAnimator)
    {
        _playerAnimator = playerAnimator;
    }

    ~PlayerAnimation()
    {
        _playerAnimator = null;
    }

    public void PlayMovementAnimation()
    {
        if (!_playerAnimator.GetBool(_IDLE_NAME))
            _playerAnimator.SetBool(_IDLE_NAME, true);
    }

    public void StopMovementAnimation()
    {
        if (_playerAnimator.GetBool(_IDLE_NAME))
            _playerAnimator.SetBool(_IDLE_NAME, false);
    }
}
