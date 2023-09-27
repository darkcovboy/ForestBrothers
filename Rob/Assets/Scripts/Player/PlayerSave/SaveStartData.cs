using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SaveData", menuName = "SaveData")]
public class SaveStartData : ScriptableObject
{
    public int StartMoney;
    public AnimalType CurrentSkin;
    public int FirstLevelId;
    public List<AnimalType> UnlockedSkins;
    [Range(1, 10)] public int AnimalsCapacity;

    private void OnValidate()
    {
        if(!UnlockedSkins.Contains(CurrentSkin))
        {
            UnlockedSkins.Add(CurrentSkin);
        }
    }
}