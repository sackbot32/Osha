using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HudManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text healthText;
    [SerializeField]
    private TMP_Text pointsText;
    [SerializeField]
    public GameObject winHud;
    [SerializeField]
    public GameObject loseHud;

    /// <summary>
    /// Sets the points on the hud to be the ones recieved
    /// </summary>
    /// <param name="points"></param>
    public void UpdatePoints(int points)
    {
        pointsText.text = points.ToString("000000");
    }
    /// <summary>
    /// Sets the health of the hud to be one recieved
    /// </summary>
    /// <param name="health"></param>
    public void UpdateHealth(float health)
    {
        healthText.text = "Vida:" + Mathf.FloorToInt(health).ToString("00");
    }
}
