using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject HowToPlayUI;


    void Start()
    {
        
    }

 
    void Update()
    {
        
    }

    public void GoToGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void HowToPlay()
    {
        HowToPlayUI.SetActive(true);
    }

    public void Back()
    {
        HowToPlayUI.SetActive(false);
    }
}
