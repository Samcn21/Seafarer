using UnityEngine;
using System.Collections;
using System;

public class TimeController : Photon.MonoBehaviour
{
    //public float GameplayDurationSec
    //{
    //    get { return _gameplayDuration; }
    //    set
    //    {
    //        if (_gameplayDuration <= 0)
    //        {
    //            GameOver();
    //            return;
    //        }
    //        if (_gameplayDuration <= 60)
    //        {
    //            //OnAlertTimer();
    //            return;
    //        }
    //    }
    //}

    [Range(1, 60)]
    [SerializeField]
    private float _gameplayDuration = 20;
    [SerializeField]
    private float _gameplayDurationSec;

    [SerializeField]
    private bool _hasGameEvents = true;

    [SerializeField]
    private float _gameEventPeriod = 5;
    private float _gameEventPeriodSec;

    [SerializeField]
    private float _timeSyncPeriod = 10;

    [SerializeField]
    private float _timeCounter;
    void Start()
    {
        //_gameplayDuration = 0.06f;  //3 seconds
        _timeCounter = _timeSyncPeriod;
        _gameplayDurationSec = _gameplayDuration * 60;
        _gameEventPeriodSec = _gameEventPeriod * 60;
    }

    void Update()
    {
        if (StateManager.Instance.CurrentActiveState == GameData.GameStates.Play)
        {
            if (PhotonNetwork.isMasterClient)
            {
                //timer countdown
                _gameplayDurationSec -= Time.deltaTime;

                //timer sync, every 10 second sends the time to the other clients to sync time if they have delay
                _timeCounter -= Time.deltaTime;
                if (_timeCounter <= 0)
                {
                    _timeCounter = _timeSyncPeriod;
                    GetComponent<PhotonView>().RPC("SetTimes", PhotonTargets.Others, _gameplayDurationSec);
                }
            }
            else
            {
                _gameplayDurationSec = GetGamePlayDuration(); //get master's time every 10 seconds
                _gameplayDurationSec -= Time.deltaTime;
            }

            if (_gameplayDurationSec <= 0) 
            {
                _gameplayDurationSec = 0;
                GameOver();
            }
        }
    }

    [PunRPC]
    public void SetTimes(float gameplayDuration)
    {
        _gameplayDurationSec = gameplayDuration;
    }

    public float GetGamePlayDuration()
    {
        return _gameplayDurationSec;
    }

    public string GetRemainTime()
    {
        TimeSpan time = TimeSpan.FromSeconds(_gameplayDurationSec);
        return string.Format("{1:D2}:{2:D2}", time.Hours, time.Minutes, time.Seconds);
    }

    private void GameOver()
    {
        //set the state machine on winning condition
        //the rest on winning condition
        //TODO: find the winner
        Debug.Log("THE END");
    }
}
