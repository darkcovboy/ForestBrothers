using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InitText : MonoBehaviour
{
    [SerializeField] private TMP_Text _initText;
    [SerializeField] private TMP_Text _startText;

    public static InitText Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void ChangeText()
    {
        _startText.gameObject.SetActive(true);

        _initText.gameObject.SetActive(false);
    }
}
