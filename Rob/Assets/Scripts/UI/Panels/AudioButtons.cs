using UnityEngine;
using UnityEngine.UI;

public class AudioButtons : MonoBehaviour
{
    [SerializeField] private Button _offAudio;
    [SerializeField] private Button _onAudio;

    private void OnEnable()
    {
        _offAudio.onClick.AddListener(OffAudio);
        _onAudio.onClick.AddListener(OnAudio);
    }

    private void OnDisable()
    {
        _offAudio.onClick.RemoveListener(OffAudio);
        _onAudio.onClick.RemoveListener(OnAudio);
    }

    private void OnAudio()
    {
        _onAudio.gameObject.Deactivate();
        _offAudio.gameObject.Activate();
        AudioListener.pause = true;
        AudioListener.volume = 0;
    }

    private void OffAudio()
    {
        _onAudio.gameObject.Activate();
        _offAudio.gameObject.Deactivate();
        AudioListener.pause = false;
        AudioListener.volume = 1.0f;
    }
}
