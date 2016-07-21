using UnityEngine;
using System.Collections;
using TouchScript;

public class InputController : MonoBehaviour
{
    private Vector3 _clickedPosition;
    private Vector2 _screenPosition;

    [SerializeField]
    private bool _canPlayerMove = true;

    void Start() 
    {
        _canPlayerMove = true;
    }
    private void OnEnable()
    {
        if (TouchManager.Instance != null)
        {
            TouchManager.Instance.TouchesBegan += touchesBeganHandler;
        }
    }

    private void OnDisable()
    {
        if (TouchManager.Instance != null)
        {
            TouchManager.Instance.TouchesBegan -= touchesBeganHandler;
        }
    }

    private void touchesBeganHandler(object sender, TouchEventArgs e)
    {
        foreach (var point in e.Touches)
        {
            _screenPosition = point.Position;
            _clickedPosition = new Vector3(_screenPosition.x, _screenPosition.y, Camera.main.transform.position.y - this.transform.position.y);
            
            Ray ray = Camera.main.ScreenPointToRay(_clickedPosition);
            Transform _hitTransform = GetHitTransformInfo(ray);
            
            if (_hitTransform != null)
            {
                if (_hitTransform.tag == "City")
                {
                    //check if the hit city is in player's action range
                    if (this.GetComponent<PlayerController>().CitiesInActionRange().Contains(_hitTransform.gameObject.GetComponent<CityController>().GetCityName()))
                    {
                        GUIManager.Instance.PanelCity.OpenPanel(_hitTransform.gameObject.GetComponent<CityController>().GetCityName(), this.GetComponent<PlayerController>().GetMyTeam());
                    }
                }
                //else
                //{
                //    _canPlayerMove = true;
                //}
            }
        }
    }

    private Transform GetHitTransformInfo(Ray ray) 
    {
        RaycastHit[] hitList = Physics.RaycastAll(ray);

        Transform target = null;
        foreach (RaycastHit hit in hitList) 
        {
            //now we don't wanna see any properties for ourself, but if we click on ourselves it could  show our scores and
            // what we achieved so far but now doesn't matter
            if (hit.collider.name != this.transform.name)
            {
                return hit.transform;
            }
        }
        return target;
    }

    void Update()
    {
        //Seeing the raycast
        Debug.DrawLine(Camera.main.transform.position, Camera.main.ScreenToWorldPoint(_clickedPosition), Color.red);
    }

    public bool CanPlayerMove()
    {
        return _canPlayerMove;
    }
}
