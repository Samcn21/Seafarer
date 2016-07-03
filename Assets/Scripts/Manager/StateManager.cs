using UnityEngine;
using System.Collections;
using Assets.Scripts.States;
using Assets.Scripts.Interfaces;

public class StateManager : MonoBehaviour
{
    private IStateBase activeState;
    void Start()
    {
        activeState = new BeginState(this);
    }

    void Update()
    {
        if (activeState != null){
            activeState.StateUpdate();
        }
    }
    public void SwitchState(IStateBase newState) {
        activeState = newState;
    }
}
