using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AM.UpBall.MODULE;

public enum BALLSTATE
{
    IDLE = 0,
    JUMP_ONCE,
    JUMP_TWICE,
    NONEMOVEING
}
namespace AM.UpBall.InGame.Item
{
    public class Ball : MonoBehaviour
    {
        public Rigidbody2D myRigidbody;
        public GameObject objSlowDuration;
        public GameObject objTrailRender;
        public Image ImgDurationEdge;
        public BALLSTATE _ballState = BALLSTATE.IDLE;

        float _currentMovePower;
        Vector2 _currentMovePowerVector;
        private Vector3 _trajectyVelocity;

        private Coroutine _slowCrt = null;
        private Coroutine _trailCrt = null;

        private bool _isSlowing = false;        // 슬로우 중인지 체크
        private float _SlowDurationCount = 0f;  // 슬로우 시간 체크
        private float _slowScale = 0.3f;

        private bool _isFirstTouch = true;
        private bool _isCheck = false;

        private GameObject parentMoveObj;
        private GameObject parentObj;
        // Use this for initialization
        void Start()
        {
            DisableDurationEdge();
            parentMoveObj = GameObject.Find("G_InGameObject");
            parentObj = GameObject.Find("G_Ball");
        }

        // Update is called once per frame
        private void Update()
        {
            _currentMovePower = myRigidbody.velocity.magnitude;
            _currentMovePowerVector = myRigidbody.velocity;
            if (LevelingData.IsDie || LevelingData.IsExit)
                return;

            if (Input.GetMouseButtonDown(0))
            {
                TouchPower.instance.SetDownEvent(Input.mousePosition);
            }

            Time.fixedDeltaTime = Time.timeScale * .02f;
            if (_ballState == BALLSTATE.IDLE || _ballState == BALLSTATE.JUMP_ONCE)
            {
                if (Input.GetMouseButton(0))
                {
                    TouchPower.instance.SetStayEvent(Input.mousePosition);
                    if (TouchPower.instance.MovePower != 0.0f)
                    {
                        GetComponent<trajectory>().Pressed(transform.position, TouchPower.instance.Direction * TouchPower.instance.MovePower);
                    }
                    SetStateEvent(1);
                }

                if (Input.GetMouseButtonUp(0))
                {
                    TouchPower.instance.SetUpEvent(Input.mousePosition);
                    // 점프할때는 무조건 처음 속도 0
                    if (TouchPower.instance.MovePower != 0f)
                    {
                        TouchPower.instance.SetTouchPowerInfo(Input.mousePosition);

                        if (myRigidbody.velocity != Vector2.zero)
                            myRigidbody.velocity = Vector2.zero;

                        myRigidbody.AddForce(TouchPower.instance.Direction * TouchPower.instance.MovePower, ForceMode2D.Impulse);
                        _trajectyVelocity = GetComponent<trajectory>().temp;

                        SetStateEvent(0);
                    }
                }
            }

            if (transform.position.y <= -9.3f)
            {
                ClearPlayer();
            }

            CheckUnderWall();
        }

        void ClearPlayer()
        {
            GetComponent<trajectory>().DisableTragectoryPonints();
            TouchPower.instance.objStart.SetActive(false);
            TouchPower.instance.objEnd.SetActive(false);
            gameObject.SetActive(false);
            SetSlowMotionTime(false);
            LevelingData.IsDie = true;
            UIManager.instance.ShakeCamera(0.05f, 0.5f);
            UIManager.instance.YouDied();
            DisableDurationEdge();
        }

        private void DisableDurationEdge()
        {
            ImgDurationEdge.fillAmount = 0f;
        }

        void SetStateIDLE()
        {
            if (_isCheck)
                _ballState = BALLSTATE.IDLE;
        }

        private void SetStateEvent(int mouseEvent)
        {
            if (mouseEvent == 0) // up
            {
                DisableDurationEdge();
                Sound.instance.PlayEffSound(SOUND.S_JUMP);

                if (_isFirstTouch)
                {
                    WallManager.instance.HideGround();
                    _isFirstTouch = false;
                }

                TouchPower.instance.MovePower = 0f;

                SetSlowMotionTime(false);

                if (_ballState == BALLSTATE.IDLE)
                {
                    _ballState = BALLSTATE.JUMP_ONCE;
                    // 0.1초 딜레이동안 WALL과 충돌중이면 상태를 IDLE로 변경
                    Invoke("SetStateIDLE", 0.1f);
                }
                else if (_ballState == BALLSTATE.JUMP_ONCE)
                {
                    _ballState = BALLSTATE.JUMP_TWICE;
                }
            }
            else if (mouseEvent == 1) // stay
            {
                if (_ballState == BALLSTATE.JUMP_ONCE && _isSlowing == false)
                {
                    SetSlowMotionTime(true);
                }
            }
        }
        // direction : true +, flase -
        IEnumerator SetSlowDurtion(bool direction)
        {
            _SlowDurationCount = 0f;

            ImgDurationEdge.fillAmount = 1f;

            while (_SlowDurationCount < 1.0f)
            {
                _SlowDurationCount = Mathf.Clamp01(_SlowDurationCount + Time.deltaTime / LevelingData.slowDurationTime);
                if (direction)
                {
                    objSlowDuration.transform.localScale = new Vector3(1.0f * _SlowDurationCount, 1.0f * _SlowDurationCount, 0f);
                    ImgDurationEdge.fillAmount = 1.0f * _SlowDurationCount;
                }
                yield return null;
            }
            SetSlowMotionTime(false);
            _ballState = BALLSTATE.NONEMOVEING;

            TouchPower.instance.objStart.SetActive(false);
            TouchPower.instance.objEnd.SetActive(false);
        }

        private void SetSlowMotionTime(bool Eanble)
        {
            _isSlowing = Eanble;
            if (Eanble)
            {
                Time.timeScale = _slowScale;
                LevelingData.curTimeScale = _slowScale;
                _slowCrt = StartCoroutine(SetSlowDurtion(true));
            }
            else
            {
                Time.timeScale = 1.0f;
                LevelingData.curTimeScale = 1.0f;
                ClearSlowMotion();
            }
        }

        private void ClearSlowMotion()
        {
            objSlowDuration.transform.localScale = Vector3.zero;
            DisableDurationEdge();
            GetComponent<trajectory>().DisableTragectoryPonints();

            if (_slowCrt != null)
            {
                StopCoroutine(_slowCrt);
                _slowCrt = null;
            }
        }

        private void CheckUnderWall()
        {
            float LengthY = 0.23f;
            float LengthX = 0.4f;
            Vector2 MyPos = transform.position;
            bool isCheck = false;

            for (int i = -1; i <= 1; ++i)
            {
                // RayCast 확인.. 방향은 밑으로
                Ray2D ray = new Ray2D(MyPos + new Vector2((LengthX / 2) * i, 0f), Vector2.down);
                RaycastHit2D Hit = Physics2D.Raycast(ray.origin, ray.direction, LengthY);
                Debug.DrawRay(ray.origin, LengthY * ray.direction, Color.blue, 1);

                if (Hit.collider != null && Hit.collider.gameObject.tag.Equals("Ground"))
                {
                    Wall wall = Hit.collider.gameObject.transform.GetComponent<Wall>();
                    // 충돌중인 오브젝트가 wall 스크립트를 가지고 있을때
                    if (wall != null)
                    {
                        // 이미 점수 체크한 벽인지 확인후 점수를 올려줌
                        if (wall.IsScore == false)
                        {
                            UIManager.instance.PlusScroe();
                            wall.IsScore = true;
                            WallManager.instance.CheckWllScore(wall);
                        }
                        // Ball도 Wall과 같이 내려가게 자식 오브젝트로 바꿔줌
                        if (_ballState == BALLSTATE.IDLE)
                        {
                            parentObj.transform.SetParent(Hit.collider.gameObject.transform);
                            isCheck = true;
                            myRigidbody.velocity = Vector2.zero;
                        }
                    }
                }
            }
            // Wall에 없으면 Ball은 같이 내려가지 않아도됨
            if (isCheck == false)
            {
                parentObj.transform.SetParent(parentMoveObj.transform);
                _isCheck = false;
            }
            else
            {
                _isCheck = true;
            }
        }

        private void DisableTrail()
        {
            objTrailRender.gameObject.SetActive(false);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag.Equals("Wall"))
            {
                if (_currentMovePower >= 8.0f)
                    UIManager.instance.ShakeCamera(0.05f, 0.1f);

                _currentMovePower -= 2f;
                Vector3 incomingVector = _currentMovePowerVector;//transform.position - _startMovePos;  //입사각
                                                                 //incomingVector = incomingVector.normalized * _currentMovePower;
                Vector3 inverseVector = -incomingVector; //입사각의 반대각

                Vector3 normalVector = collision.contacts[0].normal; //법선벡터

                Vector3 reflectVector = Vector3.Reflect(incomingVector, normalVector); //반사각

                myRigidbody.velocity = Vector2.zero;

                if (_trajectyVelocity != Vector3.zero) // 궤적이 있을경우에는 궤적 방향으로
                    myRigidbody.AddForce(_trajectyVelocity, ForceMode2D.Impulse);
                else
                    myRigidbody.AddForce(reflectVector, ForceMode2D.Impulse);

                GetComponent<trajectory>().temp = Vector3.zero;
                _trajectyVelocity = Vector3.zero;

                Debug.Log(" _currentMovePower : " + _currentMovePower);
            }

            if (collision.gameObject.tag.Equals("Ground"))
            {
                if (_currentMovePower >= 8.0f)
                    UIManager.instance.ShakeCamera(0.025f, 0.1f);

                _trajectyVelocity = Vector3.zero;

                if (transform.position.y > collision.transform.position.y)
                {
                    // 충돌할 경우 슬로우모션 없애주고 Ball 상태 초기화
                    _ballState = BALLSTATE.IDLE;
                    SetSlowMotionTime(false);
                    if (myRigidbody.velocity != Vector2.zero)
                        myRigidbody.velocity = Vector2.zero;

                    _trailCrt = StartCoroutine(Tween.instance.DelayMethod(0.3f, DisableTrail));
                }
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.tag.Equals("Ground"))
            {
                if (_trailCrt != null)
                {
                    StopCoroutine(_trailCrt);
                    _trailCrt = null;
                }

                objTrailRender.gameObject.SetActive(true);

            }
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.tag.Equals("Ground"))
            {
            }
        }
    }
}