using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PanelCity : MonoBehaviour, IPanelControl
{
    [SerializeField]
    private GameObject panelCity;

    [SerializeField]
    private Text CityName;

    [SerializeField]
    private Text CityOwner;

    [SerializeField]
    private Text CityInfo;

    [SerializeField]
    private Text CityDefence;

    [SerializeField]
    private Button btnAttack;

    [SerializeField]
    private Button[] btnAlliances;

    [SerializeField]
    private GameData.TeamCountry _askerCountry;

    [SerializeField]
    private GameData.TeamCountry _chosenAlliance;

    [SerializeField]
    private GameData.City _city;

    [SerializeField]
    private List<GameData.TeamCountry> _possibleAlliances;

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

        foreach (Button btn in btnAlliances)
        {
            btn.interactable = false;
            btn.GetComponent<Image>().enabled = false;
        }
    }

    public void OpenPanel(GameData.City cityName, GameData.TeamCountry askerCountry)
    {
        ShowPanel();
        CityName.text = cityName.ToString();
        _city = cityName;
        _askerCountry = askerCountry;


        foreach (GameObject city in GameManager.Instance.allCities)
        {
            CityController CityController = city.GetComponent<CityController>();
            if (CityController.GetCityName() == _city)
            {
                CityDefence.text = CityController.GetCityDefence().ToString();

                //if city is under attack or under siege disable the attack button
                if (CityController.GetCityDefenceStatus() == GameData.DefenceStatus.UnderAttack)
                {
                    btnAttack.enabled = false;
                    btnAttack.GetComponent<Image>().enabled = false;
                    CityInfo.text = "The city is under attack by other countries now, come back later!";
                }
                else if (CityController.GetCityDefenceStatus() == GameData.DefenceStatus.UnderSiege)
                {
                    btnAttack.enabled = false;
                    btnAttack.GetComponent<Image>().enabled = false;
                    CityInfo.text = "The city is under siege by other countries now, come back later!";
                }
                else
                {
                    //TODO: check if the city occupied by two countries including the asker country, it must say
                    //you can make alliance and attack to a city that you already captured!
                    btnAttack.enabled = true;
                    btnAttack.GetComponent<Image>().enabled = true;
                    CityInfo.text = "";
                }

                //city has no owner means it's neutral or after event became neutral again!
                if (CityController.GetCityOwners().Count == 0)
                {
                    CityOwner.text = "Neutral City";
                }
                //city has one owner
                else if (CityController.GetCityOwners().Count == 1)
                {
                    foreach (GameData.TeamCountry owner in CityController.GetCityOwners())
                    {
                        CityOwner.text = "Current Owner: " + owner.ToString();
                    }
                }
                //city has multiple owner
                else
                {
                    CityOwner.text = "Current Owners: ";
                    foreach (GameData.TeamCountry owner in CityController.GetCityOwners())
                    {
                        CityOwner.text += owner.ToString() + " ";
                    }
                }

            }
        }

    }

    public void Attack()
    {
        //find the selected city between all cities
        foreach (GameObject city in GameManager.Instance.allCities)
        {
            //finding the clicked city between all cities
            if (city.GetComponent<CityController>().GetCityName() == _city)
            {
                //check if city is free and neutral ---> singular attack
                if (city.GetComponent<CityController>().GetCityDefenceStatus() == GameData.DefenceStatus.Free && city.GetComponent<CityController>().GetCityStatus() == GameData.CityStatus.Neutral)
                {
                    //change city status to under attack in all network
                    city.GetComponent<PhotonView>().RPC("SetCityStatus", PhotonTargets.All, GameData.DefenceStatus.UnderAttack);

                    //change player mode from exploring to attacking
                    foreach (GameObject player in GameManager.Instance.GetAllPlayers())
                    {
                        if (player.GetComponent<PlayerController>().GetMyTeam() == _askerCountry)
                        {
                            player.GetComponent<PhotonView>().RPC("ChangePlayMode", PhotonTargets.AllBufferedViaServer, GameData.TeamPlayMode.Attacking);
                        }
                    }
                    GUIManager.Instance.PanelQuestion.OpenPanel(_askerCountry, _city);
                    HidePanel();
                }
                //TODO: for attack to make alliance need to find alliance and open that panel first
                //check if city is free and occupied by one team ---> make alliance then attack
                else if (city.GetComponent<CityController>().GetCityDefenceStatus() == GameData.DefenceStatus.Free && city.GetComponent<CityController>().GetCityStatus() == GameData.CityStatus.OccupiedByOneCountry)
                {
                    btnAttack.enabled = false;
                    btnAttack.GetComponent<Image>().enabled = false;
                    CityInfo.fontSize = 12;
                    CityInfo.text = "You cannot attack this city alone, you need to make an alliance first with one of the teams in the below";
                    FindInActionRangePlayers(city);
                }
                //TODO: for attack to make alliance need to find alliance and open that panel first
                //check if city is free and occupied by two teams ---> make alliance then attack if my country is not one of the owners
                else if (city.GetComponent<CityController>().GetCityDefenceStatus() == GameData.DefenceStatus.Free && city.GetComponent<CityController>().GetCityStatus() == GameData.CityStatus.OccupiedByAllies)
                {
                    FindInActionRangePlayers(city);
                }

            }
        }

    }

    //this method is invoked only by Onclick() function when player wants to make alliance
    public void ChosenAlliance(string team)
    {
        //converting chosen country from string to enum which saved in GameData Class
        if (GameData.TeamCountry.IsDefined(typeof(GameData.TeamCountry), team))
        {
            _chosenAlliance = (GameData.TeamCountry)GameData.TeamCountry.Parse(typeof(GameData.TeamCountry), team, true);

            foreach (GameObject player in GameManager.Instance.GetAllPlayers())
            {
                if (player.GetComponent<PlayerController>().GetMyTeam() == _chosenAlliance)
                    player.GetComponent<PhotonView>().RPC("InviteAlliance" ,PhotonTargets.Others ,_chosenAlliance, _askerCountry, _city);
            }
        }
    }

    //find all players that have this city in their action range except me
    [PunRPC]
    private void FindInActionRangePlayers(GameObject city)
    {
        //search for all players in city's action range
        foreach (GameData.TeamCountry player in city.GetComponent<CityController>().GetPlayersInActionRange())
        {
            if (player != _askerCountry)
            {
                foreach (Button btnAlliance in btnAlliances)
                {
                    if (btnAlliance.name.Contains(player.ToString()))
                    {
                        btnAlliance.interactable = true;
                        btnAlliance.GetComponent<Image>().enabled = true;
                    }
                }
            }
        }
    }
}
