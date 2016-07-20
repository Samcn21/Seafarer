using UnityEngine;
using System.Collections;
using TouchScript;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameData.TeamCountry _myTeam;

    public bool _isTouchMovement { get; private set; }

    private Vector3 _nextPosition;
    private Vector2 _screenPosition;

    [SerializeField]
    private float _speed = 100;

    [SerializeField]
    private float _actionRange;

    void Start()
    {
        if (GameManager.Instance.GetGameStatus(GameData.GameStatus.UsingGPS))
        {
            //TODO a class for GPS calculation
        }
        else
        {
            _isTouchMovement = true;
            _speed = GameManager.Instance.GetPlayerSpeed();
        }

        this.GetComponent<SphereCollider>().radius = GameManager.Instance.GetPlayerActionRange();
    }
    public void ChooseMyTeam(GameData.TeamCountry team)
    {
        _myTeam = team;
    }

    public GameData.TeamCountry GetMyTeam()
    {
        return _myTeam;
    }

    //returns the questions that have been asked from this team
    public int[] GetMyQuestions(GameData.TeamCountry team) 
    {
        return new int[] { 1, 2, 5, 10 };
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
            _nextPosition = Camera.main.ScreenToWorldPoint(new Vector3(_screenPosition.x, _screenPosition.y, Camera.main.transform.position.y - this.transform.position.y));
        }
    }

    private void Update()
    {
        if (_isTouchMovement & StateManager.Instance.CurrentActiveState == GameData.GameStates.Play)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, _nextPosition, _speed * Time.deltaTime);
            this.transform.position = new Vector3(this.transform.position.x, 1, this.transform.position.z);
        }
    }
}
