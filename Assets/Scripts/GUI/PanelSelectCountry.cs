using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelSelectCountry : MonoBehaviour, IPanelControl
{
    public GameObject panelSelectCountry;
    public Button btnDenmark;
    public Button btnEngland;
    public Button btnFrance;
    public Button btnGermany;
    public Button btnHolland;
    public Button btnPortugal;
    public Button btnSpain;
    public Button btnVenice;

    public GameObject[] allButtons;
    void Awake()
    {
        if (!panelSelectCountry)
            Debug.LogError("panelSelectCountry is not found!");
    }
    void Start() 
    {
        allButtons = GameObject.FindGameObjectsWithTag("CountryButton"); 
    }

    public void ShowPanel()
    {
        panelSelectCountry.GetComponent<CanvasGroup>().alpha = 1;
    }

    public void HidePanel()
    {
        panelSelectCountry.GetComponent<CanvasGroup>().alpha = 0;
    }

    public void ChosenCountry(string team) 
    {
        //Debug.Log(team.ToString());
    }
}
