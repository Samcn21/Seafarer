using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    //Technical options
    public bool usingGPS = false;
    public bool connectingAdminPanel = false;
    public bool isPhotonOnline = true;

    //Gameplay Status
    public string pinCode = "1234";

    [SerializeField]
    private int maxTeams = 8;

    [Range(1, 4)]
    [SerializeField]
    private int minTeams = 4;

    [Range(1, 60)]
    [SerializeField]
    private float gameplayDuration = 20;
    private float gameplayDurationSeconds = 1200;

    [SerializeField]
    private bool hasEvents = true;

    [SerializeField]
    private float eventPeriod = 5;
    private float eventPeriodSeconds = 300;

    //Screen Measurements
    [SerializeField]
    private int scrHight;
    [SerializeField]
    private int scrWidth;
    [SerializeField]
    private float aspectRatio;
    public Camera mainCamera;

    //Game Objects in the scene
    public GameObject[] allPlayers;
    public GameObject[] allCities;

    void Awake()
    {
        if (instance)
        {
            DestroyImmediate(this);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject.transform.parent);

        if (!isPhotonOnline)
        {
            minTeams = 1;
        }
    }

    void Start() 
    {
        scrHight = Screen.height;
        scrWidth = Screen.width;
        aspectRatio = (float)Screen.height / Screen.width;

        if (aspectRatio < 0.7f)
        {
            mainCamera.orthographicSize = 11.2f;
        }
        else 
        {
            mainCamera.orthographicSize = 15f;
        }
    }

    void Update()
    {
        //TODO
        //if we are in "ready to play" state we should get the gameplay status from JSON and set here
        //gameplayDurationSeconds = gameplayDuration * 60;
        //eventPeriod = eventPeriodSeconds * 60;
    }
}
