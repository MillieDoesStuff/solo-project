using Unity.VisualScripting;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class Health_orb : MonoBehaviour
{
    public int speed = 12;
    public AudioClip on_hit_audio_clip;

    //

    private AudioSource on_hit_audio_source;
    private GameObject player;
    private player_controller player_script;
    private Rigidbody rb;

    void Start()
     {
        player = GameObject.Find("Player");
        on_hit_audio_source = player.GetComponent<AudioSource>();
        player_script = player.GetComponent<player_controller>();
        rb = GetComponent<Rigidbody>();
    }
   
    void Update()
    {
        Vector3 direction = player.transform.position - transform.position; // Distance difference

        rb.linearVelocity = new Vector3 (direction.normalized.x * speed,rb.linearVelocity.y,direction.normalized.z * speed); // Yes movement speed increses if the player is further away
    }

   void OnCollisionEnter(Collision collision) 
   {
        if (collision.gameObject == player)
        {
           player_script.health += 50;
           on_hit_audio_source.PlayOneShot(on_hit_audio_clip); // Play audio
           Destroy(gameObject);
        }
    }
}
