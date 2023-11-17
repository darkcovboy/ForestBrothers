using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class AdRewardedButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _text;

    private RewardedVideo _rewardedVideo;


    private void Start()
    {
        _text.text = RewardedVideo.AddMoneyCount.ToString();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(Show);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Show);
    }

    [Inject]
    public void Constructor(RewardedVideo rewardedVideo)
    {
        _rewardedVideo = rewardedVideo;
    }

    private void Show()
    {
        _rewardedVideo.AddMoney();
        gameObject.Deactivate();
    }
}
