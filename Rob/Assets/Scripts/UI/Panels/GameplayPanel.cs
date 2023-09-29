using UnityEngine;
using TMPro;
using Zenject;

public class GameplayPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _levelNumber;

    [Inject]
    public void Constructor(PlayerSave playerSave)
    {
        _levelNumber.text = $"Level {playerSave.SaveData.LastLevelName}";
    }
}
