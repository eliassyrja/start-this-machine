using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public SoundType[] types;
    private SimilarSound[] sounds;

    // Start is called before the first frame update
    void Awake()
    {
		foreach (SoundType types in types)
		{
            foreach (SimilarSound sound in types.sounds)
            {
                sound.source = gameObject.AddComponent<AudioSource>();
                sound.source.clip = sound.clip;
                sound.source.volume = sound.volume;
                sound.source.pitch = sound.pitch;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play(string soundsType)
    {
        
		foreach (SoundType type in types)
		{
            if (type.name == soundsType)
			{
                SoundType soundtype = type;
                float randomSound = Mathf.Floor(UnityEngine.Random.Range(0, soundtype.sounds.Length));
                SimilarSound s = soundtype.sounds[(int)randomSound];

                s.source.PlayOneShot(s.clip);

                Debug.Log("Audio " + s.name.ToString() + " played");
            }
		}
        
    }
}
