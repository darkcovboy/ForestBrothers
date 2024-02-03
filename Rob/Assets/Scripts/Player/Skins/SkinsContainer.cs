using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SkinsContainer", menuName = "Container/Skins")]
public class SkinsContainer : ScriptableObject
{
    [SerializeField] private List<AnimalData> _animalDatas;

    public IEnumerable<AnimalData> AnimalData => _animalDatas;

    /*
    private void OnValidate()
    {
        var animalSkinsDuplicates = _animalDatas.GroupBy(item => item.AnimalType)
            .Where(array => array.Count() > 1);

        if (animalSkinsDuplicates.Count() > 0)
            throw new InvalidOperationException(nameof(_animalDatas));
    }
    */
}
