using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text bestScoreText;//added. shows best score
    public GameObject GameOverText;

    private bool m_Started = false;
    private int m_Points;

    private bool m_GameOver = false;


    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
        //load best score if exist
        RefreshBestScore();
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        //changing and saving score list in DataManager
        AddToScoreList();
        SaveNewScoreList();
        RefreshBestScore();

        m_GameOver = true;
        GameOverText.SetActive(true);
    }

    //adding new score to the score list
    private void AddToScoreList()
    {
        int scoreListLenth = DataManager.Instance.namesAndScore.Count;

        if (scoreListLenth == 0 ||//if list empty
            DataManager.Instance.namesAndScore[scoreListLenth - 1].score >= m_Points)//if less than last element
        {
            DataManager.Instance.namesAndScore.Add((DataManager.Instance.playerName, m_Points));
            Debug.Log("Score added.");
        }
        else // finding position and insert score 
        {
            //prevents multiple additions
            bool isScoreAdded = false;
            for (int i = 0; i < scoreListLenth; i++)
            {
                while (!isScoreAdded)
                {
                    //if new score more than score on i position
                    if (DataManager.Instance.namesAndScore[i].score < m_Points)
                    {
                        DataManager.Instance.namesAndScore.Insert(i, (DataManager.Instance.playerName, m_Points));
                        isScoreAdded = true;
                    }
                    break;
                }
            }
            Debug.Log("Score added.");
        }

    }

    //refreshing best score text
    private void RefreshBestScore()
    {
        if (DataManager.Instance != null && DataManager.Instance.namesAndScore.Count != 0)
        {
            bestScoreText.text = $"Best Score : {DataManager.Instance.namesAndScore[0].names} : {DataManager.Instance.namesAndScore[0].score}";
            Debug.Log("Best score refreshed");
        }
    }
    //saves changes in score list
    private void SaveNewScoreList()
    {
        if (DataManager.Instance != null)
        {
            DataManager.Instance._SaveData();
        }
    }

    //back button method
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
