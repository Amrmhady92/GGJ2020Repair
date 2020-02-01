using UnityEngine.Audio;
using UnityEngine;


[System.Serializable]
public class Sound
{
    public enum Type
    {
        Player1_Sounds,
        Player2_Sounds,
        Player3_Sounds,
        Player4_Sounds,
        All_Players_Sounds,
        Environment_Sound,
        Menu_Sound
    }

    public AudioClip clip;

    public string name;
    public Type type;

    [Range(0f, 1f)]
    public float volume;

    [Range(.1f, 3f)]
    public float pitch;

    public bool looping;

    [Range(-1f, 1f)]
    public float stereo;

    //[HideInInspector]
    public AudioSource source;

}

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        for (int i = 0; i < System.Enum.GetValues(typeof(Sound.Type)).Length; i++)
        {
            Sound.Type temp_type = (Sound.Type)i;
            GameObject new_object = new GameObject(temp_type.ToString());
            new_object.transform.SetParent(this.transform);
        }

        foreach (Sound s in sounds)
        {
            if (s.type == Sound.Type.Menu_Sound)
                s.source = transform.Find("Menu_Sounds").gameObject.AddComponent<AudioSource>();

            else if (s.type == Sound.Type.Player1_Sounds)
                s.source = transform.Find("Player1_Sounds").gameObject.AddComponent<AudioSource>();

            else if (s.type == Sound.Type.Player2_Sounds)
                s.source = transform.Find("Player2_Sounds").gameObject.AddComponent<AudioSource>();

            else if (s.type == Sound.Type.Player3_Sounds)
                s.source = transform.Find("Player3_Sounds").gameObject.AddComponent<AudioSource>();

            else if (s.type == Sound.Type.Player4_Sounds)
                s.source = transform.Find("Player4_Sounds").gameObject.AddComponent<AudioSource>();

            else if (s.type == Sound.Type.All_Players_Sounds)
                s.source = transform.Find("All_Players_Sounds").gameObject.AddComponent<AudioSource>();

            else if (s.type == Sound.Type.Environment_Sound)
                s.source = transform.Find("Environment_Sound").gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.looping;
            s.source.panStereo = s.stereo;
        }
    }

    public void Play(string name)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == name)
                s.source.Play();
        }
    }

    public void Stop(string name)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == name)
                s.source.Stop();
        }
    }

    public void fadeOut(string name, float time)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == name)
                s.source.volume = Mathf.Lerp(s.volume, 0f, time);
        }
    }

    public bool isPlaying(string name)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == name)
            {
                if (s.source.isPlaying)
                    return true;
                else
                    return false;
            }
        }
        return false;
    }

    public void ResetSounds()
    {
        foreach (Sound s in sounds)
        {
            s.source.Stop();
            s.source.volume = s.volume;
        }
    }

    public void setVolume(string name, float p_volume)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == name)
                s.source.volume = p_volume;
        }
    }

    public void setClip(string name, AudioClip p_clip)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == name)
                s.source.clip = p_clip;
        }
    }

    public float getClipLength(string name)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == name)
                return s.source.clip.length;
        }
        return 0;
    }



}



