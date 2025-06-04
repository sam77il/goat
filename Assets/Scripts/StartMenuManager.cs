using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject inputBox;
    [SerializeField] private TMP_InputField playerNameInput;

    public void StartGame()
    {
        ToggleInputBox(true);
    }

    public void CloseInputBox()
    {
        ToggleInputBox(false);
    }

    public void SubmitInputBox()
    {
        string playerName = playerNameInput.text.Trim();
        if (!string.IsNullOrEmpty(playerName))
        {
            GameManager.Instance.StartGame(playerName);
        }
        else
        {
            Debug.LogWarning("Player name cannot be empty.");
        }
    }

    public void ToggleInputBox(bool active)
    {
        if (inputBox != null)
        {
            inputBox.SetActive(active);
        }
    }
}
