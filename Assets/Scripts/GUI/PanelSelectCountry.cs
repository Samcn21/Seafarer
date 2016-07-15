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

    public GameObject[] allCountryButtons;
    void Awake()
    {
        if (!panelSelectCountry)
            Debug.LogError("panelSelectCountry is not found!");
    }
    void Start() 
    {
        allCountryButtons = GameObject.FindGameObjectsWithTag("CountryButton"); 
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
        foreach (GameObject button in allCountryButtons)
        {
            if (button.GetComponent<Button>().name.ToString() != team)
            {
                button.SetActive(false);
                //TODO country chosen and need to send to the network nobody else shouldn't be able to take this
            }
            else
            {
                button.GetComponent<Button>().interactable = false;
            }
        }
        //Debug.Log(team.ToString());
    }
}
