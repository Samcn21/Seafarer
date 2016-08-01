using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelSiege : PanelParent
{
    public GameObject panelSiege;
    public Text city;
    public Text countryInviter;
    public Text countryInvited;
    public Text info;
    public Text countryInvitedResult;
    public Text countryInviterResult;
    public Text cityResult;
    public Button rollDiceOK;

    [SerializeField]
    private GameData.TeamCountry _invited;

    [SerializeField]
    private GameData.TeamCountry _inviter;

    [SerializeField]
    private GameData.City _city;

    [SerializeField]
    private int _inviterDiceNumber = 0;

    [SerializeField]
    private int _invitedDiceNumber = 0;

    [SerializeField]
    private int _cityDiceNumber = 0;

    void Awake()
    {
        if (!panelSiege)
            Debug.LogError("panelInfo is not found!");
    }

    public void SetPanelInfo(GameData.City cityName, GameData.TeamCountry inviter, GameData.TeamCountry invited, int cityDefenceNumber)
    {
        _invited = invited;
        _inviter = inviter;
        _city = cityName;

        city.text = cityName.ToString();
        countryInviter.text = inviter.ToString();
        countryInvited.text = invited.ToString();
        info.text = "roll a dice!";
        countryInvitedResult.text = string.Empty;
        countryInviterResult.text = string.Empty;
        cityResult.text = cityDefenceNumber.ToString();


    }

    public int GetCityDefence()
    {
        _cityDiceNumber = Random.Range(1, 21);
        return _cityDiceNumber;
    }

    private int GetMyDiceNumber()
    {
        return Random.Range(1, 11);
    }

    public void RollDice()
    {
        foreach (GameObject player in GameManager.Instance.GetAllPlayers())
        {
            player.GetComponent<PhotonView>().RPC("SetDiceNumber", PhotonTargets.All, GetMyDiceNumber(), GameManager.Instance.GetMyPlayer());
        }
        CheckResult();
    }

    private void CheckResult()
    {
        foreach (GameObject player in GameManager.Instance.GetAllPlayers())
        {
            PlayerController PlayerController = player.GetComponent<PlayerController>();
            PhotonView PhotonView = player.GetComponent<PhotonView>();

            if (PhotonView.isMine && PlayerController.GetMyTeam() == _inviter && PlayerController.GetDiceNumber() != 0)
            {
                _inviterDiceNumber = PlayerController.GetDiceNumber();
                countryInviterResult.text = PlayerController.GetDiceNumber().ToString();
            }

            if (PhotonView.isMine && PlayerController.GetMyTeam() == _invited && PlayerController.GetDiceNumber() != 0)
            {
                _invitedDiceNumber = PlayerController.GetDiceNumber();
                countryInvitedResult.text = PlayerController.GetDiceNumber().ToString();
            }

            //TODO: should _rolledInvited and _rolledInviter be set through network if inviterDiceNum and invitedDiceNum != 0
        }

        if (_rolledInviter && _rolledInvited)
        {
            rollDiceOK.GetComponentInChildren<Text>().text = "OK";

            if ((_inviterDiceNumber + _invitedDiceNumber) > _cityDiceNumber)
            {
                info.text = _inviter.ToString() + " and " + _invited.ToString() + " get " + (_inviterDiceNumber + _invitedDiceNumber) + " on the dice together which is higher than city's number. you can now attack the city";
            }
            else
            {
                info.text = _inviter.ToString() + " and " + _invited.ToString() + " get " + (_inviterDiceNumber + _invitedDiceNumber) + " on the dice together which is lower than city's number. the siege is broken!";
            }

        }
    }

    public void OK()
    {
        if (rollDiceOK.GetComponentInChildren<Text>().text == "OK")
        {
            //HidePanel();
            Debug.Log("ask the question!");
        }
    }
}
