using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blasting : MonoBehaviour
{
    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Shoot a bullet
            Instantiate(bullet, transform.position, transform.rotation);
        }
    }
}
