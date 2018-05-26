using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {
                    Rigidbody rigidBody;
					AudioSource audioSource;
                    [SerializeField] float rcsThrust = 100f;
                    [SerializeField] float mainThrust = 100f;
                    [SerializeField] AudioClip mainEngine;
                     [SerializeField] AudioClip success;
                      [SerializeField] AudioClip death;

                    enum State {Alive ,Trancending , Dying};
                    State state = State.Alive;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
	
	}
	
	// Update is called once per frame
	void Update ()
    {   if( state== State.Alive){
        Rotate();
		Thrust();
    }
    }
    void OnCollisionEnter(Collision collision){

        if(state !=State.Alive){
            return;
        }

        switch (collision.gameObject.tag){
            case "Friendly"  : //do nothing 
            break;
            case "Finish":
           StartSuccessSequence();
                break;

            default:
                StartDeathSequence();
                //kill player
                break;
        }
    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(death);
        Invoke("LoadFirstLevel", 1f);
    }

    private void StartSuccessSequence()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        state = State.Trancending;
        Invoke("LoadNextScene", 1f);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }
     private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void ApplyThrust()
    {
        //can thrust while rotating 
        print("Thrusting !");
        rigidBody.AddRelativeForce(Vector3.up * mainThrust);
        if (!audioSource.isPlaying)
        {   //so that it doesn't layer on top of each other
            audioSource.PlayOneShot(mainEngine);
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
