using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kretanjeKugle : MonoBehaviour
{

    public Rigidbody rb;
    [SerializeField]
    //private float jacinaUdarca = 800f;
    public Transform stick;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collInfo)
    {
        if (collInfo.collider.tag != "podloga")
        {
            rb.velocity /= 1.8f;
        }
       
    }

}
