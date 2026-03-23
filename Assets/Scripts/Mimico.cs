using NUnit.Framework;
using UnityEditor.Animations;
using UnityEngine;

public class Mimico : MonoBehaviour
{

    [Header("Interaction")]
    [SerializeField] private Transform Player;
    [SerializeField] public int minDist;
    [SerializeField] private int attackDist;
    [SerializeField] private bool isHungry = true;
    [SerializeField] public bool hasDropped;
    [SerializeField] public bool hasEaten;
    [SerializeField] private GameObject statuePrefab;
    [SerializeField] public Transform firePoint;
    [SerializeField] public float throwSpeed = 10f;
    private float timeWasFed;
    private bool wasRejected;

    [Header("Audio")]
    public AudioClip hungrySound;
    public AudioClip eatingSound;
    public AudioClip rejectSound;
    public AudioClip burpSound;
    private AudioSource source;
    private float soundCooldown = 7f;
    private float lastPlayed;

    [Header("Animation")]
    private Animator animator;
    private bool isClosing;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.speed = 0f;

        source = GetComponent<AudioSource>();
        lastPlayed = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        // Processing after getting food
        if (hasEaten && isHungry && Time.time >= timeWasFed + 5.2)
        {
            isHungry = false;
        }

        // Ends Puzzle
        if (!isHungry && !hasDropped)
        {
            PlayBurp();
            DropStatue();
            hasDropped = true;
        }

        // Check Proximity (Wake Up)
        float dist = Vector3.Distance(Player.position, transform.position);
        if (dist < minDist)
        {
            animator.speed = 1f;

            if (isHungry && Time.time >= lastPlayed + soundCooldown)
            {
                lastPlayed = Time.time;
                hasEaten = true;
                source.PlayOneShot(hungrySound);
            }
        }

        // Check Attack
        if (dist < attackDist)
            animator.SetBool("isAttack", true);
        else
            animator.SetBool("isAttack", false);
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;

        if (go.CompareTag("Food"))
        {
            Fish fish = go.GetComponent<Fish>();

            if (fish.isCooked) {
                Destroy(other);
                timeWasFed = Time.time;
                PlayEating();
                hasEaten = true;
            } else
            {
                wasRejected = true;
            }
        } else
        {
            wasRejected = true;
        }


        if (wasRejected)
        {
            wasRejected = false;
            Transform transf = go.GetComponent<Transform>();
            transf.position = firePoint.position;
            transf.rotation = firePoint.rotation;

            PlayReject();

            // Throw Object
            Rigidbody rb = go.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = firePoint.forward * throwSpeed;
                rb.useGravity = true;
            }
        }

    }

    void DropStatue()
    {
        // Spawn Statue
        GameObject statue = Instantiate(
            statuePrefab,
            firePoint.position,
            firePoint.rotation
        );

        // Throw Statue
        Rigidbody rb = statue.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = firePoint.forward * throwSpeed;
            rb.useGravity = true;
        }

        // // Play gun sound
        // if (shootSound != null && source != null)
        // {
        //     source.PlayOneShot(shootSound);
        // }
    }

    void PlayEating()
    {
        source.PlayOneShot(eatingSound);
    }

    void PlayReject()
    {
        source.PlayOneShot(rejectSound);
    }

    void PlayBurp()
    {
        source.PlayOneShot(burpSound);
    }
}
