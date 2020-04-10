using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DMapHabitat", menuName = "DMapHabitat")]
public class DMapHabitat: ScriptableObject
{
    public string habitatName;

    public string[] objectNames;
    public int[] percents;
}
