using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class TutorialOverlayTrigger : SubtitleMessage {

	public bool removeTutorialOnExit = false;
	public bool fireOnce = false;
	public bool playRandom = false;
	private bool hasFired = false;
    private InputController ic;

    new void Start()
    {
        base.Start();
        ic = GameObject.FindGameObjectWithTag("InputController").GetComponent<InputController>();
    }

    //void OnTriggerEnter(Collider other) {
    //    if (!fireOnce || !hasFired) {
    //        if (this.enabled) {
    //            StartCoroutine(WaitForInputManager(other.gameObject));
    //        }
    //    }
    //}

    //IEnumerator WaitForInputManager(GameObject other)
    //{
    //    while (ic.isDisabled)
    //    {
    //        yield return new WaitForEndOfFrame();
    //    }
    //    if (other.CompareTag("Player") && tm != null)
    //    {
    //        hasFired = true;
    //        if (!playRandom)
    //            tm.UserDidEnterTutorialSequence(tutorialMessages.Reverse().ToArray());
    //        else
    //            tm.UserDidEnterTutorialSequence(tutorialMessages[UnityEngine.Random.Range(0, tutorialMessages.Length)]);
    //    }
    //    yield return null;
    //}

    //void OnTriggerExit(Collider other) {
    //    if(this.enabled && removeTutorialOnExit)
    //        if (other.gameObject.CompareTag("Player")){
    //            tm.UserDidExitTutorialSequence(tutorialMessages,removeTutorialOnExit);
    //    }
    //}
}
