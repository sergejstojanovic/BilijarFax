using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    //Player Movement
    private Vector3 velocity = Vector3.zero;
    [SerializeField]
    private float speed = 50f;
    public Camera mainCam;
    private ConfigurableJoint cjoint;
    [SerializeField] private float flyingSpeed = -1.0f;
    public float flyingHeight = 0f;

    //Sticky bomb
    public GameObject sBomb;
    public GameObject shootPoint;
    public int bombVelocity = 40;
    private int maxNumberOfBombs = 3;
    [HideInInspector]
    public int numberOfBombs = 0;

    //Game Mechanics
    [HideInInspector]
    public bool readyToDetonate = false;
    [SerializeField] public float detonatingHeight = 20.0f;
    [SerializeField] private GameObject newRoundTxt;

    //Reference to scripts
    private followingText ballNumScript;


    // Start is called before the first frame update
    void Start()
    {
        ballNumScript = sBomb.GetComponent<followingText>();
        readyToDetonate = false;
        rb = GetComponent<Rigidbody>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        //Sticky bomb:
        //Debug.Log("playerSCalled");
        //numberOfBombs = 0;
        //sBomb = GetComponent<GameObject>();
        cjoint = GetComponent<ConfigurableJoint>();
        
    }

    // Update is called once per frame
    void Update()
    {

            //Calculate movement direction---------------------------------------------------------------------------
            float xMov = Input.GetAxisRaw("Horizontal");
            float yMov = Input.GetAxisRaw("Vertical");

            Vector3 moveHorizontal = transform.right * xMov;
            Vector3 moveVertical = transform.forward * yMov;

            velocity = (moveHorizontal + moveVertical).normalized * speed;

            //Calculate rotation-----------------------------------------------------------------------------------
            float yRot = Input.GetAxisRaw("Mouse X");


        if (!readyToDetonate)
        {

            //Player FLYING------------------------------------------------------------------------------------------
            if (Input.GetButton("Jump") && flyingHeight >= -5)
            {
                //Debug.Log("flyyy");
                flyingHeight += -flyingSpeed * Time.deltaTime;
                cjoint.targetPosition = new Vector3(0, flyingHeight, 0);
            }
            else if (Input.GetButton("flyDown") && flyingHeight <= 0)
            {
                flyingHeight += flyingSpeed * Time.deltaTime;
                cjoint.targetPosition = new Vector3(0, flyingHeight, 0);
            }

            //rotation of bomb direction ( up, down );------------------------------------------------------------------
            rotateTrajectoryLine();

               // Debug.Log(numberOfBombs);
            //instantiate and eject bomb---------------------------------------------------------------------------------
            if (Input.GetKeyDown(KeyCode.Mouse0) && (numberOfBombs <= maxNumberOfBombs))
            {
                GameObject createdBomb = Instantiate(sBomb, shootPoint.transform.position, transform.rotation);
                ejectBomb(createdBomb);

            }

        }
        else
        {
            if(cjoint.targetPosition != new Vector3(0, flyingHeight, 0))
                cjoint.targetPosition = new Vector3(0, flyingHeight, 0);
            if(numberOfBombs <= 1)
            {
                newRoundTxt.SetActive(true);

                if (Input.GetKeyDown("space"))
                {
                    readyToDetonate = false;
                    transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
                    flyingHeight = 0;
                    numberOfBombs = 1;
                    ballNumScript.bombNumber1.SetActive(false);
                    ballNumScript.bombNumber2.SetActive(false);
                    ballNumScript.bombNumber3.SetActive(false);
                    newRoundTxt.SetActive(false);
                }
            }
        }
    }

    //Run every physics iteration
    void FixedUpdate()
    {
        //rotation = mainCam.transform.;
        PerformRotation();
        PerformMovement();
    }

    //rotate up/down
    void rotateTrajectoryLine()
    {
        
        shootPoint.transform.rotation = Quaternion.Euler(new Vector3(mainCam.transform.rotation.eulerAngles.x + 50, mainCam.transform.rotation.eulerAngles.y, mainCam.transform.rotation.eulerAngles.z));
    }

    //Rotate Player
    void PerformRotation()
    {
        rb.MoveRotation( Quaternion.Euler(new Vector3(0,mainCam.transform.rotation.eulerAngles.y,0)));
    }

    //ejectBomb
    public void ejectBomb(GameObject bomb)
    {
        bomb.GetComponent<Rigidbody>().velocity = shootPoint.transform.up * bombVelocity;
    }

    //Move Player
    void PerformMovement()
    {

        if(velocity != Vector3.zero)
        {
            rb.MovePosition(transform.position + velocity * Time.deltaTime);
        }

    }
}
