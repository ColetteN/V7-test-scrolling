using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTSmanager : MonoBehaviour {
    private bool _initializeError = false;
    public string _inputText = "";
    private int _speechId = 0;
    private float _pitch = 1f, _speechRate = 0.7f;
    private int _selectedLocale = 0;
    private string[] _localeStrings;

    // Use this for initialization
    void Start()
    {
        // Screen.sleepTimeout = SleepTimeout.NeverSleep;
        TTSManager.Initialize(transform.name, "OnTTSInit");
    }

    void Awake() {
     DontDestroyOnLoad(this.gameObject);
 }
   
    
	public void PlaySpeech(){
		TTSManager.SetPitch(_pitch);
		TTSManager.SetSpeechRate(_speechRate);
		TTSManager.SetLanguage(TTSManager.ENGLISH);
 		TTSManager.Speak(_inputText, false, TTSManager.STREAM.Music, 1f, 0f, transform.name, "OnSpeechCompleted", "speech_" + (++_speechId));
	}

    void OnDestroy()
    {
        TTSManager.Shutdown();
    }


    void OnSpeechCompleted(string id)
    {
        Debug.Log("Speech '" + id + "' is complete.");
    }

    void OnSynthesizeCompleted(string id)
    {
        Debug.Log("Synthesize of speech '" + id + "' is complete.");
    }
}