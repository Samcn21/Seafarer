﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;


[System.Serializable]
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

    [SerializeField]
    private GameData.TeamCountry _chosenTeam;

    [SerializeField]
    private List<GameData.TeamCountry> _alreadyTakenCountry = new List<GameData.TeamCountry>();

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
        panelSelectCountry.GetComponent<CanvasGroup>().interactable = true;
        GameManager.Instance.SetPlayerInteract(false);
    }

    public void HidePanel()
    {
        panelSelectCountry.GetComponent<CanvasGroup>().alpha = 0;
        panelSelectCountry.GetComponent<CanvasGroup>().interactable = false;
        GameManager.Instance.SetPlayerInteract(true);
    }

    //this method is invoked only by Onclick() function on the country buttons in chosen country panel
    public void ChosenCountry(string team) 
    {
        //converting chosen country from string to enum which saved in GameData Class
        if (GameData.TeamCountry.IsDefined(typeof(GameData.TeamCountry), team))
        { 
            _chosenTeam = (GameData.TeamCountry) GameData.TeamCountry.Parse(typeof(GameData.TeamCountry), team, true);

            //if already take then exit the function
            if (_alreadyTakenCountry.Contains(_chosenTeam))
            {
                return;
            }
            else
            { 
                _alreadyTakenCountry.Add(_chosenTeam);
            }
        }

        //Disabling other countries and informing network that this country is taken already
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
        
        //instantiate chosen country
        NetworkManager.Instance.Instantiate.InstantiateMe(_chosenTeam);

        //turn off this panel and show ready panel if I already instantiated

        if (NetworkManager.Instance.Instantiate.HasMyInstance())
        {
            GUIManager.Instance.PanelReady.ShowPanelChosenCountry(_chosenTeam);
            GUIManager.Instance.PanelSelectCountry.HidePanel();
            
        }
        else
        { 
            //TODO the room is full, you cannot enter (making instantiate has been failed)
        }

    }
}
