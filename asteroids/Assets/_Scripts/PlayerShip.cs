using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    public GameObject geometry;
    public bool testMode = false;
    public float thrust = 5f;
    public float rotSpeed = 5f;
    public int shieldLevel = 4;
    public bool startUpShield = true;

    [Header("Projectile Details")]
    public GameObject projectilePrefab;
    public float projectileSpeed;
    // these two variables are used to control where the projectile fires from. 
    public GameObject[] guns;
    private int gun = 0;

    [Header("Death Stuff")]
    public bool exploding = false;
    public bool explosionTest = false;
    public GameObject explosion;
    public Vector3 explosionOffset = new Vector3(0, 0, 30);
    public float deathDelay = 2;

    [Header("Level Up")]
    public float projectileSpeedIncrement = 5;

    private bool rotatedLeft = false;

    Rigidbody rb;

    void Awake()
    {
        Messenger.AddListener(Messages.WAVE_CLEAR, LevelUp);
    }

    // Use this for initialization
    void Start()
    {
        if (!testMode)
            Messenger.Broadcast(Messages.INST_SHIP);
        rb = GetComponent<Rigidbody>();
        StartCoroutine("StartingSafe");
    }

    IEnumerator StartingSafe()
    {
        shieldLevel = 4;
        while (shieldLevel > 0)
        {
            yield return new WaitForSeconds(1);
            shieldLevel--;
        }
        startUpShield = false;
        StopCoroutine("StartingSafe");
    }

    // Update is called once per frame
    void Update()
    {
        if (!exploding)
        {
            float vInput = Input.GetAxis("Vertical");
            float hInput = Input.GetAxis("Horizontal");

            // stop the ship rotating
            if (rotatedLeft && hInput > 0)
                rb.angularVelocity = Vector3.zero;
            else if (!rotatedLeft && hInput < 0)
                rb.angularVelocity = Vector3.zero;

            // Apply force to ship based on input
            rb.AddForce(rb.transform.up * thrust * vInput, ForceMode.Acceleration);
            rb.AddForce(rb.transform.right * thrust * hInput, ForceMode.Acceleration);

            // Rotate ship with A/D or Left Arrow/Right Arrow
            rb.rotation *= Quaternion.Euler(0, 0, hInput * -rotSpeed);
            // Allow the ship to fire
            if (Input.GetKeyDown(KeyCode.Space))
            {
                TempFire();
            }
        }
    }

    void TempFire()
    {
        // guns[gun] gets the current gun location from the guns prefab
        GameObject projGO = Instantiate<GameObject>(projectilePrefab, guns[gun].transform);
        // make gun noise
        AudioSource gunfire = guns[gun].GetComponent<AudioSource>();
        if (gunfire != null)
        {
            if (gunfire.mute)
                gunfire.mute = false;
            gunfire.Play();
        }
        // set up the next guns object: 
        gun = (gun + 1) % guns.Length;
        Rigidbody rigidB = projGO.GetComponent<Rigidbody>();
        rigidB.velocity = transform.up * projectileSpeed;
    }

    void OnCollisionEnter(Collision c)
    {
        if (exploding) return;
        if (c.gameObject.tag == "Asteroid")
        {
            shieldLevel--;

            if (shieldLevel < 0)
            {
                shieldLevel = 0;
                StartCoroutine("Death");
            }
            //c.gameObject.GetComponent<Asteroid>().StartCoroutine("Split");
            Destroy(c.gameObject);
        }
    }

    void LevelUp()
    {
        if (exploding) return;
        projectileSpeed += projectileSpeedIncrement;
        shieldLevel++;
    }

    IEnumerator Death()
    {
        exploding = true;
        if (geometry != null)
            geometry.SetActive(false);
        AudioSource boomSound = GetComponent<AudioSource>();
        if (boomSound != null)
        {
            if (boomSound.mute)
                boomSound.mute = false;
            boomSound.Play();
        }
        if (!testMode)
            Messenger.Broadcast(Messages.DEST_SHIP);
        GameObject boom = Instantiate<GameObject>(explosion, gameObject.transform);
        boom.transform.position += explosionOffset;
        DeleteAfterSeconds boomClock = boom.GetComponent<DeleteAfterSeconds>();
        if (boomClock != null)
            boomClock.duration = deathDelay;
        if (explosionTest && boomClock != null)
        {
            boomClock.duration = -1;
            StopCoroutine("Death");
        }

        yield return new WaitForSeconds(deathDelay);
        Destroy(gameObject);
    }

}
