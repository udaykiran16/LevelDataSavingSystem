using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        // here we should get the integer value of levelUnlocked from JSON
    }

    //Unlock levels
    void Unlocklevel()
    {
        for (int i = 0; i < levelLocks.Length; i++)
        {
            if (i <= levelUnlocked)
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
