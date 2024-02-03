using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkinView : MonoBehaviour, IPointerClickHandler
{
    public event Action<SkinView> Click;

    [SerializeField] private Sprite _standartBackground;
    [SerializeField] private Sprite _highlitedBackground;

    [SerializeField] private Image _contentImage;
    [SerializeField] private Image _lockImage;

    [SerializeField] private Price _price;

    [SerializeField] private Image _selectionText;

    private Image _backgroundImage;

    public AnimalSkinItem AnimalSkinItem { get; private set; }
    public bool IsLock { get; private set; }
    public bool IsSelected { get; private set; }
    public int Price => AnimalSkinItem.Price;
    public AnimalViewModel AnimalViewModel => AnimalSkinItem.AnimalViewModel;

    public void Initiliaze(AnimalSkinItem animalSkinItem)
    {
        _backgroundImage = GetComponent<Image>();
        _backgroundImage.sprite = _standartBackground;

        AnimalSkinItem = animalSkinItem;

        _contentImage.sprite = animalSkinItem.Image;

        _price.Show(animalSkinItem.Price);
    }

    public void OnPointerClick(PointerEventData eventData) => Click?.Invoke(this);

    public void Lock()
    {
        IsLock = true;
        _lockImage.gameObject.Activate();
        _price.Show(AnimalSkinItem.Price);
    }

    public void Unlock()
    {
        IsLock = false;
        _lockImage.gameObject.Deactivate();
        _price.Hide();
    }

    public void Select()
    {
        IsSelected = true;
        _selectionText.gameObject.Activate();
    }
    
    public void Unselect()
    {
        IsSelected = false;
        _selectionText.gameObject.Deactivate();
    }

    public void Highlight() => _backgroundImage.sprite = _highlitedBackground;

    public void UnHighlight() => _backgroundImage.sprite = _standartBackground;
}
