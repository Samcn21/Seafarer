using UnityEngine;
using System.Collections;
using TouchScript;

public class InputController : MonoBehaviour
{
    public Vector3 _clickedPosition;
    public Vector2 _screenPosition;

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
            //    //We clicked on something either city or player
            //    //TODO: 
            //    //We need to do something
                if (_hitTransform.tag == "City") // and the city is in acceptable radius
                {
                    GUIManager.Instance.PanelCity.OpenPanel(_hitTransform.name);
                    //GUIManager.Instance.PanelCity.Op
                    //GUIManager.Instance.PanelCity.
                }
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
}
