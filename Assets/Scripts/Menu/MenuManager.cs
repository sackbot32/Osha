using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    //Components
    public GameObject[] panels;
    //0 main menu
    //1 Level Select
    //2 Settings?
    public Slider[] soundSlider;
    //0 sfx
    //1 music

    private void Start()
    {
        if(soundSlider.Length > 0)
        {
            //soundSlider[0].onValueChanged.AddListener(AudioManager.instance.ChangeSfxVolume);
            //soundSlider[1].onValueChanged.AddListener(AudioManager.instance.ChangeMusicVolume);
        }
        ChangePanel(0);
        if (PlayerPrefs.HasKey(UsefulConstants.SFXVOLPARAM))
        {
            AudioManager.instance.mixer.SetFloat(UsefulConstants.SFXVOLPARAM,PlayerPrefs.GetFloat(UsefulConstants.SFXVOLPARAM));
            AudioManager.instance.mixer.SetFloat(UsefulConstants.MUSICVOLPARAM,PlayerPrefs.GetFloat(UsefulConstants.MUSICVOLPARAM));
            soundSlider[0].value = Mathf.Pow(10, (PlayerPrefs.GetFloat(UsefulConstants.SFXVOLPARAM) / 20)) ;
            soundSlider[1].value = Mathf.Pow(10, (PlayerPrefs.GetFloat(UsefulConstants.MUSICVOLPARAM) / 20));
        } 

    }


    //meant to start the game but rather have it with an input
    public void ChangeToScene(int index)
    {
        SceneManager.LoadScene(index);
    }
    /// <summary>
    /// From the list of panels it chooses one
    /// </summary>
    /// <param name="whichOne"></param>
    public void ChangePanel(int whichOne)
    {
        foreach (GameObject panel in panels) 
        {
            panel.SetActive(false);
        }
        panels[whichOne].SetActive(true);
    }
    public void Quit()
    {
        print("Game left");
        Application.Quit();
    }
}
