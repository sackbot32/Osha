using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelItem : MonoBehaviour
{
    //Components
    [SerializeField]
    public TMP_Text highScore;
    [SerializeField]
    public TMP_Text levelName;
    public UnityEngine.UI.Button levelButton;
    //Data
    public int whichLevel;

    private void Start()
    {
        highScore = transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
        levelButton = transform.GetChild(1).gameObject.GetComponent<UnityEngine.UI.Button>();
        levelName = levelButton.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
    }

    public void GoToLevel()
    {
        SceneManager.LoadScene(whichLevel);
    }

}
