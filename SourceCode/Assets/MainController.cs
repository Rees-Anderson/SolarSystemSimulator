using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/* Rees Anderson
 * 12.5.21
 * CSS 451
 * Final Project
 * 
 * Class Description:
 * Primary controller for the application, handles external input and UI, in turn modifying the model.
 */

public class MainController : MonoBehaviour
{
    public MainModel mainModel;
    public Camera mainCamera;
    public Dropdown viewTargetDropdown;
    public InputField timeScaleField;

    public Text[] informationTexts;

    private int previousViewDropdownValue = -1;

    public Text DockingStatusText;
    public Text SpeedText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ViewDropdownIndexCheck();
        ProcessTimeScaleField();
        UpdateInformationText();
        UpdateScoutText();
    }

    void UpdateScoutText()
    {
        if (mainModel.rocketIsDocked)
        {
            DockingStatusText.text = "Docked Status: Docked";
        }
        else
        {
            DockingStatusText.text = "Docked Status: Undocked";
        }

        SpeedText.text = "Current Velocity: " + mainModel.rocketScript.velocity.ToString() + " units per second";
    }

    void UpdateInformationText()
    {
        for (int i = 0; i < informationTexts.Length; i++)
        {
            if (i == viewTargetDropdown.value)
            {
                informationTexts[i].gameObject.SetActive(true);
            }
            else
            {
                informationTexts[i].gameObject.SetActive(false);
            }
        }
    }

    void ProcessTimeScaleField()
    {
        if (timeScaleField.text != null && timeScaleField.text != "")
        {
            mainModel.TimeScale = int.Parse(timeScaleField.text);
        }
        else
        {
            mainModel.TimeScale = 0;
            timeScaleField.text = "";
        }
    }

    void updateLookTargetLocation(Vector3 targetMovement)
    {
        //mainModel.cameraManipulation.updateLookAt = false;
        mainCamera.transform.position += targetMovement;
        mainModel.lookAtPosition.transform.position = mainModel.viewTargetObject.GetComponent<SceneNode>().currentNodeLocation;
        //mainModel.cameraManipulation.updateLookAt = true;
    }

    //Quits the Application
    public void Quit()
    {
        Debug.Log("Quit Button Pressed!");
        Application.Quit();
    }

    //Resets the Scene
    public void ReloadScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    //Checks the index of the dropdown and sets main model view target
    private void ViewDropdownIndexCheck()
    {
        if (viewTargetDropdown.value == 0)
        {
            mainModel.viewTargetObject = GameObject.Find("SunNode");
            mainModel.camZoomLimit = 1000f;

            updateLookTargetLocation(Vector3.zero);

            if (viewTargetDropdown.value != previousViewDropdownValue)
            {
                mainModel.cameraManipulation.MoveCameraLocation(5000f);
            }

            previousViewDropdownValue = viewTargetDropdown.value;
        }
        else if (viewTargetDropdown.value == 1)
        {
            mainModel.viewTargetObject = GameObject.Find("MercuryNode");
            mainModel.camZoomLimit = 10f;

            updateLookTargetLocation(mainModel.lastMercuryMove);

            if (viewTargetDropdown.value != previousViewDropdownValue)
            {
                mainModel.cameraManipulation.MoveCameraLocation(50f);
            }

            previousViewDropdownValue = viewTargetDropdown.value;
        }
        else if (viewTargetDropdown.value == 2)
        {
            mainModel.viewTargetObject = GameObject.Find("VenusNode");
            mainModel.camZoomLimit = 15f;

            updateLookTargetLocation(mainModel.lastVenusMove);

            if (viewTargetDropdown.value != previousViewDropdownValue)
            {
                mainModel.cameraManipulation.MoveCameraLocation(50f);
            }

            previousViewDropdownValue = viewTargetDropdown.value;
        }
        else if (viewTargetDropdown.value == 3)
        {
            mainModel.viewTargetObject = GameObject.Find("EarthNode");
            mainModel.camZoomLimit = 15f;

            updateLookTargetLocation(mainModel.lastEarthMove);

            if (viewTargetDropdown.value != previousViewDropdownValue)
            {
                mainModel.cameraManipulation.MoveCameraLocation(50f);
            }

            previousViewDropdownValue = viewTargetDropdown.value;
        }
        else if (viewTargetDropdown.value == 4)
        {
            mainModel.viewTargetObject = GameObject.Find("TheMoonNode");
            mainModel.camZoomLimit = 7f;

            updateLookTargetLocation(mainModel.lastEarthMove + mainModel.lastMoonMove);

            if (viewTargetDropdown.value != previousViewDropdownValue)
            {
                mainModel.cameraManipulation.MoveCameraLocation(50f);
            }

            previousViewDropdownValue = viewTargetDropdown.value;
        }
        else if (viewTargetDropdown.value == 5)
        {
            mainModel.viewTargetObject = GameObject.Find("MarsNode");
            mainModel.camZoomLimit = 12f;

            updateLookTargetLocation(mainModel.lastMarsMove);

            if (viewTargetDropdown.value != previousViewDropdownValue)
            {
                mainModel.cameraManipulation.MoveCameraLocation(50f);
            }

            previousViewDropdownValue = viewTargetDropdown.value;
        }
        else if (viewTargetDropdown.value == 6)
        {
            mainModel.viewTargetObject = GameObject.Find("PhobosNode");
            mainModel.camZoomLimit = 5f;

            updateLookTargetLocation(mainModel.lastMarsMove + mainModel.lastPhobosMove);

            if (viewTargetDropdown.value != previousViewDropdownValue)
            {
                mainModel.cameraManipulation.MoveCameraLocation(10f);
            }

            previousViewDropdownValue = viewTargetDropdown.value;
        }
        else if (viewTargetDropdown.value == 7)
        {
            mainModel.viewTargetObject = GameObject.Find("DeimosNode");
            mainModel.camZoomLimit = 5f;

            updateLookTargetLocation(mainModel.lastMarsMove + mainModel.lastDeimosMove);

            if (viewTargetDropdown.value != previousViewDropdownValue)
            {
                mainModel.cameraManipulation.MoveCameraLocation(10f);
            }

            previousViewDropdownValue = viewTargetDropdown.value;
        }
        else if (viewTargetDropdown.value == 8)
        {
            mainModel.viewTargetObject = GameObject.Find("JupiterNode");
            mainModel.camZoomLimit = 150f;

            updateLookTargetLocation(mainModel.lastJupiterMove);

            if (viewTargetDropdown.value != previousViewDropdownValue)
            {
                mainModel.cameraManipulation.MoveCameraLocation(300f);
            }

            previousViewDropdownValue = viewTargetDropdown.value;
        }
        else if (viewTargetDropdown.value == 9)
        {
            mainModel.viewTargetObject = GameObject.Find("IoNode");
            mainModel.camZoomLimit = 10f;

            updateLookTargetLocation(mainModel.lastJupiterMove + mainModel.lastIoMove);

            if (viewTargetDropdown.value != previousViewDropdownValue)
            {
                mainModel.cameraManipulation.MoveCameraLocation(50f);
            }

            previousViewDropdownValue = viewTargetDropdown.value;
        }
        else if (viewTargetDropdown.value == 10)
        {
            mainModel.viewTargetObject = GameObject.Find("EuropaNode");
            mainModel.camZoomLimit = 10f;

            updateLookTargetLocation(mainModel.lastJupiterMove + mainModel.lastEuropaMove);

            if (viewTargetDropdown.value != previousViewDropdownValue)
            {
                mainModel.cameraManipulation.MoveCameraLocation(50f);
            }

            previousViewDropdownValue = viewTargetDropdown.value;
        }
        else if (viewTargetDropdown.value == 11)
        {
            mainModel.viewTargetObject = GameObject.Find("GanymedeNode");
            mainModel.camZoomLimit = 10f;

            updateLookTargetLocation(mainModel.lastJupiterMove + mainModel.lastGanymedeMove);

            if (viewTargetDropdown.value != previousViewDropdownValue)
            {
                mainModel.cameraManipulation.MoveCameraLocation(50f);
            }

            previousViewDropdownValue = viewTargetDropdown.value;
        }
        else if (viewTargetDropdown.value == 12)
        {
            mainModel.viewTargetObject = GameObject.Find("CallistoNode");
            mainModel.camZoomLimit = 10f;

            updateLookTargetLocation(mainModel.lastJupiterMove + mainModel.lastCallistoMove);

            if (viewTargetDropdown.value != previousViewDropdownValue)
            {
                mainModel.cameraManipulation.MoveCameraLocation(50f);
            }

            previousViewDropdownValue = viewTargetDropdown.value;
        }
        else if (viewTargetDropdown.value == 13)
        {
            mainModel.viewTargetObject = GameObject.Find("SaturnNode");
            mainModel.camZoomLimit = 150f;

            updateLookTargetLocation(mainModel.lastSaturnMove);

            if (viewTargetDropdown.value != previousViewDropdownValue)
            {
                mainModel.cameraManipulation.MoveCameraLocation(300f);
            }

            previousViewDropdownValue = viewTargetDropdown.value;
        }
        else if (viewTargetDropdown.value == 14)
        {
            mainModel.viewTargetObject = GameObject.Find("RheaNode");
            mainModel.camZoomLimit = 10f;

            updateLookTargetLocation(mainModel.lastSaturnMove + mainModel.lastRheaMove);

            if (viewTargetDropdown.value != previousViewDropdownValue)
            {
                mainModel.cameraManipulation.MoveCameraLocation(50f);
            }

            previousViewDropdownValue = viewTargetDropdown.value;
        }
        else if (viewTargetDropdown.value == 15)
        {
            mainModel.viewTargetObject = GameObject.Find("TitanNode");
            mainModel.camZoomLimit = 10f;

            updateLookTargetLocation(mainModel.lastSaturnMove + mainModel.lastTitanMove);

            if (viewTargetDropdown.value != previousViewDropdownValue)
            {
                mainModel.cameraManipulation.MoveCameraLocation(50f);
            }

            previousViewDropdownValue = viewTargetDropdown.value;
        }
        else if (viewTargetDropdown.value == 16)
        {
            mainModel.viewTargetObject = GameObject.Find("IapetusNode");
            mainModel.camZoomLimit = 10f;

            updateLookTargetLocation(mainModel.lastSaturnMove + mainModel.lastIapetusMove);

            if (viewTargetDropdown.value != previousViewDropdownValue)
            {
                mainModel.cameraManipulation.MoveCameraLocation(50f);
            }

            previousViewDropdownValue = viewTargetDropdown.value;
        }
        else if (viewTargetDropdown.value == 17)
        {
            mainModel.viewTargetObject = GameObject.Find("UranusNode");
            mainModel.camZoomLimit = 150f;

            updateLookTargetLocation(mainModel.lastUranusMove);

            if (viewTargetDropdown.value != previousViewDropdownValue)
            {
                mainModel.cameraManipulation.MoveCameraLocation(300f);
            }

            previousViewDropdownValue = viewTargetDropdown.value;
        }
        else if (viewTargetDropdown.value == 18)
        {
            mainModel.viewTargetObject = GameObject.Find("TitaniaNode");
            mainModel.camZoomLimit = 10f;

            updateLookTargetLocation(mainModel.lastUranusMove + mainModel.lastTitaniaMove);

            if (viewTargetDropdown.value != previousViewDropdownValue)
            {
                mainModel.cameraManipulation.MoveCameraLocation(50f);
            }

            previousViewDropdownValue = viewTargetDropdown.value;
        }
        else if (viewTargetDropdown.value == 19)
        {
            mainModel.viewTargetObject = GameObject.Find("OberonNode");
            mainModel.camZoomLimit = 10f;

            updateLookTargetLocation(mainModel.lastUranusMove + mainModel.lastOberonMove);

            if (viewTargetDropdown.value != previousViewDropdownValue)
            {
                mainModel.cameraManipulation.MoveCameraLocation(50f);
            }

            previousViewDropdownValue = viewTargetDropdown.value;
        }
        else if (viewTargetDropdown.value == 20)
        {
            mainModel.viewTargetObject = GameObject.Find("NeptuneNode");
            mainModel.camZoomLimit = 150f;

            updateLookTargetLocation(mainModel.lastNeptuneMove);

            if (viewTargetDropdown.value != previousViewDropdownValue)
            {
                mainModel.cameraManipulation.MoveCameraLocation(300f);
            }

            previousViewDropdownValue = viewTargetDropdown.value;
        }
        else if (viewTargetDropdown.value == 21)
        {
            mainModel.viewTargetObject = GameObject.Find("TritonNode");
            mainModel.camZoomLimit = 10f;

            updateLookTargetLocation(mainModel.lastNeptuneMove + mainModel.lastTritonMove);

            if (viewTargetDropdown.value != previousViewDropdownValue)
            {
                mainModel.cameraManipulation.MoveCameraLocation(50f);
            }

            previousViewDropdownValue = viewTargetDropdown.value;
        }
    }
}
