using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{
    //Components
    [SerializeField]
    private GameObject pausePanel;
    [SerializeField] 
    private GameObject defaultButton;
    [SerializeField]
    private InputActionReference pauseButton;
    public Slider[] soundSlider;
    //0 sfx
    //1 music
    //Data
    private bool paused;

    private void Start()
    {
        pausePanel = GameObject.FindGameObjectWithTag("Pause");
        //I grab the parts of the pause menu automatically this way
        defaultButton = pausePanel.transform.GetChild(0).transform.GetChild(0).gameObject;
        soundSlider[0] = pausePanel.transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).gameObject.GetComponent<Slider>();
        soundSlider[1] = pausePanel.transform.GetChild(0).transform.GetChild(1).transform.GetChild(3).gameObject.GetComponent<Slider>();
        pausePanel.SetActive(false);
        paused = false;
        soundSlider[0].onValueChanged.AddListener(PauseChangeSFXVolume);
        soundSlider[1].onValueChanged.AddListener(PauseChangeMusicVolume);
        if (PlayerPrefs.HasKey(UsefulConstants.SFXVOLPARAM))
        {
            print("llega a sfx");
            AudioManager.instance.mixer.SetFloat(UsefulConstants.SFXVOLPARAM, PlayerPrefs.GetFloat(UsefulConstants.SFXVOLPARAM));
            soundSlider[0].value = Mathf.Pow(10, (PlayerPrefs.GetFloat(UsefulConstants.SFXVOLPARAM) / 20));

        }
        if (PlayerPrefs.HasKey(UsefulConstants.MUSICVOLPARAM))
        {
            print("llega a music");
            AudioManager.instance.mixer.SetFloat(UsefulConstants.MUSICVOLPARAM, PlayerPrefs.GetFloat(UsefulConstants.MUSICVOLPARAM));
            soundSlider[1].value = Mathf.Pow(10, (PlayerPrefs.GetFloat(UsefulConstants.MUSICVOLPARAM) / 20));
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (pauseButton.action.WasPressedThisFrame())
        {
            PauseResume();
        }
        
    }

    public void PauseResume()
    {
        paused = !paused;
        if (paused)
        {
            CursorChangeStatic.ChangeToSelectCursor();
            Time.timeScale = 0.0f;
            EventSystem.current.SetSelectedGameObject(defaultButton);
            pausePanel.SetActive(true);
        }
        else
        {
            CursorChangeStatic.ChangeToAimCursor();
            Time.timeScale = 1.0f;
            pausePanel.SetActive(false);
        }
    }

    public void PauseChangeSFXVolume(float volume)
    {
        AudioManager.instance.ChangeSfxVolume(volume);
    }
    public void PauseChangeMusicVolume(float volume)
    {
        AudioManager.instance.ChangeMusicVolume(volume);
    }

}
