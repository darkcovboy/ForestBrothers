using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;
using PlayerPrefs = Agava.YandexGames.PlayerPrefs;
using System.ComponentModel;

public class InitializeSDK : MonoBehaviour
{
    [SerializeField] private PlayerSaveContainer _playerSaveContainer;

    private IEnumerator Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
yield return YandexGamesSdk.Initialize();
#endif


        Instantiate(_playerSaveContainer);
        yield break;
#if UNITY_WEBGL && !UNITY_EDITOR
YandexGamesSdk.GameReady();
#endif
    }
}
