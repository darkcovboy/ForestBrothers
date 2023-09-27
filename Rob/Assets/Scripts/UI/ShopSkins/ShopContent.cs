using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Shop Content", menuName = "Shop Content")]
public class ShopContent : ScriptableObject
{
    [SerializeField] private List<AnimalSkinItem> _animalSkinsItems;

    public IEnumerable<AnimalSkinItem> AnimalSkins => _animalSkinsItems;

    private void OnValidate()
    {
        var animalSkinsDuplicates = _animalSkinsItems.GroupBy(item => item.AnimalType)
            .Where(array => array.Count() > 1);

        if (animalSkinsDuplicates.Count() > 0)
            throw new InvalidOperationException(nameof(_animalSkinsItems));
    }
}
