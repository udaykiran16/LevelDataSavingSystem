using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class Node : MonoBehaviour
{
    public int nodeIndex;
    public GameObject starsHolder; //Stars holder
    private List<GameObject> starsImagesList = new List<GameObject>(); //List with stars

    public GameObject firstStarImage; //first star image reference
    public GameObject secondStarImage; //second star image reference
    public GameObject thirdStarImage; //third star image reference

    private int numberOfStars;
    // Start is called before the first frame update
    void Start()
    {
        starsImagesList.Clear();
        starsImagesList.Add(firstStarImage);
        starsImagesList.Add(secondStarImage);
        starsImagesList.Add(thirdStarImage);
        UpdateStars();
    }


    public void UpdateStars()
    {
        //Based on number of stars activate the images
        starsHolder.gameObject.SetActive(true);

        // Here we should get the number of stars from JSON 
        JSONNode LevelData = JSON.Parse(PlayerPrefs.GetString(GameSettings.LEVEL_DATA_PP));

        numberOfStars = LevelData["LevelData"]["level_" + nodeIndex.ToString()]["numStars"].AsInt;

        if (starsImagesList.Count > 0)
        {
            //Lets set all the stars off
            for (int i = 0; i < starsImagesList.Count; i++)
            {
                starsImagesList[i].gameObject.SetActive(false);
            }

            //For the number of stars lets activate them
            for (int i = 0; i < numberOfStars; i++)
            {
                starsImagesList[i].gameObject.SetActive(true);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
