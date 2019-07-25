using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AM.UpBall.InGame;
/// <summary>
/// 무한맵 스크롤.
/// </summary>
namespace AM.UpBall
{
    public class MapScroll : MonoBehaviour
    {
        public GameObject[] objBG;
        public float speed;
        public float LimitY;
        public float DisntaceY;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            for (int i = 0; i < objBG.Length; ++i)
            {
                // speed값 만큼 배경 두개를 계속 내려준다.
                objBG[i].transform.localPosition += new Vector3(0f, LevelingData.moveSpeed * Time.deltaTime, 0f);

                // 가장 밑으로 갔을때 체크
                if(objBG[i].transform.localPosition.y <= LimitY)
                {
                    // 내 위에 있는 배경에 위치 기준으로 움직인다.
                    objBG[i].transform.localPosition = new Vector3(0f, objBG[i==0 ? 1 : 0].transform.localPosition.y + DisntaceY, 1f);
                }

            }
        }
    }
}
