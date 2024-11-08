public class PlayerMovement
{
    private const float _MOVEMENT_SPEED = 10.0f;

    private UnityEngine.Rigidbody2D _playerRb;
    private UnityEngine.Transform _playerTr;

    public PlayerMovement(UnityEngine.Rigidbody2D playerRb, UnityEngine.Transform playerTr)
    {
        _playerRb = playerRb;
        _playerTr = playerTr;
    }

    ~PlayerMovement()
    {
        _playerRb = null;
        _playerTr = null;
    }

    public void Move(UnityEngine.Vector2 dir)
    {
        float speed = _MOVEMENT_SPEED * UnityEngine.Time.deltaTime;
        _playerRb.MovePosition((UnityEngine.Vector2)_playerTr.position + dir * speed);

        TryToRotateByDirection(dir.x);
    }

    private void TryToRotateByDirection(float horDir)
    {
        if (horDir < 0) _playerTr.localScale = new UnityEngine.Vector3(-1.0f, 1.0f, 1.0f);
        else if(horDir > 0) _playerTr.localScale = new UnityEngine.Vector3(1.0f, 1.0f, 1.0f);
    }
}
