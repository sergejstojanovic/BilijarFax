using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ostaleKugle : MonoBehaviour
{

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collInfo) { 
        if(collInfo.collider.tag == "rupa")
        {
            rb.isKinematic = true;
            StartCoroutine(WaitAndDestroy(0.5f, collInfo.collider.transform));
        }

    }

    private IEnumerator WaitAndDestroy(float waitTime, Transform onEnter)
    {
        Vector3 target = new Vector3(onEnter.position.x, onEnter.position.y + 4, onEnter.position.z);
        while (Vector3.Distance(transform.position, target) > 0.05f)
        {
            transform.position = Vector3.Lerp(transform.position, target, 1f * Time.deltaTime);

            yield return null;
        }

        Debug.Log("started corutine");

        yield return new WaitForSeconds(waitTime);
        
        Debug.Log("started corutine");
        Destroy(this.gameObject);
    }
}
