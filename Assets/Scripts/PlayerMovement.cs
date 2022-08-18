using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{  
    public Transform pivBone,Bone ;
    public float _sensitivity = 0.7f , sens = 0.2f , sensCamY = 12;
    private Vector3 _mouseReference ;
    private Vector3 _mouseOffset;
    private Vector3 _rotation = Vector3.zero , rot = Vector3.zero;
    private bool _isRotating;
    public Vector3 Offset;
    public GameObject Cross; 
    private void LateUpdate()
    {
        Bone.LookAt(pivBone);
        Bone.rotation = Bone.rotation * Quaternion.Euler(Offset);        
        
        if (Input.GetMouseButton(0))
        {
            if (_isRotating) {
                // offset
                _mouseOffset = (Input.mousePosition - _mouseReference);          
                // apply rotation
                _rotation.y = (_mouseOffset.x) * _sensitivity;
                _rotation.z = -(_mouseOffset.y) * _sensitivity;
                rot.y = (_mouseOffset.y) * sens;
                // rotate
                transform.eulerAngles += _rotation;               
                var x = transform.eulerAngles;
                /*
                var xx = pivBone.localPosition;
                float angle = xx.z;
                angle = (angle > 180) ? angle - 360 : angle;
                xx.z = Mathf.Clamp(angle, -19, 10);
                */
                var c = Cross.transform.localEulerAngles;
                c.x += -(rot.y * sensCamY);

                Cross.transform.localEulerAngles = c;

                pivBone.localPosition += rot;

                transform.eulerAngles = new Vector3(0,x.y,0);          
                // store new mouse position
                _mouseReference = Input.mousePosition;
                
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            _isRotating = true;
            _mouseReference = Input.mousePosition;
          
        }
        if (Input.GetMouseButtonUp(0))
        {
            _isRotating = false;
        }
    }
}
