using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SetingsUIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputNameField;
    [SerializeField]
    private TMP_Dropdown gameSpeedDropdown;

    private void Start()
    {
        //load data from DataManager
        if (DataManager.Instance != null)
        {
            inputNameField.text = DataManager.Instance.playerName;
            gameSpeedDropdown.value = DataManager.Instance.gameSpeed;
        }
        else
        {
            inputNameField.text = "Debug";
        }
    }

    //back to main menu
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    //save changes in DataManager
    public void SaveOnClick()
    {
        if (DataManager.Instance != null)
        {
            DataManager.Instance.playerName = inputNameField.text;
            DataManager.Instance.gameSpeed = gameSpeedDropdown.value;
            DataManager.Instance._SaveData();
        }
    }
}
