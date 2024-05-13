using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Road road;
    private AudioSource audioSource;
    private AudioClip song;
    private void Awake() {
        player.OnCountdownChange += Player_OnCountdownChange;
        audioSource = GetComponent<AudioSource>();
        song = GetComponent<AudioClip>();
        audioSource.Stop();
        road.OnFailSong += Road_OnFailSong;
    }

    private void Road_OnFailSong (object sender, Road.OnFailSongEventArgs e) {
        audioSource.Stop();
    }
    private void Player_OnCountdownChange(object sender, Player.OnCountdownChangeEventArgs e) {
        if (e.countdown == 0) {
            PlaySong(song, transform.position);
        }
    }
    private void PlaySong(AudioClip audioClip, Vector3 position, float volume = 0.4f) {
        audioSource.Play();
    }
}
