using NUnit.Framework;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class HiddenWall : MonoBehaviour
{
    [SerializeField] private XRSocketInteractor socket;

    private Rigidbody rb;
    [SerializeField] private float moveSpeed = 0.5f;

    private AudioSource src;
    [SerializeField] public AudioClip movingDoor;

    private bool isOpened = false;
    private bool canStop = false;
    private float timeStarted;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        src = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOpened && socket.hasSelection) {
            var obj = socket.firstInteractableSelected.transform;
            if (obj.CompareTag("Statue"))
            {
                OpenHiddenWall();
            }
            isOpened = true;
            timeStarted = Time.time;
        }

        if (isOpened && !canStop && Time.time >= timeStarted + 4f) {
            // Espera 4 secs ate parar
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.constraints = RigidbodyConstraints.FreezePosition;
            canStop = true;
        }

        
    }

    public void OpenHiddenWall()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();

        transform.position += transform.forward * 0.05f;
        rb.linearVelocity = -transform.right * moveSpeed;
        PlayMovingSound();
    }

    public void PlayMovingSound()
    {
        if (src == null)
            src = GetComponent<AudioSource>();

        src.PlayOneShot(movingDoor);
    }
}
