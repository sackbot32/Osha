using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelNames", menuName = "Levels/Names")]
[System.Serializable]
public class StringListSerialize : ScriptableObject
{
    [SerializeField]
    public string[] levelNames;
}
