using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _snapPoint;
    [SerializeField] private GameItemDataManager _dataManager;
    [SerializeField] private PlayerDataManager _playerDataManager;
    [SerializeField] private TMPro.TextMeshProUGUI _timerText;
     
    private PlayerUse _use;
    private Rigidbody2D _rb;
    private PlayerInput _input;
    private PlayerMovement _movement;
    private PlayerAnimation _animation;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0.0f;

        _input = new PlayerInput();
        _movement = new PlayerMovement(_rb, transform);
        _animation = new PlayerAnimation(_animator);

        _use = new PlayerUse(transform, _snapPoint, _playerDataManager, UpdateGameItems, _timerText);
    }

    private void Start()
    {
        if (_dataManager.TryGetCurrentItem(out GameItem item))
            _use.SetCurrentItem(item);
    }

    void Update()
    {
        MoveObject();
        UsePlayerGameItem();

        _use.TryUpdateItemTimer();
    }

    private void MoveObject()
    {
        if (_input.TryGetMovementInput(out Vector2 dir))
        {
            _movement.Move(dir);
            _animation.PlayMovementAnimation();
        }
        else
            _animation.StopMovementAnimation();
    }

    private void UsePlayerGameItem()
    {
        if (_input.TryGetUseInput())
            _use.UseItem();

        if (_input.TrySwitchToNextWeapon())
        {
            _dataManager.SwitchToNextItem(_use.GetLeftTime());
            if (_dataManager.TryGetCurrentItem(out GameItem item))
                _use.SetCurrentItem(item);
        }

        if (_input.TrySwitchToPrevWeapon())
        {
            _dataManager.SwitchToPrevItem(_use.GetLeftTime());
            if (_dataManager.TryGetCurrentItem(out GameItem item))
                _use.SetCurrentItem(item);
        }
    }

    public void UpdateGameItems()
    {
        _dataManager.InitAvailableGameItems();
        if (_dataManager.TryGetCurrentItem(out GameItem item))
            _use.SetCurrentItem(item);
    }
}
