using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    private Vector3 movementVector = Vector3.right;
    private Rigidbody rb;
    private ZigZagBuilder builder;

    [Header("Movement Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float fallMultiplier = 30f;
    [SerializeField] private float trackHeight = 1.5f;
    [SerializeField] private float deathHeight = -5f;

    private Vector3 startPosition;
    private bool isMoving = false;

    [Header("Audio")]
    [SerializeField] private AudioClip clickSound;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.freezeRotation = true;

        builder = FindFirstObjectByType<ZigZagBuilder>();
        startPosition = transform.position;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        UIManager.instance.ShowTapToPlay();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isMoving)
            {
                isMoving = true;
                UIManager.instance.HideTapToPlay();
                UIManager.instance.HideGameOver();
                ScoreManager.instance.ResetScore();
            }

            movementVector = (movementVector == Vector3.right) ? Vector3.forward : Vector3.right;

            if (ScoreManager.instance != null)
                ScoreManager.instance.AddScore(1);

            // تشغيل صوت الكليك
            if (clickSound != null && audioSource != null)
                audioSource.PlayOneShot(clickSound, 1f); // volume = 1
        }

        if (transform.position.y < deathHeight)
        {
            ResetGame();
        }
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            rb.linearVelocity = new Vector3(movementVector.x * speed, rb.linearVelocity.y, movementVector.z * speed);

            if (!IsOnTrack())
                rb.AddForce(Vector3.down * fallMultiplier, ForceMode.Acceleration);
        }
        else
        {
            rb.linearVelocity = Vector3.zero;
        }
    }

    bool IsOnTrack()
    {
        return transform.position.y < trackHeight;
    }

    void ResetGame()
    {
        rb.linearVelocity = Vector3.zero;
        transform.position = startPosition;
        movementVector = Vector3.right;
        isMoving = false;

        if (builder != null)
            builder.ResetBuilderPath();

        UIManager.instance.ShowGameOver(
            ScoreManager.instance.currentScore,
            ScoreManager.instance.bestScore
        );
    }
}
