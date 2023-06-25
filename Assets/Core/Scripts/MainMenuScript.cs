using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] PlayerSettings settings;
    [SerializeField] Slider lookSlider;

    private void Start()
    {
        lookSlider.value = settings.GetLookSpeed();
    }

    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void LookSpeed()
    {
        settings.SetLookSpeed(lookSlider.value);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
