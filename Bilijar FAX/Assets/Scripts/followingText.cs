using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class followingText : MonoBehaviour
{

    [SerializeField]
    public GameObject bombNumber1;
    [SerializeField]
    public GameObject bombNumber2;
    [SerializeField]
    public GameObject bombNumber3;

    public Camera cam;

    //reference to other scripts
    private stckyBomb bombScript;
    
    // Start is called before the first frame update
    void Start()
    {
        bombScript = this.gameObject.GetComponent<stckyBomb>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = cam.WorldToScreenPoint(transform.position);

        
        if (bombNumber1.transform.position != pos && bombScript.id == 1)
        {
            bombNumber1.SetActive(true);
            //bombNumber1.transform.SetParent(gameObject.transform);
            bombNumber1.transform.position = pos;
            //Debug.Log("laliii");
        }
        else if(bombNumber2.transform.position != pos && bombScript.id == 2)
        {
            bombNumber2.SetActive(true);
            //bombNumber2.transform.SetParent(gameObject.transform);
            bombNumber2.transform.position = pos;
        }
        else if(bombNumber3.transform.position != pos && bombScript.id == 3)
        {
            bombNumber3.SetActive(true);
            //bombNumber3.transform.SetParent(gameObject.transform);
            bombNumber3.transform.position = pos;
        }
    }
}
