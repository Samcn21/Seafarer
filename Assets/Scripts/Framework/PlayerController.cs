using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TouchScript;

[System.Serializable]
public class PlayerController : Photon.MonoBehaviour
{
    [SerializeField]
    private int _myTeamID;

    [SerializeField]
    private GameData.TeamCountry _myTeam;

    [SerializeField]
    private GameData.TeamPlayMode _myPlayMode = GameData.TeamPlayMode.Exploring;

    [SerializeField]
    private float _myTotalPoints = 0;

    [SerializeField]
    private bool _isAlone;

    [SerializeField]
    private List<GameData.TeamCountry> _allies;

    [SerializeField]
    private List<GameData.City> _capturedCitiesAlone;

    [SerializeField]
    private List<GameData.City> _capturedCitiesAlly;

    [SerializeField]
    private List<GameData.City> _capturedCities;

    //TODO: use this dictionary for adding cities that player cannot comeback for a couple of mins
    [SerializeField]
    private Dictionary<float, GameData.City> forbidenCities = new Dictionary<float, GameData.City>();

    [SerializeField]
    private List<int> _questionsTotal;

    [SerializeField]
    private List<int> _questionsTrue;

    [SerializeField]
    private List<GameData.City> _citiesInActionRange = new List<GameData.City>();

    public bool _isTouchMovement { get; private set; }

    private Vector3 _nextPosition;
    private Vector2 _screenPosition;

    [SerializeField]
    private float _speed = 100;

    int run = 0;
    void Start()
    {
        run = 0;

        if (GameManager.Instance.GetGameStatus(GameData.GameStatus.UsingGPS))
        {
            //TODO a class for GPS calculation
        }
        else
        {
            _isTouchMovement = true;
            _speed = GameManager.Instance.GetPlayerSpeed();
        }

        //get the radius from game manager based on map size
        this.GetComponent<SphereCollider>().radius = GameManager.Instance.GetPlayerActionRange();
        this.GetComponent<FogOfWarUnit>().radius = GameManager.Instance.GetPlayerFOWRadius();

        //in the beginning the country is alone:
        _isAlone = true;

        //GetComponent<PhotonView>().RPC("SetMyTeamID", PhotonTargets.All, PhotonNetwork.player.ID);
        //SetMyTeamID(PhotonNetwork.player.ID);

        //Debug.Log(FindCurrentAlly());
    }

    void Update()
    {
        if (_isTouchMovement & StateManager.Instance.CurrentActiveState == GameData.GameStates.Play)
        {
            if (GameManager.Instance.CanPlayerInteract())
            {
                //Debug.Log( GameManager.Instance.CanPlayerInteract());
                this.transform.position = Vector3.Lerp(this.transform.position, _nextPosition, _speed * Time.deltaTime);
                this.transform.position = new Vector3(this.transform.position.x, 1, this.transform.position.z);
            }

        }
    }

    [PunRPC]
    public void SetMyTeamID(int value)
    {
        _myTeamID = value;
    }

    public void ChooseMyTeam(GameData.TeamCountry team)
    {
        _myTeam = team;
    }

    public GameData.TeamCountry GetMyTeam()
    {
        return _myTeam;
    }

    public GameData.TeamCountry FindCurrentAlly()
    {
        if (_allies.Count - 1 <= 0)
        {
            return _myTeam;
        }
        else
        {
            return _allies[_allies.Count - 1];
        }
    }

    public List<GameData.TeamCountry> Allies() 
    {
        return _allies;
    }

    //returns the questions that have been asked from this team
    public List<int> GetTotalQuestions(GameData.TeamCountry team)
    {
        return _questionsTotal;
    }

    [PunRPC]
    public void AddToTotalQuestions(int questionNumber, GameData.TeamCountry askerCountry)
    {
        if (_myTeam == askerCountry)
            _questionsTotal.Add(questionNumber);
    }

    private void OnEnable()
    {
        if (TouchManager.Instance != null)
        {
            TouchManager.Instance.TouchesBegan += touchesBeganHandler;
        }
    }

    private void OnDisable()
    {
        if (TouchManager.Instance != null)
        {
            TouchManager.Instance.TouchesBegan -= touchesBeganHandler;
        }
    }

    private void touchesBeganHandler(object sender, TouchEventArgs e)
    {
        foreach (var point in e.Touches)
        {
            if (GameManager.Instance.CanPlayerInteract())
            {
                _screenPosition = point.Position;
                _nextPosition = Camera.main.ScreenToWorldPoint(new Vector3(_screenPosition.x, _screenPosition.y, Camera.main.transform.position.y - this.transform.position.y));
            }
        }
    }





    //A city or player comes to player's action range
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "City")
        {
            //check if the city is not already in the list, then add it.
            if (!_citiesInActionRange.Contains(other.gameObject.GetComponent<CityController>().GetCityName()))
            {
                _citiesInActionRange.Add(other.gameObject.GetComponent<CityController>().GetCityName());
            }
        }
    }

    //When a city exits player's action range
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "City")
        {
            //check if the City is not already in the list, then remove it
            if (_citiesInActionRange.Contains(other.gameObject.GetComponent<CityController>().GetCityName()))
            {
                _citiesInActionRange.Remove(other.gameObject.GetComponent<CityController>().GetCityName());
            }
        }
    }

    //retrun cities in range
    public List<GameData.City> CitiesInActionRange()
    {
        return _citiesInActionRange;
    }

    public bool IsAlone()
    {
        return _isAlone;
    }

    public void SetIsAlone(bool value)
    {
        _isAlone = value;
    }

    [PunRPC]
    public void ChangePlayMode(GameData.TeamPlayMode mode)
    {
        _myPlayMode = mode;
    }


    [PunRPC]
    public void ChangePlayerStatus(GameData.City capturedCity, int questionTrue, bool isCorrectAnswer)
    {
        if (isCorrectAnswer)
        {
            //add to captured city
            _capturedCities.Add(capturedCity);

            //if captured the city alone
            if (IsAlone())
            {
                _capturedCitiesAlone.Add(capturedCity);
            }
            //captured cities with allies
            else
            {
                _capturedCitiesAlly.Add(capturedCity);
            }

            //add to answered questions
            _questionsTrue.Add(questionTrue);

        }
        else
        {
            //Nothing for now
        }
    }

    [PunRPC]
    public void ChangePlayerStatusAllies(GameData.City capturedCity, int questionTrue)
    {
        //add the city to captured with allies
        _capturedCitiesAlly.Add(capturedCity);

        //add the city to captured cities
        _capturedCities.Add(capturedCity);

        //add to answered question
        _questionsTrue.Add(questionTrue);
    }

    [PunRPC]
    public void ChangePlayerStatusInvited(GameData.TeamCountry invited, GameData.TeamCountry inviter, bool isSeigeBroken)
    {
        if (!isSeigeBroken)
        {
            if (_myTeam == inviter)
            {
                _myPlayMode = GameData.TeamPlayMode.Attacking;
                _isAlone = false;
                _allies.Add(invited);
            }

            if (_myTeam == invited)
            {
                _myPlayMode = GameData.TeamPlayMode.Attacking;
                _isAlone = false;
                _allies.Add(inviter);
            }
        }
        else
        {
            if (_myTeam == inviter || _myTeam == invited)
            {
                _myPlayMode = GameData.TeamPlayMode.Exploring;
                _isAlone = true;
                _allies.Clear();
            }
        }
    }

    //send invitation to possible alliance
    [PunRPC]
    public void InviteAlliance(GameData.TeamCountry receiver, GameData.TeamCountry sender, GameData.City city)
    {
        string msg = sender.ToString() + " wants to ally with you to attack " + city.ToString() + " Would you like to join them? ";
        //I'm the reciever! sender wants to ally with me!
        if (photonView.isMine && _myTeam == receiver)
        {
            GUIManager.Instance.PanelAllianceInvitation.ShowInvitationMessage(msg, receiver, sender, city);
        }
    }

    [PunRPC]
    public void Receiver(bool answer, GameData.TeamCountry invited, GameData.TeamCountry inviter, GameData.City city)
    {
        //if this is my player and I was the country who sent the invitation for alliance
        if (photonView.isMine && _myTeam == inviter)
        {
            //accepted
            if (answer)
            {
                GUIManager.Instance.PanelCity.HidePanel();
                GUIManager.Instance.PanelSiege.ShowPanel();
                GUIManager.Instance.PanelInfo.ShowMessage(invited + " joined you. Now the city should defend itself.");

                GUIManager.Instance.PanelSiege.SetPanelInfo(city, inviter, invited);
            }
            //rejected
            else
            {
                GUIManager.Instance.PanelInfo.ShowMessage(invited + " wouldn't like to ally with you!");
            }
        }
    }

    [PunRPC]
    public void SetRandomQuestionNumber(int randomNumber, GameData.TeamCountry invited)
    {
        Debug.Log("SetRandomQuestionNumber " + GameManager.Instance.GetMyPlayerTeam());
        if (photonView.isMine && _myTeam == invited)
        {
            GameManager.Instance.QuestionBank.SetRandomQuestionNumber(randomNumber);
        }
    }

    //TODO:
    //photon networking event manager:
    //https://doc.photonengine.com/en/pun/current/tutorials/rpcsandraiseevent

    [PunRPC]
    public void SetMyTotalPoints(float points) 
    {

        if (PhotonNetwork.player.ID == 2)
        {
            Debug.Log("Gotta!");
            
        }

      //  _myTotalPoints += points;
    }

    public int GetMyTotalPoints()
    {
        return (int)_myTotalPoints;
    }
}
