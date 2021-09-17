using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    [SerializeField] private Paddle.Side SideThatScored;

    new AudioSource audio;
    public AudioClip SoundToPlay;
    public float Volume;


    private void OnTriggerEnter2D(Collider2D Collider)
    {
        Ball ball = Collider.GetComponent<Ball>();
        audio = GetComponent<AudioSource>();//play audio on paddle hit


        if (ball)
        {
            GameController.instance.Score(SideThatScored);//receives info from game controller method named "Score"
            audio.PlayOneShot(SoundToPlay, Volume);
        }

    }
    
  
}
