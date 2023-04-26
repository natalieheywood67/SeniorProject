using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{

    public float mapSize;
    public float speed;
    public int visionRadius;
    public GameObject target;

    private int state = 0;
    private int mainState = 0;
    private float deltaMove = 0;
    private Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.Translate(new Vector3(visionRadius - mapSize, 0, visionRadius - mapSize));
        //gameObject.transform.Rotate(new Vector3(0, 1, 0), 180);
        targetPos = target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float x = gameObject.transform.position.x;
        float z = gameObject.transform.position.z;

        float disX = Mathf.Abs(x - targetPos.x);
        float disZ = Mathf.Abs(z - targetPos.z);

        if (mainState == 0) // Searching for human
        {
            if (disX <= 1.5 * visionRadius && disZ <= 1.5 * visionRadius)
            {
                mainState = 1;
                return;
            }

            if (state == 0) // Increase x
            {
                gameObject.transform.Translate(new Vector3(speed, 0, 0));
                deltaMove += speed;
                if (deltaMove >= (2 * mapSize) - (2 * visionRadius))
                {
                    state = 1;
                    deltaMove = 0;
                }

            }
            else if (state == 1) // Increase z
            {
                gameObject.transform.Translate(new Vector3(0, 0, speed));
                deltaMove += speed;
                if (deltaMove >= 2 * visionRadius)
                {
                    state = 2;
                    deltaMove = 0;
                }

            }
            else if (state == 2) // Decrease x
            {
                gameObject.transform.Translate(new Vector3(-speed, 0, 0));
                deltaMove += speed;
                if (deltaMove >= (2 * mapSize) - (2 * visionRadius))
                {
                    state = 3;
                    deltaMove = 0;
                }
            }
            else // state == 3 // Decrease z
            {
                gameObject.transform.Translate(new Vector3(0, 0, speed));
                deltaMove += speed;
                if (deltaMove >= 2 * visionRadius)
                {
                    state = 0;
                    deltaMove = 0;
                }
            }

        }
        else if (mainState == 1) // Flying to human
        {
            /*if (disX < .01 && disZ < .01) // Above human
            {
                mainState = 2;
            }
            else if (disX < .01) // X component above human
            {

            }
            else if (disZ < .01) // Y component above human
            {

            }
            else // Completely out of range
            {
                float moveVectorX = (targetPos.x - x) / speed;
                float moveVectorZ = (targetPos.z - z) / speed;
                gameObject.transform.Translate(new Vector3(moveVectorX, 0, moveVectorZ));

            }*/

            float moveVectorX = (targetPos.x - x) * (speed / 10);
            float moveVectorZ = (targetPos.z - z) * (speed / 10);
            gameObject.transform.Translate(new Vector3(moveVectorX, 0, moveVectorZ));

            if (disX < .01 && disZ < .5)
            {
                mainState = 2;
                deltaMove = 0;
            }
        }
        else if (mainState == 2) // Descending down to human
        {
            if (deltaMove >= 15)
            {
                mainState = 3;
                deltaMove = 0;
            }
            gameObject.transform.Translate(new Vector3(0, -speed / 5, 0));
            deltaMove += (speed / 5);

        }
        else if (mainState == 3)
        {
            if (deltaMove >= 15)
            {
                mainState = 4;
                deltaMove = 0;
            }
            gameObject.transform.Translate(new Vector3(-speed / 5, speed / 20, speed / 5));
            deltaMove += (speed / 5);
        }

        

    }
}
