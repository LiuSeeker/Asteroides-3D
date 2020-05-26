using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public float speed = 8; // velocidade do jogador
    public float gravity = -50f; // valor da gravidade
    public float jumpForce = 15;
    public LayerMask groundMask;
    CharacterController character;
    Vector3 velocity;
    bool isGrounded;
    public GameManager gm;
    public Text texto;
    ParticleSystem ps;

    public AudioSource audioS;
    public bool audioIsPlaying = false;
    public bool isRunning = false;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        character = gameObject.GetComponent<CharacterController>();
        anim = gameObject.GetComponent<Animator>();
        ps = transform.Find("SprintTrail").GetComponent<ParticleSystem>();
        audioS = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Verifica se encostando no chão (o centro do objeto deve ser na base)
        isGrounded = Physics.CheckSphere(transform.position, 0.2f, groundMask);

        // Se no chão e descendo, resetar velocidade
        if (isGrounded && velocity.y < 0.0f)
        {
            velocity.y = -1.0f;
        }
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        anim.SetFloat("Correndo", z);

        // Rotaciona personagem
        transform.Rotate(0, x * speed * 10 * Time.deltaTime, 0);

        // Move personagem
        if (Input.GetKey(KeyCode.LeftShift) && gm.playerRunPowerUp)
        {
            speed = 20;
            if (!isRunning)
            {
                StartCoroutine(WaitForPS());
            }
        }
        else
        {
            speed = 10;
            ps.Stop();
        }
        if (Input.GetKey("l"))
        {
            speed = 40;
        }
        Vector3 move = transform.forward * z;
        if(z != 0f && isGrounded)
        {
            if (!audioIsPlaying)
            {
                StartCoroutine(WaitForSound());
            }
        }
        else
        {
            audioS.Stop();
        }
        character.Move(move * Time.deltaTime * speed);

        // Aplica gravidade no personagem
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = jumpForce;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }
        character.Move(velocity * Time.deltaTime);
    }

    public void setSpawnPos(Vector3 pos)
    {
        transform.localPosition = pos;
        transform.position = pos;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.name == "RunChestTrigger")
        {
            if (!gm.playerRunPowerUp)
            {
                texto.text = "Pressione 'j' para abrir bau";
                if (Input.GetKey("j"))
                {
                    other.transform.Find("Particles").GetComponent<ParticleSystem>().Play();
                    other.transform.GetComponent<AudioSource>().Play();
                    gm.playerRunPowerUp = true;
                    gm.changeCanvasText();
                }
            }
            else
            {
                texto.text = "Agora voce pode correr com 'shift'";
            }
            
        }
        else if (other.name == "PortaTemplo")
        {
            texto.text = "Pressione 'j' para entrar";
            if (Input.GetKey("j"))
            {
                gm.LoadTempleScene(null);
            }
        }
        else if (other.name == "PortaPoco")
        {
            texto.text = "Pressione 'j' para entrar";
            if (Input.GetKey("j"))
            {
               gm.LoadWellScene(null);
            }
        }
        else if (other.name == "PortaPocoOut")
        {
            texto.text = "Pressione 'j' para sair";
            if (Input.GetKey("j"))
            {
                gm.LoadPlanetScene(null);
            }
        }
        else if (other.name == "Porta")
        {
            texto.text = "Pressione 'j' para sair";
            if (Input.GetKey("j"))
            {
                gm.LoadPlanetScene("TempleSpawn");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        texto.text = "";
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

    IEnumerator WaitForPS()
    {
        isRunning = true;
        ps.Play();
        while (ps.isPlaying)
        {
            yield return null;
        }
        isRunning = false;
    }
}
