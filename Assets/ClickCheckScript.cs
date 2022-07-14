using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickCheckScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log(Input.mousePosition);
            Vector3 mouseLoc = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mouseCastLoc = new Vector2(mouseLoc.x, mouseLoc.y);
            RaycastHit2D hit = Physics2D.Raycast(mouseCastLoc, Vector2.zero);

            if (hit != false)
            {
                BoxScript hitBoxScript = hit.collider.gameObject.GetComponent<BoxScript>();
                if (hitBoxScript != null)
                {
                    if (!hitBoxScript.getMarked())
                    {
                        hitBoxScript.setMarked(true);
                        hitBoxScript.drawCross(true);
                    }

                    if (hitBoxScript.getMarked())
                    {
                        hitBoxScript.checkSurroundings();
                    }
                }
            }
        }
    }


}
