using UnityEngine;
using System.Collections;

public abstract class SubtitleMessage : MonoBehaviour {
	[System.Serializable]
	public struct TutorialMessagesStruct {
		//public ActiveAvatarType showAvatar;
		[TextArea(3,10)]
		public string messageLocalizedID;
		public float duration;
		public GameObject marker;
		public string englishAudioEventString;
	}
	
	public TutorialMessagesStruct[] tutorialMessages;
	protected TutorialOverlayManager tm;

	protected void Start () {
		GameObject tmGO = GameObject.FindGameObjectWithTag ("TutorialManager");
		if(tmGO != null)
			tm = tmGO.GetComponent<TutorialOverlayManager>(); 
		
		foreach (TutorialMessagesStruct messageStruct in tutorialMessages) {
			if(messageStruct.marker)
				messageStruct.marker.SetActive(false);
		}
	}

	public TutorialMessagesStruct[] GetArray(){
		return tutorialMessages;
	}

}
