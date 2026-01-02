using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ItemCollisionSound : MonoBehaviour
{
    public AudioClip dropSound;    
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (dropSound == null)
        {
            Debug.LogWarning("Drop sound not assigned!");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Room") || collision.gameObject.CompareTag("Table"))
        {
            if (audioSource != null && dropSound != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
}
