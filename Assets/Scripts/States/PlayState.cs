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
            

            allPlayers = GameObject.FindGameObjectsWithTag("Player");
            GameManager.Instance.allPlayers = allPlayers;
        }
        public void StateUpdate()
        {
           /* switch (StateManager.testingMode)
            {
                case GameData.TestingMode.NoTesting:
                    //TODO:
                    // I should make a class that checks internet connection and returns true/false
                    // the internet connection check must check the connection every 20 seconds
                    // there must be another class to check GPS every 30-60 seconds
                    //StateManager.SwitchState(new EnterPinCode(StateManager));
                    break;
                case GameData.TestingMode.TestingOfflinePhoton:
                    //StateManager.SwitchState(new Play2vs2(StateManager));
                    break;
                case GameData.TestingMode.TestingOnlinePhoton:
                    //StateManager.SwitchState(new ColorAssignFFA(StateManager));
                    break;
            }
            //if (Input.GetKeyUp(KeyCode.Space))
            //{
            //    //StateManager.SwitchState(new PlayState(StateManager));
            //}*/
        }
        public void StateShowGUI()
        {

        }

        public void StateFixedUpdate() 
        { 
        
        }

    }
}