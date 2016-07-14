﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    //reference to other classes
    public StateManager StateManager;
    public GUIManager GUIManager;

    //Technical options
    public bool usingGPS = false;
    public bool connectingAdminPanel = false;
    public bool isPhotonOnline = true;

    //Gameplay Status
    public string pinCode = "1234";

    [SerializeField]
    private int maxTeams = 8;

    [Range(1, 4)]
    [SerializeField]
    private int minTeams = 4;

    [Range(1, 60)]
    [SerializeField]
    private float gameplayDuration = 20;
    private float gameplayDurationSeconds = 1200;

    [SerializeField]
    private bool hasEvents = true;

    [SerializeField]
    private float eventPeriod = 5;
    private float eventPeriodSeconds = 300;

    void Awake()
    {
        if (instance)
        {
            DestroyImmediate(this);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject.transform.parent);

        if (!isPhotonOnline)
        {
            minTeams = 1;
        }
    }

    void Update()
    {
        //TODO
        //if we are in "ready to play" state we should get the gameplay status from JSON and set here
        //gameplayDurationSeconds = gameplayDuration * 60;
        //eventPeriod = eventPeriodSeconds * 60;
    }
}