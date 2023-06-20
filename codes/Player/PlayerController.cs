using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] Camera cam;

    public CharacterController controller;
    private Rigidbody rigidbody;
    public float jumpHeight = 3f;
    public float dash = 5f;
    public float rotSpeed = 3f;

    [SerializeField] Vector3 camPos;
    public Vector3 camRot;

    // Start is called before the first frame update
    float magnitude;
    void Start()
    {
        PosInit();
        magnitude = (Vector3.zero + camPos_FPP_Walk).magnitude;
        controller = GetComponent<CharacterController>();
    }

    public bool isFPP = true;
    public float turnSpeed = 4.0f; // 마우스 회전 속도    
    private float xRotate_TPP = 0.0f;
    private float yRotate_TPP = 0f;

    public Vector3 camAngle_TPP;

    public float speed;

    public float horizontalMovement;
    public float verticalMovement;

    [SerializeField] public FixedJoystick variableJoystick;



    [SerializeField] private Animator playerAnim;

    private void Awake()
    {
        camAngle_TPP = cam.transform.eulerAngles;
        cam.transform.position = transform.position + camPos;

        //추가
        _offset = transform.position - cam.transform.position;
        distance = 3f;//Vector3.Distance(transform.position, cam.transform.position);

        isFPP = true;
        if(!(stateAfterLoadScene == 2))
        {
            _offset = transform.position + cam.transform.position;
        }
    }

    public Vector3 velocity = new Vector3(0f, 0f, 0f);

    [SerializeField] public Vector3 camPos_FPP_Walk;
    [SerializeField] public Vector3 camPos_FPP_Run;
    public Vector3 gravity = new Vector3(0f, -9.8f, 0f);

    public static bool isRun = false;

    public void JoysticMove()
    {
        if (variableJoystick.Vertical != 0 || variableJoystick.Horizontal != 0)
        {
            playerAnim.SetBool("IsWalk", true);
            Vector3 direction = cam.transform.forward * variableJoystick.Vertical + cam.transform.right * variableJoystick.Horizontal;

            direction = new Vector3(direction.x, 0f, direction.z);
            if (isRun)
            {
                playerAnim.SetBool("IsRun", true);
                controller.Move(direction * speed * 2.5f * Time.deltaTime);
            }
            else
            {
                playerAnim.SetBool("IsRun", false);
                controller.Move(direction * speed * Time.deltaTime);
            }

            
            //transform.forward = direction;
            transform.forward = Vector3.Lerp(transform.forward, new Vector3(cam.transform.forward.x, 0f, cam.transform.forward.z), 10f * Time.deltaTime);
        }
        else
        {
            playerAnim.SetBool("IsWalk", false);
            playerAnim.SetBool("IsRun", false);
        }
    }
    public void FPPMove()
    {
        controller.Move(velocity * Time.deltaTime);


        if (horizontalMovement != 0f || verticalMovement != 0f)
        {

            playerAnim.SetBool("IsWalk", true);
            Vector3 moveDirection = cam.transform.forward * verticalMovement + cam.transform.right * horizontalMovement;
            moveDirection = new Vector3(moveDirection.x, 0f, moveDirection.z);



            // 이동 처리
            moveDirection.Normalize();
            if (Input.GetKey(KeyCode.LeftShift))
            {
                playerAnim.SetBool("IsRun", true);
                controller.Move(moveDirection * speed * 3f * Time.deltaTime);
            }
            else
            {
                playerAnim.SetBool("IsRun", false);
                controller.Move(moveDirection * speed * Time.deltaTime);

            }
            transform.forward = Vector3.Lerp(transform.forward, new Vector3(cam.transform.forward.x, 0f, cam.transform.forward.z), 10f * Time.deltaTime);
        }
        else
        {
            playerAnim.SetBool("IsWalk", false);
            playerAnim.SetBool("IsRun", false);
        }


    }

    public void FPPJump()
    {
        

        if (velocity.y <= 0.01f)
        {
            velocity.y = 9.8f * 2f / 3f;
            playerAnim.SetBool("IsJump", true);
        }
        
    }

    public void FPPJumpAnim()
    {
        if (controller.isGrounded && velocity.y < 0f)
        {
            playerAnim.SetBool("IsJump", false);
        }

    }
    public Vector3 newFPPPos;

    public Vector3 playerDirect;

    public bool isblocked = false;

    [SerializeField] GameObject touchParticle;
    [SerializeField] Camera particleCam;

    
    public void StartParticle()
    {
        Vector3 particlePos = particleCam.ScreenToWorldPoint(Input.mousePosition);
        particlePos.z = 0f;

        if(touchParticle.TryGetComponent<RectTransform>(out RectTransform rect))
        {
            rect.position = particlePos;
        }

        if (touchParticle.TryGetComponent<ParticleSystem>(out ParticleSystem particle))
        {
            particle.Play();
        }
        Debug.Log("파티클 실행");
    }

    private Vector2 startPos;
    private bool isDragging;
    void Update()
    {

        if (controller.isGrounded && velocity.y <= 0.05f)
        {
            velocity.y = 0f;
            playerAnim.SetBool("IsJump", false);
        }
        velocity.y += -9.8f * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartParticle();
        }

        if (Input.GetMouseButtonDown(0))
        {
            StartParticle();
        }

        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        FixedCamPosition();

        JoysticMove();

        if (isFPP)
        {



            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                float horizontalRotate;
                float verticalRotate;
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        startPos = touch.position;
                        isDragging = true;
                        break;
                    case TouchPhase.Moved:
                        if (isDragging)
                        {
                            Vector2 delta = new Vector2();
                            if (touch.position.x > Screen.width/2)
                            {
                                delta = touch.position - startPos;
                            }

                            horizontalRotate = 5f* delta.x / Screen.width;
                            verticalRotate = 5f *delta.y / Screen.height;

                            // 사용자가 드래그하는 동안 수행할 동작
                            // horizontalInput와 verticalInput를 이용하여 입력 처리

                            float yRotateSize1 = horizontalRotate * turnSpeed;
                            
                            float yRotate1 = cam.transform.eulerAngles.y + yRotateSize1;

                            float xRotateSize1 = -verticalRotate * turnSpeed;
                            xRotate_TPP = Mathf.Clamp(xRotate_TPP + xRotateSize1, -85, 85);
                            cam.transform.eulerAngles = new Vector3(xRotate_TPP, yRotate1, 0);
                        }
                        break;
                    case TouchPhase.Ended:
                        isDragging = false;
                        break;
                }
                
                if(Input.touchCount > 1)
                {
                    Touch touch2 = Input.GetTouch(1);
                    switch (touch2.phase)
                    {
                        case TouchPhase.Began:
                            startPos = touch2.position;
                            isDragging = true;
                            break;
                        case TouchPhase.Moved:
                            if (isDragging)
                            {
                                Vector2 delta = new Vector2();
                                if (touch2.position.x > Screen.width/2)
                                {
                                    delta = touch2.position - startPos;
                                }

                                horizontalRotate = 5f * delta.x / Screen.width;
                                verticalRotate = 5f * delta.y / Screen.height;

                                // 사용자가 드래그하는 동안 수행할 동작
                                // horizontalInput와 verticalInput를 이용하여 입력 처리
                                //playerDirect = cam.transform.forward * verticalMovement + cam.transform.right * horizontalMovement;

                                float yRotateSize1 = horizontalRotate * turnSpeed;

                                float yRotate1 = cam.transform.eulerAngles.y + yRotateSize1;

                                float xRotateSize1 = -verticalRotate * turnSpeed;
                                xRotate_TPP = Mathf.Clamp(xRotate_TPP + xRotateSize1, -85, 85);
                                cam.transform.eulerAngles = new Vector3(xRotate_TPP, yRotate1, 0);
                            }
                            break;
                        case TouchPhase.Ended:
                            isDragging = false;
                            break;
                    }
                }
            }
            else
            {
                playerDirect = cam.transform.forward * verticalMovement + cam.transform.right * horizontalMovement;
            }

            // 좌우로 움직인 마우스의 이동량 * 속도에 따라 카메라가 좌우로 회전할 양 계산
            float yRotateSize = Input.GetAxis("Mouse X") * turnSpeed;
            // 현재 y축 회전값에 더한 새로운 회전각도 계산
            float yRotate = cam.transform.eulerAngles.y + yRotateSize;

            // 위아래로 움직인 마우스의 이동량 * 속도에 따라 카메라가 회전할 양 계산(하늘, 바닥을 바라보는 동작)
            float xRotateSize = -Input.GetAxis("Mouse Y") * turnSpeed;
            // 위아래 회전량을 더해주지만 -45도 ~ 80도로 제한 (-45:하늘방향, 80:바닥방향)
            // Clamp 는 값의 범위를 제한하는 함수
            xRotate_TPP = Mathf.Clamp(xRotate_TPP + xRotateSize, -85, 85);

            // 카메라 회전량을 카메라에 반영(X, Y축만 회전)
            cam.transform.eulerAngles = new Vector3(xRotate_TPP, yRotate, 0);

           
            Vector3 bb = transform.position + new Vector3(0f, 2f, 0f);
            cam.transform.position = bb + (cam.transform.rotation * new Vector3(0.5f, 0.2f, -2.5f));


        }
        else
        {

            FixedCamPosition();

            JoysticMove();
          

        }




        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isFPP)
            {
                cam.transform.position = transform.position + camPos;
                cam.transform.eulerAngles = camRot;
            }

            isFPP = !isFPP;
        }



    }
    public void FixedCamPosition()
    {
        cam.transform.position = transform.position + camPos;
    }

    private void AnimInit()
    {
        playerAnim.SetBool("IsWalk", false);
        playerAnim.SetBool("IsRun", false);
    }

    [SerializeField] GameObject forgePos;
    [SerializeField] GameObject shopPos;

    public static int stateBeforeLoadVillageScene = 0;
    public static int stateAfterLoadScene = 0;
    public void PosInit()
    {

        // 상대 위치를 이동하려는 오브젝트의 로컬 축으로 변환

        // 이동하려는 오브젝트의 위치를 대상 오브젝트의 로컬 축으로 이동
        if (stateBeforeLoadVillageScene == (int)LoadSceneState.Forge)
        {
            transform.position = forgePos.transform.position;
            transform.rotation = Quaternion.identity;
        }
        else if (stateBeforeLoadVillageScene == (int)LoadSceneState.Shop)
        {
            transform.position = shopPos.transform.position;
            transform.Rotate(0f, 90f, 0f);
        }
    }
   

    public Transform target; // 카메라가 따라다닐 타겟
    public float distance = 5f; // 카메라와 타겟 사이의 거리
    public float height = 2f; // 카메라와 타겟 사이의 높이
    public float smoothSpeed = 10f; // 카메라 이동 시 속도

    private Vector3 _offset; // 카메라 위치와 타겟 위치의 차이
    private RaycastHit _hitInfo; // 레이캐스트 결과 저장용 변수
    
}
