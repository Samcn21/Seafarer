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
    }

    public void InstantiateMe(GameData.TeamCountry chosenCountry)
    {
        if (!_isThereInstance)
        {
            GameObject mySpawnSpot = _allRespawnSpots[Random.Range(0, _allRespawnSpots.Length)];
            GameObject myPlayer = (GameObject)PhotonNetwork.Instantiate(chosenCountry.ToString(), mySpawnSpot.gameObject.transform.position, Quaternion.identity, 0);
            ((MonoBehaviour)myPlayer.GetComponent("PlayerController")).enabled = true;
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
        if (_isThereInstance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int CountOfInstances() 
    {
        return _countOfInstances;
    }
}
