using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sounds[] sounds, music;
    public static AudioManager Instance;
    [SerializeField] private AudioMixerGroup musicMixer, soundMixer;

    private void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        foreach (Sounds sound in sounds)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.loop = sound.loop;
            sound.audioSource.clip = sound.audioClip;
            sound.audioSource.volume = sound.volume;
            sound.audioSource.pitch = sound.pitch;
            sound.audioSource.outputAudioMixerGroup = soundMixer;
            sound.audioSource.playOnAwake = false;
        }

        foreach (Sounds music in music)
        {
            music.audioSource = gameObject.AddComponent<AudioSource>();
            music.audioSource.loop = music.loop;
            music.audioSource.clip = music.audioClip;
            music.audioSource.volume = music.volume;
            music.audioSource.pitch = music.pitch;
            music.audioSource.outputAudioMixerGroup = musicMixer;
            music.audioSource.playOnAwake = false;
        }
    }

    public void Play(string name, Sounds[] arrayType)
    {
        Sounds sounds = System.Array.Find(arrayType, sound => sound.name == name);
        if (sounds == null)
        {
            Debug.Log("The sound " + name + " couldn't be found");
            return;
        }
        sounds.audioSource.Play();
    }

    public void Pause(string name, Sounds[] arrayType)
    {
        Sounds sounds = System.Array.Find(arrayType, sound => sound.name == name);
        if (sounds == null)
        {
            Debug.Log("The sound " + name + " couldn't be found");
            return;
        }
        sounds.audioSource.Pause();
    }

    public void UnPause(string name, Sounds[] arrayType)
    {
        Sounds sounds = System.Array.Find(arrayType, sound => sound.name == name);
        if (sounds == null)
        {
            Debug.Log("The sound " + name + " couldn't be found");
            return;
        }
        sounds.audioSource.UnPause();
    }

    public void UpdatePlay(string name, Sounds[] arrayType)
    {
        Sounds sounds = System.Array.Find(arrayType, sound => sound.name == name);
        if (sounds == null)
        {
            Debug.Log("The sound " + name + " couldn't be found");
            return;
        }

        if(!sounds.audioSource.isPlaying)
            sounds.audioSource.Play();

        sounds.audioSource.Pause();
        sounds.audioSource.UnPause();
    }

    public void Stop(string name, Sounds[] arrayType)
    {
        Sounds sounds = System.Array.Find(arrayType, sound => sound.name == name);
        if (sounds == null)
        {
            Debug.Log("The sound " + name + " couldn't be found");
            return;
        }
        sounds.audioSource.Stop();
    }

    public void SetMusicVolume(float volumeVal) =>
        musicMixer.audioMixer.SetFloat("MusicParam", Mathf.Log10(volumeVal) * 20);
        
    public void SetSoundVolume(float volumeVal) =>
        soundMixer.audioMixer.SetFloat("SoundParam", Mathf.Log10(volumeVal) * 20);
}
