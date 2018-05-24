using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {
                    Rigidbody rigidBody;
					AudioSource audioSource;
                    [SerializeField] float rcsThrust = 100f;
                    [SerializeField] float mainThrust = 100f;

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
    void OnCollisionEnter(Collision collision){
        switch (collision.gameObject.tag){
            case "Friendly"  : //do nothing 
            break;
            default : print("Dead");
            //kill player
            break;
        }
    }
 private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {      //can thrust while rotating 
            print("Thrusting !");
            rigidBody.AddRelativeForce(Vector3.up*mainThrust);
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
		
        float rotationThisFrame = rcsThrust * Time.deltaTime;
        
        rigidBody.freezeRotation = true ;  // take manual control of rotation 
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
            print("Rotating Left !");
        }

        else if (Input.GetKey(KeyCode.D))
        {
         transform.Rotate(-Vector3.forward * rotationThisFrame);
            print("Rotating Right !");
        }

		rigidBody.freezeRotation = false ; // resume physics control of rotation . 
    }

   
}
