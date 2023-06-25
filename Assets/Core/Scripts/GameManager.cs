using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] AudioSource victoryVoiceLine;
    [SerializeField] AudioSource gameOverVoiceLine;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject lossUI;
    [SerializeField] GameObject victoryUI;

    //called by boss dieing 
    public void Victory()
    {
        VictoryUI();
        victoryVoiceLine.Play();
        animator.SetTrigger("Victory");
    }

    //called by player when died
    public void PlayerDied()
    {
        //show death screen
        LossUI();
        gameOverVoiceLine.Play();
        animator.SetTrigger("Lost");
    }

    public void ShowUI()
    {
        gameOverUI.SetActive(true);
    }

    public void LossUI()
    {
        lossUI.SetActive(true);
    }
    public void VictoryUI()
    {
        victoryUI.SetActive(true);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }

}
