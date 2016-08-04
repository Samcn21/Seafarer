using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelAllianceInvitation : PanelParent
{
    public GameObject panelAllianceInvitation;
    public Text textInfo;
    public Button yes;
    public Button no;

    [SerializeField]
    private GameData.TeamCountry _invited;

    [SerializeField]
    private GameData.TeamCountry _inviter;

    [SerializeField]
    private GameData.City _city;

    [SerializeField]
    private int _cityDiceNumber = 0;

    void Awake()
    {
        if (!panelAllianceInvitation)
            Debug.LogError("panelAllianceInvitation is not found!");
    }

    public void ShowInvitationMessage(string msg, GameData.TeamCountry invited, GameData.TeamCountry inviter, GameData.City city)
    {
        _invited = invited;
        _inviter = inviter;
        _city = city;
        textInfo.text = msg;
        ShowPanel();
    }

    private int GetCityDefence()
    {
        _cityDiceNumber = Random.Range(1 , 2);
        return _cityDiceNumber;
    }

    public void AcceptRejectInvitation(bool answer)
    {
        GetCityDefence();
        foreach (GameObject player in GameManager.Instance.GetAllPlayers())
        {
            if (answer)
            {
                //1. send acceptance to all network, the inviter will recieve their answer
                player.GetComponent<PhotonView>().RPC("Receiver", PhotonTargets.All, true, _invited, _inviter, _city);
                player.GetComponent<PhotonView>().RPC("ChangePlayerStatusInvited", PhotonTargets.All, _invited, _inviter, false);
                foreach (GameObject city in GameManager.Instance.allCities)
                {
                    //finding the clicked city between all cities
                    if (city.GetComponent<CityController>().GetCityName() == _city)
                    {
                        //change city status to under siege in all network
                        city.GetComponent<PhotonView>().RPC("SetCityStatus", PhotonTargets.All, GameData.DefenceStatus.UnderSiege);
                        city.GetComponent<PhotonView>().RPC("SetCityDiceNumber", PhotonTargets.All, _cityDiceNumber);
                    }
                }
                GUIManager.Instance.PanelSiege.SetPanelInfo(_city, _inviter, _invited);

                //these should be always the last to run
                GUIManager.Instance.PanelCity.HidePanel();
                GUIManager.Instance.PanelSiege.ShowPanel();
                
            }
            else
            {
                //1. send rejection to all network, the inviter will recieve their answer
                player.GetComponent<PhotonView>().RPC("Receiver", PhotonTargets.All, false, _invited, _inviter);
            }
        }
        HidePanel();
    }
}
