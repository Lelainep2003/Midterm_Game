using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangerController : MonoBehaviour
{
    // Reference to the SpriteRenderer component on the player
    public SpriteRenderer Sprite;
    // Coroutine that temporarily changes the sprite's color to orange, then back to white
    public IEnumerator FlashOrange()
    {
        // Change the sprite's color to orange
        Sprite.color = new Color(1f, 0.65f, 0f); 
        // Wait 0.5 seconds before changing it back
        yield return new WaitForSeconds(0.5f);
        // Revert the sprite's color back to white

        Sprite.color = Color.white;
    }
}


// {
//     //This is about as simple a script as you can imagine
//     //It makes it so that if you hit the space bar, the attached sprite changes colors
//     
//     //This is the SpriteRenderer component in charge of drawing this object's sprite
//     public SpriteRenderer SR;
//
//     //This is the color we want the object to turn
//     public Color TargetColor = Color.red;
//     
//     //Any code inside of Update's {} brackets runs once per frame
//     void Update()
//     {
//         //This if statement can be read "If I hit space, change the sprite's color"
//         if (Input.GetKeyDown(KeyCode.Space))
//         {
//             //Here we update the SpriteRenderer's color to be whatever TargetColor is set to be
//             SR.color = TargetColor;
//         }
//     }
// }

