using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Animal Skin", menuName = "Animal Skin Item")]
public class AnimalSkinItem : ScriptableObject
{
    [Header("ShopInfo")]
    [SerializeField] private AnimalType _animalType;
    [SerializeField, Range(0,10000)] private int _price;
    [SerializeField] private Sprite _image;
    [SerializeField] private AnimalViewModel _animalViewModel;

    public AnimalType AnimalType => _animalType;
    public int Price => _price;
    public Sprite Image => _image;
    public AnimalViewModel AnimalViewModel => _animalViewModel;
}
