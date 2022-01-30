using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameDataScriptableObject", order = 1)]
public class GameDataScriptableObject : ScriptableObject
{
    public Dictionary<PlantType, int> PlantPrice;
    public int age;
}
