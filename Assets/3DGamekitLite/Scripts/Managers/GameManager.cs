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
    private bool finish = false;
    public int numStars;
    public float minutes;

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
        timer();
    }

    public void timer()
    {
        if (finish)
        return;

        float t = Time.time - startTime;
        minutes = ((int)t / 60);
        string seconds = (t % 60).ToString("f1");
        timerText.text = minutes.ToString() + ":" + seconds;

    }

    public void EnemiesCountDown()
    {
        enemyCount -= 1;
        Debug.Log("Enemies Left:" + enemyCount);
        if(enemyCount <= 0)
        {
           
            finish = true;
            enemyCount = 0;
            StartUI.Singleton.GameoverPanel();
            Rating();
        
        }
    }

    public void Rating()
    {
        if (minutes <= 5)
           numStars =  3;
        else if ( minutes>5 && minutes <= 8 )
           numStars = 2;
        else if (minutes > 8)
           numStars = 1;
    }


    public void LevelFailed()
    {
        StartUI.Singleton.ShowLevelFailCanvas();
    }

   
}
