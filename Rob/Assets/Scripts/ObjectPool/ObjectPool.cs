using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private int _capacity = 8;

    protected List<T> Pool = new List<T>();

    protected void Initialize(T prefab)
    {
        for (int i = 0; i < _capacity; i++)
        {
            T spawned = Instantiate(prefab, _container.transform);
            spawned.gameObject.Deactivate();
            Pool.Add(spawned);
        }
    }

    protected bool TryGetObject(out T result)
    {
        result = Pool.FirstOrDefault(p => !p.gameObject.activeSelf);

        return result != null;
    }
}
