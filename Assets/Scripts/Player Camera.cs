using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    //Target is the player.
    public GameObject target;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //used to make the camera follow the player on X,Y and Z.
        transform.position = new Vector3(target.transform.position.x, transform.position.y, -10);
    }
}
