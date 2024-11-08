using UnityEngine.EventSystems;

public class PlayerInput
{
    public PlayerInput()
    {

    }

    ~PlayerInput()
    {
    }

    public bool TryGetUseInput()
        => UnityEngine.Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject();

    public bool TryGetMovementInput(out UnityEngine.Vector2 movementDir)
    {
        float horDir = UnityEngine.Input.GetAxisRaw("Horizontal");
        float verDir = UnityEngine.Input.GetAxisRaw("Vertical");
        movementDir = GetClamppedValue(horDir, verDir);
        return !(horDir == 0.0f && verDir == 0.0f);
    }

    private UnityEngine.Vector2 GetClamppedValue(float hor, float ver)
    {
        if (System.MathF.Abs(hor) == 1.0f && System.MathF.Abs(ver) == 1.0f)
            return new UnityEngine.Vector2(hor, ver) * 0.5f;
        return new UnityEngine.Vector2(hor, ver);
    }

    public bool TrySwitchToNextWeapon()
        => UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.E);

    public bool TrySwitchToPrevWeapon()
        => UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Q);
}
