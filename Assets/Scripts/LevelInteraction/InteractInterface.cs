using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InteractInterface
{
    /// <summary>
    /// What is called when turned on
    /// </summary>
    public void TurnOn();
    /// <summary>
    /// What is called when turned off
    /// </summary>
    public void TurnOff();
    /// <summary>
    /// Returns if its on
    /// </summary>
    /// <returns></returns>
    public bool GetIsOn();
}
