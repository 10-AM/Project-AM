using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AM.UpBall.InGame;
using AM.UpBall.MODULE;

namespace AM.UpBall.InGame.Item
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance = null;
        public Text txtScore;

        public Image imgExit;
        public Image imgDie;
        public Text txtDie;
        public Image imgDieBG;
        public Text txtBestScore;

        public RectTransform objDieUI;
        public RectTransform objDownUI;

        private int _score = 0;
        private bool _ExitToggle = false;

        public int Score
        {
            get
            {
                return _score;
            }

            set
            {
                _score = value;
            }
        }

        private void Awake()
        {
            instance = this;
        }
        // Use this for initialization
        void Start()
        {
            objDieUI.gameObject.SetActive(false);

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_ExitToggle == false)
                {
                    imgExit.gameObject.SetActive(true);
                    Time.timeScale = 0f;
                    LevelingData.IsExit = true;
                    _ExitToggle = true;
                }
                else
                {
                    imgExit.gameObject.SetActive(false);
                    Time.timeScale = LevelingData.curTimeScale;
                    LevelingData.IsExit = false;
                    _ExitToggle = false;
                }
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                imgExit.gameObject.SetActive(true);
                Time.timeScale = 0f;
                LevelingData.IsExit = true;
            }
        }

        public void PlusScroe()
        {
            _score++;
            txtScore.text = _score.ToString();
            if (_score % 5 == 0)
            {
                LevelingData.SetNextLevel(_score);
            }
        }

        public void YouDied()
        {
            if (_score > GetBestScore())
                SetBestScore(_score);

            txtBestScore.text = "Best " + GetBestScore().ToString();

            Sound.instance.PlayEffSound(SOUND.S_DIE);

            TouchPower.instance.objStart.SetActive(false);
            TouchPower.instance.objEnd.SetActive(false);

            objDieUI.gameObject.SetActive(true);

            StartCoroutine(Tween.instance.SetAlpha(imgDie, 0f, 1f, 2f));
            StartCoroutine(Tween.instance.SetAlpha(imgDieBG, 0f, 82f / 255f, 2f));
            StartCoroutine(Tween.instance.SetAlpha(txtDie, 0f, 1f, 2f));
            StartCoroutine(Tween.instance.SetScale(imgDie.transform, new Vector2(0.5f, 0.5f), new Vector2(1f, 1f), 2f));

            StartCoroutine(Tween.instance.Move(objDownUI, new Vector3(0f, -814f, 0f), new Vector3(0f, -348f, 0f), 1f, 0.5f));
            StartCoroutine(Tween.instance.Move(txtBestScore.rectTransform, new Vector3(0f, 687f, 0f), new Vector3(10f, 358f, 0f), 1f, 0.5f));
        }

        public void ClickReTry()
        {
            LevelingData.ReSetData();
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }

        public void ClickHome()
        {
            LevelingData.ReSetData();
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }

        public void ShakeCamera(float power, float time)
        {
            StartCoroutine(ShakeCamera(power, time, Camera.main.transform.position));
        }

        private IEnumerator ShakeCamera(float power, float time, Vector3 originPos)
        {
            float t = 0f;
            while (t <= time)
            {
                t += Time.unscaledDeltaTime;
                Camera.main.transform.position = (Vector3)Random.insideUnitCircle * power + originPos;
                yield return null;
            }
            Camera.main.transform.position = originPos;
        }

        private int GetBestScore()
        {
            return PlayerPrefs.GetInt("BestScore", 0);
        }

        private void SetBestScore(int score)
        {
            PlayerPrefs.SetInt("BestScore", score);
        }

        public void ExitGameYes()
        {
            Application.Quit();
        }

        public void ExitGameNo()
        {
            imgExit.gameObject.SetActive(false);
            Time.timeScale = LevelingData.curTimeScale;
            LevelingData.IsExit = false;
            _ExitToggle = false;
        }
    }
}