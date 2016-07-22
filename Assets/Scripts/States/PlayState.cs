using UnityEngine;
using System.Collections;
using Assets.Scripts.Interfaces;

namespace Assets.Scripts.States
{
    public class PlayState : IStateBase
    {
        private StateManager StateManager;
        private GameObject[] allPlayers;

        public PlayState(StateManager managerRef)
        {
            StateManager = managerRef;
            StateManager.CurrentActiveState = GameData.GameStates.Play;
            allPlayers = GameObject.FindGameObjectsWithTag("Player");
            GameManager.Instance.allPlayers = allPlayers;
        }
        public void StateUpdate()
        {
            //    //StateManager.SwitchState(new PlayState(StateManager));
        }

        public void StateShowGUI()
        {

        }

        public void StateFixedUpdate() 
        { 
        
        }

    }
}