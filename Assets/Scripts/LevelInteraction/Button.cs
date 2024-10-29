using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    //Sprites to change
    [Header("Setting")]
    [SerializeField]
    private Sprite onSprite;
    [SerializeField]
    private Sprite offSprite;

    private SpriteRenderer sP;

    private void Start()
    {
        sP = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") || collision.CompareTag("GrabPoint"))
        {
            //TurnOn
            sP.sprite = onSprite;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("GrabPoint"))
        {
            //TurnOn
            sP.sprite = offSprite;
        }
    }
}
