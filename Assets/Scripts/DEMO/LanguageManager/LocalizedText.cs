using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LocalizedText : MonoBehaviour {

    public string localizedID = string.Empty;
    void Start()
    {
        LocalizeText();
    }

    public void LocalizeText()
    {
        Text text = GetComponent<Text>();
        if (text != null)
        {
			if(localizedID == "Application.loadedLevelName") {
				text.text = LanguageManager.Instance.Get("LevelNames/" + Application.loadedLevelName);
			}
			else { 
            text.text = LanguageManager.Instance.Get(localizedID);
			}
		}
        else { 
           text =  GetComponentInChildren<Text>();
           if (text != null)
           {
				if (localizedID == "Application.loadedLevelName") {
					text.text = LanguageManager.Instance.Get("LevelNames/"+Application.loadedLevelName);
				}
				else {
					text.text = LanguageManager.Instance.Get(localizedID);
				}
           }
        }

    }
}
