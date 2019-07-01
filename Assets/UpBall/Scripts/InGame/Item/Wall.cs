using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public GameObject[] arrWall;
    public BoxCollider2D collider2D;

    private bool _isScore = false; // score를 더해준 벽인지 체크하는 변수
    private bool _isUse = false;
    private float _moveSpeed = 0f;
    private int _count = 0;

    public bool IsUse
    {
        get
        {
            return _isUse;
        }

        set
        {
            _isUse = value;
        }
    }

    public bool IsScore
    {
        get
        {
            return _isScore;
        }

        set
        {
            _isScore = value;
        }
    }

    public int Count
    {
        get
        {
            return _count;
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (IsUse)
        {
            transform.localPosition += new Vector3(0f, LevelingData.moveSpeed * Time.deltaTime, 0f);

            if (transform.localPosition.y <= -5.32f)
            {
                if (WallManager.instance.sprGround.color.a != 0f)
                {
                    WallManager.instance.HideGround();
                }
                ClearWall();
            }
        }
    } 

    private void ClearWall()
    {
        WallManager.instance.DeleteWall(this);
        _count = 0;
        IsUse = false;
        IsScore = false;
        Disable();

        collider2D.enabled = false;
    }

    public void SetWallInfo(int Count, float MoveSpeed, Vector3 startPos)
    {
        _count = Count;
        _moveSpeed = MoveSpeed;
        transform.localPosition = startPos;

        SetWallObjCnt(Count);
        collider2D.enabled = true;
    }

    public void Disable()
    {
        for (int i = 0; i < arrWall.Length; ++i)
        {
            arrWall[i].gameObject.SetActive(false);
        }
    }

    private void SetWallObjCnt(int Count)
    {
        if (arrWall.Length <= Count)
        {
            return;
        }
        for (int i = 0; i < arrWall.Length; ++i)
        {
            arrWall[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < Count; ++i)
        {
            arrWall[i].gameObject.SetActive(true);
        }

        collider2D.size = new Vector2(2.5f - (5 - Count) * 0.5f, 0.5f);
        collider2D.offset = new Vector2(0 - (5 - Count) * 0.25f, 0f);
    }
}
