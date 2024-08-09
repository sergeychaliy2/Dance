using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [Header("Audio_List"),Space]
    [SerializeField] private List<AudioClip> audioClips = new List<AudioClip>();
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private int currentTrackIndex = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioClips.Count > 0)
        {
            PlayCurrentTrack();
        }
        else
        {
            Debug.LogWarning("Список аудиоклипов пуст!");
        }
    }

    void OnGUI()
    {
#if UNITY_STANDALONE_WIN
        if (Event.current.type == EventType.KeyDown)
        {
            if (Event.current.keyCode == KeyCode.Y)
            {
                CancelInvoke();
                PlayNextTrack();
            }
            else if (Event.current.keyCode == KeyCode.T)
            {
                CancelInvoke();
                PlayPreviousTrack();
            }
            else if (Event.current.keyCode == KeyCode.Space)
            {
                TogglePlayPause();
            }
        }
#endif
    }

    public void PlayNextTrack()
    {
        if (audioClips.Count == 0)
        {
            Debug.LogWarning("Список аудиоклипов пуст!");
            return;
        }

        currentTrackIndex = (currentTrackIndex + 1) % audioClips.Count;

        PlayCurrentTrack();
    }

    public void PlayPreviousTrack()
    {
        if (audioClips.Count == 0)
        {
            Debug.LogWarning("Список аудиоклипов пуст!");
            return;
        }

        currentTrackIndex--;
        if (currentTrackIndex < 0)
        {
            currentTrackIndex = audioClips.Count - 1;
        }

        PlayCurrentTrack();
    }

    public void PlayCurrentTrack()
    {
        if (audioClips.Count > 0)
        {
            audioSource.clip = audioClips[currentTrackIndex];
            audioSource.Play();
            Invoke("PlayNextTrack", audioSource.clip.length);
        }
        else
        {
            Debug.LogWarning("Список аудиоклипов пуст!");
        }
    }

    public void StopCurrentTrack()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
            CancelInvoke();
        }
    }

    public void TogglePlayPause()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
            CancelInvoke();
        }
        else if (audioSource.time > 0f)
        {
            audioSource.UnPause();
            Invoke("PlayNextTrack", audioSource.clip.length - audioSource.time);
        }
    }
}
