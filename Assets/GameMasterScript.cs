using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMasterScript : MonoBehaviour
{
    // Start is called before the first frame update

    private static int gridSize;
   

    void Start()
    {
        GameObject boxTemplate  = GameObject.Find("Box");
        int drawParameter = gridSize + 1;

        float boxWidth = Screen.width * 1f / drawParameter;
        float boxHeight = Screen.height * 1f / drawParameter;
        float scalingFactor = 55f / drawParameter;
        float lineWidth = GameObject.Find("Line").GetComponent<LineRenderer>().startWidth;

        Vector3 upperRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        float aspectWidth = upperRight.x / upperRight.y;
           
        //Adjusting box scale to fit the screen
        if (Screen.width > Screen.height)
        {
            boxTemplate.transform.localScale = new Vector3(scalingFactor * aspectWidth, scalingFactor, boxTemplate.transform.localScale.z);
        } else

        {
            boxTemplate.transform.localScale = new Vector3(scalingFactor, scalingFactor * aspectWidth, boxTemplate.transform.localScale.z);
        }

        //Placement of boxes
        for (int vertCount = 1; vertCount < drawParameter; vertCount++)
        {
            for (int horizCount = 1; horizCount < drawParameter; horizCount++)
            {
                float xPosAdjust = horizCount * boxWidth;
                float yPosAdjust = vertCount * boxHeight;
                
                Vector3 boxPos = Camera.main.ScreenToWorldPoint(new Vector3(xPosAdjust, yPosAdjust, 0f));
                boxPos = new Vector3(boxPos.x - lineWidth / 2, boxPos.y - lineWidth / 2, 0f);

                GameObject boxCopy = Instantiate(boxTemplate, boxPos, Quaternion.identity);
            }
        }
    }

    //User input for size of grid
    public void getGridSize(string s)
    {
        int.TryParse(s, out gridSize);
    }

}