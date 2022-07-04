using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Original By Kelvin Sung
 * Modified By Rees Anderson
 * 12.5.21
 * Final Project
 * 
 * Modified version of TexturePlacement to use Matrix3x3 and is specifically used to "rotate" Mercury by offsetting the texture over time.
*/

public class MercuryRotation : MonoBehaviour
{
    public MainModel mainModel;

    private Vector2 Offset = Vector2.zero;
    private Vector2 Scale = Vector2.one;
    private float Rotation = 0.0f;

    Vector2[] mInitUV = null; // initial values

    private void Start()
    {
        Mesh theMesh = GetComponent<MeshFilter>().mesh;
        Vector2[] uv = theMesh.uv;

        SaveInitUV(uv);
    }

    public void SaveInitUV(Vector2[] uv)
    {
        mInitUV = new Vector2[uv.Length];
        for (int i = 0; i < uv.Length; i++)
            mInitUV[i] = uv[i];
    }

    // Update is called once per frame
    void Update()
    {
        Mesh theMesh = GetComponent<MeshFilter>().mesh;
        Vector2[] uv = theMesh.uv;

        Offset = new Vector2(mainModel.MercuryRotation, 0.0f);
        //Scale = 
        //Rotation = 

        Matrix3x3 T = Matrix3x3Helpers.CreateTranslation(Offset);
        Matrix3x3 S = Matrix3x3Helpers.CreateScale(Scale); //Do nothing
        Matrix3x3 R = Matrix3x3Helpers.CreateRotation(Rotation); //Do nothing

        for (int i = 0; i < uv.Length; i++)
        {
            uv[i] = T * R * S * mInitUV[i];
        }
        theMesh.uv = uv;
    }
}
