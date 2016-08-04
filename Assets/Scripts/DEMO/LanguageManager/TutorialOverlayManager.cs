using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class TutorialOverlayManager : MonoBehaviour{//, ITutorialEvents {
    //GameObject noAvatarGO;
    //GameObject avatar1GO;
    //GameObject avatar2GO;

    //AudioManager am;
    //// Use this for initialization
    ////void Start () {
    ////    //PlayerPrefs.DeleteAll();
    ////    noAvatarGO = transform.parent.Find("NoAvatar").gameObject;
    ////    avatar1GO = transform.parent.Find("Avatar1").gameObject;
    ////    avatar2GO = transform.parent.Find("Avatar2").gameObject;
		

    ////    GameObject ac = GameObject.FindGameObjectWithTag ("AudioController");
    ////    if (ac != null)
    ////        am = ac.GetComponent<AudioManager> ();

    ////    StopTutorialSequence ();
    ////}
	
    //// Update is called once per frame
    //void Update () {
		
    //}

    //#region Did hit trigger interface
    //public void UserPressedSomthing(SubtitleMessage.TutorialMessagesStruct[] trigger){
    //    StopTutorialSequence();
    //    StartCoroutine (StartTutorialSequence (trigger));
    //}


    //public void UserPressedSomthing(SubtitleMessage.TutorialMessagesStruct trigger){
    //    SubtitleMessage.TutorialMessagesStruct[] tmp = new SubtitleMessage.TutorialMessagesStruct[1];
    //    tmp [0] = trigger;
    //    StopTutorialSequence();
    //    StartCoroutine (StartTutorialSequence (tmp));
    //}

    //public void UserDidEnterTutorialSequence(SubtitleMessage.TutorialMessagesStruct trigger){
    //    SubtitleMessage.TutorialMessagesStruct[] tmp = new SubtitleMessage.TutorialMessagesStruct[1];
    //    tmp [0] = trigger;
    //    StopTutorialSequence();
    //    StartCoroutine (StartTutorialSequence (tmp));
    //}

    //public void UserDidEnterTutorialSequence(SubtitleMessage.TutorialMessagesStruct[] trigger){
    //    StopTutorialSequence();
    //    StartCoroutine (StartTutorialSequence (trigger));
    //}

    //public void UserDidExitTutorialSequence(SubtitleMessage.TutorialMessagesStruct[] trigger,bool removeTutorialOnExit = false ){
    //    //Debug.Log("First");
    //    //if (removeTutorialOnExit) {
    //        //Debug.Log("User exited trigger");
    //    DisableAllTutorialMarkers (trigger);
    //    StopTutorialSequence();
    //    //}

    //}

    //#endregion

    ////private IEnumerator StartTutorialSequence(SubtitleMessage.TutorialMessagesStruct[] nestedCoroutine){
    ////    for (int i = nestedCoroutine.Length - 1; i>=0 ; i--) {
    ////        DisableAllTutorialMarkers (nestedCoroutine);
    ////        yield return StartCoroutine(ShowTutorialMessage(nestedCoroutine[i]));
    ////    }
    ////    DisableAllTutorialMarkers (nestedCoroutine);
    ////    StopTutorialSequence();
    ////    yield return null;
    ////}

    ////private IEnumerator ShowTutorialMessage (SubtitleMessage.TutorialMessagesStruct tutorialMessageStruct){

    //    //Text text;

    //    //switch (tutorialMessageStruct.showAvatar)
    //    //{
    //    //case ActiveAvatarType.Avatar1:
    //    //    //enable the avatar 1 and disable the others
    //    //    //Debug.Log("Case 1");
    //    //    text = avatar1GO.transform.Find("Avatar1Text").GetComponent<Text>();
    //    //    avatar1GO.SetActive(true);
    //    //    avatar1GO.GetComponentInChildren<Animator>().SetBool("Play",true);
    //    //    avatar2GO.SetActive(false);
    //    //    noAvatarGO.SetActive(false);
    //    //    break;
    //    //case ActiveAvatarType.Avatar2:
    //    //    //Debug.Log("Case 2");
    //    //    text = avatar2GO.transform.Find("Avatar2Text").GetComponent<Text>();
			
    //    //    avatar1GO.SetActive(false);
    //    //    avatar2GO.SetActive(true);
    //    //    avatar2GO.GetComponentInChildren<Animator>().SetBool("Play",true);
    //    //    noAvatarGO.SetActive(false);
    //    //    break;
    //    //default:
    //    //    //Debug.Log("no avatar case");
    //    //    text = noAvatarGO.transform.Find("NoAvatarText").GetComponent<Text>();
			
    //    //    avatar1GO.SetActive(false);
    //    //    avatar2GO.SetActive(false);
    //    //    noAvatarGO.SetActive(true);
    //    //    break;
    //    //}
    //    //if (text == null)
    //    //    yield return null;

    //    //if (text.isActiveAndEnabled) {
    //    //    LocalizedText lt = text.GetComponent<LocalizedText>();
    //    //    if(lt != null)
    //    //        lt.localizedID = tutorialMessageStruct.messageLocalizedID;
    //        //switch (tutorialMessageStruct.showAvatar)
    //        //{
    //        //    case ActiveAvatarType.Avatar1:
    //        //        text.text = "Vega: \n"+LanguageManager.Instance.Get(tutorialMessageStruct.messageLocalizedID);
    //        //        break;
    //        //    case ActiveAvatarType.Avatar2:
    //        //    text.text = "L3NN-0N: \n"+LanguageManager.Instance.Get(tutorialMessageStruct.messageLocalizedID);
    //        //        break;
    //        //    default:
    //        //        text.text = LanguageManager.Instance.Get(tutorialMessageStruct.messageLocalizedID);
    //        //        break;
    //        //}
    //    //}


    //    //if(tutorialMessageStruct.marker)
    //    //    tutorialMessageStruct.marker.SetActive (true);

    //    //if (tutorialMessageStruct.englishAudioEventString.Contains ("Say"))
    //    //    am.TriggerEvent ("StopAllSay");
    //    //string selectedLanguage = PlayerPrefs.GetString("Language", "Danish");
    //    //if (selectedLanguage == "English" || tutorialMessageStruct.englishAudioEventString == "Play_Lennon_Happy") {
    //    //    if (tutorialMessageStruct.englishAudioEventString != null && (tutorialMessageStruct.englishAudioEventString != "")) {
    //    //        am.TriggerEvent (tutorialMessageStruct.englishAudioEventString);
    //    //    }
    //    //} else {
    //    //    am.TriggerEvent (tutorialMessageStruct.englishAudioEventString + "_DA");
    //    ////	if (tutorialMessageStruct.danishAudioEventString!=null && (tutorialMessageStruct.danishAudioEventString != "")) {
    //    ////	}
    //    //}

    //    //yield return new WaitForSeconds(tutorialMessageStruct.duration);


    ////}

    ////private void StopTutorialSequence(){
    ////    ClearTutorial ();
    ////    StopAllCoroutines ();
    ////    //yield return null;
    ////}

    ////private void ClearTutorial(){
    ////    avatar1GO.SetActive(false);
    ////    avatar2GO.SetActive(false);
    ////    noAvatarGO.SetActive(false);
    ////}

    ////private void DisableAllTutorialMarkers(SubtitleMessage.TutorialMessagesStruct[] nestedCoroutine){
    ////    foreach (SubtitleMessage.TutorialMessagesStruct messageStruct in nestedCoroutine) {
    ////        if(messageStruct.marker)
    ////            messageStruct.marker.SetActive(false);
    ////    }
    ////}


}
