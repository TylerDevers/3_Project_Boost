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
    [SerializeField] ParticleSystem mainEngineParticle;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] float loadLevelDelay = 2f;

    enum State {Alive, Dying, Transcending};
    State state = State.Alive;
    

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () 
    {
        if (state == State.Alive)
        {
            RespondToThrustInput();
            RespondToRotateInput();
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
                StartSuccessSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }
    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(death);
        deathParticles.Play();
        Invoke("LoadFirstLevel", loadLevelDelay);
    }

    private void StartSuccessSequence()
    {
        state = State.Transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticles.Play();
        Invoke("LoadNextLevel", loadLevelDelay);
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(1);
    }

    private void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space)) // can thrust while rotating
        { 
            ApplyThrust();
            mainEngineParticle.Play();
        }
        else
        {
            audioSource.Stop();
            mainEngineParticle.Stop();
        }
    }

    private void ApplyThrust()
    {
        rigidBody.AddRelativeForce(Vector3.up * (mainThrust * 10) * Time.deltaTime);
        if (!audioSource.isPlaying) //so it doesn't layer
        {
            audioSource.PlayOneShot(mainEngine);
        }
    }

    private void RespondToRotateInput()
    {
		rigidBody.freezeRotation = true; //takes manual control of rotation
		
		float rotationThisFrame = rcsThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }
		rigidBody.freezeRotation = false; //releases manual control of rotation.
    }

}
