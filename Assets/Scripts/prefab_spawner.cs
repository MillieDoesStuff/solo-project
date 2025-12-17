using System;
using UnityEngine;
using Unity.Mathematics;


public class prefab_spawner : MonoBehaviour
{
    public GameObject prefab_object;
    private float time_passed;
    private float rand;
    

    void Start()
    {
        rand = UnityEngine.Random.Range(2.0f, 15.0f);
    }
    void Update()
    {
        time_passed += Time.deltaTime;
        if (time_passed >= rand)
        {
            Instantiate(prefab_object, transform.position, transform.rotation);
            time_passed = 0.0f;
            rand = UnityEngine.Random.Range(2.0f, 15.0f);
        }
    }
}