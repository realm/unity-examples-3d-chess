using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameButton : MonoBehaviour
{
    [SerializeField] private InputField nameInputField = default;
    [SerializeField] private InputField gameIdInputField = default;

    public void StartGameButtonPressed()
    {
        var name = nameInputField.text;
        PlayerPrefs.SetString(Constants.PlayerPrefsKeys.Name, name);

        var gameId = gameIdInputField.text;
        PlayerPrefs.SetString(Constants.PlayerPrefsKeys.GameId, gameId);

        SceneManager.LoadScene("MainScene");
    }
}
