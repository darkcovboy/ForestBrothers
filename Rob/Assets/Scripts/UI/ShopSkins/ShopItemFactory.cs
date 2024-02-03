using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemViewFactory", menuName = "Shop/ShopItemViewFactory")]
public class ShopItemFactory : ScriptableObject
{
    [SerializeField] private SkinView _skinViewPrefab;

    public SkinView Get(AnimalSkinItem animalSkin, Transform parent)
    {
        SkinView instance = Instantiate(_skinViewPrefab, parent);
        instance.Initiliaze(animalSkin);
        return instance;
    }
}
