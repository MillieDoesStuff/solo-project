using Unity.VisualScripting;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class enemy : MonoBehaviour
{
    public int health = 2;
    public int speed = 7;
    public AudioClip on_hit_audio_clip;

    //

    private AudioSource on_hit_audio_source;
    private GameObject player;
    private player_controller player_script;
    private bool force_being_applied = false;
    private Rigidbody rb;
    private bool I_Frames = false;
    private Renderer our_renderer;

    

     private IEnumerator movement_control_back()
    {
      our_renderer.material.SetColor("_BaseColor", Color.black);
      yield return new WaitForSeconds(0.7f);
      force_being_applied = false;
      our_renderer.material.SetColor("_BaseColor", Color.white);
    }

    private IEnumerator collision_CD()
    {
        I_Frames = true;
        yield return new WaitForSeconds(0.5f);
        I_Frames = false;
    }


    void Start()
     {
        player = GameObject.Find("Player");
        on_hit_audio_source = player.GetComponent<AudioSource>();
        player_script = player.GetComponent<player_controller>();
        rb = GetComponent<Rigidbody>();
        our_renderer = GetComponent<Renderer>();
        our_renderer.material.SetColor("_BaseColor", Color.white);
    }
   
    void Update()
    {
        Vector3 direction = player.transform.position - transform.position; // Distance difference
        if (force_being_applied == false)
        {
            rb.linearVelocity = new Vector3 (direction.normalized.x * speed,rb.linearVelocity.y,direction.normalized.z * speed); // Yes movement speed increses if the player is further away
        }
    }

   void OnCollisionEnter(Collision collision) 
   {
		GameObject otherObj = collision.gameObject;
		
        if (otherObj == player && I_Frames == false){
            StartCoroutine(collision_CD());
            if (player_script.attacking == true)
            {
                on_hit_audio_source.PlayOneShot(on_hit_audio_clip); // Play audio
                player_script.enemy_hit = true;
                health -= 1; // Always take 1 hit of damage
                if (health <= 0)
                {
                    player_script.score += 1;
                    Destroy(gameObject);
                    return;
                }

                

                Vector3 our_direction = transform.position - otherObj.transform.position;
                force_being_applied = true;
                rb.AddForce(our_direction * 300,ForceMode.Acceleration);
                StartCoroutine(movement_control_back());
                return;
            }

            player_script.take_damage(transform,50); // Then finally take damage
        }
    }


}
