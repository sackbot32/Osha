using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    //Components
    [SerializeField]
    private GameObject pausePanel;
    [SerializeField] 
    private GameObject defaultButton;
    [SerializeField]
    private InputActionReference pauseButton;
    //Data
    private bool paused;

    private void Start()
    {
        paused = false;
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
            Time.timeScale = 0.0f;
            EventSystem.current.SetSelectedGameObject(defaultButton);
            pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1.0f;
            pausePanel.SetActive(false);
        }
    }

}
