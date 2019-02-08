using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SimpleJSON;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Singleton { get; private set; }

    public Button[] levelButtons;
    public GameObject[] levelLocks;
    private int levelUnlocked;

    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
        else
            Destroy(gameObject);
    }

    public void LoadTheScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    void Start()
    {
        if(PlayerPrefs.HasKey(GameSettings.LEVEL_DATA_PP) == false)
        {
            initializeData();
        }

        Unlocklevel();
       
    }

    public void initializeData()
    {
        JSONNode LeveldataJSON = JSON.Parse("{}");

        LeveldataJSON["LevelData"]["levelUnlocked"].AsInt = 1;
        LeveldataJSON["LevelData"]["level_1"]["numStars"].AsInt = 0;

        PlayerPrefs.SetString(GameSettings.LEVEL_DATA_PP, LeveldataJSON.ToJSON(1));
        PlayerPrefs.Save();
        Debug.Log("Initialized Player data: " + PlayerPrefs.GetString(GameSettings.LEVEL_DATA_PP));

    }

    //Unlock levels
    void Unlocklevel()
    { // here we should get the integer value of levelUnlocked from JSON
        JSONNode LevelData = JSON.Parse(PlayerPrefs.GetString(GameSettings.LEVEL_DATA_PP));

        levelUnlocked = LevelData["LevelData"]["levelUnlocked"].AsInt;

        for (int i = 0; i < levelLocks.Length; i++)
        {
            if (i < levelUnlocked)
            {
                levelButtons[i].interactable = true;
                levelLocks[i].gameObject.SetActive(false);
            }
            else
            {
                levelButtons[i].interactable = false;
                levelLocks[i].gameObject.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
