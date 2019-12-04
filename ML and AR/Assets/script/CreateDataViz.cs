using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.
using TMPro;


public class CreateDataViz : MonoBehaviour
{
    int scaleSize = 1000;
    int scaleSizeFactor = 100;
    float binDistance = 0.1f;
    float offset = 0;

    //Add a label for the prediction year
    public TextMeshProUGUI textPredictionYear;

    //Check if image target is detected
    public GameObject Target;
    private bool detected = false;
    // The anchor object of your graph
    public GameObject GraphAnchor;

    //The update function is executed every frame
    void Update()
    {
        if (Target.activeSelf == true && detected == false)
        {
            Debug.Log("Image Target Detected");
            detected = true;
            // we moved this function from Start to Update
            CreateGraph();
        }
    }


    // Use this for initialization
    void Start()
    {
    }


    public void ClearChilds(Transform parent)
    {
        offset = 0;
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }
    }

    // Here we allow the use to increase and decrease the size of the data visualization
    public void DecreaseSize()
    {
        scaleSize += scaleSizeFactor;
        CreateGraph();
    }

    public void IncreaseSize()
    {
        scaleSize -= scaleSizeFactor;
        CreateGraph();
    }

    //Reset the size of the graph
    public void ResetSize()
    {
        scaleSize = 1000;
        CreateGraph();
    }



    public void CreateGraph()
    {
        Debug.Log("creating the graph");
        ClearChilds(GraphAnchor.transform);
        for (var i = 0; i < LinearRegression.quantityValues.Count; i++)
        {
            //Reduced the number of arguments of the function
            createBin((float)LinearRegression.quantityValues[i] / scaleSize, GraphAnchor);
            offset += binDistance;
        }
        Debug.Log("creating the graph: " + LinearRegression.PredictionOutput);

        // Let's add the predictio as the last bar, only if the user made a prediction
        if (LinearRegression.PredictionOutput != 0)
        {
            //Reduced the number of arguments of the function
            createBin((float)LinearRegression.PredictionOutput / scaleSize, GraphAnchor);
            offset += binDistance;
            textPredictionYear.text = "Prediction of " + LinearRegression.PredictionYear;
        }
        else
        {
            textPredictionYear.text = " ";

        }
    }

    //Reduced the number of arguments of the function
    void createBin(float Scale_y, GameObject _parent)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.SetParent(_parent.transform, true);

        //We use the localScale of the parent object in order to have a relative size
        Vector3 scale = new Vector3(GraphAnchor.transform.localScale.x / LinearRegression.quantityValues.Count, Scale_y, GraphAnchor.transform.localScale.x / 8);
        cube.transform.localScale = scale;

        //We use the position and rotation of the parent object in order to align our graph
        cube.transform.localPosition = new Vector3(offset - GraphAnchor.transform.localScale.x, (Scale_y / 2) - (GraphAnchor.transform.localScale.y / 2), 0);
        cube.transform.rotation = GraphAnchor.transform.rotation;

        // Let's add some colours
        cube.GetComponent<MeshRenderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

    }
}