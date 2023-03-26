using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;

    public GameManager gm;
    private void Awake()
    {
        gm=GameObject.Find("GameManager").GetComponent<GameManager>();

    }
    private void Update()
    {
        scoreText.text = gm.scoree.ToString();
        if (gm.scoree > PlayerPrefs.GetInt("BestScore",0))
        {
            PlayerPrefs.SetInt("BestScore", gm.scoree);
        }
        bestScoreText.text =PlayerPrefs.GetInt("BestScore").ToString();
    }
    public void TryAgin()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }
    public void UnPause()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
