using Lean.Localization;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class TestLanguage : MonoBehaviour
{
    [SerializeField] private LeanLocalization _leanLocalization;
    [SerializeField] private string _language;

    [Button]
    public void SetLanguage()
    {
        switch (_language)
        {
            case "en":
                _leanLocalization.SetCurrentLanguage("English");
                break;
            case "tr":
                _leanLocalization.SetCurrentLanguage("Turkish");
                break;
            case "ru":
                _leanLocalization.SetCurrentLanguage("Russian");
                break;
        }
    }
    [Button]
    public void OffMusic()
    {
        AudioListener.pause = true;
        AudioListener.volume = 0;
        Time.timeScale = 0;
    }

    [Button]
    public void PlayMusic()
    {
        AudioListener.pause = false;
        AudioListener.volume = 1;
        Time.timeScale = 1;
    }

}
