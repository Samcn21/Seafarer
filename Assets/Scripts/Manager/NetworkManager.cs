using UnityEngine;
using System.Collections;

public class NetworkManager : Photon.MonoBehaviour
{

    private static NetworkManager _instance = null;
    public static NetworkManager Instance
    {
        get
        {
            return _instance;
        }
    }

    [SerializeField]
    private float retryPeriod = 15; // try every 15 seconds for connection

    [SerializeField]
    private string _version = "SeafarerPrototype 1.";
    private string _buildVersion = "";
    private bool _connectInUpdate = false;

    //[SerializeField]
    private bool _joinedRoom = false;

    public InternetConnection InternetConnection;
    public Instantiate Instantiate;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }
    
    public virtual void Start()
    {
        //PhotonNetwork.autoJoinLobby = false;
        _buildVersion = _version + SceneManagerHelper.ActiveSceneBuildIndex;
    }

    public virtual void Update()
    {
        if (_connectInUpdate && !PhotonNetwork.connected)
        {
            _connectInUpdate = false;
            PhotonNetwork.ConnectUsingSettings(_buildVersion);
        }

        //TODO: must check once a while //Also can check with android plugin
        /*
        if (InternetConnection.IsInternetConnected())
        {
            Debug.Log(InternetConnection.GetHtmlFromUrl("http://google.com").ToString());
        }
        else
        {
            Debug.Log("NotConnected");
        }
         */
    }

    public virtual void OnGUI() 
    {
        if (PhotonNetwork.connectionStateDetailed.ToString() == "Joined")
        {
            GUIManager.Instance.PanelConnectionStatus.networkStatus.text = "Network Status: " + PhotonNetwork.connectionStateDetailed.ToString() + " to room " + GameManager.Instance.GetGamePinCode();
        }
        else
        {
            GUIManager.Instance.PanelConnectionStatus.networkStatus.text = "Network Status: " + PhotonNetwork.connectionStateDetailed.ToString();
        }

        GUIManager.Instance.PanelConnectionStatus.networkConStatus.text = "Network Connection: " + PhotonNetwork.connectionState.ToString();
    }
    public void OfflineMode()
    {
        PhotonNetwork.offlineMode = true;
    }
    public void Connect()
    {
        _connectInUpdate = true;
    }

    public string ConnectionStatus()
    {
        return PhotonNetwork.connectionStateDetailed.ToString();
    }

    //This method even if the network is in connecting mode return false
    public bool IsConnected()
    {
        if (PhotonNetwork.connectionState.ToString() == "Disconnected")
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public virtual void OnConnectedToMaster()
    {
        //Debug.Log("OnConnectedToMaster: Now we are connected, try to join the room number (pin code): " + GameManager.Instance.GamePinCode());
        PhotonNetwork.JoinRoom(GameManager.Instance.GetGamePinCode());
    }

    public virtual void OnPhotonJoinRoomFailed()
    {
        //Debug.Log("OnPhotonJoinRoomFailed: Means there is no room called: " + GameManager.Instance.GamePinCode() + " Trying to create and join it");
        PhotonNetwork.CreateRoom(GameManager.Instance.GetGamePinCode(), new RoomOptions() { maxPlayers = GameManager.Instance.GetMaximumTeams() }, null);
    }

    public virtual void OnFailedToConnectToPhoton(DisconnectCause cause)
    {
        Debug.LogError("Cause: " + cause);
    }

    public void OnJoinedRoom()
    {
        _joinedRoom = true;
    }

    public bool IsJoinedRoom()
    {
        if (_joinedRoom)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //check if minimum number of players satisfies the game starting
    public bool IsMinimumTeamsValid()
    {
        if (GameManager.Instance.GetMinimumTeams() <= NetworkManager.Instance.Instantiate.CountOfInstances())
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
