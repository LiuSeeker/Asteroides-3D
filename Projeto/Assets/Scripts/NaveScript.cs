using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaveScript : MonoBehaviour
{
    float torque = 0.05f;
    float speed = 1.0f;

    public GameObject bullet;
    public AudioSource audioS;
    public bool audioIsPlaying = false;

    public float fireDelta = 0.5F;
    private float nextFire = 0.5F;
    private float myTime = 0.0F;
    // Start is called before the first frame update
    void Start()
    {
        audioS = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        bool p = Input.GetKey("i");
        bool r = Input.GetKey("k");
        bool stop = Input.GetKey(KeyCode.Space);

        Rigidbody rb = GetComponent<Rigidbody>();
        if (p)
        {
            rb.AddForce(transform.forward * speed);
            if (!audioIsPlaying)
            {
                StartCoroutine(WaitForSound());                
            }
        }
        else if (r)
        {
            rb.AddForce(transform.forward * speed * -1);
            if (!audioIsPlaying)
            {
                StartCoroutine(WaitForSound());
            }
        }
        else
        {
            audioS.Stop();
            audioIsPlaying = false;
        }
        if (stop)
        {
            rb.velocity = rb.velocity * 0.95f;
            rb.angularVelocity = rb.angularVelocity * 0.95f;
        }
        rb.AddTorque(transform.up * torque * h);
        rb.AddTorque(transform.right * torque * -v);
    }

    void Update()
    {
        myTime = myTime + Time.deltaTime;

        if (Input.GetKey("j") && myTime > nextFire)
        {
            nextFire = myTime + fireDelta;
            GameObject instancia = Instantiate(bullet, transform.position + (transform.forward * 2), transform.rotation) as GameObject;
            instancia.GetComponent<Rigidbody>().velocity = 40.0f * transform.forward;
            Destroy(instancia, 5.0f); // Destroi o tiro depois de 5 segundos
            nextFire = nextFire - myTime;
            myTime = 0.0F;
        }
    }

    IEnumerator WaitForSound()
    {
        audioIsPlaying = true;
        audioS.Play();
        while (audioS.isPlaying)
        {
            yield return null;
        }
        audioIsPlaying = false;
    }


}
