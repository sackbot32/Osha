using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureButton : MonoBehaviour
{
    //Sprites to change
    [Header("Setting")]
    [SerializeField]
    private Sprite onSprite;
    [SerializeField]
    private Sprite offSprite;
    [Tooltip("The object it turns on")]
    public GameObject interactableGameObject;
    //Data
    private InteractInterface interactableItem;
    private SpriteRenderer sP;

    private void Start()
    {
        sP = GetComponentInChildren<SpriteRenderer>();
        interactableItem = interactableGameObject.GetComponent<InteractInterface>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") || collision.CompareTag("GrabPoint"))
        {
            if (!interactableItem.GetIsOn())
            {
                AudioManager.instance.PlaySfx(10);
                interactableItem.TurnOn();
            }
            sP.sprite = onSprite;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("GrabPoint"))
        {
            AudioManager.instance.PlaySfx(11);
            interactableItem.TurnOff();
            sP.sprite = offSprite;
        }
    }
}
