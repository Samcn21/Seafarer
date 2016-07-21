using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TouchScript;

[System.Serializable]
public class PlayerController : MonoBehaviour
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
    private int[] _questionsTotal;

    [SerializeField]
    private int[] _questionsTrue;

    [SerializeField]
    private List<GameData.TeamCountry> _playersInActionRange = new List<GameData.TeamCountry>();

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

        this.GetComponent<SphereCollider>().radius = GameManager.Instance.GetCityActionRange();

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
    public int[] GetTotalQuestions(GameData.TeamCountry team)
    {
        return _questionsTotal;
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
            _screenPosition = point.Position;
            _nextPosition = Camera.main.ScreenToWorldPoint(new Vector3(_screenPosition.x, _screenPosition.y, Camera.main.transform.position.y - this.transform.position.y));
        }
    }

    private void Update()
    {
        if (_isTouchMovement & StateManager.Instance.CurrentActiveState == GameData.GameStates.Play)
        {
            if (Vector3.Distance(_nextPosition, transform.position) < 50 && GameManager.Instance.CanPlayerInteract())//&& this.GetComponent<InputController>().CanPlayerMove())
            {
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
        //is a player    
        else
        {
            //check if the player is not already in the list, then add it.
            if (!_playersInActionRange.Contains(other.gameObject.GetComponent<PlayerController>().GetMyTeam()))
            {
                _playersInActionRange.Add(other.gameObject.GetComponent<PlayerController>().GetMyTeam());
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
        //is a player
        else
        {
            //check if the player is not already in the list, then remove it.
            if (_playersInActionRange.Contains(other.gameObject.GetComponent<PlayerController>().GetMyTeam()))
            {
                _playersInActionRange.Remove(other.gameObject.GetComponent<PlayerController>().GetMyTeam());
            }
        }
    }

    //retrun cities in range
    public List<GameData.City> CitiesInActionRange() 
    {
        return _citiesInActionRange;
    }

    //retrun players in range
    public List<GameData.TeamCountry> PlayersInActionRange()
    {
        return _playersInActionRange;
    }
}
