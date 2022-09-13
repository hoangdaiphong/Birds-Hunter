using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameGUIManager : Singleton<GameGUIManager>
{
    public GameObject homeGui;
    public GameObject gameGui;

    // Tham chieu den 2 Dialog
    public Dialog gameDialog;
    public Dialog pauseDialog;

    // Tham chieu den image
    public Image fireRateFilled;
    public Text timerText;
    public Text killedCountingText; // Nhung con chim da duoc ban ha

    Dialog m_curDialog;

    public Dialog CurDialog { get => m_curDialog; set => m_curDialog = value; }

    public override void Awake()
    {
        // Se khong luu du lieu khi load
        MakeSingleton(false);
    }

    public void ShowGameGui(bool isShow)
    {
        if (gameGui)
            gameGui.SetActive(isShow);

        if (homeGui)
            homeGui.SetActive(!isShow);
    }

    public void UpdateTimer(string time)
    {
        if (timerText)
            timerText.text = time;
    }

    public void UpdateKilledCounting(int killed)
    {
        if(killedCountingText)
            killedCountingText.text = "x" + killed.ToString();
    }

    public void UpdateFireRate(float rate)
    {
        if (fireRateFilled)
            fireRateFilled.fillAmount = rate;
    }

    public void PauseGame()
    {
        // Dung tat ca hoat dong
        Time.timeScale = 0f;

        if(pauseDialog)
        {
            pauseDialog.Show(true);

            /* Cap nhat thanh tich cao nhat cua nguoi choi, tu dong luu trong bo nho va se 
             khong mat khi nguoi choi tat game */
            pauseDialog.UpdateDialog("GAME PAUSE", "BEST KILLED : x" + Prefs.bestScore);

            // Dong bang hien thi
            m_curDialog = pauseDialog;
        }

    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;

        if(m_curDialog)
            m_curDialog.Show(false);
    }

    public void BackToHome()
    {
        ResumeGame();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Replay()
    {
        if(m_curDialog)
            m_curDialog.Show(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        ResumeGame();

        Application.Quit();
    }
}
