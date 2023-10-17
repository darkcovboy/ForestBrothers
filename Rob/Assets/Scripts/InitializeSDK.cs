using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;

public class InitializeSDK : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return YandexGamesSdk.Initialize();

        YandexGamesSdk.GameReady();
    }
}
