using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class SwitchColor : MonoBehaviour
{
    public bool updateColorAndCollision;
    public Material materialGreen;
    public Material materialRed;

    public Color greenOpaque;
    public Color greenTranparant;
    public Color redOpaque;
    public Color redTranparant;

    private GameObject[] greenTagArray;
    private GameObject[] redTagArray;

    private bool greenCollisionIsActive = true;

    void CreateArraysWithTaggedObj() // Store all objects tagged with in arrays:
    {
        greenTagArray = GameObject.FindGameObjectsWithTag("greenTag");
        redTagArray = GameObject.FindGameObjectsWithTag("redTag");
    }


    void OnValidate() // When updating the object "Controller - SwitchColor", thiss triggers: 
    {
        CreateArraysWithTaggedObj();

        // Set the correct starting color and collliion based on the tag:
        foreach (GameObject objGreen in greenTagArray)
        {
            objGreen.GetComponent<MeshRenderer>().material = materialGreen;
            objGreen.GetComponent<MeshCollider>().enabled = false;
        }
        foreach (GameObject objRed in redTagArray)
        {
            objRed.GetComponent<MeshRenderer>().material = materialRed;
            objRed.GetComponent<MeshCollider>().enabled = true;
        }

    }

    void Start()
    {
        CreateArraysWithTaggedObj();

        // Set the correct starting color and collliion based on the tag: 
        foreach (GameObject objGreen in greenTagArray)
        {
            objGreen.GetComponent<MeshRenderer>().material = materialGreen;
            objGreen.GetComponent<MeshCollider>().enabled = false;
        }
        foreach (GameObject objRed in redTagArray)
        {
            objRed.GetComponent<MeshRenderer>().material = materialRed;
            objRed.GetComponent<MeshCollider>().enabled = true;
        }
    }


    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            // Switch which color is active:
            greenCollisionIsActive = !greenCollisionIsActive;


            // If GREEN should have active collision:
            if (greenCollisionIsActive == true)
            {
                // Update all Green objects:
                foreach (GameObject objGreen in greenTagArray)
                {
                    objGreen.GetComponent<MeshRenderer>().material.color = greenOpaque;
                    objGreen.GetComponent<MeshCollider>().enabled = true;
                    Debug.Log("loop");
                }

                // Update all Red objects:
                foreach (GameObject objRed in redTagArray)
                {
                    objRed.GetComponent<MeshRenderer>().material.color = redTranparant;
                    objRed.GetComponent<MeshCollider>().enabled = false;
                    Debug.Log("loop");
                }
            }


            // If RED should have active collision:
            if (greenCollisionIsActive == false)
            {
                // Update all Green objects:
                foreach (GameObject objGreen in greenTagArray)
                {
                    objGreen.GetComponent<MeshRenderer>().material.color = greenTranparant;
                    objGreen.GetComponent<MeshCollider>().enabled = false;
                    Debug.Log("loop");
                }

                // Update all Red objects:
                foreach (GameObject objRed in redTagArray)
                {
                    objRed.GetComponent<MeshRenderer>().material.color = redOpaque;
                    objRed.GetComponent<MeshCollider>().enabled = true;
                    Debug.Log("loop");
                }
            } 
        }
    }
}
