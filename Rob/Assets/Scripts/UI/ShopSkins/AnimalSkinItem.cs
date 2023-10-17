using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Animal Skin", menuName = "Animal Skin Item")]
public class AnimalSkinItem : ScriptableObject
{
    [Header("ShopInfo")]
    [SerializeField] private AnimalType _animalType;
    [SerializeField, Range(0,10000)] private int _price;
    [SerializeField] private bool _isByingForRealMoney;
    [ShowIf("_isByingForRealMoney")]
    [SerializeField, Range(10, 500)] private int _realMoneyPrice;
    [ShowIf("_isByingForRealMoney")]
    [SerializeField] private int _productId;
    [SerializeField] private Sprite _image;
    [SerializeField] private AnimalViewModel _animalViewModel;

    public AnimalType AnimalType => _animalType;
    public int Price => _price;
    public Sprite Image => _image;
    public AnimalViewModel AnimalViewModel => _animalViewModel;
    public bool BuiyngForRealMoney => _isByingForRealMoney;
    public int RealMoneyPrice => _realMoneyPrice;

    public int ProductId => _productId;
}
