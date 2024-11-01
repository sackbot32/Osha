using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFiller : MonoBehaviour
{
    //Components
    [SerializeField]
    private GameObject levelItemPrefab;
    [SerializeField]
    private StringListSerialize names;
    //Settings
    private int min;
   

    private void Start()
    {
        min = UsefulConstants.MINSCENE;
        for (int i = min + SceneManager.GetActiveScene().buildIndex; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            LevelItem levelI = Instantiate(levelItemPrefab,transform).GetComponent<LevelItem>();
            levelI.whichLevel = i;
            levelI.levelName.text = names.levelNames[i - (min + SceneManager.GetActiveScene().buildIndex)];
            print(names.levelNames[i - (min + SceneManager.GetActiveScene().buildIndex)]);
            if(PlayerPrefs.GetInt(UsefulConstants.BEATENLEVELPREF) < (i-1))
            {
                levelI.levelButton.interactable = false;
                levelI.highScore.text = "No disponible";
            } else
            {
                levelI.highScore.text = "HS: " + PlayerPrefs.GetInt(names.levelNames[i - (min + SceneManager.GetActiveScene().buildIndex)]).ToString("000000");

            }
        }
    }
}
