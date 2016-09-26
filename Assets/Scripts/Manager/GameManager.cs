using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GameManager : MonoBehaviour
{
    //References
    public QuestionBank QuestionBank;
    public TimeController TimeController;
    public DiceController DiceController;

    private static GameManager _instance = null;
    public static GameManager Instance { get { return _instance; } }

    //Technical options
    [SerializeField]
    private bool _usingGPS = false;
    [SerializeField]
    private bool _connectingAdminPanel = false;
    [SerializeField]
    private bool _onlinePhoton = true;
    [SerializeField]
    private bool _hasPinCodePanel = true;




    //Gameplay Status
    [SerializeField]
    private string _pinCode = "1234";

    [SerializeField]
    private byte _maxTeams = 8;

    [Range(1, 4)]
    [SerializeField]
    private byte _minTeams = 4;



    //Screen Measurements
    public Camera mainCamera;
    [SerializeField]
    private bool _fogOfWar = true;
    [SerializeField]
    private int _screenHight;
    [SerializeField]
    private int _screenWidth;
    [SerializeField]
    private float _aspectRatio;
    private float _currentCameraSize;
    [SerializeField]
    private float _minCameraSize = 45f;
    [SerializeField]
    private float _maxCameraSize = 60f;

    //Game Objects in the scene
    [SerializeField]
    private GameData.TeamCountry _myPlayer;
    [SerializeField]
    private float _playerSpeed;
    [SerializeField]
    private float _playerActionRange;
    [SerializeField]
    private float _playerFOWRadius;
    private bool _canPlayerInteract = true;
    public GameObject[] allPlayers;
    public Dictionary<GameData.TeamCountry, int> TeamsIdNames = new Dictionary<GameData.TeamCountry, int>();

    [SerializeField]
    private float _cityActionRange;
    public GameObject[] allCities;


    private float timer;
    void Awake()
    {

        if (_instance)
        {
            DestroyImmediate(this);
            return;
        }
        _instance = this;
        //DontDestroyOnLoad(gameObject.transform.parent);

        if (!_onlinePhoton)
        {
            _minTeams = 1;
        }
    }

    void Start()
    {
        _screenHight = Screen.height;
        _screenWidth = Screen.width;
        _aspectRatio = (float)Screen.height / Screen.width;

        if (_aspectRatio < 0.7f)
        {
            mainCamera.orthographicSize = _minCameraSize;
            _currentCameraSize = _minCameraSize;
        }
        else
        {
            mainCamera.orthographicSize = _maxCameraSize;
            _currentCameraSize = _maxCameraSize;
        }
        allCities = GameObject.FindGameObjectsWithTag("City");

        //fow is 5 times bigger than player action range
        _playerFOWRadius = _playerActionRange * 5;

        //Do we have fog of war?
        Camera.main.GetComponent<FogOfWar>().enabled = (_fogOfWar) ? true : false;
    }

    void Update()
    {
        //not using GPS means we can move the avatars with mouse / touch
        if (!_usingGPS)
            _canPlayerInteract = (GUIManager.Instance.IsAnyPanelOpen()) ? false : true;


        if (Input.anyKeyDown)
        {

            foreach (KeyValuePair<GameData.TeamCountry, int> pair in TeamsIdNames)
            {
                Debug.Log(pair.Key + " - " + pair.Value);
            }
        }
    }

    public bool GetGameStatus(GameData.GameStatus status)
    {
        switch (status)
        {
            case GameData.GameStatus.UsingGPS:
                return _usingGPS;

            case GameData.GameStatus.ConnectingAdminPanel:
                return _connectingAdminPanel;

            case GameData.GameStatus.OnlinePhoton:
                return _onlinePhoton;

            case GameData.GameStatus.HasPinCodePanel:
                return _hasPinCodePanel;

            default:
                return false;
        }
    }

    [PunRPC] //class: instantiate
    public void SetTeamsIdName(GameData.TeamCountry countryValue, int IdKey)
    {
        //if the country is already joined 
        if (TeamsIdNames.ContainsKey(countryValue))
        {
            //the player joined the map and disconnected but came back again so we remove their pre-id and add a new one
            TeamsIdNames.Remove(countryValue);
            TeamsIdNames.Add(countryValue, IdKey);
        }
        //it's the first time the team joining the game
        else
        {
            TeamsIdNames.Add(countryValue, IdKey);
        }
    }

    public int GetTeamID( GameData.TeamCountry country) 
    {
        foreach (KeyValuePair<GameData.TeamCountry, int> pair in TeamsIdNames)
        {
            if (pair.Key == country)
                return pair.Value;
        }
        return 0;
    }

    public GameObject[] GetAllPlayers()
    {
        GetComponent<PhotonView>().RPC("FindAllPlayers", PhotonTargets.All);
        return allPlayers;
    }

    [PunRPC] //class: this
    public void FindAllPlayers()
    {
        allPlayers = GameObject.FindGameObjectsWithTag("Player");
    }

    public string GetGamePinCode()
    {
        return _pinCode.ToString();
    }

    public byte GetMaximumTeams()
    {
        return _maxTeams;
    }

    public byte GetMinimumTeams()
    {
        return _minTeams;
    }

    public float GetCurrentCameraSize()
    {
        return _currentCameraSize;
    }

    public float GetPlayerSpeed()
    {
        return _playerSpeed;
    }

    public bool CanPlayerInteract()
    {
        return _canPlayerInteract;
    }

    public void SetPlayerInteract(bool value)
    {
        _canPlayerInteract = value;
    }
    public GameData.TeamCountry GetMyPlayerTeam()
    {
        return _myPlayer;
    }

    public PlayerController GetMyPlayerController()
    {
        foreach (GameObject player in GetAllPlayers())
        {
            if (player.GetComponent<PlayerController>().GetMyTeam() == GetMyPlayerTeam())
                return player.GetComponent<PlayerController>();
        }
        return null;
    }

    public void SetMyPlayer(GameData.TeamCountry value)
    {
        _myPlayer = value;
    }

    public float GetPlayerActionRange()
    {
        //TODO:
        //this action range must be based on calculation of physical world then convert to 
        //player's sphiere collider radius
        return _playerActionRange;
    }

    public float GetPlayerFOWRadius()
    {
        return _playerFOWRadius;
    }

    public float GetCityActionRange()
    {
        //TODO: 
        //this action range can be based on area or based on screen size (cities need to be interactive and touchable)
        return _cityActionRange;
    }



}
