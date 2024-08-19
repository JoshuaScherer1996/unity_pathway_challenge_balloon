using UnityEngine;
using UnityEngine.UIElements;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;

    private const float FloatForce = 20.0f;
    private const float GravityModifier = 1.5f;
    private const float TopBound = 14.5f;
    private const float BottomBound = 0.5f;
    private Rigidbody _playerRb;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource _playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;


    // Start is called before the first frame update
    private void Start()
    {
        Physics.gravity *= GravityModifier;
        _playerAudio = GetComponent<AudioSource>();
        _playerRb = GetComponent<Rigidbody>();

        // Apply a small upward force at the start of the game
        _playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);

    }

    // Update is called once per frame
    private void Update()
    {
        // While space is pressed and player is low enough, float up
        if (Input.GetKey(KeyCode.Space) && !gameOver)
        {
            _playerRb.AddForce(Vector3.up * FloatForce);
        }

        if (gameObject.transform.position.y >= TopBound)
        {
            _playerRb.AddForce(Vector3.down * 0.5f, ForceMode.Impulse);
        }
        
        if (gameObject.transform.position.y <= BottomBound)
        {
            _playerRb.AddForce(Vector3.up * 0.5f, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            _playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        } 

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            _playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);

        }

    }

}
