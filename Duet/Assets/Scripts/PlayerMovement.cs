using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    #region Singleton class : PlayerMovement

    public static PlayerMovement Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    #endregion

    [SerializeField] float speed;
    [SerializeField] float rotationSpeed;

    [SerializeField] CircleCollider2D redBall;
    [SerializeField] CircleCollider2D blueBall;

    Rigidbody2D rb;
    Vector3 startPos;

    Camera cam;
    float touchPosX;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        MoveUp();
        startPos = transform.position;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.isGameOver)
        {
            if(Input.GetMouseButtonDown(0))
            {
                touchPosX = cam.ScreenToWorldPoint(Input.mousePosition).x;
                Debug.Log(touchPosX);
            }
            if (Input.GetMouseButton(0))
            {
                if (touchPosX > 0.01)
                {
                    RotateRight();
                }
                else
                {
                    RotateLeft();
                }
            }
            else
                rb.angularVelocity = 0f;


#if UNITY_EDITOR
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                RotateLeft();
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                RotateRight();
            }

            if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                rb.angularVelocity = 0f;
            }
#endif
        }
    }

    void MoveUp()
    {
        rb.velocity = Vector2.up * speed;
    }

    void RotateLeft()
    {
        rb.angularVelocity = rotationSpeed;
    }

    void RotateRight()
    {
        rb.angularVelocity = -rotationSpeed;
    }

    public void Restart()
    {
        redBall.enabled = false;
        blueBall.enabled = false;

        rb.angularVelocity = 0f;
        rb.velocity = Vector2.zero;


        transform.DORotate(Vector3.zero, 1f).SetDelay(1f).SetEase(Ease.InOutBack);

        transform.DOMove(startPos, 1f).SetDelay(1f).SetEase(Ease.OutFlash)
            .OnComplete(()=>
            {
                redBall.enabled = true;
                blueBall.enabled = true;

                GameManager.Instance.isGameOver = false;

                MoveUp();
            });
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("LevelEnd"))
        {
            Destroy(other.gameObject);
            Debug.Log("Win");

            int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;

            if (currentLevelIndex < SceneManager.sceneCountInBuildSettings)
                SceneManager.LoadSceneAsync(++currentLevelIndex);
        }
    }

}
