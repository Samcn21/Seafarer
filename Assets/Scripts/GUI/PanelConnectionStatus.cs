using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelConnectionStatus : MonoBehaviour, IPanelControl
{
    public GameObject panelConnectionStatus;
    public Text internetStatus;
    public Text GPSStatus;
    public Text networkConStatus;
    public Text networkStatus;

    void Awake()
    {
        if (!panelConnectionStatus)
            Debug.LogError("PanelConnectionStatus is not found!");

        if (!internetStatus)
            Debug.LogError("InternetStatus is not found!");

        if (!GPSStatus)
            Debug.LogError("GPSStatus is not found!");

        if (!networkStatus)
            Debug.LogError("NetworkStatus is not found!");
    }

    public void ShowPanel()
    {
        panelConnectionStatus.GetComponent<CanvasGroup>().alpha = 1;
        panelConnectionStatus.GetComponent<CanvasGroup>().interactable = true;
        GameManager.Instance.SetPlayerInteract(false);
    }

    public void HidePanel()
    {
        panelConnectionStatus.GetComponent<CanvasGroup>().alpha = 0;
        panelConnectionStatus.GetComponent<CanvasGroup>().interactable = false;
        GameManager.Instance.SetPlayerInteract(true);
    }
}
