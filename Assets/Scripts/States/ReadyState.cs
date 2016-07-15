using UnityEngine;
using System.Collections;
using Assets.Scripts.Interfaces;
using UnityEngine.UI;

namespace Assets.Scripts.States
{
    public class ReadyState : IStateBase
    {
        private StateManager StateManager;

        public ReadyState(StateManager managerRef)
        {
            StateManager = managerRef;
            StateManager.CurrentActiveState = GameData.GameStates.Ready;

        }

        public void StateUpdate()
        {
            if (GameManager.Instance.GameStatus(GameData.GameStatus.OnlinePhoton))
            {
                if (!NetworkManager.Instance.IsConnected())
                {
                    NetworkManager.Instance.Connect();
                }
            }
            else
            {
                NetworkManager.Instance.OfflineMode();
            }
        }

        public void StateShowGUI()
        {

        }

        public void StateFixedUpdate()
        {

        }

    }
}