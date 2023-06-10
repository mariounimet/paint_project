using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftBoundary : MonoBehaviour
{
    public Vector3 vectorCamObj;
    private float ObjectHeight;
    private float ObjectWidth;
    private bool isXAxis;
    private bool isRight;
    private Vector3 screenBounds;
    private BoxCollider2D collider;
    private bool isTop;
    private float colliderSum;
    private float camSum;

    // Start is called before the first frame update
    void Start()
    {
        isXAxis = true;
        isTop = true;
        isRight = false;

        float CamHeight = 2f * Camera.main.orthographicSize;
        float CamWidth = CamHeight * Camera.main.aspect;

        setNewPositionBoundary(isXAxis, isRight, isTop, CamHeight, CamWidth);
        setNewSizeBoundary(isXAxis, isRight, isTop, CamHeight, CamWidth);

        vectorCamObj = transform.position - Camera.main.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.transform.position + vectorCamObj;
    }

    void setNewPositionBoundary( bool isInXAxis, bool isRight, bool isTop, float CamHeight, float CamWidth){

        collider = transform.GetComponent<BoxCollider2D>();
        Vector3 NewPosition = transform.position;

        if (isInXAxis)
        {
            colliderSum = collider.size.x / 2;
            camSum = CamWidth / 2;
            if (isRight)
            {
                NewPosition.y = Camera.main.transform.position.y;
                NewPosition.x = Camera.main.transform.position.x + camSum + colliderSum;
            }
            else
            {
                NewPosition.y = Camera.main.transform.position.y;
                NewPosition.x = Camera.main.transform.position.x - camSum - colliderSum;
            }
        } else
        {
            colliderSum = collider.size.y / 2;
            camSum = CamHeight / 2;
            if (isTop)
            {
                NewPosition.y = Camera.main.transform.position.y + camSum + colliderSum;
                NewPosition.x = Camera.main.transform.position.x;
            }
            else
            {
                NewPosition.y = Camera.main.transform.position.y - camSum - colliderSum;
                NewPosition.x = Camera.main.transform.position.x; 
            }
        }
        
        transform.position = NewPosition;

        Vector2 colliderNewSize = new Vector2(CamWidth, collider.size.y);
        collider.size = colliderNewSize;
    }

    void setNewSizeBoundary( bool isInXAxis, bool isRight, bool isTop, float CamHeight, float CamWidth){
        collider = transform.GetComponent<BoxCollider2D>();

        if (isInXAxis)
        {
            Vector2 colliderNewSize = new Vector2(collider.size.y, CamHeight);
            collider.size = colliderNewSize;
        }
        else
        {
            Vector2 colliderNewSize = new Vector2(CamWidth, collider.size.y);
            collider.size = colliderNewSize;
        }
    }
}