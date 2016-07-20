using UnityEngine;
using System.Collections;

public class CityController : MonoBehaviour 
{

    [SerializeField]
    private float _cityActionRange;
	void Start () 
    {
        this.GetComponent<SphereCollider>().radius = GameManager.Instance.GetCityActionRange();
	}
}
