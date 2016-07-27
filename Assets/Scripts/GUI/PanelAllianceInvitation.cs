using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelAllianceInvitation : MonoBehaviour, IPanelControl
{
    public GameObject panelAllianceInvitation;
    public Text textInfo;
    public Button yes;
    public Button no;

    void Awake()
    {
        if (!panelAllianceInvitation)
            Debug.LogError("panelAllianceInvitation is not found!");
    }

    public void ShowPanel()
    {
        panelAllianceInvitation.GetComponent<CanvasGroup>().alpha = 1;
        panelAllianceInvitation.GetComponent<CanvasGroup>().interactable = true;
    }

    public void HidePanel()
    {
        panelAllianceInvitation.GetComponent<CanvasGroup>().alpha = 0;
        panelAllianceInvitation.GetComponent<CanvasGroup>().interactable = false;
    }

    public void ShowInvitationMessage(string msg) 
    {
        ShowPanel();
        textInfo.text = msg;
    }

}
