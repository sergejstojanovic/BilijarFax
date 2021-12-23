using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class TrajectoryLine : MonoBehaviour
{

    private Rigidbody rb;
    private PlayerController playerScript;
    private LineRenderer lr;

    [SerializeField]
    private int numPoints = 50;

    [SerializeField]
    private float timeBetweenPoints = 0.1f;

    //Layer that will stop line being drawn
    [SerializeField]
    private LayerMask collidableLayers;
    List<Vector3> points ;
    

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GetComponent<PlayerController>();
        lr = GetComponent<LineRenderer>();
        points = new List<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {

        lr.positionCount = numPoints;
        Vector3 startingPoint = playerScript.shootPoint.transform.position;
        Vector3 startingVelocity = playerScript.shootPoint.transform.up * playerScript.bombVelocity;
        for(float i = 0; i < numPoints; i += timeBetweenPoints)
        {
            
            Vector3 newPoint = startingPoint + i * startingVelocity;
            newPoint.y = startingPoint.y + startingVelocity.y * i + Physics.gravity.y / 2f * i * i;
            points.Add(newPoint);
            if(Physics.OverlapSphere(newPoint, 0.1f, collidableLayers).Length > 0)
            {
                lr.positionCount = points.Count;
                break;
            }
            //Debug.Log();
        }
        lr.SetPositions(points.ToArray());
            points.Clear();
        }
        else
        {
            lr.positionCount = 0;
        }
        

    }
}
