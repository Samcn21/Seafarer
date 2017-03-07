using UnityEngine;
using System.Collections;

public interface ITutorialEvents{
	void UserDidEnterTutorialSequence (SubtitleMessage.TutorialMessagesStruct[] trigger);
	void UserDidExitTutorialSequence (SubtitleMessage.TutorialMessagesStruct[] trigger, bool removeTutorialOnExit);
}
