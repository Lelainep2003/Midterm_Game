using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public Vector2 minPosition = new Vector2(-7f, -4f);
    public Vector2 maxPosition = new Vector2(7f, 4f);
    
    //The player calls this function on the coin whenever they bump into it
    //You can change its contents if you want something different to happen on collection
    //For example, what if the coin teleported to a new location instead of being destroyed?

    public void GetBumped()
    {
        // Teleport coin to a new random position within bounds
        float newX = Random.Range(minPosition.x, maxPosition.x);
        float newY = Random.Range(minPosition.y, maxPosition.y);
        transform.position = new Vector3(newX, newY, 0f);
        //This destroys the coin
        Destroy(gameObject);
    }

    // Trigger coin collection when player collides
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GetBumped();
        }
    }
}