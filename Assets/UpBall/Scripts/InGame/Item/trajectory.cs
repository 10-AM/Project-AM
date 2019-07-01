using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class trajectory : MonoBehaviour
{
    public GameObject pfTrajectory;
    public Vector3 temp;
    private int numOfTrajectoryPoints = 10;
    private List<GameObject> trajectoryPoints = new List<GameObject>();
    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < numOfTrajectoryPoints; ++i)
        {
            GameObject dot = Instantiate(pfTrajectory);
            dot.gameObject.SetActive(false);
            trajectoryPoints.Add(dot);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            DisableTragectoryPonints();
        }
    }

    public void Pressed(Vector3 startPos, Vector3 ForceV)
    {
        setTrajectoryPoints(startPos, ForceV);
    }

    private void SubsetTrajectoryPoints(Vector3 pStartPosition, Vector3 pVelocity, int maxNum, int curNum)
    {
        float velocity = Mathf.Sqrt((pVelocity.x * pVelocity.x) + (pVelocity.y * pVelocity.y));
        float angle = Mathf.Rad2Deg * (Mathf.Atan2(pVelocity.y, pVelocity.x));
        float fTime = 0;
        int tragectoryNum = maxNum;
        fTime += 0.1f;
        for (int i = curNum; i < tragectoryNum; i++)
        {
            float dx = velocity * fTime * Mathf.Cos(angle * Mathf.Deg2Rad);
            float dy = velocity * fTime * Mathf.Sin(angle * Mathf.Deg2Rad) - (Physics2D.gravity.magnitude * fTime * fTime / 2.0f);
            Vector3 pos = new Vector3(pStartPosition.x + dx, pStartPosition.y + dy, -2);
            trajectoryPoints[i].GetComponent<SpriteRenderer>().SetAlpha(0.1f + (0.9f * ((float)i / (float)tragectoryNum)));
            trajectoryPoints[i].transform.position = pos;
            trajectoryPoints[i].gameObject.SetActive(true);
            trajectoryPoints[i].transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(pVelocity.y - (Physics.gravity.magnitude) * fTime, pVelocity.x) * Mathf.Rad2Deg);
            fTime += 0.1f;
        }
    }

    private void setTrajectoryPoints(Vector3 pStartPosition, Vector3 pVelocity)
    {
        DisableTragectoryPonints();
        float velocity = Mathf.Sqrt((pVelocity.x * pVelocity.x) + (pVelocity.y * pVelocity.y));
        Vector3 nomalvec;
        float angle = Mathf.Rad2Deg * (Mathf.Atan2(pVelocity.y, pVelocity.x));
        float fTime = 0;
        int tragectoryNum = (int)(numOfTrajectoryPoints * (TouchPower.instance.MovePower / TouchPower.instance.MaxPower));
        fTime += 0.1f;
        for (int i = 0; i < tragectoryNum; i++)
        {
            float dx = velocity * fTime * Mathf.Cos(angle * Mathf.Deg2Rad);
            float dy = velocity * fTime * Mathf.Sin(angle * Mathf.Deg2Rad) - (Physics2D.gravity.magnitude * fTime * fTime / 2.0f);
            Vector3 pos = new Vector3(pStartPosition.x + dx, pStartPosition.y + dy, -2);
            trajectoryPoints[i].GetComponent<SpriteRenderer>().SetAlpha(0.1f + (0.9f * ((float)i / (float)tragectoryNum)));
            trajectoryPoints[i].transform.position = pos;
            trajectoryPoints[i].gameObject.SetActive(true);
            trajectoryPoints[i].transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(pVelocity.y - (Physics.gravity.magnitude) * fTime, pVelocity.x) * Mathf.Rad2Deg);
            fTime += 0.1f;


            bool checkLeft = WallManager.instance.IsLeftWallCol(pos);
            bool checkRight = WallManager.instance.IsRightWallCol(pos);

            if (checkLeft || checkRight)
            {
                if (checkRight)
                    nomalvec = Vector3.left;
                else
                    nomalvec = Vector3.right;
                Vector3 incomingVector = pos - pStartPosition;  //입사각
                incomingVector = incomingVector.normalized * ((incomingVector / fTime).magnitude - 2f);
                Vector3 inverseVector = -incomingVector; //입사각의 반대각

                Vector3 normalVector = nomalvec; //법선벡터

                Vector3 reflectVector = Vector3.Reflect(incomingVector, normalVector); //반사각

                temp = reflectVector;

                Debug.Log("dx dy : " + new Vector2(Mathf.Abs(dx), Mathf.Abs(dy)));

                SubsetTrajectoryPoints(pos, reflectVector, tragectoryNum, i);
                return;
            }
            temp = Vector3.zero;
        }
    }

    public void DisableTragectoryPonints()
    {
        for (int i = 0; i < trajectoryPoints.Count; ++i)
        {
            trajectoryPoints[i].gameObject.SetActive(false);
        }
    }
}
