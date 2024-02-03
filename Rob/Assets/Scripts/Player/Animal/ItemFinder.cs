using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ItemFinder : MonoBehaviour
{
    [SerializeField] private Animal _animal;
    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_animal.IsItemHandled == true)
        {
            return;
        }
        else
        {
            if (other.TryGetComponent<Item>(out Item item))
            {
                item.ConnectTo(_animal.PointHandler);
                _animal.SetItem(item);
            }
        }
    }
}
