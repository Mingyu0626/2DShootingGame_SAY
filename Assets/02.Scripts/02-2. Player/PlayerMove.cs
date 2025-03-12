using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // MonoBehaviour : 여러 가지 이벤트 함수를 자동으로 호출해주는 기능
    // Component : 게임 오브젝트에 추가할 수 있는 여러 가지 기능

    // 목표 : 키보드 입력에 따라 플레이어를 이동시키고 싶다.
    [SerializeField]
    private float _speed = 3f;
    private float _speedVariance = 1f;
    private float _horizontalLimit = 3.0f;
    private float _verticalLimit = 3.0f;

    public float Speed
    {
        get { return _speed; }
        private set
        {
            if (0 <= value)
            {
                _speed = value;
                Debug.Log(_speed);
            }
        }
    }
    public float SpeedVariance
    {
        get { return _speedVariance; }
        private set { _speedVariance = value; }
    }
    public float HorizontalLimit
    {
        get { return _horizontalLimit; }
        private set { _horizontalLimit = value; }
    }
    public float VerticalLimit
    {
        get { return _verticalLimit; }
        private set { _verticalLimit = value; }
    }

    private void Start()
    {
    }

    private void Update()
    {
        // transform.Translate -> 이동하다라는 뜻으로, 매개 변수로 '속도'를 받는다.
        // 속도 : 방향 * 속력
        // 스칼라 : 크기

        // 벡터 : 크기 + 방향
        // Vector2 velocity = new Vector2(-100f, 0f);
        // Vector2 velocity2 = new Vector2(-1, 0) * 100f;
        // Vector2 velocity3 = new Vector2(-1, 1) * 50f;

        // 초당 3M(unit)만큼 위로 움직여라!
        // transform.Translate(Vector2.up * _moveSpeed * Time.deltaTime);
        // Time.deltaTime : 프레임 간 시간 간격을 의미
        // 30프레임은, Time.deltatTime이 1/30초, 60프레임은 1/60초

        // 키보드, 마우스, 터치, VR/AR, 조이스틱 등 외부에서 들어오는 
        // 입력 소스는 모두 'Input' 클래스를 통해 관리할 수 있다.

        // GetAxisRaw는 가속도 X, 즉 리턴값은 -1, 0, 1중 하나다.
        // GetAxis는 가속도 O, 즉 리턴값은 -1 ~ 1 사이의 실수

        // 벡터로부터 방향만 가져오는 것을 정규화라고 한다.
        // 정규화를 통해, 상하좌우 이동과 대각선 이동의 속도를 동일하게 바꿔줄 수 있다.
        // direction.Normalize();
        // 매직넘버 하드코딩하지 말자, 변수 ㄱㄱ


        // 과제 1 및 2
        if (IsPlayerInView())
        {
            Move();
            ClampPlayerHorizontalPosition();
        }
        else
        {
            ReversePlayerVerticalPosition();
        }

        // 과제 3
        SpeedCheck();
    }
    private void Move()
    {
        // 정규화 유무로 대각선 이동속도가 달라지냐 같아지냐가 결정된다.
        // 정규화 사용 시 : 대각선 이동속도 = 상하좌우 이동속도
        // 정규화 미사용 시 : 대각선 이동속도 > 상하좌우 이동속도
        Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")).normalized;

        transform.Translate(direction * Speed * Time.deltaTime);
        // Translate 대신 아래처럼 사용할 수도 있음, 3D 플젝에서는 형변환이 필요없으니, 아래처럼 써도 될듯
        // transform.position += (Vector3)(direction * Speed) * Time.deltaTime;
    }
    private bool IsPlayerInView()
    {
        return Mathf.Abs(transform.position.x) <= _verticalLimit;
        //Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        //return (0 <= screenPoint.x && screenPoint.x <= 1 && 
        //    0 <= screenPoint.y && screenPoint.y <= 1);
    }
    
    private void ClampPlayerHorizontalPosition()
    {
        transform.position =
                new Vector2(transform.position.x,
                    Mathf.Clamp(transform.position.y, -HorizontalLimit, HorizontalLimit));
    }

    private void ReversePlayerVerticalPosition()
    {
        transform.position =
               new Vector2(
                   Mathf.Clamp(-transform.position.x, -VerticalLimit, VerticalLimit),
                   Mathf.Clamp(transform.position.y, -HorizontalLimit, HorizontalLimit));
    }

    private void SpeedCheck()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Speed += SpeedVariance;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Speed -= SpeedVariance;
        }
    }

}
