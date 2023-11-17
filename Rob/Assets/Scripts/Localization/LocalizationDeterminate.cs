using Lean.Localization;
using UnityEngine;
using UnityEngine.Events;
using Agava.YandexGames;
using System.Collections;

public class LocalizationDeterminate : MonoBehaviour
{
    [SerializeField] private LeanLocalization _leanLocalization;
    [SerializeField] private string _language;

    public event UnityAction<string> OnLanguageDeterminated;

#if UNITY_EDITOR
    private void Start()
    {
        ChooseLanguage(_language);
        OnLanguageDeterminated?.Invoke(_language); 
    }
#elif UNITY_WEBGL

private IEnumerator Start()
    {
        yield return YandexGamesSdk.Initialize();
        _language = YandexGamesSdk.Environment.i18n.lang;
        OnLanguageDeterminated?.Invoke(_language);
        ChooseLanguage(_language);
    }
#endif

    private void ChooseLanguage(string lang)
    {
        switch (lang)
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
}
