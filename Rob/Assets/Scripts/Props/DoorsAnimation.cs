using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DoorsAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private readonly string DoorsOpenAnimation = "DoorsOpen";

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Animal>(out Animal animal))
        {
            Debug.Log("¬ход");
            _animator.Play(DoorsOpenAnimation);
            GetComponent<Collider>().enabled = false;
        }
    }
}
