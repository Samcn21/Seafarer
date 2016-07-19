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

    //References
    public PlayerController PlayerController;

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
    private float _currentCameraSize;
    [SerializeField]
    private float _minCameraSize = 45f;
    [SerializeField]
    private float _maxCameraSize = 45f;
    public Camera mainCamera;

    //Game Objects in the scene
    [SerializeField]
    private float _playerSpeed;
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
            mainCamera.orthographicSize = _minCameraSize;
            _currentCameraSize = _minCameraSize;
        }
        else 
        {
            mainCamera.orthographicSize = _maxCameraSize;
            _currentCameraSize = _maxCameraSize;
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

    public float GetPlayerSpeed ()
    {
        return _playerSpeed;
    }

    void Update()
    {
        //TODO
        //if we are in "ready to play" state we should get the gameplay status from JSON and set here
        //gameplayDurationSeconds = gameplayDuration * 60;
        //eventPeriod = eventPeriodSeconds * 60;
    }
}
