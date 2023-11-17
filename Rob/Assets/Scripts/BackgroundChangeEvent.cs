using UnityEngine;
using Agava.WebUtility;

public class BackgroundChangeEvent : MonoBehaviour
{
    private bool _audioOff;

    private void OnEnable()
    {
        WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;
    }

    private void OnDisable()
    {
        WebApplication.InBackgroundChangeEvent -= OnInBackgroundChange;
    }

    private void OnInBackgroundChange(bool inBackground)
    {
        Debug.Log("Â background " + inBackground);

        if(inBackground)
        {
            _audioOff = AudioListener.pause;
            AudioListener.pause = true;
            AudioListener.volume = 0;
            Time.timeScale = 0;
        }
        else
        {
            AudioListener.pause = _audioOff;
            AudioListener.volume = 1;
            Time.timeScale = 1;
        }
    }
}
