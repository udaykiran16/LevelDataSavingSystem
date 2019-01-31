using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamekit3D;
using UnityEngine.UI;
using SimpleJSON;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Singleton { get; private set; }
    private GameObject[] enemies;
    private int enemyCount;

    [SerializeField]
    public int levelIndex;
    public Text timerText;
    private float startTime;
    private bool finish = false;

    [SerializeField]
    public int numStars;

    [SerializeField]
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
     
        initializeData();
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
        JSONNode LevelData = JSON.Parse(PlayerPrefs.GetString(GameSettings.LEVEL_DATA_PP));
       
        numStars = LevelData["LevelData"]["levelIndex"]["numStars"].AsInt;

        if (minutes <= 5)
           numStars =  3;
        else if ( minutes>5 && minutes <= 8 )
           numStars = 2;
        else if (minutes > 8)
           numStars = 1;
        LevelData["LevelData"]["levelIndex"]["numStars"].AsInt = numStars;
        PlayerPrefs.SetString(GameSettings.LEVEL_DATA_PP, LevelData.ToJSON(1));
        PlayerPrefs.Save();
    }

    public void initializeData()
    {
        JSONNode LeveldataJSON = JSON.Parse("{}");
        LeveldataJSON["LevelData"]["levelIndex"].AsInt = levelIndex;
        LeveldataJSON["LevelData"]["levelIndex"]["numStars"].AsInt = 0;
        PlayerPrefs.SetString(GameSettings.LEVEL_DATA_PP, LeveldataJSON.ToJSON(1));
        PlayerPrefs.Save();
        Debug.Log("Initialized Player data: " + PlayerPrefs.GetString(GameSettings.LEVEL_DATA_PP));

    }
    public void LevelFailed()
    {
        StartUI.Singleton.ShowLevelFailCanvas();
    }

   
}
