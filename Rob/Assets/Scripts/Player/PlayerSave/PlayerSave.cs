using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using System.Linq;

public class PlayerSave
{
    private const string _startDataPath = "SaveData/StartData";
    private const string _savesKey = "GameDataJSON";

    public event UnityAction<AnimalData> OnSkinChanged;
    public SaveData SaveData { get; private set; }

    private SkinsContainer _skinContainer;

    public PlayerSave(SkinsContainer skinContainer)
    {
        PlayerPrefs.DeleteAll();
        if (PlayerPrefs.HasKey(_savesKey))
        {
            string json = PlayerPrefs.GetString(_savesKey);
            SaveData = JsonUtility.FromJson<SaveData>(json);
        }
        else
        {
            SaveStartData saveStartData = Resources.Load<SaveStartData>(_startDataPath);
            SaveData = LoadNewData(saveStartData);
            Save();
        }
        _skinContainer = skinContainer;
    }

    public void LevelWin(int money, int levelIndex)
    {
        SaveData.Money = money;
        //Поменять потому что в теории у меня будет бесконечное кол левелов, поэтому тут нужно по-другому сохранять
        SaveData.LastLevel = levelIndex;
    }

    public void OpenNewSkin(AnimalType skin)
    {
        if (SaveData.UnlockedSkins.Contains(skin))
            throw new Exception();

        SaveData.UnlockedSkins.Add(skin);
        Save();
    }

    public void SelectSkin(AnimalType skin)
    {
        if(SaveData.UnlockedSkins.Contains(skin))
            SaveData.SelectedSkin = skin;
        else
            throw new Exception();

        OnSkinChanged?.Invoke(_skinContainer.AnimalData.First<AnimalData>(selectSkin => selectSkin.AnimalType == skin));
        Save();
    }

    private void Save()
    {
        string jsonData = JsonUtility.ToJson(SaveData);
        PlayerPrefs.SetString(_savesKey, jsonData);
        PlayerPrefs.Save();
    }

    private SaveData LoadNewData(SaveStartData startData)
    {
        return new SaveData(startData.StartMoney, startData.FirstLevelId, startData.CurrentSkin, startData.UnlockedSkins, startData.AnimalsCapacity);
    }
}

public class SaveData
{
    public int Money;
    public int LastLevel;
    public AnimalType SelectedSkin;
    public List<AnimalType> UnlockedSkins;
    public int Capacity;

    public SaveData(int money, int level, AnimalType selectedSkin, List<AnimalType> unlockedSkins, int capacity)
    {
        Money = money;
        LastLevel = level;
        SelectedSkin = selectedSkin;
        UnlockedSkins = unlockedSkins;
        Capacity = capacity;
    }
}
