using UnityEngine;

public class Bomb : MonoBehaviour
{
    public AudioSource explosionSound;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            explosionSound.Play();
            FindObjectOfType<GameManager>().Explode();            
        }
    }
}
