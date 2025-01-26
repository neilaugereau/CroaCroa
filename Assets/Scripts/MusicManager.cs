using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [Header("Liste des morceaux")]
    [SerializeField] private List<AudioClip> musicTracks = new List<AudioClip>();

    [Header("Composants")]
    [SerializeField] private AudioSource audioSource;

    private int currentTrackIndex = 0;

    private void Awake()
    {
        // Assure qu'il y a un AudioSource attaché
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        // Configure l'AudioSource
        audioSource.loop = false; // On veut passer au prochain morceau quand un morceau finit
        audioSource.playOnAwake = false; // Ne joue pas automatiquement au lancement
    }

    private void Start()
    {
        if (musicTracks.Count > 0)
        {
            PlayTrack(currentTrackIndex);
        }
        else
        {
            Debug.LogWarning("Aucune musique dans la liste !");
        }
    }

    private void Update()
    {
        // Vérifie si la musique actuelle est terminée
        if (!audioSource.isPlaying && audioSource.clip != null)
        {
            PlayNextTrack();
        }
    }

    public void PlayTrack(int trackIndex)
    {
        if (trackIndex >= 0 && trackIndex < musicTracks.Count)
        {
            currentTrackIndex = trackIndex;
            audioSource.clip = musicTracks[currentTrackIndex];
            audioSource.Play();
        }
        else
        {
            Debug.LogError("Index hors limite !");
        }
    }

    public void PlayNextTrack()
    {
        currentTrackIndex = (currentTrackIndex + 1) % musicTracks.Count; // Passe au suivant, revient au premier si c'est le dernier
        PlayTrack(currentTrackIndex);
    }

    public void PlayPreviousTrack()
    {
        currentTrackIndex = (currentTrackIndex - 1 + musicTracks.Count) % musicTracks.Count; // Passe au précédent, revient au dernier si c'est le premier
        PlayTrack(currentTrackIndex);
    }

    public void PauseMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }
    }

    public void ResumeMusic()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.UnPause();
        }
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }
}
