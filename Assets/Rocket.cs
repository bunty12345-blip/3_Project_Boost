using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {
                    Rigidbody rigidBody;
					AudioSource audioSource;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Rotate();
		Thrust();
    }
 private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {      //can thrust while rotating 
            print("Thrusting !");
            rigidBody.AddRelativeForce(Vector3.up);
            if (!audioSource.isPlaying)
            {   //so that it doesn't layer on top of each other
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
    private void Rotate()
    {
		rigidBody.freezeRotation = true ;  // take manual control of rotation 
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward);
            print("Rotating Left !");
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward);
            print("Rotating Right !");
        }

		rigidBody.freezeRotation = false ; // resume physics control of rotation . 
    }

   
}
