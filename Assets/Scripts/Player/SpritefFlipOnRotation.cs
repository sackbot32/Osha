using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritefFlipOnRotation : MonoBehaviour
{
    //Components
    [SerializeField]
    private Transform pivot;
    [SerializeField]
    private SpriteRenderer[] xFlippers;
    [SerializeField]
    private SpriteRenderer[] yFlippers;
    //Settings
    [Header("Settings")]
    [Tooltip("If it goes above this value it will flip")]
    public float maxRot;
    [Tooltip("If it goes bellow this value it will flip")]
    public float minRot;

    private void Update()
    {
        if(pivot.rotation.eulerAngles.z > minRot && pivot.rotation.eulerAngles.z < maxRot)
        {
            Flipping(true);
        } else
        {
            Flipping(false);

        }
    }

    private void Flipping(bool flip)
    {
        foreach (SpriteRenderer xSprite in xFlippers)
        {
            xSprite.flipX = flip;
        }
        foreach (SpriteRenderer ySprite in yFlippers)
        {
            ySprite.flipY = flip;
        }
    }

}
