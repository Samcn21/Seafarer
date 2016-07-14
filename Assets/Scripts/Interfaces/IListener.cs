using UnityEngine;
using System.Collections;


public interface IListener
{
    void OnEvent(GameData.InternalEventType EventType, Component Sender, object Param = null);
}
