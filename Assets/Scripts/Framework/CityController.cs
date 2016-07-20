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
}
