using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Animator animator;

    //called by boss dieing 
    public void EndOfLevel()
    {
        animator.SetTrigger("End of level");
    }

    //called by player when died
    public void PlayerDied()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
