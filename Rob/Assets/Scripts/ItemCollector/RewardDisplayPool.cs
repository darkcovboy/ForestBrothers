using TMPro;
using UnityEngine;

public class RewardDisplayPool : ObjectPool<RewardDisplay>
{
    [SerializeField] private RewardDisplay _rewardDisplayPrefab;
    [SerializeField] private Transform _startPoint;

    private void Start()
    {
        Initialize(_rewardDisplayPrefab);

        foreach (var item in Pool)
        {
            item.gameObject.transform.LookAt(Camera.main.transform);
        }
    }

    public void ShowTextPopup(int reward)
    {
        if (TryGetObject(out RewardDisplay text))
        {
            text.gameObject.Activate();
            text.ShowText(reward);
        }
    }
}
