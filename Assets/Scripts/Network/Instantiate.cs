using UnityEngine;
using System.Collections;

public class Instantiate : MonoBehaviour {

    [SerializeField]
    private GameObject[] _allRespawnSpots;

    private bool _isThereInstance = false;

    [SerializeField]
    private int _countOfInstances = 0;

    void Start() 
    {
        //finding respawn spots. only if not using gps
        if (!GameManager.Instance.GetGameStatus(GameData.GameStatus.UsingGPS))
        {
            _allRespawnSpots = GameObject.FindGameObjectsWithTag("Respawn");
        }

        //Debug.Log(PhotonNetwork.playerName + " : " + PhotonNetwork.player.name + " : " + PhotonNetwork.player.ID + " : " + PhotonNetwork.player.isLocal);
        //PhotonNetwork.playerName will return the name of the local player.
        //PhotonPlayer.name will also return the name of the local player.
        //PhotonNetwork.player.name will also return the name of the local player.
        //PhotonNetwork.player.ID will return the ID of the local player.
        //PhotonNetwork.player.isLocal will return if the player is the local player.
    }

    public void InstantiateMe(GameData.TeamCountry chosenCountry)
    {
        if (!_isThereInstance)
        {
            GameObject mySpawnSpot = _allRespawnSpots[Random.Range(0, _allRespawnSpots.Length)];
            GameObject myPlayer = (GameObject)PhotonNetwork.Instantiate(chosenCountry.ToString(), mySpawnSpot.gameObject.transform.position, Quaternion.identity, 0);
            PhotonNetwork.playerName = chosenCountry.ToString();
            myPlayer.GetComponent<PhotonView>().RPC("SetMyTeamID", PhotonTargets.All, PhotonNetwork.player.ID);
            GameManager.Instance.GetComponent<PhotonView>().RPC("SetTeamsIdName", PhotonTargets.AllBufferedViaServer, chosenCountry, PhotonNetwork.player.ID);
            ((MonoBehaviour)myPlayer.GetComponent("PlayerController")).enabled = true;
            ((MonoBehaviour)myPlayer.GetComponent("InputController")).enabled = true;
            ((MonoBehaviour)myPlayer.GetComponent("FogOfWarUnit")).enabled = true;


            //if we use camera zoom in/out or 3D
            //myPlayer.transform.FindChild("Main Camera").gameObject.SetActive(true);

            //we can also calculate by chosen countries in panel select country or number of players in the scene through game manager
            _countOfInstances++;
            _isThereInstance = true;
        }
        else
        {
            return;
        }
    }

    public bool HasMyInstance() 
    {
        return (_isThereInstance) ? true : false;
    }

    public int CountOfInstances() 
    {
        return _countOfInstances;
    }
}
