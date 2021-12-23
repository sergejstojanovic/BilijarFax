using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay_UI : MonoBehaviour
{

    [SerializeField]
    private GameObject pickupText;
    [SerializeField]
    private float maxDistanceToPickup = 10.0f;
    [SerializeField]
    private Camera mainCam;
    [SerializeField]
    private LayerMask mask;

    //reference to other scripts
    private PlayerController PlayerCtrl;
    private stckyBomb bombScript;

    //bomb picking
    private bool picking = false;
    private GameObject bomb;
    [SerializeField]
    private GameObject shootPoint;
    private bool holdingBomb = false;
    [SerializeField]
    private float speedOfPickingBomb = 2f;

    // Start is called before the first frame update
    void Start()
    {
        PlayerCtrl = GetComponent<PlayerController>();
        //mainCam = GetComponent<Camera>();
        mask = LayerMask.GetMask("sBomb");
        picking = false;
        holdingBomb = false;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit, maxDistanceToPickup, mask))
        {
            pickupText.SetActive(true);
            if (Input.GetKeyDown("f") && !holdingBomb)
            {
                bombScript = hit.collider.gameObject.GetComponent<stckyBomb>();
                bomb = hit.collider.gameObject;
                holdingBomb = true;
                picking = true;
            }
        }
        else
            pickupText.SetActive(false);

        if (picking)
        {
            Vector3 target = shootPoint.transform.position;
            pickingBomb(bomb, target);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && holdingBomb)
        {
            //Debug.Log("eject bomb" + bomb);
            bomb.transform.parent = null;
            bomb.GetComponent<Rigidbody>().isKinematic = false;
            PlayerCtrl.ejectBomb(bomb);
            holdingBomb = false;
        }

    }

    void pickingBomb(GameObject bomb, Vector3 target)
    {
        
        if (Vector3.Distance(bomb.transform.position, target) > 0.5f)
        {
            bomb.transform.position = Vector3.Lerp(bomb.transform.position, target, speedOfPickingBomb * Time.deltaTime);
        }
        else
        {
            bomb.GetComponent<Rigidbody>().isKinematic = true;
            bomb.transform.SetParent(gameObject.transform);
            bomb.transform.position = shootPoint.transform.position;
            picking = false;
        }
    }

    
}