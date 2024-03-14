using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100;
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] AudioClip rocketVoice;

    [SerializeField] ParticleSystem rocketParticle;
    [SerializeField] ParticleSystem rocketLeftParticle;
    [SerializeField] ParticleSystem rocketRightParticle;
    AudioSource audioSource;
    Rigidbody rb;
    void Start()
    {
        rb=GetComponent<Rigidbody>();
        audioSource=GetComponent<AudioSource>();
    }

    
    void Update()
    {
        ProcessThrust();
        ProcessRotation();  
    }
    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            startThrusting();
        }
        else
        {
            stopThrusting();
        }
    }
    void startThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime); // vector3.up =vector3(0,1,0);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(rocketVoice);
        }
        if (!rocketParticle.isPlaying)
        {
            rocketParticle.Play();
        }
    }
    void stopThrusting()
    {
        audioSource.Stop();
        rocketParticle.Stop();
    }
    void ProcessRotation()
    {
        if( Input.GetKey(KeyCode.A))
        {
            leftRotation();
        }
        else if( Input.GetKey(KeyCode.D))
        {
            rightRotation();
        }
        else
        {
            stopRotation();
        }

    }
    void leftRotation()
    {
        ApplyRotation(rotationThrust);
        if (!rocketRightParticle.isPlaying)
        {
            rocketRightParticle.Play();
        }
    }
    void rightRotation()
    {
        ApplyRotation(-rotationThrust);
        if (!rocketLeftParticle.isPlaying)
        {
            rocketLeftParticle.Play();
        }
    }
    void stopRotation()
    {
        rocketLeftParticle.Stop();
        rocketRightParticle.Stop();
    }
    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);// vector3.forward =vector3(0,0,1);
        rb.freezeRotation = false; 
    }

}
