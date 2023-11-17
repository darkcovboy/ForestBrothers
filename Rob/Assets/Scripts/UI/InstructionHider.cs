using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class InstructionHider : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private CanvasGroup _canvasGroup;

    private readonly float _secondsTime = 0.03f;
    private readonly float _alphaDelimetr = 0.03f;

    private IEnumerator StartHide()
    {
        while(_canvasGroup.alpha > 0)
        {
            _canvasGroup.alpha -= _alphaDelimetr;
            yield return new WaitForSeconds(_secondsTime);
        }

        gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Клик");
        StartCoroutine(StartHide());
    }
}
