using UnityEngine;
using Zenject;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class ItemCounter : MonoBehaviour
{
    private ItemCollector _itemCollector;
    private TMP_Text _text;

    [Inject]
    public void Constructor(ItemCollector itemCollector)
    {
        _itemCollector = itemCollector;
        _text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        _itemCollector.OnCountItemsChanged += OnValueChange;
    }

    private void OnDisable()
    {
        _itemCollector.OnCountItemsChanged += OnValueChange;
    }

    private void OnValueChange(int current, int max)
    {
        _text.text = $"{current}/{max}";
    }
}
