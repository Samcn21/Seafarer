using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelCity : MonoBehaviour, IPanelControl
{
    public GameObject panelCity;
    public Text CityName;

    void Awake()
    {
        if (!panelCity)
            Debug.LogError("panelCity is not found!");
    }

    public void ShowPanel()
    {
        panelCity.GetComponent<CanvasGroup>().alpha = 1;
        panelCity.GetComponent<CanvasGroup>().interactable = true;
    }

    public void HidePanel()
    {
        panelCity.GetComponent<CanvasGroup>().alpha = 0;
        panelCity.GetComponent<CanvasGroup>().interactable = false;
    }

    public void OpenPanel(string cityName)
    {
        ShowPanel();
        CityName.text = cityName;
    }
}
