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

    public void AcceptRejectInvitation(bool answer)
    {

        foreach (GameObject player in GameManager.Instance.GetAllPlayers())
        {
            if (answer)
            {
                //1. send acceptance to all network, the inviter will recieve their answer
                player.GetComponent<PhotonView>().RPC("Receiver", PhotonTargets.All, true, _invited, _inviter);
                player.GetComponent<PhotonView>().RPC("ChangePlayerStatusInvited", PhotonTargets.All, _invited, _inviter);
                foreach (GameObject city in GameManager.Instance.allCities)
                {
                    //finding the clicked city between all cities
                    if (city.GetComponent<CityController>().GetCityName() == _city)
                    {
                        //change city status to under siege in all network
                        city.GetComponent<PhotonView>().RPC("SetCityStatus", PhotonTargets.All, GameData.DefenceStatus.UnderSiege);
                    }
                }
                GUIManager.Instance.PanelCity.HidePanel();
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
