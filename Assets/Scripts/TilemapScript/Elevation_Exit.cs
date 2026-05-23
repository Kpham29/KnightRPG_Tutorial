using UnityEngine;

public class Elevation_Exit : MonoBehaviour
{
    public Collider2D[] mountainColliders;
    public Collider2D[] boundaryColliers;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var mountain in mountainColliders)
                mountain.enabled = true;
            foreach (var boundary in boundaryColliers)
                boundary.enabled = false;
            other.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 5;
            Debug.Log("Elevation Exit");
        }
    }
}