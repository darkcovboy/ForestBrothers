using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField] private Button _exitButton;

    private void OnEnable()
    {
        _exitButton.onClick.AddListener(ClosePanel);

    }

    private void OnDisable()
    {
        _exitButton.onClick.RemoveListener(ClosePanel);
    }

    private void ClosePanel() => gameObject.Deactivate();
}
