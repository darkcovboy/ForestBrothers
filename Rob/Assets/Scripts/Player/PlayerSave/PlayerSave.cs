using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using System.Linq;
using UnityEngine.SceneManagement;
using PlayerPrefs = Agava.YandexGames.PlayerPrefs;
using System.Collections;
using Agava.YandexGames;
using System.Threading.Tasks;
using UnityEngine.UI;
#if UNITY_WEBGL && !UNITY_EDITOR
using Agava.YandexGames;

#endif


public class PlayerSave
{
    private const string _startDataPath = "SaveData/StartData";
    private const string _savesKey = "GameDataJSON";

    public event UnityAction<AnimalData> OnSkinChanged;
    public SaveData SaveData { get; private set; }

    public SkinsContainer SkinContainer { get; set; }

    private ICoroutineRunner _runner;

    public PlayerSave(ICoroutineRunner coroutineRunner)
    {
        _runner = coroutineRunner;
        _runner.StartCoroutine(Start());
    }

    public void UpdateMoney(int money)
    {
        Debug.Log(SaveData.Money + "  " + money);
        SaveData.Money = money;
        Save();
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

        OnSkinChanged?.Invoke(SkinContainer.AnimalData.First<AnimalData>(selectSkin => selectSkin.AnimalType == skin));
        Save();
    }

    public void CompleteLevel(int money)
    {
        if(SceneManager.sceneCountInBuildSettings < (SaveData.LastLevelId + 1))
        {
            SaveData.LastLevelId = 1;
        }

        SaveData.LastLevelName++;
        SaveData.Money = money;
        Save();

    }

    private IEnumerator Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
       yield return YandexGamesSdk.Initialize(); 
       Debug.Log("Start async");
       StartAsync();
       
#elif UNITY_EDITOR
        SaveData = LoadNewData(Resources.Load<SaveStartData>(_startDataPath));
        InitText.Instance.ChangeText();
        Loader.Instance.OnLoader();
        Loader.Instance.SetNextLevel(SaveData.LastLevelId);
        yield break;
#endif
    }

    private async void StartAsync()
    {
        Task task = LoadDataAsync();
        await task;

        Debug.Log("SaveData is null " + (SaveData == null));
        if (SaveData == null || SaveData.UnlockedSkins.Count() == 0)
        {
            SaveData = LoadNewData(Resources.Load<SaveStartData>(_startDataPath));
        }

        Loader.Instance.SetNextLevel(SaveData.LastLevelId);
        Loader.Instance.OnLoader();
        InitText.Instance.ChangeText();
        Save();
    }

    private async Task LoadDataAsync()
    {
        string json = null;

        // Вызов асинхронного метода PlayerAccount.GetCloudSaveData с использованием TaskCompletionSource
        var tcs = new TaskCompletionSource<string>();

        PlayerAccount.GetCloudSaveData((data) =>
        {
            json = data;
            tcs.SetResult(data); // Устанавливаем результат асинхронной операции
        });

        // Ожидаем завершения асинхронной операции
        await tcs.Task;

        Debug.Log("Первая попытка загрузить");
        Debug.Log(json != null);

        if (json != null)
        {
            Debug.Log("Так как json не null, то SaveData присваиваем");
            SaveData = JsonUtility.FromJson<SaveData>(json);
        }
        else
        {
            PlayerAccount.GetCloudSaveData((data) => json = data);
            Debug.Log(json + "Вторая попытка загрузить");

            if (json != null || json == "")
            {
                SaveData = null;
            }
            else
            {
                SaveData = JsonUtility.FromJson<SaveData>(json);
            }
        }
    }

    private void Save()
    {
        string jsonData = JsonUtility.ToJson(SaveData);
        Debug.Log("Сохранения");
        Debug.Log(jsonData);
#if UNITY_WEBGL && !UNITY_EDITOR
       Debug.Log("Cloud Save");
       PlayerAccount.SetCloudSaveData(jsonData);
#endif
    }

    private SaveData LoadNewData(SaveStartData startData)
    {
        return new SaveData(startData.StartMoney, startData.FirstLevelName, startData.FirstLevelId, startData.CurrentSkin, startData.UnlockedSkins, startData.AnimalsCapacity);
    }
}

public class SaveData
{
    public int Money;
    public int LastLevelName;
    public int LastLevelId;
    public AnimalType SelectedSkin;
    public List<AnimalType> UnlockedSkins;
    public int Capacity;

    public SaveData(int money, int level,int levelId, AnimalType selectedSkin, List<AnimalType> unlockedSkins, int capacity)
    {
        Money = money;
        LastLevelName = level;
        LastLevelId = levelId;
        SelectedSkin = selectedSkin;
        UnlockedSkins = unlockedSkins;
        Capacity = capacity;
    }
}
