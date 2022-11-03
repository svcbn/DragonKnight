using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JH_SceneManager : MonoBehaviour
{
    public static JH_SceneManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    public void Dragon()
    {
        SceneManager.LoadScene("DragonScene");
    }
    public void Tutorial()
    {
        SceneManager.LoadScene("TutorialScene");
    }
    public void Battle()
    {
        SceneManager.LoadScene("BattleScene");
    }
    public void GameOver()
    {
        SceneManager.LoadScene("GameOverScene");
        
    }
    public void Main()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Victory()
    {
        SceneManager.LoadScene("AfterEnemyDie");

    }

    public void MeetBoss()
    {
        SceneManager.LoadScene("MeetBoss");
    }

    public void Ending()
    {
        SceneManager.LoadScene("EndingScene");
        
    }

   public void FireVillage()
    {
        SceneManager.LoadScene("FireScene");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
