using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPower : MonoBehaviour
{

    public static TouchPower instance = null;

    public GameObject objStart;
    public GameObject objEnd;

    private Vector3 _startMousePos;

    private Vector3 _direction;
    private float _movePower;

    private float _maxPower = 12f;
    private float _maxDistance = 200f;

    public Vector3 Direction
    {
        get
        {
            return _direction;
        }

        set
        {
            _direction = value;
        }
    }

    public float MovePower
    {
        get
        {
            return _movePower;
        }

        set
        {
            _movePower = value;
        }
    }

    public float MaxPower
    {
        get
        {
            return _maxPower;
        }

        set
        {
            _maxPower = value;
        }
    }

    public Vector3 StartMousePos
    {
        get
        {
            return _startMousePos;
        }

        set
        {
            _startMousePos = value;
        }
    }

    private void Awake()
    {
        instance = this;
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (LevelingData.IsDie)
            return;

        //if (Input.GetMouseButtonDown(0))
        //{
        //    objStart.SetActive(true);
        //    objStart.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(StartMousePos).x, Camera.main.ScreenToWorldPoint(StartMousePos).y, 0f);
        //}
        //if (Input.GetMouseButtonUp(0))
        //{
        //    objStart.SetActive(false);
        //    objEnd.SetActive(false);
        //}
        //if (Input.GetMouseButton(0))
        //{
        //    Vector3 vEnd = GetObjEndPos(Input.mousePosition);
        //    objEnd.transform.localPosition = new Vector3(vEnd.x, vEnd.y, 0f);
        //    objEnd.SetActive(true);
        //}
    }
    public void SetUpEvent(Vector3 mousePosition)
    {
        objStart.SetActive(false);
        objEnd.SetActive(false);
    }

    public void SetDownEvent(Vector3 mousePosition)
    {
        StartMousePos = mousePosition;
        objStart.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(StartMousePos).x, Camera.main.ScreenToWorldPoint(StartMousePos).y, 0f);
    }

    public void SetStayEvent(Vector3 mousePosition)
    {
        Vector3 vEnd = GetObjEndPos(mousePosition);
       instance.objEnd.transform.localPosition = new Vector3(vEnd.x, vEnd.y, 0f);
        objEnd.SetActive(true);
        objStart.SetActive(true);

        SetTouchPowerInfo(mousePosition);
    }

    public void SetTouchPowerInfo(Vector3 _endMousePos)
    {
        Direction = (StartMousePos - _endMousePos).normalized;
        float distance = Mathf.Abs(Vector3.Distance(_endMousePos, StartMousePos));

        if (distance >= _maxDistance)
            distance = _maxDistance;

        // 얘는 월드 좌표랑 상관없으니 괜찮
        MovePower = (distance / _maxDistance) * MaxPower;
    }

    public Vector3 GetObjEndPos(Vector3 _endMousePos)
    {
        Vector3 endPos = (StartMousePos - _endMousePos).normalized;
        Vector3 zeroPos = Camera.main.ScreenToWorldPoint(Vector3.zero); // 0기준

        float distance = Vector3.Distance(_endMousePos, StartMousePos);

        if (distance >= _maxDistance)
            distance = _maxDistance;

        endPos *= distance;

        // 월드 좌표로 바꿔주고 0을 기준으로
        endPos = new Vector3(zeroPos.x - Camera.main.ScreenToWorldPoint(endPos).x, zeroPos.y - Camera.main.ScreenToWorldPoint(endPos).y, 0f);
        // startobj 좌표 기준으로 바꿔주기
        endPos = new Vector3(objStart.transform.localPosition.x + endPos.x, objStart.transform.localPosition.y + endPos.y);

        return endPos;
    }
}
