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

	void Start () 
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

        }
        else
        { 
            //TODO: add to forbiden country dictionary for a few minutes.

            //change defence status
            _defenceStatus = defenceStatus;
        }

    }
}
