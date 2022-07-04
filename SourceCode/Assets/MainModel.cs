using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Rees Anderson with components taken from lecture
 * 12.5.21
 * CSS 451
 * Final Project
 * 
 * Class Description:
 * Primary model for the application, stores the state of the application, modified by the MainController
 */

[ExecuteInEditMode]

public class MainModel : MonoBehaviour
{
    public SceneNode Level0;
    public MainController mainController;
    public GameObject currentlySelectedObject = null;
    public GameObject lookAtPosition = null;
    public GameObject viewTargetObject = null;

    public GameObject[] planetLines;
    public GameObject[] moonLines;

    public Toggle canSeePlanetLines;
    public Toggle canSeeMoonLines;

    public CameraManipulation cameraManipulation;

    public float camZoomLimit = 5.0f;

    public Vector3 lastMercuryMove;
    public Vector3 lastVenusMove;
    public Vector3 lastEarthMove;
    public Vector3 lastMarsMove;
    public Vector3 lastJupiterMove;
    public Vector3 lastSaturnMove;
    public Vector3 lastUranusMove;
    public Vector3 lastNeptuneMove;

    public Vector3 lastMoonMove;
    public Vector3 lastPhobosMove;
    public Vector3 lastDeimosMove;
    public Vector3 lastIoMove;
    public Vector3 lastEuropaMove;
    public Vector3 lastGanymedeMove;
    public Vector3 lastCallistoMove;
    public Vector3 lastRheaMove;
    public Vector3 lastTitanMove;
    public Vector3 lastIapetusMove;
    public Vector3 lastTitaniaMove;
    public Vector3 lastOberonMove;
    public Vector3 lastTritonMove;

    public float TimeScale = 1.0f; //Real time at 1.0f - One day per second at 86,400.0f
    public float SunRotation = 0.0f;
    public float MercuryRotation = 0.0f;
    public float VenusRotation = 0.0f;
    public float EarthRotation = 0.0f;
    public float MarsRotation = 0.0f;
    public float JupiterRotation = 0.0f;
    public float SaturnRotation = 0.0f;
    public float UranusRotation = 0.0f;
    public float NeptuneRotation = 0.0f;

    public SceneNode rocket;
    public TerraformingRocket rocketScript;
    public bool rocketIsDocked = true;
    public bool previousDockedState = true;

    public SceneNode rocketTip;
    public SceneNode theSun;
    public SceneNode[] TerraformingCandidates;
    public GameObject[] TerraformingCandidateObjects;

    public Texture terraformedPlanetTexture;

    public GameObject scoutCameraBlock;

    private void Start()
    {

    }

    private void Update()
    {
        Matrix4x4 i = Matrix4x4.identity;
        Level0.CompositeXform(ref i);

        updateLinesBetweenPlanets();
        updateLinesBetweenPlanetAndMoons();

        UpdateSunRotation();
        UpdateMercuryRotation();
        UpdateVenusRotation();
        UpdateEarthRotation();
        UpdateMarsRotation();
        UpdateJupiterRotation();
        UpdateSaturnRotation();
        UpdateUranusRotation();
        UpdateNeptuneRotation();

        //All other celestial objects are tidally locked meaning rotation calculation is unnecessary for the model

        updateDockedRocketLocation();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rocketIsDocked = !rocketIsDocked;
        }

        CheckRocketProximityToObjects();
        UpdateScoutCamBlocker();
    }

    void UpdateScoutCamBlocker()
    {
        if (rocketIsDocked)
        {
            scoutCameraBlock.SetActive(true);
        }
        else
        {
            scoutCameraBlock.SetActive(false);
        }
    }

    void CheckRocketProximityToObjects()
    {
        //The Sun
        if ((rocketTip.currentNodeLocation - theSun.currentNodeLocation).magnitude < 696.0f)
        {
            rocketIsDocked = true;
        }

        //Mercury
        if ((rocketTip.currentNodeLocation - TerraformingCandidates[0].currentNodeLocation).magnitude < 2.440f && !rocketIsDocked)
        {
            TerraformingCandidateObjects[0].GetComponent<Renderer>().material.SetTexture("_MainTex", terraformedPlanetTexture);
            rocketIsDocked = true;
        }

        //Venus
        if ((rocketTip.currentNodeLocation - TerraformingCandidates[1].currentNodeLocation).magnitude < 6.052f && !rocketIsDocked)
        {
            TerraformingCandidateObjects[1].GetComponent<Renderer>().material.SetTexture("_MainTex", terraformedPlanetTexture);
            rocketIsDocked = true;
        }

        //Earth
        if ((rocketTip.currentNodeLocation - TerraformingCandidates[2].currentNodeLocation).magnitude < 6.371f && !rocketIsDocked)
        {
            //TerraformingCandidateObjects[2].GetComponent<Renderer>().material.SetTexture("_MainTex", terraformedPlanetTexture);
            rocketIsDocked = true;
        }

        //The Moon
        if ((rocketTip.currentNodeLocation - TerraformingCandidates[3].currentNodeLocation).magnitude < 1.737f && !rocketIsDocked)
        {
            TerraformingCandidateObjects[3].GetComponent<Renderer>().material.SetTexture("_MainTex", terraformedPlanetTexture);
            rocketIsDocked = true;
        }

        //Mars
        if ((rocketTip.currentNodeLocation - TerraformingCandidates[4].currentNodeLocation).magnitude < 3.389f && !rocketIsDocked)
        {
            TerraformingCandidateObjects[4].GetComponent<Renderer>().material.SetTexture("_MainTex", terraformedPlanetTexture);
            rocketIsDocked = true;
        }

        //Jupiter
        if ((rocketTip.currentNodeLocation - TerraformingCandidates[5].currentNodeLocation).magnitude < 69.911f && !rocketIsDocked)
        {
            TerraformingCandidateObjects[5].GetComponent<Renderer>().material.SetTexture("_MainTex", terraformedPlanetTexture);
            rocketIsDocked = true;
        }

        //Io
        if ((rocketTip.currentNodeLocation - TerraformingCandidates[6].currentNodeLocation).magnitude < 1.821f && !rocketIsDocked)
        {
            TerraformingCandidateObjects[6].GetComponent<Renderer>().material.SetTexture("_MainTex", terraformedPlanetTexture);
            rocketIsDocked = true;
        }

        //Europa
        if ((rocketTip.currentNodeLocation - TerraformingCandidates[7].currentNodeLocation).magnitude < 1.56f && !rocketIsDocked)
        {
            TerraformingCandidateObjects[7].GetComponent<Renderer>().material.SetTexture("_MainTex", terraformedPlanetTexture);
            rocketIsDocked = true;
        }

        //Ganymede
        if ((rocketTip.currentNodeLocation - TerraformingCandidates[8].currentNodeLocation).magnitude < 2.634f && !rocketIsDocked)
        {
            TerraformingCandidateObjects[8].GetComponent<Renderer>().material.SetTexture("_MainTex", terraformedPlanetTexture);
            rocketIsDocked = true;
        }

        //Callisto
        if ((rocketTip.currentNodeLocation - TerraformingCandidates[9].currentNodeLocation).magnitude < 2.410f && !rocketIsDocked)
        {
            TerraformingCandidateObjects[9].GetComponent<Renderer>().material.SetTexture("_MainTex", terraformedPlanetTexture);
            rocketIsDocked = true;
        }

        //Saturn
        if ((rocketTip.currentNodeLocation - TerraformingCandidates[10].currentNodeLocation).magnitude < 58.232f && !rocketIsDocked)
        {
            TerraformingCandidateObjects[10].GetComponent<Renderer>().material.SetTexture("_MainTex", terraformedPlanetTexture);
            rocketIsDocked = true;
        }

        //Rhea
        if ((rocketTip.currentNodeLocation - TerraformingCandidates[11].currentNodeLocation).magnitude < 0.763f && !rocketIsDocked)
        {
            TerraformingCandidateObjects[11].GetComponent<Renderer>().material.SetTexture("_MainTex", terraformedPlanetTexture);
            rocketIsDocked = true;
        }

        //Titan
        if ((rocketTip.currentNodeLocation - TerraformingCandidates[12].currentNodeLocation).magnitude < 2.574f && !rocketIsDocked)
        {
            TerraformingCandidateObjects[12].GetComponent<Renderer>().material.SetTexture("_MainTex", terraformedPlanetTexture);
            rocketIsDocked = true;
        }

        //Iapetus
        if ((rocketTip.currentNodeLocation - TerraformingCandidates[13].currentNodeLocation).magnitude < 0.734f && !rocketIsDocked)
        {
            TerraformingCandidateObjects[13].GetComponent<Renderer>().material.SetTexture("_MainTex", terraformedPlanetTexture);
            rocketIsDocked = true;
        }

        //Uranus
        if ((rocketTip.currentNodeLocation - TerraformingCandidates[14].currentNodeLocation).magnitude < 25.362f && !rocketIsDocked)
        {
            TerraformingCandidateObjects[14].GetComponent<Renderer>().material.SetTexture("_MainTex", terraformedPlanetTexture);
            rocketIsDocked = true;
        }

        //Titania
        if ((rocketTip.currentNodeLocation - TerraformingCandidates[15].currentNodeLocation).magnitude < 0.788f && !rocketIsDocked)
        {
            TerraformingCandidateObjects[15].GetComponent<Renderer>().material.SetTexture("_MainTex", terraformedPlanetTexture);
            rocketIsDocked = true;
        }

        //Oberon
        if ((rocketTip.currentNodeLocation - TerraformingCandidates[16].currentNodeLocation).magnitude < 0.761f && !rocketIsDocked)
        {
            TerraformingCandidateObjects[16].GetComponent<Renderer>().material.SetTexture("_MainTex", terraformedPlanetTexture);
            rocketIsDocked = true;
        }

        //Neptune
        if ((rocketTip.currentNodeLocation - TerraformingCandidates[17].currentNodeLocation).magnitude < 24.622f && !rocketIsDocked)
        {
            TerraformingCandidateObjects[17].GetComponent<Renderer>().material.SetTexture("_MainTex", terraformedPlanetTexture);
            rocketIsDocked = true;
        }

        //Triton
        if ((rocketTip.currentNodeLocation - TerraformingCandidates[18].currentNodeLocation).magnitude < 1.353f && !rocketIsDocked)
        {
            TerraformingCandidateObjects[18].GetComponent<Renderer>().material.SetTexture("_MainTex", terraformedPlanetTexture);
            rocketIsDocked = true;
        }
    }

    void updateDockedRocketLocation()
    {
        if (rocketIsDocked)
        {
            rocket.transform.up = mainController.mainCamera.transform.forward;
            rocket.transform.position = mainController.mainCamera.transform.position + (rocket.transform.forward * -10.0f) + (rocket.transform.up * -10.0f);
        }

        if (rocketIsDocked == false && previousDockedState == true)
        {
            rocketScript.velocity = 25.0f;
            previousDockedState = false;
        }
        else if (rocketIsDocked == true && previousDockedState == false)
        {
            rocketScript.velocity = 0.0f;
            previousDockedState = true;
        }
    }

    public void updateLinesBetweenPlanets()
    {
        if (canSeePlanetLines.isOn)
        {
            for (int i = 0; i < planetLines.Length; i++)
            {
                planetLines[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < planetLines.Length; i++)
            {
                planetLines[i].SetActive(false);
            }
        }
    }

    public void updateLinesBetweenPlanetAndMoons()
    {
        if (canSeeMoonLines.isOn)
        {
            for (int i = 0; i < moonLines.Length; i++)
            {
                moonLines[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < moonLines.Length; i++)
            {
                moonLines[i].SetActive(false);
            }
        }
    }

    void UpdateSunRotation()
    {
        if (SunRotation <= -1) //counterclockwise
        {
            SunRotation = 0;
        }
        else
        {
            SunRotation -= TimeScale * 0.000011574f * (1.0f / 25.05f) * Time.deltaTime; //complete rotation every 25.05 earth days
        }
    }

    void UpdateMercuryRotation()
    {
        if (MercuryRotation <= -1) //counterclockwise
        {
            MercuryRotation = 0;
        }
        else
        {
            MercuryRotation -= TimeScale * 0.000011574f * (1.0f / 59.0f) * Time.deltaTime; //complete rotation every 59 earth days
        }
    }

    void UpdateVenusRotation()
    {
        if (VenusRotation >= 1) //clockwise
        {
            VenusRotation = 0;
        }
        else
        {
            VenusRotation += TimeScale * 0.000011574f * (1.0f / 243.0f) * Time.deltaTime; //complete rotation every 243 earth days
        }
    }

    void UpdateEarthRotation()
    {
        if (EarthRotation <= -1) //counterclockwise
        {
            EarthRotation = 0;
        }
        else
        {
            EarthRotation -= TimeScale * 0.000011574f * Time.deltaTime; //complete rotation once per day
        }
    }

    void UpdateMarsRotation()
    {
        if (MarsRotation <= -1) //counterclockwise
        {
            MarsRotation = 0;
        }
        else
        {
            MarsRotation -= TimeScale * 0.000011574f * (24.0f / 24.6f) * Time.deltaTime; //complete rotation every 24.6 earth hours
        }
    }

    void UpdateJupiterRotation()
    {
        if (JupiterRotation <= -1) //counterclockwise
        {
            JupiterRotation = 0;
        }
        else
        {
            JupiterRotation -= TimeScale * 0.000011574f * (24.0f / 9.8f) * Time.deltaTime; //complete rotation every 9.8 earth hours
        }
    }

    void UpdateSaturnRotation()
    {
        if (SaturnRotation <= -1) //counterclockwise
        {
            SaturnRotation = 0;
        }
        else
        {
            SaturnRotation -= TimeScale * 0.000011574f * (24.0f / 10.5f) * Time.deltaTime; //complete rotation every 10.5 earth hours
        }
    }

    void UpdateUranusRotation()
    {
        if (UranusRotation >= 1) //clockwise (and rotates on its side - this is done by rotating the gameObject 90 degrees)
        {
            UranusRotation = 0;
        }
        else
        {
            UranusRotation += TimeScale * 0.000011574f * (24.0f / 17.0f) * Time.deltaTime; //complete rotation every 17 earth hours
        }
    }

    void UpdateNeptuneRotation()
    {
        if (NeptuneRotation <= -1) //counterclockwise
        {
            NeptuneRotation = 0;
        }
        else
        {
            NeptuneRotation -= TimeScale * 0.000011574f * (24.0f / 16.0f) * Time.deltaTime; //complete rotation every 16 earth hours
        }
    }
}
