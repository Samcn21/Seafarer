using UnityEngine;
using System.Collections;

public class LanguageTest : MonoBehaviour {

	private AudioManager am;

	void Awake()
    {
		

        if (!PlayerPrefs.HasKey("Language"))
        {
            PlayerPrefs.SetString("Language", "Danish");
            LanguageManager.Instance.LoadLanguage(PlayerPrefs.GetString("Language"));
        }
        else
        {
			
			LanguageManager.Instance.LoadLanguage(PlayerPrefs.GetString("Language"));
        }
		am = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioManager>();
		//		Debug.Log("Language" + PlayerPrefs.GetString("Language"));
	}

    public void OnLanguageClick(string language)
    {
        LanguageManager.Instance.LoadLanguage(language);
        PlayerPrefs.SetString("Language", language);
        LocalizedText[] texts = FindObjectsOfType<LocalizedText>();
		//LocalizeButtons[] buttons1 = FindObjectsOfType<LocalizeButtons>();
		
		foreach (LocalizedText text in texts)
        {
            text.LocalizeText();
        }
        //if (buttons1.Length != 0) {
        //    foreach (LocalizeButtons button in buttons1) {

        //        button.LocalizeButton();
        //    }
        //}
    }

	public void ToggleLanguage() {
		string currentLanguage = PlayerPrefs.GetString("Language");
		//am.TriggerEvent("Play_MenuClickSounds");
		if (currentLanguage == "Danish") {
			PlayerPrefs.SetString("Language","English");
			LanguageManager.Instance.LoadLanguage("English");
		}
		else {
			PlayerPrefs.SetString("Language", "Danish");
			LanguageManager.Instance.LoadLanguage("Danish");
		}
		LocalizedText[] texts = FindObjectsOfType<LocalizedText>();
		foreach (LocalizedText text in texts) {
			//Debug.Log(text.name);
			text.LocalizeText();
		}
        //LocalizeButtons[] buttons2 = FindObjectsOfType<LocalizeButtons>();
        //if (buttons2.Length != 0) { 
        //foreach (LocalizeButtons button in buttons2) {

        //    //Debug.Log(button.name);
        //    button.LocalizeButton();
        //}
		//}
	}
}
