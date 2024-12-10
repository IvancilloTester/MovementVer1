using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public AudioSource source;
    public AudioClip button;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        source.clip = button;
    }

    public void Reproducir()
    {

        source.Play();
    }
}
