using TMPro;
using UnityEngine;
using Zenject;

public class FontsSwitcher : MonoBehaviour
{
    [SerializeField] private TMP_Text _textMeshPro;
    [SerializeField] private FontsContainer _fontContainer;

    private LocalizationDeterminate _localization;

    [Inject]
    public void Constructor(LocalizationDeterminate localizationDeterminate)
    {
        _localization = localizationDeterminate;
        _localization.OnLanguageDeterminated += SwitchLanguage;
    }

    private void SwitchLanguage(string language)
    {
        switch (language)
        {
            case "tr":
                _textMeshPro.font = _fontContainer.TrFont;
                break;
            case "ru":
                _textMeshPro.font = _fontContainer.RuFont;
                break;
            case "en":
                _textMeshPro.font= _fontContainer.EngFont;
                break;
        }
    }
}
