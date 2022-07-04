using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Rees Anderson
 * 12.7.21
 * CSS 451
 * Final Project
 * 
 * Class Description:
 * Controls Triton's Orbit around Neptune
 */

public class TritonOrbit : MonoBehaviour
{
    public MainModel mainModel;
    public GameObject orbitTarget;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lastLocation = transform.position;

        //Clockwise
        //Complete a 360 degree orbit once every 5.876854 days - orbit amount, time scale, time scale offset to convert to days, personal rotation modifier (1/87.97 - if this is 1 it is once per day), Time.deltaTime
        transform.RotateAround(orbitTarget.transform.position, Vector3.up, 360.0f * mainModel.TimeScale * 0.000011574f * (1.0f / 5.876854f) * Time.deltaTime);

        mainModel.lastTritonMove = transform.position - lastLocation;
    }
}
