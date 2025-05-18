using UnityEngine;

public class WallScript : MonoBehaviour
{
    // How far it moves up and down
    public float moveDistance = 2f;  
    // How fast it moves
    public float moveSpeed = 2f;     
    // Wait time before movement
    public float startMovingAfter = 10f;   
    // The initial position of the wall when the game starts
    private Vector3 startPos;
    // The time at which the game started to calculate delay
    private float startTime;

    void Start()
    {
        // Record the wall's starting position
        startPos = transform.position;
        // Record the time when the scene starts to track elapsed time
        startTime = Time.time; 
    }

    void Update()
    {
        // Calculate how much time has passed since the game started
        float elapsed = Time.time - startTime;

        // Start moving after the 10 second delay has passed
        if (elapsed >= startMovingAfter)
        {
            // Use a sine wave to calculate the new Y position for smooth up-and-down motion
            // Subtract startMovingAfter from elapsed so movement starts from 0 after the delay
            float newY = Mathf.Sin((elapsed - startMovingAfter) * moveSpeed) * moveDistance;
            
            // Apply the new Y position while keeping X and Z the same
            transform.position = new Vector3(startPos.x, startPos.y + newY, startPos.z);
        }
    }
}