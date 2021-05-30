using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour
{
    public GameObject followObject;
    public float interpValue = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = followObject.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.Lerp(transform.position, followObject.transform.position, interpValue);
    }
}
