using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIEvents : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Gameplay");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Winning()
    {
        SceneManager.LoadScene("Victory");
    }
    public void OnTriggerEnter(Collider c)
    {
        Winning();
    }
}
