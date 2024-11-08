using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    private const string _SHOP_SCENE_NAME = "Shop";
    private const string _GAME_SCENE_NAME = "MainGame";

    public void OnShopButtonClicked()
        => SceneManager.LoadScene(_SHOP_SCENE_NAME);

    public void OnGameButtonClicked()
        => SceneManager.LoadScene(_GAME_SCENE_NAME);
}
