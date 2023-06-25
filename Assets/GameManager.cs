using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] AudioSource victoryVoiceLine;
    [SerializeField] AudioSource gameOverVoiceLine;
    [SerializeField] GameObject UIToShow;

    //called by boss dieing 
    public void Victory()
    {
        victoryVoiceLine.Play();
        animator.SetTrigger("Victory");
    }

    //called by player when died
    public void PlayerDied()
    {
        //show death screen
        gameOverVoiceLine.Play();
        animator.SetTrigger("Lost");
    }

    public void ShowUI()
    {
        UIToShow.SetActive(true);
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
