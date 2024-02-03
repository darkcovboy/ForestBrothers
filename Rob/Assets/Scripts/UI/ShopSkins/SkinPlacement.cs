using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinPlacement : MonoBehaviour
{
    private const string RenderLayer = "SkinRender";

    private AnimalViewModel _currentModel;

    public void InstantiateModel(AnimalViewModel model)
    {
        if (_currentModel != null)
            Destroy(_currentModel.gameObject);

        _currentModel = Instantiate(model, transform);

        Transform[] childrens = _currentModel.GetComponentsInChildren<Transform>();
        foreach (Transform child in childrens)
            child.gameObject.layer = LayerMask.NameToLayer(RenderLayer);
    }
}
