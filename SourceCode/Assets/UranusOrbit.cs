using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Rees Anderson
 * 12.5.21
 * CSS 451
 * Final Project
 * 
 * Class Description:
 * Controls Uranus's Orbit around the sun
 */

public class UranusOrbit : MonoBehaviour
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

        //Counterclockwise - all planets orbit the sun counterclockwise
        //Complete a 360 degree orbit once every 30,688.5 days - orbit amount, time scale, time scale offset to convert to days, personal rotation modifier (If this is 1 it is once per day), Time.deltaTime
        transform.RotateAround(orbitTarget.transform.position, Vector3.up, -1.0f * 360.0f * mainModel.TimeScale * 0.000011574f * (1.0f / 30688.5f) * Time.deltaTime);

        mainModel.lastUranusMove = transform.position - lastLocation;
    }
}