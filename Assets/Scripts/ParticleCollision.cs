using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    private ParticleSystem sys;
    private ParticleSystem.Particle[] aliveParticles;
    private BoxCollider2D boxCollider;
    public PlayerStatus playerStatus;
    
    // Called when any particle meets the condition
    // of the trigger section of the particle system
    private void OnParticleTrigger()
    {
        // Check to see what needs to be initialized
        InitializeIfNeeded();

        Debug.Log("PARTICLE HIT");

        
        // Get the particles that are currently alive
        // and store them in the array
        sys.GetParticles(aliveParticles);

        // Loop through the particles in the array
        foreach (ParticleSystem.Particle particle in aliveParticles)
        {
            // Debug.Log("Particle: " + particle);
            // Check for collision
            if (boxCollider.bounds.Intersects(new Bounds(particle.position, particle.GetCurrentSize3D(sys))))
            {
                // Debug.Log("Success");
                playerStatus.TakeDamage(0.1f);
            }
        }
    }

    // Initialize the variables only once
    private void InitializeIfNeeded()
    {
        // Get the 2d trigger collider from the GameObject
        // This can be done in several different ways, I chose
        // to add a tag and find it that way
        if (boxCollider == null)
            boxCollider = GameObject.FindGameObjectWithTag("HurtBox").GetComponent<BoxCollider2D>();
        
        // Get the particle system we want to collide with and
        // add the 2d trigger collider to it
        if (sys == null)
        {
            sys = GetComponent<ParticleSystem>();
            sys.trigger.SetCollider(0, boxCollider);
        }
        
        // Initialize the array that holds the alive particles
        if (aliveParticles == null)
            aliveParticles = new ParticleSystem.Particle [sys.main.maxParticles];
    }
}
