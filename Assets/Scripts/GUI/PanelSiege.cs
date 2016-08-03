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

    [SerializeField]
    private int _myDiceNumber = 0;

    void Awake()
    {
        if (!panelSiege)
            Debug.LogError("panelInfo is not found!");
    }

    [PunRPC]
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
        _myDiceNumber = Random.Range(1, 11);
        return _myDiceNumber;
    }

    [PunRPC]
    public void SetDiceNumber(int diceNumber, GameData.TeamCountry country)
    {
        if (_invited == country)
            _invitedDiceNumber = diceNumber;

        if (_inviter == country)
            _inviterDiceNumber = diceNumber;
    }

    public void RollDice()
    {
        if (rollDiceOK.GetComponentInChildren<Text>().text != "OK")
        { 
            GetMyDiceNumber();
            GetComponent<PhotonView>().RPC("SetDiceNumber", PhotonTargets.All, _myDiceNumber, GameManager.Instance.GetMyPlayer());
        }

        //

        //if ((_inviterDiceNumber + _invitedDiceNumber) > _cityDiceNumber)
        //{
        //    info.text = _inviter.ToString() + " and " + _invited.ToString() + " get " + (_inviterDiceNumber + _invitedDiceNumber) + " on the dice together which is higher than city's number. you can now attack the city";
        //}
        //else
        //{
        //    info.text = _inviter.ToString() + " and " + _invited.ToString() + " get " + (_inviterDiceNumber + _invitedDiceNumber) + " on the dice together which is lower than city's number. the siege is broken!";
        //}


    }

    void OnGUI() 
    {
        if (_inviterDiceNumber != 0)
        {
            countryInviterResult.text = _inviterDiceNumber.ToString();
        }
        else
        {
            countryInviterResult.text = "";
        }

        if (_invitedDiceNumber != 0)
        {
            countryInvitedResult.text = _invitedDiceNumber.ToString();
        }
        else
        {
            countryInvitedResult.text = "";
        }

        if (_invitedDiceNumber != 0 && _inviterDiceNumber != 0)
        {
            rollDiceOK.GetComponentInChildren<Text>().text = "OK";
        }
    }


    public void OK()
    {
        if (rollDiceOK.GetComponentInChildren<Text>().text == "OK")
        {
            if ((_inviterDiceNumber + _invitedDiceNumber) > _cityDiceNumber)
            {
                info.text = _inviter.ToString() + " and " + _invited.ToString() + " get " + (_inviterDiceNumber + _invitedDiceNumber) + " on the dice together which is higher than city's number. you can now attack the city";
            }
            else
            {
                info.text = _inviter.ToString() + " and " + _invited.ToString() + " get " + (_inviterDiceNumber + _invitedDiceNumber) + " on the dice together which is lower than city's number. the siege is broken!";
            }

            //HidePanel();
            Debug.Log("ask the question!");
        }
    }
}
