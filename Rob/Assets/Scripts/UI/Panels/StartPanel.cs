using UnityEngine;
using UnityEngine.UI;

public class StartPanel : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private Shop _shopPanel;
    [SerializeField] private SettingsPanel _settingsPanel;
    [SerializeField] private GameplayPanel _gameplayPanel;
    [Header("Buttons")]
    [SerializeField] private Button _skinShopButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _startGameButton;

    private void OnEnable()
    {
        _settingsButton.onClick.AddListener(OpenSettingsPanel);
        _startGameButton.onClick.AddListener(StartGame);
        _skinShopButton.onClick.AddListener(OpenShopPanel);
    }

    private void OnDisable()
    {
        _settingsButton.onClick.RemoveListener(OpenSettingsPanel);
        _startGameButton.onClick.RemoveListener(StartGame);
        _skinShopButton.onClick.RemoveListener(OpenShopPanel);
    }

    private void OpenSettingsPanel() => _settingsPanel.gameObject.Activate();
    private void OpenShopPanel() => _shopPanel.gameObject.Activate();

    private void StartGame()
    {
        gameObject.Deactivate();
        _gameplayPanel.gameObject.Activate();
    }
}
