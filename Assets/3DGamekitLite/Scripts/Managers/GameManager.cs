using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamekit3D;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Singleton { get; private set; }
    private GameObject[] enemies;
    private int enemyCount;
    public int levelIndex;
    public Text timerText;
    private float startTime;

    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
        else
            Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCount = enemies.Length;
        Debug.Log("spawned Enemies:" + enemyCount);
    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.time - startTime;
        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f1");
        timerText.text = minutes + ":" + seconds;

    }
    public void EnemiesCountDown()
    {
        enemyCount -= 1;
        Debug.Log("Enemies Left:" + enemyCount);
        if(enemyCount <= 0)
        {
            enemyCount = 0;
            StartUI.Singleton.GameoverPanel();
            
        }
    }

    public void LevelFailed()
    {
        StartUI.Singleton.ShowLevelFailCanvas();
    }

   
}
