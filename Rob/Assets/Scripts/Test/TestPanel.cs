using Agava.YandexGames;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestPanel : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;

    string _json = "asdasd";

    private IEnumerator Start()
    {
        yield return YandexGamesSdk.Initialize();
    }

    public void LoadData()
    {
        PlayerAccount.GetCloudSaveData((data) =>
        {
            Debug.Log(data);
            _inputField.text = data;
        }
        );
    }

    public void SaveData()
    {
        PlayerAccount.SetCloudSaveData(_inputField.text);
    }

    public void LoadNewData()
    {
        _inputField.text = _json;
    }
}
