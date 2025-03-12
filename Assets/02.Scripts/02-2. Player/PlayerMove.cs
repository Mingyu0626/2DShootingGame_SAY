using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // MonoBehaviour : ���� ���� �̺�Ʈ �Լ��� �ڵ����� ȣ�����ִ� ���
    // Component : ���� ������Ʈ�� �߰��� �� �ִ� ���� ���� ���

    // ��ǥ : Ű���� �Է¿� ���� �÷��̾ �̵���Ű�� �ʹ�.
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
        // transform.Translate -> �̵��ϴٶ�� ������, �Ű� ������ '�ӵ�'�� �޴´�.
        // �ӵ� : ���� * �ӷ�
        // ��Į�� : ũ��

        // ���� : ũ�� + ����
        // Vector2 velocity = new Vector2(-100f, 0f);
        // Vector2 velocity2 = new Vector2(-1, 0) * 100f;
        // Vector2 velocity3 = new Vector2(-1, 1) * 50f;

        // �ʴ� 3M(unit)��ŭ ���� ��������!
        // transform.Translate(Vector2.up * _moveSpeed * Time.deltaTime);
        // Time.deltaTime : ������ �� �ð� ������ �ǹ�
        // 30��������, Time.deltatTime�� 1/30��, 60�������� 1/60��

        // Ű����, ���콺, ��ġ, VR/AR, ���̽�ƽ �� �ܺο��� ������ 
        // �Է� �ҽ��� ��� 'Input' Ŭ������ ���� ������ �� �ִ�.

        // GetAxisRaw�� ���ӵ� X, �� ���ϰ��� -1, 0, 1�� �ϳ���.
        // GetAxis�� ���ӵ� O, �� ���ϰ��� -1 ~ 1 ������ �Ǽ�

        // ���ͷκ��� ���⸸ �������� ���� ����ȭ��� �Ѵ�.
        // ����ȭ�� ����, �����¿� �̵��� �밢�� �̵��� �ӵ��� �����ϰ� �ٲ��� �� �ִ�.
        // direction.Normalize();
        // �����ѹ� �ϵ��ڵ����� ����, ���� ����


        // ���� 1 �� 2
        if (IsPlayerInView())
        {
            Move();
            ClampPlayerHorizontalPosition();
        }
        else
        {
            ReversePlayerVerticalPosition();
        }

        // ���� 3
        SpeedCheck();
    }
    private void Move()
    {
        // ����ȭ ������ �밢�� �̵��ӵ��� �޶����� �������İ� �����ȴ�.
        // ����ȭ ��� �� : �밢�� �̵��ӵ� = �����¿� �̵��ӵ�
        // ����ȭ �̻�� �� : �밢�� �̵��ӵ� > �����¿� �̵��ӵ�
        Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")).normalized;

        transform.Translate(direction * Speed * Time.deltaTime);
        // Translate ��� �Ʒ�ó�� ����� ���� ����, 3D ���������� ����ȯ�� �ʿ������, �Ʒ�ó�� �ᵵ �ɵ�
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
