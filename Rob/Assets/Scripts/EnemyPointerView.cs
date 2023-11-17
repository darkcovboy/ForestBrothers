using UnityEngine;
using UnityEngine.UI;

public class EnemyPointerView : MonoBehaviour
{
    [SerializeField] private Image _image;
    private void Awake()
    {
        _image.enabled = false;
    }
    public void Show()
    {
        if (gameObject.activeSelf != false)
            _image.enabled = true;
    }

    public void Hide()
    {
        if(gameObject.activeSelf != false)
            _image.enabled = false;
    }
}
