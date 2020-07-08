﻿using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour
{
    public static Bird instance;
    public float upForce;                   //Upward force of the "flap".
    private bool isDead = false;            //Has the player collided with a wall?

    private Animator anim;                  //Reference to the Animator component.
    private Rigidbody2D rb2d;               //Holds a reference to the Rigidbody2D component of the bird.

    public AudioClip[] sounds;
    AudioSource thisSource;

    void Start()
    {
//        AdManager.instance.CallInterstertial();
  //      AdManager.instance.callBanner();

        //Get reference to the Animator component attached to this GameObject.
        anim = GetComponent<Animator>();
        //Get and store a reference to the Rigidbody2D attached to this GameObject.
        rb2d = GetComponent<Rigidbody2D>();

        thisSource = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {

        //Don't allow control if the bird has died.
        if (isDead == false)
        {
            //Look for input to trigger a "flap".
            if (Input.GetMouseButtonDown(0))
            {
                //...tell the animator about it and then...
 //               anim.SetTrigger("Flap");
                //...zero out the birds current y velocity before...
                rb2d.velocity = Vector2.zero;
                //	new Vector2(rb2d.velocity.x, 0);
                //..giving the bird some upward force.
                rb2d.AddForce(new Vector2(0, upForce));

                // Play jumping whoosh sound
                thisSource.clip = sounds[0];
                thisSource.Play();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Health.health--; // lose a health point

        transform.position = new Vector3(0, 0, 0); // Resets the users position after they hit a column

        if (Health.health == 0)
        {
            transform.position = new Vector3(-100, 0, 0);
            ///Play Death sound
            thisSource.clip = sounds[1];
            thisSource.Play();
            // Zero out the bird's velocity
            rb2d.velocity = Vector2.zero;
            // If the bird collides with something set it to dead...
            isDead = true;
            //...tell the Animator about it...
            anim.SetTrigger("Die");
            //...and tell the game control about it.
            GameControl.instance.BirdDied();
        }
    }

}
