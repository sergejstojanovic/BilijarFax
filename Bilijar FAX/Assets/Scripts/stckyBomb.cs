using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stckyBomb : MonoBehaviour
{
    //referece to Scripts
    private PlayerController playerCtrl;
    [SerializeField]
    private GameObject Player;
    

    //when everything is ready to detonate
    private bool readyToFire = false;
    
    //id for bomb
    [HideInInspector]
    public int id;

    //bomb properties
    [SerializeField]
    private float radius = 5.0f;
    [SerializeField]
    private float explosionPower = 100.0f;
    [SerializeField]
    private LayerMask effectOn;


    // Start is called before the first frame update
    void Start()
    {
        
        id = 0;
        playerCtrl = Player.GetComponent<PlayerController>();
        //Debug.Log(playerCtrl.numberOfBombs);
        //mask = LayerMask.GetMask("belaKugla");

        
        if (playerCtrl.numberOfBombs == 1)
            id = 1;
        else if (playerCtrl.numberOfBombs == 2)
            id = 2;
        else if (playerCtrl.numberOfBombs == 3)
            id = 3;

        //Debug.Log("SBomb Called");
        playerCtrl.numberOfBombs++;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r") && playerCtrl.numberOfBombs >= 3)
        {
            readyToFire = !readyToFire;
            playerCtrl.flyingHeight = -playerCtrl.detonatingHeight;
            playerCtrl.readyToDetonate = true;
            Debug.Log(readyToFire);
        }


        

        if (readyToFire && ((Input.GetKeyDown("1") && id == 1) || (Input.GetKeyDown("2") && id == 2) || (Input.GetKeyDown("3") && id == 3)))
        {
            if(playerCtrl.numberOfBombs > 0)
                playerCtrl.numberOfBombs--;
            
            Destroy(gameObject);
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius, effectOn);
            foreach(Collider obj in colliders)
            {
                Rigidbody rb = obj.GetComponent<Rigidbody>();

                if(rb != null)
                {
                    rb.AddExplosionForce(explosionPower, transform.position, radius, 0.0f);
                }
            }
        }
        
    }

    void OnCollisionEnter(Collision collInfo)
    {
        if (collInfo.collider.tag != "Player")
        {
            this.GetComponent<Rigidbody>().isKinematic = true;
        }

    }
}
