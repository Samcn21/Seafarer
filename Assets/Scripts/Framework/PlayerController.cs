using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TouchScript;

[System.Serializable]
public class PlayerController : Photon.MonoBehaviour
{
    [SerializeField]
    private GameData.TeamCountry _myTeam;

    [SerializeField]
    private GameData.TeamPlayMode _myPlayMode = GameData.TeamPlayMode.Exploring;

    [SerializeField]
    private int _myTotalPoints = 0;

    [SerializeField]
    private bool _isAlone;

    [SerializeField]
    private List<GameData.TeamCountry> _allies;

    [SerializeField]
    private List<GameData.City> _capturedCitiesAlone;

    [SerializeField]
    private List<GameData.City> _capturedCitiesAlly;

    [SerializeField]
    private List<GameData.City> _capturedCitie;

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

    void Start()
    {
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
        this.GetComponent<SphereCollider>().radius = GameManager.Instance.GetCityActionRange();

        //in the beginning the country is alone:
        _isAlone = true;

        //Debug.Log(FindCurrentAlly());
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

    //returns the questions that have been asked from this team
    public List<int> GetTotalQuestions(GameData.TeamCountry team)
    {
        return _questionsTotal;
    }

    public void AddToTotalQuestions(int questionNumber)
    {
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

    private void Update()
    {
        if (_isTouchMovement & StateManager.Instance.CurrentActiveState == GameData.GameStates.Play)
        {
            if ( GameManager.Instance.CanPlayerInteract())
            {
                //Debug.Log( GameManager.Instance.CanPlayerInteract());
                this.transform.position = Vector3.Lerp(this.transform.position, _nextPosition, _speed * Time.deltaTime);
                this.transform.position = new Vector3(this.transform.position.x, 1, this.transform.position.z);
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

    [PunRPC]
    public void ChangePlayMode(  GameData.TeamPlayMode mode) 
    {
        _myPlayMode = mode;
    }


    [PunRPC]
    public void ChangePlayerStatus(GameData.City capturedCity, int questionTrue, bool isCorrectAnswer) 
    {
        if (isCorrectAnswer)
        {
            //add to captured city
            _capturedCitie.Add(capturedCity);

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

    //send invitation to possible alliance
    [PunRPC]
    public void InviteAlliance(GameData.TeamCountry receiver, GameData.TeamCountry sender, GameData.City city)
    {
        string msg = sender.ToString() + " wants to ally with you to attack " + city.ToString() + " Would you like to ally with them? ";
        //I'm the reciever! sender wants to ally with me!
        if (photonView.isMine && _myTeam == receiver)
        {
                Debug.Log(_myTeam + " - " + receiver);
                GUIManager.Instance.PanelAllianceInvitation.ShowInvitationMessage(msg);
        }
    }
}
