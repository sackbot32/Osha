using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    //Settings
    public int pointForWin;
    public bool waitForWin;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LevelManager.instance.AddPoint(pointForWin);
            if(waitForWin)
            {
                //In case I wanna wait for the winning 
            } else
            {
                LevelManager.instance.EndGame(true);
            }
        }
    }
}
