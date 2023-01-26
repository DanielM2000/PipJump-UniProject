using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    public bool _isDoorOpen = false;

    Vector3 _doorClosedpos;

    Vector3 _doorOpenpos;

    float _doorSpeed = 10f;

    private void Awake()
    {
        _doorClosedpos = transform.position;
        _doorOpenpos = new Vector3(transform.position.x,
            transform.position.y + 3f, transform.position.z);
    }

    private void Update()
    {
        if(_isDoorOpen)
        {
            OpenDoor();

        }
        else if (!_isDoorOpen)
        {
            CloseDoor();
        }
    }

    //checks to see if door is already open.
    void OpenDoor()
    {
        if(transform.position != _doorOpenpos)
        {
            transform.position = Vector3.MoveTowards(transform.position, _doorOpenpos,
                _doorSpeed * Time.deltaTime);
        }
    }

    //checks to see if door is already closed.
    void CloseDoor()
    {
        if (transform.position != _doorClosedpos)
        {
            transform.position = Vector3.MoveTowards(transform.position,_doorClosedpos,
                _doorSpeed * Time.deltaTime);
        }
    }

}
