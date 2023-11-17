using UnityEngine;
using Zenject;

public class PlayerSaveContainer : MonoBehaviour, ICoroutineRunner
{
    public static PlayerSave PlayerSave { get; private set; }

    private void Awake()
    {
        if (PlayerSave != null)
        {
            Destroy(this.gameObject);
            return;
        }

        PlayerSave = new PlayerSave(this);
        DontDestroyOnLoad(gameObject);
    }
}
