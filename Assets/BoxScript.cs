using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    private Vector3 boxPos;
    private bool isMarked;
    private bool isChecked;

    private List<Collider2D> surroundings;
   
    // Start is called before the first frame update

    void Start()
    {
        boxPos = transform.position;

        isMarked = false;
        isChecked = false;

        surroundings = new List<Collider2D>();
        GetComponent<BoxCollider2D>().OverlapCollider(new ContactFilter2D().NoFilter(), surroundings);

        setCrossArms();
        drawCross(false);
    }

    public void checkSurroundings()
    {
        List<BoxScript> markedSurroundings = new List<BoxScript>();
        
        for (int i = 0; i < surroundings.Count; i++)
        {
            
            BoxScript surroundingBox = surroundings[i].gameObject.GetComponent<BoxScript>();

            if (surroundingBox.getMarked() && !(surroundingBox.getChecked()))
            {

                surroundingBox.setChecked(true);
                markedSurroundings.Add(surroundingBox);
                surroundingBox.checkSurroundings();
                surroundingBox.setChecked(false);
            }

            if (!(markedSurroundings.Count < 2))
            {
                for (int k = 0; k < markedSurroundings.Count; k++)
                {
                    markedSurroundings[k].setMarked(false);

                }

                setMarked(false);
            }

        }
    }

    void setCrossArms()
    {
        if (transform.childCount == 0)
        {
            GameObject crossArm1 = Instantiate(GameObject.Find("Line"), Vector3.zero, Quaternion.identity);
            GameObject crossArm2 = Instantiate(GameObject.Find("Line"), Vector3.zero, Quaternion.identity);
            float lineWidth = GameObject.Find("Line").GetComponent<LineRenderer>().startWidth;

            crossArm1.transform.SetParent(transform);
            crossArm2.transform.SetParent(transform);

            float xAdjust = (transform.localScale.x / 10) - lineWidth / 2;
            float yAdjust = (transform.localScale.y / 10) - lineWidth / 2;

            Vector3[] crossArm1Draw =
            {
            new Vector3(boxPos.x - xAdjust, boxPos.y + yAdjust, boxPos.z),
            new Vector3(boxPos.x + xAdjust, boxPos.y - xAdjust, boxPos.z)
        };

            Vector3[] crossArm2Draw =
            {
            new Vector3(boxPos.x + xAdjust, boxPos.y + yAdjust, boxPos.z),
            new Vector3(boxPos.x - xAdjust, boxPos.y - yAdjust, boxPos.z)
        };
  
            crossArm1.GetComponent<LineRenderer>().startColor = Color.blue;
            crossArm1.GetComponent<LineRenderer>().endColor = Color.blue;
            crossArm1.GetComponent<LineRenderer>().SetPositions(crossArm1Draw);

            crossArm2.GetComponent<LineRenderer>().startColor = Color.blue;
            crossArm2.GetComponent<LineRenderer>().endColor = Color.blue;
            crossArm2.GetComponent<LineRenderer>().SetPositions(crossArm2Draw);
        }
    }

    public void setMarked(bool markValue)
    {
        isMarked = markValue;
        drawCross(markValue);
    }

    public void setChecked(bool checkValue)
    {
        isChecked = checkValue;
    }

    public bool getMarked()
    {
        return isMarked;
    }

    public bool getChecked()
    {
        return isChecked;
    }

    public void drawCross(bool drawValue)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(drawValue);
        }

    }

}
