using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ForgeCharacterController : MonoBehaviour
{
    [SerializeField] Camera cam;

    public CharacterController controller;
    private Rigidbody rigidbody;
    //public float speed = 10f;
    public float jumpHeight = 3f;
    public float dash = 5f;
    public float rotSpeed = 3f;

    //private Vector3 dir = Vector3.zero;
    [SerializeField] Vector3 camPos;
    public Vector3 camRot;

    // Start is called before the first frame update
    float magnitude;
    void Start()
    {
        PosInit();
        magnitude = (Vector3.zero + camPos_FPP_Walk).magnitude;
        //rigidbody = this.GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();

        cam.transform.Rotate(60f, 180f, 0f);
        transform.Rotate(0f, 180f, 0f);
    }

    public bool isFPP = true;
    public float turnSpeed = 4.0f; // ���콺 ȸ�� �ӵ�    
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

        //�߰�
        _offset = transform.position - cam.transform.position;
        distance = 3f;//Vector3.Distance(transform.position, cam.transform.position);

        isFPP = true;
        if (!(stateAfterLoadScene == 2))
        {
            _offset = transform.position + cam.transform.position;
        }

        
    }

    public Vector3 velocity = new Vector3(0f, 0f, 0f);

    [SerializeField] public Vector3 camPos_FPP_Walk;
    [SerializeField] public Vector3 camPos_FPP_Run;
    public Vector3 gravity = new Vector3(0f, -9.8f, 0f);


    public void FPPMove()
    {
        //FPPJumpAnim();
        FPPJump();
        controller.Move(velocity * Time.deltaTime);


        if (horizontalMovement != 0f || verticalMovement != 0f)
        {

            playerAnim.SetBool("IsWalk", true);
            Vector3 moveDirection = cam.transform.forward * verticalMovement + cam.transform.right * horizontalMovement;
            moveDirection = new Vector3(moveDirection.x, 0f, moveDirection.z);



            // �̵� ó��


            //transform.forward = moveDirection;
            //transform.forward = Vector3.Lerp(transform.forward, moveDirection, 30f * Time.deltaTime);
            moveDirection.Normalize();
            if (Input.GetKey(KeyCode.LeftShift))
            {
                playerAnim.SetBool("IsRun", true);
                /*Vector3 bb = new Vector3(transform.forward.x, 0f, transform.forward.z);
                bb.Normalize();
                //Vector3 newVec = new Vector3(bb.x, camPos_FPP_Run.y, bb.z);
                Vector3 newVec = camPos_FPP_Run;
                //cam.transform.position = transform.position + camPos_FPP_Run;
                cam.transform.position = transform.position + newVec;*/
                controller.Move(moveDirection * speed * 3f * Time.deltaTime);
            }
            else
            {
                playerAnim.SetBool("IsRun", false);
                //Vector3.Lerp(transform.position, transform.position + moveDirection, 10f * Time.deltaTime);
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
        if (controller.isGrounded && velocity.y <= 0.05f)
        {
            velocity.y = 0f;
            playerAnim.SetBool("IsJump", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && velocity.y <= 0.01f)
        {
            velocity.y = 9.8f * 2f / 3f;
            playerAnim.SetBool("IsJump", true);
        }

        velocity.y += -9.8f * Time.deltaTime;
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

    /*[SerializeField] GameObject touchParticle;
    [SerializeField] Camera particleCam;


    public void StartParticle()
    {
        Vector3 particlePos = particleCam.ScreenToWorldPoint(Input.mousePosition);
        //Vector3 particlePos = particleCam.WorldToScreenPoint(Input.mousePosition);
        particlePos.z = 0f;

        //touchParticle.transform.position = particlePos;
        if (touchParticle.TryGetComponent<RectTransform>(out RectTransform rect))
        {
            rect.position = particlePos;
        }

        if (touchParticle.TryGetComponent<ParticleSystem>(out ParticleSystem particle))
        {
            particle.Play();
        }
        Debug.Log("��ƼŬ ����");
    }*/

    [SerializeField] GameObject touchParticle;
    [SerializeField] Camera particleCam;


    public void StartParticle()
    {
        Vector3 particlePos = particleCam.ScreenToWorldPoint(Input.mousePosition);
        //particlePos = new Vector3(particlePos.x * CameraResolution.scaleWidth, particlePos.y * CameraResolution.scaleHeight, 0f);
        //Vector3 particlePos = particleCam.WorldToScreenPoint(Input.mousePosition);
        particlePos.z = 0f;

        //touchParticle.transform.position = particlePos;
        if (touchParticle.TryGetComponent<RectTransform>(out RectTransform rect))
        {
            rect.position = particlePos;
        }

        if (touchParticle.TryGetComponent<ParticleSystem>(out ParticleSystem particle))
        {
            particle.Play();
        }
        Debug.Log("��ƼŬ ����");
    }

    private Vector2 startPos;
    private bool isDragging;

    [SerializeField] TextMeshProUGUI widthText;
    [SerializeField] TextMeshProUGUI touchText;
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
        /*if (Input.GetKey(KeyCode.UpArrow))
        {
            verticalMovement = 1f;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            verticalMovement = -1f;
        }
        else
        {
            verticalMovement = 0f;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            horizontalMovement = 1f;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            horizontalMovement = -1f;
        }
        else
        {
            horizontalMovement = 0f;
        }*/

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
                            Vector2 delta = new Vector2(0f, 0f);
                            if (touch.position.x > Screen.width / 2f)
                            {
                                delta = touch.position - startPos;

                                touchText.text = "Touch Point: " + touch.position.x.ToString();
                                widthText.text = "Screen Width: " + Screen.width.ToString();

                                Debug.Log("��ġ ������: " + touch.position.x);
                                Debug.Log("ȭ�� �ʺ� ������: " + Screen.width/2);
                            }

                            horizontalRotate = 5f * delta.x / Screen.width;
                            verticalRotate = 5f * delta.y / Screen.height;

                            // ����ڰ� �巡���ϴ� ���� ������ ����
                            // horizontalInput�� verticalInput�� �̿��Ͽ� �Է� ó��
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

                if (Input.touchCount > 1)
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
                                Vector2 delta = new Vector2(0f, 0f);
                                if (touch2.position.x > Screen.width / 2)
                                {
                                    delta = touch2.position - startPos;
                                }

                                horizontalRotate = 5f * delta.x / Screen.width;
                                verticalRotate = 5f * delta.y / Screen.height;

                                // ����ڰ� �巡���ϴ� ���� ������ ����
                                // horizontalInput�� verticalInput�� �̿��Ͽ� �Է� ó��
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

            //newFPPPos = new Vector3(FPPPos.x*Mathf.Cos(yRotate_TPP), FPPPos.y, FPPPos.z*Mathf.Sin(yRotate_TPP));
            //cam.transform.position = Vector3.Lerp(cam.transform.position, transform.position + camPos_FPP_Walk, 0.1f * Time.deltaTime);
            //Vector3 bb = new Vector3(transform.forward.x, 0f, transform.forward.z);
            //bb.Normalize();
            //Vector3 newVec = new Vector3(bb.x, camPos_FPP_Walk.y, bb.z);
            //Vector3 newVec = camPos_FPP_Walk;
            //cam.transform.position = transform.position + newVec;
            //cam.transform.rotation = transform.rotation;
            //cam.transform.position = transform.position + FPPPos;

            // �¿�� ������ ���콺�� �̵��� * �ӵ��� ���� ī�޶� �¿�� ȸ���� �� ���
            /*float yRotateSize = Input.GetAxis("Mouse X") * turnSpeed;
            // ���� y�� ȸ������ ���� ���ο� ȸ������ ���
            float yRotate = cam.transform.eulerAngles.y + yRotateSize;

            // ���Ʒ��� ������ ���콺�� �̵��� * �ӵ��� ���� ī�޶� ȸ���� �� ���(�ϴ�, �ٴ��� �ٶ󺸴� ����)
            float xRotateSize = -Input.GetAxis("Mouse Y") * turnSpeed;
            // ���Ʒ� ȸ������ ���������� -45�� ~ 80���� ���� (-45:�ϴù���, 80:�ٴڹ���)
            // Clamp �� ���� ������ �����ϴ� �Լ�
            xRotate_TPP = Mathf.Clamp(xRotate_TPP + xRotateSize, -85, 85);
            //yRotate_TPP = Mathf.Clamp(yRotate_TPP + yRotateSize, -90, 90);

            // ī�޶� ȸ������ ī�޶� �ݿ�(X, Y�ุ ȸ��)
            cam.transform.eulerAngles = new Vector3(xRotate_TPP, yRotate, 0);*/
            //transform.rotation = Quaternion.Lerp(transform.rotation, cam.transform.rotation, 10f * Time.deltaTime);

            //Debug.Log(magnitude);

            //cam.transform.position = transform.position - new Vector3(cam.transform.forward.x, 0f, cam.transform.forward.z) * magnitude + new Vector3(0f, 2.2f, 0f);//Vector3.Cross(new Vector3(-0.5f, 2.2f, 0f), cam.transform.forward.normalized);
            //cam.transform.position = transform.position + (transform.rotation * new Vector3(0.5f, 2.2f, -2.5f));//.normalized * magnitude;



            if (!(stateAfterLoadScene == 2))
            {
                //Debug.Log("asd");


                Vector3 targetPosition = target.position + Vector3.up * height;

                int wallLayerMask = 1 << LayerMask.NameToLayer("Wall");
                if (Physics.Raycast(transform.position + new Vector3(0f, 2f, 0f), -cam.transform.forward, out _hitInfo, distance))
                {
                    // ����ĳ��Ʈ�� ������ ������Ʈ ���ʿ� ī�޶� ��ġ ����

                    cam.transform.position = Vector3.Lerp(cam.transform.position, _hitInfo.point, 20f * smoothSpeed * Time.deltaTime);
                    //isblocked = true;
                }
                else
                {
                    //Debug.Log("ccc");
                    Vector3 bb = transform.position + new Vector3(0f, 2f, 0f);
                    cam.transform.position = bb + (cam.transform.rotation * new Vector3(0.5f, 0.2f, -2.5f));
                }
                // A���� B�� ���ϴ� ���� ���� ���
                Vector3 directionToTarget = cam.transform.position - transform.position;

                // ���� ���͸� ����ȭ�Ͽ� ũ�⸦ 1�� ����
                Vector3 normalizedDirection = directionToTarget.normalized;

                /*if (Physics.Raycast(transform.position + new Vector3(0f, 2f, 0f), -cam.transform.forward, out _hitInfo, distance, LayerMask.GetMask("Wall")))
                {
                    // ����ĳ��Ʈ�� ������ ������Ʈ ���ʿ� ī�޶� ��ġ ����
                    Debug.Log("aaa");
                    float dist = (_hitInfo.point - transform.position + new Vector3(0f, ydistance, 0f)).magnitude * ratio;
                    //cam.transform.position = (transform.position + new Vector3(0f, ydistance, 0f)) + -cam.transform.forward * dist;

                    cam.transform.position = Vector3.Lerp(cam.transform.position, _hitInfo.point, 20f * smoothSpeed * Time.deltaTime);
                    //isblocked = true;
                }
                else if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hitInfo, distance, LayerMask.GetMask("Wall")))
                {
                    // ����ĳ��Ʈ�� ������ ������Ʈ ���ʿ� ī�޶� ��ġ ����
                    Debug.Log("bbb");
                    float dist = (_hitInfo.point - transform.position + new Vector3(0f, ydistance, 0f)).magnitude * 0.95f;
                    //cam.transform.position = (transform.position + new Vector3(0f, ydistance, 0f)) + -cam.transform.forward * dist;

                    //cam.transform.position = Vector3.Lerp(cam.transform.position, _hitInfo.point, 20f * smoothSpeed * Time.deltaTime);
                    cam.transform.position = Vector3.Lerp(cam.transform.position, (transform.position + new Vector3(0f, ydistance, 0f)) + -cam.transform.forward * dist, 20f * smoothSpeed * Time.deltaTime);
                    //isblocked = true;


                } else if (Physics.Raycast(cam.transform.position, -cam.transform.right, out _hitInfo, distance, LayerMask.GetMask("Wall")))
                {
                    // ����ĳ��Ʈ�� ������ ������Ʈ ���ʿ� ī�޶� ��ġ ����
                    Debug.Log("ccc");
                    float dist = (_hitInfo.point - transform.position + new Vector3(0f, ydistance, 0f)).magnitude * 0.95f;
                    //cam.transform.position = (transform.position + new Vector3(0f, ydistance, 0f)) + -cam.transform.forward * dist;

                    //cam.transform.position = Vector3.Lerp(cam.transform.position, _hitInfo.point, 20f * smoothSpeed * Time.deltaTime);
                    cam.transform.position = Vector3.Lerp(cam.transform.position, (transform.position + new Vector3(0f, ydistance, 0f)) + -cam.transform.right * dist, 20f * smoothSpeed * Time.deltaTime);
                    //isblocked = true;


                }
                else if (Physics.Raycast(cam.transform.position, (transform.position + new Vector3(0f, ydistance, 0f) - cam.transform.position).normalized, out _hitInfo, distance, LayerMask.GetMask("Wall")))
                {
                    // ����ĳ��Ʈ�� ������ ������Ʈ ���ʿ� ī�޶� ��ġ ����
                    Debug.Log("ddd");
                    float dist = (_hitInfo.point - transform.position + new Vector3(0f, ydistance, 0f)).magnitude * 0.95f;
                    //cam.transform.position = (transform.position + new Vector3(0f, ydistance, 0f)) + -cam.transform.forward * dist;

                    //cam.transform.position = Vector3.Lerp(cam.transform.position, _hitInfo.point, 20f * smoothSpeed * Time.deltaTime);
                    cam.transform.position = Vector3.Lerp(cam.transform.position, _hitInfo.point * dist, 20f * smoothSpeed * Time.deltaTime);
                    //isblocked = true;


                }
                else
                {
                    //Debug.Log("ccc");
                    Vector3 bb = transform.position + new Vector3(0f, 2f, 0f);
                    cam.transform.position = bb + (cam.transform.rotation * new Vector3(0.5f, 0.2f, -2.5f));
                }*/

                /*if (Physics.Raycast(cam.transform.position, (transform.position + new Vector3(0f, ydistance, 0f) - cam.transform.position).normalized, out _hitInfo, distance, LayerMask.GetMask("Wall")))
                {
                    // ����ĳ��Ʈ�� ������ ������Ʈ ���ʿ� ī�޶� ��ġ ����
                    Debug.Log("CC");
                    float dist = (_hitInfo.point - transform.position + new Vector3(0f, ydistance, 0f)).magnitude * 0.95f;
                    //cam.transform.position = (transform.position + new Vector3(0f, ydistance, 0f)) + -cam.transform.forward * dist;

                    //cam.transform.position = Vector3.Lerp(cam.transform.position, _hitInfo.point, 20f * smoothSpeed * Time.deltaTime);
                    cam.transform.position = Vector3.Lerp(cam.transform.position, _hitInfo.point, 20f * smoothSpeed * Time.deltaTime);
                    //isblocked = true;


                }
                else
                {
                    //Debug.Log("ccc");
                    Vector3 bb = transform.position + new Vector3(0f, 2f, 0f);
                    cam.transform.position = bb + (cam.transform.rotation * new Vector3(0.5f, 0.2f, -2.5f));
                }*/
            }
            //FPPMove();

        }
        else
        {

            FixedCamPosition();

            JoysticMove();
            /*
            //Debug.Log(transform.forward);
            if (verticalMovement != 0f || horizontalMovement != 0f)
            {
                Vector3 direction = Vector3.forward * verticalMovement + Vector3.right * horizontalMovement;

                direction.Normalize();

                if (!(direction == Vector3.zero))
                {
                    /*if (Mathf.Sign(transform.forward.x) != Mathf.Sign(verticalMovement) || Mathf.Sign(transform.forward.z) != Mathf.Sign(horizontalMovement))
                    {
                        transform.Rotate(0, 0.01f, 0);
                    }
                    //transform.forward = direction;
                    transform.forward = Vector3.Lerp(transform.forward, direction, rotSpeed*10f*Time.deltaTime);
                    //transform.eulerAngles = direction;
                    
                }
                /*if (dir != Vector3.zero)
                {
                    if (Mathf.Sign(transform.forward.x) != Mathf.Sign(dir.x) || Mathf.Sign(transform.forward.z) != Mathf.Sign(dir.z))
                    {
                        transform.Rotate(0, 1, 0);
                    }
                    transform.forward = Vector3.Lerp(transform.forward, dir, 5f * rotSpeed * Time.deltaTime);
                }

                _velocity.y = -9.8f;
                controller.Move(direction * speed * Time.deltaTime);
                controller.Move(_velocity * Time.deltaTime);
                //transform.position = (transform.position + direction * speed * Time.deltaTime);
                

                /*cam.transform.eulerAngles = camAngle_TPP;
                transform.position = (this.gameObject.transform.position + dir * speed * Time.deltaTime);*/

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

    [SerializeField] float ydistance = 0f;
    [SerializeField] float ratio = 0f;
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
        //Vector3 relativePosition = forgePos.transform.position - transform.position;

        // ��� ��ġ�� �̵��Ϸ��� ������Ʈ�� ���� ������ ��ȯ
        //Vector3 localPosition = transform.InverseTransformDirection(relativePosition);

        // �̵��Ϸ��� ������Ʈ�� ��ġ�� ��� ������Ʈ�� ���� ������ �̵�
        //transform.Translate(localPosition);
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
    /*public Vector3 aa;
    private void FixedUpdate()
    {

        
    }*/
    /*[SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float mouseSensitivity = 100f;

    private Camera playerCamera;
    private Vector3 velocity = Vector3.zero;
    private float verticalLookRotation;

    private void Start()
    {
        playerCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        verticalLookRotation += mouseY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);

        playerCamera.transform.localEulerAngles = new Vector3(-verticalLookRotation, 0f, 0f);

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 moveHorizontal = transform.right * horizontalInput;
        Vector3 moveVertical = transform.forward * verticalInput;

        velocity = (moveHorizontal + moveVertical).normalized * moveSpeed;


        transform.Translate(velocity * Time.deltaTime, Space.World);
    }*/

    public void JoysticMove()
    {
        if (variableJoystick.Vertical != 0 || variableJoystick.Horizontal != 0)
        {
            playerAnim.SetBool("IsWalk", true);
            Vector3 direction = cam.transform.forward * variableJoystick.Vertical + cam.transform.right * variableJoystick.Horizontal;

            direction = new Vector3(direction.x, 0f, direction.z);
            if (false)
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

    public Transform target; // ī�޶� ����ٴ� Ÿ��
    public float distance = 5f; // ī�޶�� Ÿ�� ������ �Ÿ�
    public float height = 2f; // ī�޶�� Ÿ�� ������ ����
    public float smoothSpeed = 10f; // ī�޶� �̵� �� �ӵ�

    private Vector3 _offset; // ī�޶� ��ġ�� Ÿ�� ��ġ�� ����
    private RaycastHit _hitInfo; // ����ĳ��Ʈ ��� ����� ����


    /* private void FixedUpdate()
     {

     }*/
}
