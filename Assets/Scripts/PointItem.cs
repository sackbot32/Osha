using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointItem : MonoBehaviour
{
    //Components
    private Collider2D col;
    //Settings
    public int pointForWin;
    //Data
    private bool gotten;

    private void Start()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !gotten)
        {
            LevelManager.instance.AddPoint(pointForWin);
            col.enabled = false;
            //For now ill disable it, it could do a animation later
            gameObject.SetActive(false);
            gotten = true;

        }
    }
}
