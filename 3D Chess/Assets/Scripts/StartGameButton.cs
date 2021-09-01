using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameButton : MonoBehaviour
{
    [SerializeField] private InputField inputField = default;

    private readonly string gameIdKey = "GAME_ID_KEY";

    public void StartGameButtonPressed()
    {
        var gameId = inputField.text;
        PlayerPrefs.SetString(gameIdKey, gameId);
        SceneManager.LoadScene("MainScene");
    }
}
