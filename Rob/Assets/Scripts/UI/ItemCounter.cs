using UnityEngine;
using Zenject;
using TMPro;
using UnityEngine.UI;

public class ItemCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Slider _slider;

    private IItemChangedHandler _itemCollector;

    [Inject]
    public void Constructor(IItemChangedHandler itemCollector)
    {
        _itemCollector = itemCollector;
        _itemCollector.OnCountItemsChanged += OnValueChange;
    }

    private void OnDisable()
    {
        _itemCollector.OnCountItemsChanged += OnValueChange;
    }

    private void OnValueChange(int current, int max)
    {
        _text.text = $"{current}/{max}";
        _slider.value = current;
        _slider.maxValue = max;
    }
}
