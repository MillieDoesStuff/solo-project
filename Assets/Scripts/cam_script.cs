using UnityEngine;

public class cam_script : MonoBehaviour
{
    private Transform player_transform;

    void Start()
    {
        player_transform = GameObject.Find("Player").transform;
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(player_transform.position.x+3,player_transform.position.y+8,player_transform.position.z); // Keeps the camera fixed to the player
    }
}
