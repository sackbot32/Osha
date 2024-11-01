using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    //Components
    public HudManager hud;
    [SerializeField]
    private StringListSerialize names;
    //Data
    public int points;

    

    private void Start()
    {
        Time.timeScale = 1f;
        instance = this;
        hud = GetComponent<HudManager>();
    }

    /// <summary>
    /// Adds points to the point variable and updates de hud
    /// </summary>
    /// <param name="add"></param>
    public void AddPoint(int add)
    {
        points += add;
        hud.UpdatePoints(points);
    }
    /// <summary>
    /// Called when the level is ended, it recieves a bool that tells if it the player has beat it or lost
    /// </summary>
    /// <param name="win"></param>
    public void EndGame(bool win)
    {
        if (win)
        {
            //when winning and having a higher level beaten it updates it with the current level index
            if(PlayerPrefs.GetInt(UsefulConstants.BEATENLEVELPREF) < SceneManager.GetActiveScene().buildIndex)
            {
                PlayerPrefs.SetInt(UsefulConstants.BEATENLEVELPREF, SceneManager.GetActiveScene().buildIndex);
            }
            //when winning and having a higher score on this level it updates it with the points and using the levels name
            if (PlayerPrefs.GetInt(names.levelNames[SceneManager.GetActiveScene().buildIndex - UsefulConstants.MINSCENE]) < points)
            {
                PlayerPrefs.SetInt(names.levelNames[SceneManager.GetActiveScene().buildIndex - UsefulConstants.MINSCENE], points);

            }

            Time.timeScale = 0f;
            hud.winHud.SetActive(true);
        } else
        {
            Time.timeScale = 0f;
            hud.loseHud.SetActive(true);
        }
    }
    /// <summary>
    /// Restarts the current level
    /// </summary>
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    /// <summary>
    /// Recieves a integer and makes the player go to that specific scene
    /// </summary>
    /// <param name="level"></param>
    public void GoToSpecificScene(int level)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(level);
    }
    /// <summary>
    /// Goes the menu scene, indicated by the constant in UsefulConstants
    /// </summary>
    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(UsefulConstants.MENUINDEX);
    }
    /// <summary>
    /// Goes to the next level in the index
    /// </summary>
    public void GoToNextLevel()
    {
        Time.timeScale = 1f;
        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextIndex);
        } else
        {
            SceneManager.LoadScene(UsefulConstants.MENUINDEX);

        }
    }
}
