using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private static int gridSize;

    void Start()
    {

        Vector3 lineStart = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));

        Vector3 vertLineEnd = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0f));
        Vector3 horizLineEnd = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0f));

        drawLines(lineStart, vertLineEnd, gridSize + 1);
        drawLines(lineStart, horizLineEnd, gridSize + 1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void drawLines(Vector3 lineBorderStart, Vector3 lineBorderEnd, int lineCount)
    {
        float boxWidth = (float) Screen.width / lineCount;
        float boxHeight = (float) Screen.height / lineCount;
        float lineWidth = GameObject.Find("Line").GetComponent<LineRenderer>().startWidth;
        Vector3 posVector;

        for (int drawCount = 0; drawCount < lineCount; drawCount++)
        {
            if (lineBorderEnd.y < lineBorderEnd.x)
            {
                posVector = Camera.main.ScreenToWorldPoint(new Vector3((2*drawCount + 1) * boxWidth / 2, 0f, -1f));
                posVector.x = posVector.x + lineWidth/2;
               
            }

            else
            {
                posVector = Camera.main.ScreenToWorldPoint(new Vector3(0f, (2 * drawCount + 1) * boxHeight / 2, -1f));
                posVector.y = posVector.y + lineWidth/2;
                
            }
            
            Vector3[] linePoints = { lineBorderStart - posVector, lineBorderEnd - posVector };
            GameObject lineCopy = Instantiate(GameObject.Find("Line"), Camera.main.ScreenToWorldPoint(Vector3.zero), Quaternion.identity);

            lineCopy.transform.SetParent(transform);
            lineCopy.GetComponent<LineRenderer>().startColor = Color.red;
            lineCopy.GetComponent<LineRenderer>().endColor = Color.red;
            lineCopy.GetComponent<LineRenderer>().SetPositions(linePoints);
        }
    }

    public void getGridSize(string s)
    {
        gridSize = int.Parse(s);
    }
}
