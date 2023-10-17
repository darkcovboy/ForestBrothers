using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName ="FontsContainer")]
public class FontsContainer : ScriptableObject
{
    [SerializeField] private TMP_FontAsset _engFont;
    [SerializeField] private TMP_FontAsset _ruFont;
    [SerializeField] private TMP_FontAsset _trFont;

    public TMP_FontAsset RuFont => _ruFont;
    public TMP_FontAsset EngFont => _engFont;
    public TMP_FontAsset TrFont => _trFont;
}
