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

    [SerializeField]
    private bool _canRollDice;

    [SerializeField]
    private bool _canInvitedSeeMsg;

    void Awake()
    {
        if (!panelSiege)
            Debug.LogError("panelInfo is not found!");

    }

    public override void ShowPanel()
    {
        base.ShowPanel();
        _canRollDice = true;
        //_canInvitedSeeMsg = false;
    }

    [PunRPC]
    public void SetPanelInfo(GameData.City cityName, GameData.TeamCountry inviter, GameData.TeamCountry invited)
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
    }

    private int GetMyDiceNumber()
    {
        _myDiceNumber = GameManager.Instance.DiceController.GetPlayerDiceNumber();
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


    public void GetCityDiceNumber()
    {
        foreach (GameObject city in GameManager.Instance.allCities)
        {
            //finding city dice number
            if (city.GetComponent<CityController>().GetCityName() == _city)
            {
                _cityDiceNumber = city.GetComponent<CityController>().GetCityDiceNumber();
            }
        }
    }
    public void RollDice()
    {
        if (rollDiceOK.GetComponentInChildren<Text>().text != "OK" && _canRollDice)
        {
            GetMyDiceNumber();
            GetCityDiceNumber();
            GetComponent<PhotonView>().RPC("SetDiceNumber", PhotonTargets.All, _myDiceNumber, GameManager.Instance.GetMyPlayerTeam());
            _canRollDice = false;
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
        if (_cityDiceNumber != 0)
        {
            cityResult.text = _cityDiceNumber.ToString();
        }
        else
        {
            cityResult.text = "";
        }

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

            if (!_canInvitedSeeMsg)
            {
                if ((_inviterDiceNumber + _invitedDiceNumber) > _cityDiceNumber)
                {
                    info.text = _inviter.ToString() + " and " + _invited.ToString() + " get " + (_inviterDiceNumber + _invitedDiceNumber) + " on the dice together which is higher than city's number. you can now attack the city";
                }
                else
                {
                    info.text = _inviter.ToString() + " and " + _invited.ToString() + " get " + (_inviterDiceNumber + _invitedDiceNumber) + " on the dice together which is lower than city's number. the siege is broken!";
                }
            }
            else
            {
                info.text = "Please wait until " + _inviter + " accept to attack!";
            }
        }
    }

    public void ResetPanel()
    {
        rollDiceOK.GetComponentInChildren<Text>().text = "Roll Dice";
        _invited = GameData.TeamCountry.___;
        _inviter = GameData.TeamCountry.___;
        _inviterDiceNumber = 0;
        _invitedDiceNumber = 0;
        _cityDiceNumber = 0;
        _myDiceNumber = 0;
        _canRollDice = false;
    }

    public void OK()
    {
        if (rollDiceOK.GetComponentInChildren<Text>().text == "OK")
        {
            //allies can attack now
            if ((_inviterDiceNumber + _invitedDiceNumber) > _cityDiceNumber)
            {
                if (GameManager.Instance.GetMyPlayerTeam() == _inviter) //only inviter goes to panel question
                {
                    HidePanel();
                    GUIManager.Instance.PanelQuestion.OpenPanel(_inviter, _invited, _city);
                    ResetPanel();
                }
                else if (GameManager.Instance.GetMyPlayerTeam() == _invited && GameManager.Instance.QuestionBank.GetRandomQuestionNumber() == 0) //means inviter still didn't click on OK!
                {
                    _canInvitedSeeMsg = true;
                }
                else if (GameManager.Instance.GetMyPlayerTeam() == _invited && GameManager.Instance.QuestionBank.GetRandomQuestionNumber() != 0)
                {
                    HidePanel();
                    GUIManager.Instance.PanelQuestion.OpenPanelInvited(_inviter, _invited, _city);
                    ResetPanel();
                }
            }
            else //the siege is broken! changing everything to normal
            {
                foreach (GameObject player in GameManager.Instance.GetAllPlayers())
                {
                    player.GetComponent<PhotonView>().RPC("ChangePlayerStatusInvited", PhotonTargets.All, _invited, _inviter, true);
                }

                foreach (GameObject city in GameManager.Instance.allCities)
                {
                    if (city.GetComponent<CityController>().GetCityName() == _city)
                    {
                        city.GetComponent<PhotonView>().RPC("SetCityStatus", PhotonTargets.All, GameData.DefenceStatus.Free);
                        city.GetComponent<PhotonView>().RPC("SetCityDiceNumber", PhotonTargets.All, 0);
                    }
                }

                HidePanel();
                ResetPanel();
            }
        }
    }
}