using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    //Components
    public GameObject[] panels;
    //0 main menu
    //1 Level Select
    //2 Settings
    public GameObject[] defaultButtons;
    //They are asigned to the default buttons of the panels
    public Slider[] soundSlider;
    //0 sfx
    //1 music

    private void Start()
    {
        //Habria que implementar un settings de fps y vsync pero por ahora
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
        ChangePanel(0);
        if (PlayerPrefs.HasKey(UsefulConstants.SFXVOLPARAM))
        {
            print("llega a sfx");
            AudioManager.instance.mixer.SetFloat(UsefulConstants.SFXVOLPARAM,PlayerPrefs.GetFloat(UsefulConstants.SFXVOLPARAM));
            soundSlider[0].value = Mathf.Pow(10, (PlayerPrefs.GetFloat(UsefulConstants.SFXVOLPARAM) / 20)) ;
            
        }
        if (PlayerPrefs.HasKey(UsefulConstants.MUSICVOLPARAM))
        {
            print("llega a music");
            AudioManager.instance.mixer.SetFloat(UsefulConstants.MUSICVOLPARAM, PlayerPrefs.GetFloat(UsefulConstants.MUSICVOLPARAM));
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
        EventSystem.current.SetSelectedGameObject(defaultButtons[whichOne]);
        panels[whichOne].SetActive(true);
    }
    public void Quit()
    {
        print("Game left");
        Application.Quit();
    }

    public void MenuChangeSFXVolume(float volume)
    {
        AudioManager.instance.ChangeSfxVolume(volume);
    }
    public void MenuChangeMusicVolume(float volume)
    {
        AudioManager.instance.ChangeMusicVolume(volume);
    }
    
}
