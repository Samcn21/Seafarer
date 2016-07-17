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
            if (GameManager.Instance.GetGameStatus(GameData.GameStatus.OnlinePhoton))
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

            //check when joined a room
            if (NetworkManager.Instance.IsJoinedRoom())
                GUIManager.Instance.PanelSelectCountry.ShowPanel();

            //Hide select country
            if (NetworkManager.Instance.Instantiate.HasMyInstance())
            {
                GUIManager.Instance.PanelSelectCountry.HidePanel();
            }

            //switch to play state
            if (GUIManager.Instance.PanelReady.CanPlayNow())
            {
                GUIManager.Instance.PanelReady.HidePanel();
                StateManager.PreActiveState = GameData.GameStates.Ready;
                StateManager.SwitchState(new PlayState(StateManager));
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