using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource effectSource;
    public AudioSource itemAudioSource;
    public GameManager gameManager;
    public TutorialManager tutorialManager;

    public void PlayItemSound(AudioClip sound, string itemName)
    {
        if (!itemAudioSource.isPlaying) {
            itemAudioSource.PlayOneShot(sound);
            if(itemName != "") 
            {
                if (gameManager != null)
                {
                    gameManager.ShowDraggedItemName(itemName);
                }
                else if (tutorialManager != null) { 
                    tutorialManager.ShowDraggedItemName(itemName);
                }
            }
        }
    }

    public void PlaySoundEffect(AudioClip sound, float volume)
    {
        if (!effectSource.isPlaying)
        {
            effectSource.PlayOneShot(sound,volume);
        }
    }

    public void PlayMusic()
    {
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
}
