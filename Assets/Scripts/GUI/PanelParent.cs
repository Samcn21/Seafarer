using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelParent : Photon.MonoBehaviour, IPanelControl
{
    public virtual void ShowPanel()
    {
        GetComponent<CanvasGroup>().alpha = 1;
        GetComponent<CanvasGroup>().interactable = true;
    }

    public virtual void HidePanel()
    {
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().interactable = false;
    }
}
