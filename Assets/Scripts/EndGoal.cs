using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGoal : MonoBehaviour
{
    public string nextLevel;
    
    public GameObject cameraAudio;
    AudioSource audioSource;
    
    void Start() {
       audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            cameraAudio.GetComponent<AudioSource>().Stop();
            audioSource.Play();
            GameObject.FindGameObjectWithTag("StateManager").GetComponent<StateManager>().SwitchState(GameState.Victory);
        }
    }
}
