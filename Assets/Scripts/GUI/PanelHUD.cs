using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelHUD : PanelParent
{
    public GameObject panelHUD;
    public Text timer;

    void Awake()
    {
        if (!panelHUD)
            Debug.LogError("panelHUD is not found!");
    }

    void OnGUI() 
    {
        timer.text = GameManager.Instance.TimeController.GetRemainTime();
    }
}
