using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

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

    [Range(1, 60)]
    [SerializeField]
    private float _gameplayDuration = 20;
    private float _gameplayDurationSeconds = 1200;

    [SerializeField]
    private bool _hasGameEvents = true;

    [SerializeField]
    private float _gameEventPeriod = 5;
    private float _gameEventPeriodSeconds = 300;

    //Screen Measurements
    [SerializeField]
    private int _screenHight;
    [SerializeField]
    private int _screenWidth;
    [SerializeField]
    private float _aspectRatio;
    public Camera mainCamera;

    //Game Objects in the scene
    public GameObject[] allPlayers;
    public GameObject[] allCities;

    void Awake()
    {
        
        if (_instance)
        {
            DestroyImmediate(this);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject.transform.parent);

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
            mainCamera.orthographicSize = 11.2f;
        }
        else 
        {
            mainCamera.orthographicSize = 15f;
        }

    }

    public bool GameStatus(GameData.GameStatus status)
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

    public string GamePinCode()
    {
        return _pinCode.ToString();
    }

    public byte MaximumTeams() 
    {
        return _maxTeams;
    }
    void Update()
    {
        //TODO
        //if we are in "ready to play" state we should get the gameplay status from JSON and set here
        //gameplayDurationSeconds = gameplayDuration * 60;
        //eventPeriod = eventPeriodSeconds * 60;
    }
}
