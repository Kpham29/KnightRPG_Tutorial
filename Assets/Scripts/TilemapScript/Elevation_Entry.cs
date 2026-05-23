using UnityEngine;

public class Elevation_Entry : MonoBehaviour
{
    public Collider2D[] mountainColliders;
    public Collider2D[] boundaryColliers;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var mountain in mountainColliders)
                mountain.enabled = false;
            foreach (var boundary in boundaryColliers)
                boundary.enabled = true;
            other.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 15;
        }
    }
}