using UnityEngine;

public enum Ambient {
    test_ambient
}

public enum Sound {
    click
}

//----------------------------------------------------------------------------------------------------------------------

public class AudioManager: MonoBehaviour {
    AudioSource _sfxSource;
    AudioSource _ambientSource;

    public bool Muted {
        get => _sfxSource.volume == 0;
        set {
            var volume = value == true ? 0 : 1;
            _sfxSource.volume = volume;
            _ambientSource.volume = volume;
        }
    }

    public void PlaySound(Sound clip) {
        var sfx = LD.DB.Sound($"{clip}");
        if (sfx != null) {
            _sfxSource.PlayOneShot(sfx);
        }
    }

    public void PlayAmbient(Ambient clip) {
        var sfx = LD.DB.Sound($"{clip}");
        if (sfx != null) {
            _ambientSource.clip = sfx;
            _ambientSource.Play();
        }
    }

    // Private

    void Awake() {
        _sfxSource = buildSource("SFX source");
        _ambientSource = buildSource("Ambient source");
        _ambientSource.loop = true;
    }

    AudioSource buildSource(string name) {
        var go = new GameObject(name);
        var source = go.AddComponent<AudioSource>();
        source.playOnAwake = false;
        go.transform.SetParent(transform);
        return source;
    }
}
