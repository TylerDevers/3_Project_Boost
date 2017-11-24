using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

	Rigidbody rigidBody;
	AudioSource audio;
	[SerializeField] float rcsThrust = 100f;
	[SerializeField] float mainThrust = 100f;

    enum State {Alive, Dying, Transcending};
    State state = State.Alive;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		audio = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () {
        if (state == State.Alive){
            Thrust();
            Rotate();
        }
	}

    void OnCollisionEnter(Collision collision) 
    {
        if (state != State.Alive) 
        {
            return;
        
        }
        switch (collision.gameObject.tag) 
        {
            case "Friendly":
                break;
            case "Finish":
                state = State.Transcending;
                Invoke("LoadNextLevel", 1f);
                break;
            default:
                state = State.Dying;
                Invoke("LoadLevel1", 2f);
                break;
        }
    }

    private void LoadLevel1()
    {
        SceneManager.LoadScene(0);
        state = State.Alive;
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(1);
        state = State.Alive;
    }

    private void Thrust()
    {
		float thrustThisFrame = mainThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        { // can thrsut while rotating
            print("Thursting");
            rigidBody.AddRelativeForce(Vector3.up * thrustThisFrame);
            if (!audio.isPlaying)
            {
                audio.Play();
            }
        }
		else
        {
            audio.Stop();
        }
    }

    private void Rotate()
    {
		rigidBody.freezeRotation = true; //takes manual control of rotation
		
		float rotationThisFrame = rcsThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            print("Rotate LEft");
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            print("ROtate Right");
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }
		rigidBody.freezeRotation = false; //releases manual control of rotation.
    }

}
