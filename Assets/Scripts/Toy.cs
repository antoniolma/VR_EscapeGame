using UnityEngine;

public class Toy : MonoBehaviour
{
    public AudioClip squeakSound;
    private AudioSource source;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        source = GetComponent<AudioSource>();

        if (source == null)
        {
            source = gameObject.AddComponent<AudioSource>();
        }
    }

    public void MakeSound()
    {
        if (squeakSound != null && source != null) 
            source.PlayOneShot(squeakSound);
    }
}
