using System;
using UnityEngine;
using Unity.Mathematics;


public class prefab_spawner : MonoBehaviour
{
    public GameObject prefab_object;
    public GameObject prefab_object_2;
    private float time_passed;
    private float rand;
    private int rand2;
    

    void Start()
    {
        rand = UnityEngine.Random.Range(2.0f, 15.0f);
        rand2 = UnityEngine.Random.Range(1,6);
    }
    void Update()
    {
        time_passed += Time.deltaTime;
        if (time_passed >= rand)
        {
            Debug.Log(rand2);
            if (rand2 == 1) {Instantiate(prefab_object_2, transform.position, transform.rotation); }
            if (rand2 >= 2) {Instantiate(prefab_object, transform.position, transform.rotation); }
            
            time_passed = 0.0f;
            rand = UnityEngine.Random.Range(2.0f, 15.0f);
            rand2 = UnityEngine.Random.Range(1,6);
        }
    }
}