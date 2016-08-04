using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GameManager : MonoBehaviour
{
    //References
    public QuestionBank QuestionBank;
    public RollDice RollDice;

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
    private float _currentCameraSize;
    [SerializeField]
    private float _minCameraSize = 45f;
    [SerializeField]
    private float _maxCameraSize = 45f;
    public Camera mainCamera;

    //Game Objects in the scene
    [SerializeField]
    private float _playerSpeed;

    [SerializeField]
    private GameData.TeamCountry _myPlayer;

    [SerializeField]
    private bool _canPlayerInteract = true;

    [SerializeField]
    private float _playerActionRange;

    [SerializeField]
    private float _cityActionRange;

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

        allCities = GameObject.FindGameObjectsWithTag("City");

        //string allnames = "";
        //foreach (GameObject city in allCities)
        //{ 
        //    allnames += city.name.ToString() + " ";
        //}

        //Debug.Log(allnames);
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

    
    public GameObject[] GetAllPlayers() 
    {
        GetComponent<PhotonView>().RPC("FindAllPlayers", PhotonTargets.All);
        return allPlayers;
    }

    [PunRPC]
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

    public float GetPlayerSpeed ()
    {
        return _playerSpeed;
    }

    public bool CanPlayerInteract() 
    {
        return _canPlayerInteract;
    }

    public void SetPlayerInteract(bool value)
    {
    //    if (value)
    //    {
    //        _canPlayerInteract = false;
    //        StartCoroutine(WaitForInteraction());
            _canPlayerInteract = value;
    //    }
    }
    //IEnumerator WaitForInteraction()
    //{
    //    Debug.Log("waiting");
    //    yield return new WaitForSeconds(2);
    //    Debug.Log("waited 2 secs");
        
    //}

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

    public float GetCityActionRange()
    { 
        //TODO: 
        //this action range can be based on area or based on screen size (cities need to be interactive and touchable)

        return _cityActionRange;
    }                                    


    void Update()
    {
        if (!_usingGPS ) 
        {
            if (GUIManager.Instance.IsAnyPanelOpen())
            {
                _canPlayerInteract = false;
            }
            else
            {
                _canPlayerInteract = true;
            }
        }

        //TODO
        //if we are in "ready to play" state we should get the gameplay status from JSON and set here
        //gameplayDurationSeconds = gameplayDuration * 60;
        //eventPeriod = eventPeriodSeconds * 60;
    }
}
