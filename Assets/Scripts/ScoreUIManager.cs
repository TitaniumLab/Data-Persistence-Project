using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreUIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI namesListText;
    [SerializeField]
    private TextMeshProUGUI scoreListText;
    private int maxScorePositions = 10;

    private void Start()
    {
        if (DataManager.Instance != null)
        {
            int listlength = DataManager.Instance.namesAndScore.Count;
            //shows the first "maxScorePositions" positions
            for (int i = 0; i < maxScorePositions; i++)
            {
                //if score list in the DataManager shorter then 10
                if (listlength != 0 && i < listlength && DataManager.Instance.namesAndScore[i].score != 0)
                {
                    namesListText.text += $"{i + 1}.\t {DataManager.Instance.namesAndScore[i].names} \n";
                    scoreListText.text += $"{DataManager.Instance.namesAndScore[i].score}\n";
                }
                else
                {
                    namesListText.text += $"{i + 1}.\n";
                }
            }
        }
    }
    //back to main menu button
    public void MainMenuOnClock()
    {
        SceneManager.LoadScene("Menu");
    }
}
