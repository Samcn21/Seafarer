using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CityController : MonoBehaviour
{
    [SerializeField]
    private GameData.City _cityName;

    [SerializeField]
    private GameData.CitySet _citySet;

    [SerializeField]
    private GameData.Continent _continent;

    [SerializeField]
    private bool _isPlayerCapital;

    [SerializeField]
    private GameData.CityStatus _cityStatus;

    //a city without owner means the city is Neutral 
    [SerializeField]
    private List<GameData.TeamCountry> _cityOwners = new List<GameData.TeamCountry>();

    [SerializeField]
    private int _defence;

    [SerializeField]
    private GameData.DefenceStatus _defenceStatus;

    [SerializeField]
    private float _pointsPerMinute = 2;

    [SerializeField]
    private List<GameData.TeamCountry> _playersInActionRange = new List<GameData.TeamCountry>();

    void Start()
    {
        _defence = 1;

        //make city's collider with ratio of map size
        this.GetComponent<SphereCollider>().radius = GameManager.Instance.GetCityActionRange();


        //check if this city is one of the players' capital
        if (GameData.TeamCC.ContainsValue(_cityName))
        {
            _isPlayerCapital = true;
        }
        else
        {
            _isPlayerCapital = false;
        }

    }

    public GameData.City GetCityName()
    {
        return _cityName;
    }

    public int GetCityDefence()
    {
        return _defence;
    }

    public GameData.CityStatus GetCityStatus()
    {
        return _cityStatus;
    }

    public List<GameData.TeamCountry> GetCityOwners()
    {
        return _cityOwners;
    }

    public GameData.DefenceStatus GetCityDefenceStatus()
    {
        return _defenceStatus;
    }

    //retrun players in range
    public List<GameData.TeamCountry> GetPlayersInActionRange()
    {
        return _playersInActionRange;
    }

    //When a player enters city's action range
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!_cityOwners.Contains(other.GetComponent<PlayerController>().GetMyTeam()))
            {
                //check if the player is not already in the list, then add it.
                if (!_playersInActionRange.Contains(other.gameObject.GetComponent<PlayerController>().GetMyTeam()))
                {
                    this.GetComponent<PhotonView>().RPC("SetPlayersInActionRange", PhotonTargets.All, other.gameObject.GetComponent<PlayerController>().GetMyTeam(), true);
                }
            }
        }
    }

    //When a player exits city's action range
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!_cityOwners.Contains(other.GetComponent<PlayerController>().GetMyTeam()))
            {
                //check if the player is not already in the list, then remove it.
                if (_playersInActionRange.Contains(other.gameObject.GetComponent<PlayerController>().GetMyTeam()))
                {
                    this.GetComponent<PhotonView>().RPC("SetPlayersInActionRange", PhotonTargets.All, other.gameObject.GetComponent<PlayerController>().GetMyTeam(), false);
                }
            }
        }
    }

    //set action range in the network
    [PunRPC]
    public void SetPlayersInActionRange(GameData.TeamCountry value, bool isAdding)
    {
        if (isAdding)
        {
            if (!_playersInActionRange.Contains(value))
                _playersInActionRange.Add(value);
        }
        else
        {
            if (_playersInActionRange.Contains(value))
                _playersInActionRange.Remove(value);
        }
    }

    [PunRPC]
    public void SetCityStatus(GameData.DefenceStatus value)
    {
        _defenceStatus = value;
    }

    [PunRPC]
    public void ChangeCityStatus(GameData.TeamCountry cityOwner, GameData.CityStatus cityStatus, GameData.DefenceStatus defenceStatus, bool isCorrectAnswer)
    {
        //if player answered to a question correctly
        if (isCorrectAnswer)
        {
            //add the owners
            _cityOwners.Add(cityOwner);

            //TODO: Icannot send List<CityOwners> through PunRPC in question panel so I need to check players allies here then add the alies here

            //not neutral anymore (occupied by one or allies contries)
            _cityStatus = cityStatus;

            //add to defence based on number of owners
            //_defence = (cityOwners.Count > 1) ? 3 : 2;
            _defence = 2;

            //change defence status
            _defenceStatus = defenceStatus;

            //remove from city action range
            if (_playersInActionRange.Contains(cityOwner))
            {
                _playersInActionRange.Remove(cityOwner);
            }

        }
        else
        {
            //TODO: add to forbiden country dictionary for a few minutes.

            //change defence status
            _defenceStatus = defenceStatus;
        }

    }
}
