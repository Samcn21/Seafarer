﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CapturePointCounter : MonoBehaviour
{
    public bool Reset
    {
        get { return _reset; }
        set
        {
            _reset = value;
            if (_reset)
            {
                _reset = false;
                _timer = 0;
            }
        }
    }

    public float Timer
    {
        get { return _timer; }
        set
        {
            _timer = value;

            if ((int)_timer == 0)
            {
                Debug.Log("TODO: Send the point calculation to each player!!! " + _timer);
                //Send The Points!
                //foreach (GameData.TeamCountry owner in _owners)
                //{
                //    SendThePoints(1, owner);
                //}
            }
        }
    }

    [SerializeField]
    private float _timer = 0;

    [SerializeField]
    private float _savedTimer = 0;

    [SerializeField]
    private float _pointCalculationTimer = 10;

    [SerializeField]
    private bool _reset = false;

    [SerializeField]
    private bool _canCount = false;

    [SerializeField]
    private List<GameData.TeamCountry> _owners;

    [SerializeField]
    private int _countOfRun = 0;

    void Start()
    {
        _pointCalculationTimer = GameManager.Instance.TimeController.PointCalculationTimer;
    }

    void Update()
    {
        if (_canCount)
            Timer += Time.deltaTime;

        if (Input.GetMouseButtonUp(0))
            SendThePoints(1, GameManager.Instance.GetMyPlayerTeam());
    }

    public void StartCounting(List<GameData.TeamCountry> owners, float pointsPerMinute)
    {
        _owners = owners;
        Reset = true;
        _canCount = (owners.Count == 0) ? false : true;

        Debug.Log("Hello: " + GetComponent<CityController>().GetCityName());
    }

    private void SendThePoints(float points, GameData.TeamCountry country)
    {
        foreach (GameObject player in GameManager.Instance.GetAllPlayers())
        {
            //Debug.Log(GameManager.Instance.GetTeamID(country));
        //_countOfRun++;
        player.GetComponent<PhotonView>().RPC("SetMyTotalPoints", PhotonPlayer.Find(2), points);
        }
    }

    [PunRPC] //class: this
    public void Tester(float points)
    {
        if (PhotonNetwork.player.ID == 2)
        { 
            Debug.Log("Yes " + points);
            //_countOfRun++;
        }
    }
}