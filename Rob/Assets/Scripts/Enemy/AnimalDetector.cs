using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class AnimalDetector : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private float _secondsToWait;

    public event UnityAction<Animal> OnAnimalCatched;

    private bool _canCatch = true;

    private void OnTriggerEnter(Collider other)
    {
        if(_canCatch && other.TryGetComponent<Animal>(out Animal animal))
        {
            OnAnimalCatched?.Invoke(animal);
            if(gameObject.activeSelf == true)
            {
                StartCoroutine(StopCatching());
            }
        }
    }

    private IEnumerator StopCatching()
    {
        _canCatch = false;

        yield return new WaitForSeconds(_secondsToWait);

        _canCatch = true;
    }
}
