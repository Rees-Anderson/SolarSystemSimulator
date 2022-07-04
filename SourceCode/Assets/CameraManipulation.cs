using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Rees Anderson
 * 11.18.21
 * CSS 451
 * MP4
 * 
 * Class Description:
 * Controls camera panning, zooming, and tumbling.
 * Tumbing components and computing look at adapted from Kevin Sung's versions
 */

public class CameraManipulation : MonoBehaviour
{
    public MainModel mainModel;
    public Transform LookAtPosition = null;

    private float scrollSpeed = 10000.0f;
    private float tumbleSpeed = 1.0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ComputeLookAt();
        ComputeMouseZoom();
        ComputeMouseTumble();
    }

    public void MoveCameraLocation(float distance)
    {
        Vector3 temp = LookAtPosition.position;

        temp.z -= distance;

        transform.position = temp;

        transform.up = Vector3.up;
    }
    
    void ComputeLookAt()
    {
        // Viewing vector is from transform.localPosition to the look at position
        Vector3 V = LookAtPosition.localPosition - transform.localPosition;
        Vector3 W = Vector3.Cross(-V, transform.up);
        Vector3 U = Vector3.Cross(W, -V);
        transform.localRotation = Quaternion.LookRotation(V, U);
    }

    void ComputeMouseZoom()
    {
        //Determine if a zoom should be allowed to take place: distance is not too small (prevent passthrough) or just zooming out (no conditions restricting zoom out)
        bool allowZoom = (transform.position - LookAtPosition.position).magnitude > mainModel.camZoomLimit || Input.GetAxis("Mouse ScrollWheel") < 0;

        //Prevent movement when using UI
        if ((Input.mousePosition - new Vector3(Screen.width, Screen.height)).magnitude > 250 && (Input.mousePosition - new Vector3(Screen.width, 0)).magnitude > 250 && allowZoom)
        {
            transform.position += transform.forward * scrollSpeed * Input.GetAxis("Mouse ScrollWheel") * Time.smoothDeltaTime;
        }
    }

    void ComputeMouseTumble()
    {
        //Left Mouse Button + Prevent movement when using UI
        if ((Input.mousePosition - new Vector3(Screen.width, Screen.height)).magnitude > 250 && (Input.mousePosition - new Vector3(Screen.width, 0)).magnitude > 250 && Input.GetMouseButton(0))
        {
            //Old Implementation - Doesn't work :(
            //transform.position += transform.right * tumbleSpeed * Input.GetAxis("Mouse X") * Time.smoothDeltaTime;
            //transform.position += transform.up * tumbleSpeed * Input.GetAxis("Mouse Y") * Time.smoothDeltaTime;

            //Rotate based on CAMERA's right axis (Orbits vertically)
            Quaternion q = Quaternion.AngleAxis(-1.0f * Input.GetAxis("Mouse Y") * tumbleSpeed, transform.right); //Added -1.0f to make rotation more intuitive (like grabbing the object)
            Matrix4x4 r = Matrix4x4.Rotate(q);
            Matrix4x4 invP = Matrix4x4.TRS(-LookAtPosition.localPosition, Quaternion.identity, Vector3.one);
            r = invP.inverse * r * invP;
            Vector3 newCameraPos = r.MultiplyPoint(transform.localPosition);

            //Allow the transform only if the angle with the vertical will be less than 80 degrees
            if (Mathf.Abs(Vector3.Dot(newCameraPos.normalized, Vector3.up)) < 0.99f) // this is about 80-degrees
            {
                transform.localPosition = newCameraPos;
                transform.localRotation = q * transform.localRotation;
            }

            //Rotate based on UNIVERSAL up axis (Orbits horozontally)
            q = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * tumbleSpeed, Vector3.up);
            r = Matrix4x4.Rotate(q);
            invP = Matrix4x4.TRS(-LookAtPosition.localPosition, Quaternion.identity, Vector3.one);
            r = invP.inverse * r * invP;
            newCameraPos = r.MultiplyPoint(transform.localPosition);
            transform.localPosition = newCameraPos;

            transform.localRotation = q * transform.localRotation;
        }
    }
}