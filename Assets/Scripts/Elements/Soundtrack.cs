using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundtrack : MonoBehaviour {
    
    //! References
    private AudioSource source;
    private static Soundtrack instance;


    //! MonoBehaviour
    void Awake() {
        source = GetComponent<AudioSource>();
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    void Start() {
        InitializeSoundtrack();
    }


    //! Soundtrack - Public
    public void RefreshSettings() {
        source.volume = Config.SOUND_VOLUME_MUSIC * Options.instance.SoundsVolume * Options.instance.MusicVolume;
    }

    //! Soundtrack - Private
    private void InitializeSoundtrack() {
        EventManager.OnUpdatedOptions += RefreshSettings;
        RefreshSettings();
    }
}
