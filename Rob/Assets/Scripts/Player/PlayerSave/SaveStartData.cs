using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SaveData", menuName = "SaveData")]
public class SaveStartData : ScriptableObject
{
    public int StartMoney;
    public AnimalType CurrentSkin;
    public int FirstLevelId;
    public int FirstLevelName;
    public List<AnimalType> UnlockedSkins;
    [Range(1, 10)] public int AnimalsCapacity;
    [Header("Prefabs")]
    public Player PlayerPrefab;
    public ItemCollector ItemCollectorPrefab;

    private void OnValidate()
    {
        if(!UnlockedSkins.Contains(CurrentSkin))
        {
            UnlockedSkins.Add(CurrentSkin);
        }
    }
}