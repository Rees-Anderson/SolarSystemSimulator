using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Rees Anderson
 * 12.5.21
 * CSS 451
 * Final Project
 * 
 * Class Description:
 * Controls a cylinder to be a line between two celestial objects
 */

public class PlanetaryLine : MonoBehaviour
{
    public GameObject P0;
    public GameObject P1;

    public float lineDiameter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        updateTransform();
    }

    public void updateTransform()
    {
        Vector3 P0Pos = P0.GetComponent<SceneNode>().currentNodeLocation;
        Vector3 P1Pos = P1.GetComponent<SceneNode>().currentNodeLocation;
        Vector3 d = P1Pos - P0Pos;
        transform.up = d.normalized;
        transform.position = P0.transform.position + (0.5f * d);
        transform.localScale = new Vector3(lineDiameter, d.magnitude * 0.5f, lineDiameter);
    }
}
