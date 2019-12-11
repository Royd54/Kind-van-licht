using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioSource Source;
    public AudioClip[] AudioArray;
    public AudioClip[] FXArray;

    public void PlayAudio(int audioClip)
    {
      //Sets the Background song to a chosen song from the array
      Source = GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();
      Source.clip = AudioArray[audioClip];
      Source.Play();
    }

    public void FXAudio(int FXClip)
    {
      //Sets the SFX to a chosen SFX from the array
      Source = GameObject.Find("AudioManager").GetComponent<AudioSource>();
      Source.clip = FXArray[FXClip];
      Source.Play();
    }
}
