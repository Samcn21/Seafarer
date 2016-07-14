using UnityEngine;
using System.Collections;
using Assets.Scripts.States;
using Assets.Scripts.Interfaces;

public class StateManager : MonoBehaviour
{
    private IStateBase activeState;
    private static StateManager instance = null;
    public static StateManager Instance
    {
        get
        {
            return instance;
        }
    }

    public GameData.GameStates CurrentActiveState;
    public GameData.GameStates PreActiveState;

    void Awake() 
    {
        if (instance)
        {
            DestroyImmediate(this);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject.transform.parent);
    }
    void Start()
    {
        activeState = new BeginState(this);
    }

    void Update()
    {
        if (activeState != null)
        {
            activeState.StateUpdate();
        }
    }

    void OnGUI()
    {
        if (activeState != null)
        {
            activeState.StateShowGUI();
        }
    }

    public void StateFixedUpdate()
    {
        if (activeState != null)
        {
            activeState.StateShowGUI();
        }
    }

    public void SwitchState(IStateBase newState) {
        activeState = newState;
    }
}
