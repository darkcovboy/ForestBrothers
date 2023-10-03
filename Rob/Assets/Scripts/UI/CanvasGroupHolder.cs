using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGroupHolder : MonoBehaviour
{
    [SerializeField] public List<GameObject> _panels;

    public void ClosePanels(GameObject gameObject)
    {
        foreach (GameObject panel in _panels)
        {
            if (panel != gameObject)
            {
                panel.Deactivate();
            }
        }
    }
}
