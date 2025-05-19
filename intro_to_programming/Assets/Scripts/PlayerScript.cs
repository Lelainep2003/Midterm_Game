using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    //These are the player's Variables, the raw info that defines them

    //The Rigidbody2D is a component that gives the player physics, and is what we use to move
    public Rigidbody2D RB;

    //TextMeshPro is a component that draws text on the screen.
    //We use this one to show our score.
    public TextMeshPro ScoreText;

    //This will control how fast the player moves
    public float Speed = 5;

    //This is how many points we currently have
    public int Score = 0;

    //This is how many Lives the player has
    public int Lives = 3;

    // Reference for UI display
    public TextMeshPro LivesText;
    // Reference to the ColorChangerController script, used to trigger color effects like flashing orange
    public ColorChangerController colorChanger;
    // Reference to the HazardSpawner script, used to control when and how hazards are spawned
    private HazardSpawner hazardSpawner;

    //Start automatically gets triggered once when the objects turns on/the game starts
    void Start()
    {
        hazardSpawner = FindFirstObjectByType<HazardSpawner>();
        //call UpdateScore to make sure score looks correct
        UpdateScore();
        //call UpdateLives to make sure score looks correct
        UpdateLives();

    }

    //Update is a lot like Start, but it automatically gets triggered once per frame
    //Most of an object's code will be called from Update--it controls things that happen in real time
    void Update()
    {
        //The code below controls the character's movement
        //First we make a variable that we'll use to record how we want to move
        Vector2 vel = new Vector2(0, 0);

        //Then we use if statement to figure out what that variable should look like

        //If I hold the right arrow key, the player should move right. . .
        if (Input.GetKey(KeyCode.RightArrow))
        {
            vel.x = Speed;
        }

        //If I hold the left arrow, the player should move left. . .
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            vel.x = -Speed;
        }

        //If I hold the up arrow, the player should move up. . .
        if (Input.GetKey(KeyCode.UpArrow))
        {
            vel.y = Speed;
        }

        //If I hold the down arrow, the player should move down. . .
        if (Input.GetKey(KeyCode.DownArrow))
        {
            vel.y = -Speed;
        }

        //Finally, I take that variable and I feed it to the component in charge of movement
        RB.linearVelocity = vel;


//SCREEN BLOCKING
//checks if player is hitting the left side of the screen
        if (transform.position.x < -7.5f)
        {
            //moves player back to the edge of the screen
            transform.position = new Vector3(-7.5f, transform.position.y, 0f);
        }

//checks if player is hitting the right side of the screen
        if (transform.position.x > 7.5f)
        {
            //moves player back to the edge of the screen
            transform.position = new Vector3(7.5f, transform.position.y, 0f);
        }

//checks if player is hitting the bottom side of the screen
        if (transform.position.y < -4.5f)
        {
            //moves player back to the edge of the screen
            transform.position = new Vector3(transform.position.x, -4.5f, 0f);
        }

//checks if player is hitting the top side of the screen
        if (transform.position.y > 4.5f)
        {
            //moves player back to the edge of the screen
            transform.position = new Vector3(transform.position.x, 4.5f, 0f);
        }
    }

    //This gets called whenever you bump into another object, like a wall or coin.
    private void OnCollisionEnter2D(Collision2D other)
    {
        //This checks to see if the thing you bumped into had the Hazard tag
        //If it does...
        if (other.gameObject.CompareTag("Hazard"))
        {
            // Ensure colorChanger is assigned before calling its FlashOrange coroutine
            if (colorChanger != null)
            {
                // Start the coroutine that temporarily changes the player's color to orange
                colorChanger.StartCoroutine(colorChanger.FlashOrange());
            }
            //Run your 'you lose' function!
            Die();
        }

        //This checks to see if the thing you bumped into has the CoinScript script on it
        CoinScript coin = other.gameObject.GetComponent<CoinScript>();
        //If it does, run the code block belows
        if (coin != null)
        {
            //Tell the coin that you bumped into them so they can self destruct or whatever
            coin.GetBumped();
            //Make your score variable go up by one. . .
            Score++;
            //And then update the game's score text
            UpdateScore();
            //Talks to hazard spawner
            hazardSpawner.CheckAndSpawn(Score);

        }
    }

    //This function updates the game's score text to show how many points you have
    //Even if your 'score' variable goes up, if you don't update the text the player doesn't know
    public void UpdateScore()
    {
        ScoreText.text = "Score: " + Score;
    }
    // Updates the lives text 
    public void UpdateLives()
    {
        LivesText.text = "Lives: " + Lives;
    }
    //If this function is called, the player character dies. The game goes to a 'Game Over' screen.
    public void Die()
    {
        Lives--;

        if (Lives > 0)
        {
            UpdateLives();

            // Optional: teleport player back to center or a safe spot
            transform.position = Vector3.zero;
        }
        else
        {
            SceneManager.LoadScene("Game Over");
        }
    }
}