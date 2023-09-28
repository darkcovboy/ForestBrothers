using DG.Tweening;
using TMPro;
using UnityEngine;

public class RewardDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _rewardText;

    [SerializeField] private float _duration = 0.4f;
    [SerializeField] private float _yDistance = 2f;
    [SerializeField] private float _punchScale = 0.1f;
    [SerializeField] private int _punchVibrato = 5;
    [SerializeField] private float _punchElasticity = 1f;

    private float _startFade;

    public void ShowText(int reward)
    {
        _rewardText.text = $"+{reward}";
        _startFade = _rewardText.alpha;
        StartAnimation();
    }

    private void StartAnimation()
    {
        transform.DOMoveY(transform.position.y + _yDistance, _duration)
            .SetEase(Ease.OutQuad);

        transform.DOPunchScale(Vector3.one * _punchScale, _duration, _punchVibrato, _punchElasticity);

        // Плавное исчезание текста
        _rewardText.DOFade(0f, _duration).OnComplete(OnAnimationComplete);

    }

    private void OnAnimationComplete()
    {
        _rewardText.alpha = _startFade;
        gameObject.Deactivate();
    }
}
