using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public List<Collectable> collectables;
    public TMP_Text scoreText;
    public TMP_Text timerText;
    public TMP_Text winText;
    public GameObject winPanel;
    public GameObject gameplayPanel;
    float timer;
    int score = 0;
    public void Update()
    {
        timer += Time.deltaTime;
        timerText.text = timer.ToString("F2");
    }
    public void Start()
    {
        foreach (var c in collectables)
        {
            c.Lock();
            if (c.isFinal)
            {
                c.SubscribeEnd(OnEnd);
            }
            else
            {
                c.SubscribeGrab(OnCollect);
            }
        }
        collectables[0].Unlock();
    }
    void OnEnd()
    {
        Cursor.lockState = CursorLockMode.None;
        winText.text = "Time: " + timer.ToString();
        winPanel.SetActive(true);
        gameplayPanel.SetActive(false);
    }
    void OnCollect()
    {
        score++;
        scoreText.text = score.ToString();
        collectables[score].Unlock();
    }

}
