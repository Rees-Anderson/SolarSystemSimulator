using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* Original By Kelvin Sung
 * Modified By Rees Anderson
 * 11.18.21
 * CSS 451
 * MP4
 * 
 * Class Description:
 * Modified version of SceneNode that allows an object to be attached (the camera for MP4)
 */

public class SceneNode : MonoBehaviour
{

    protected Matrix4x4 mCombinedParentXform;

    public Vector3 NodeOrigin = Vector3.zero;
    public List<NodePrimitive> PrimitiveList;

    public Vector3 currentNodeLocation;

    public Transform SmallCam = null;

    // Use this for initialization
    protected void Start()
    {
        InitializeSceneNode();
        // Debug.Log("PrimitiveList:" + PrimitiveList.Count);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void InitializeSceneNode()
    {
        mCombinedParentXform = Matrix4x4.identity;
    }

    // This must be called _BEFORE_ each draw!! 
    public void CompositeXform(ref Matrix4x4 parentXform)
    {
        Matrix4x4 orgT = Matrix4x4.Translate(NodeOrigin);
        Matrix4x4 trs = Matrix4x4.TRS(transform.localPosition, transform.localRotation, transform.localScale);

        mCombinedParentXform = parentXform * orgT * trs;

        // propagate to all children
        foreach (Transform child in transform)
        {
            SceneNode cn = child.GetComponent<SceneNode>();
            if (cn != null)
            {
                cn.CompositeXform(ref mCombinedParentXform);
            }
        }

        // disenminate to primitives
        foreach (NodePrimitive p in PrimitiveList)
        {
            p.LoadShaderMatrix(ref mCombinedParentXform);
        }

        //Update Grabbable Location (for easy access for other objects)
        currentNodeLocation = mCombinedParentXform.MultiplyPoint(new Vector3(0, 0, 0));

        
        if (SmallCam != null)
        {
            SmallCam.localPosition = mCombinedParentXform.MultiplyPoint(new Vector3(0, 25000f, 0));

            Vector3 up = mCombinedParentXform.GetColumn(1).normalized;
            Vector3 forward = mCombinedParentXform.GetColumn(2).normalized;

            //Align Camera's foward (the way it's looking) with the up direction of the node
            float angle = Mathf.Acos(Vector3.Dot(Vector3.forward, up)) * Mathf.Rad2Deg;
            Vector3 axis = Vector3.Cross(Vector3.forward, up);
            SmallCam.localRotation = Quaternion.AngleAxis(angle, axis);

            // Now, align the up axis
            angle = Mathf.Acos(Vector3.Dot(SmallCam.transform.up, forward)) * Mathf.Rad2Deg;
            axis = Vector3.Cross(SmallCam.transform.up, forward);
            SmallCam.localRotation = Quaternion.AngleAxis(angle + 180.0f, axis) * SmallCam.localRotation;
        }
        
    }
}