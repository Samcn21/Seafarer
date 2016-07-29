using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelInfo : PanelParent
{
    public GameObject panelInfo;
    public Text textInfo;
    public Button ok;

    void Awake()
    {
        if (!panelInfo)
            Debug.LogError("panelInfo is not found!");
    }

    public void ShowMessage(string msg) 
    {
        ShowPanel();
        textInfo.text = msg;
    }

    public void OK() 
    {
        HidePanel();
    }
}
