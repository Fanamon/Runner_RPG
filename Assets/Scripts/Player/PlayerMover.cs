using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _stepSizeByAxisX;
    [SerializeField] private float _minWidth;
    [SerializeField] private float _maxWidth;
    [SerializeField] private Transform _heroesGroup;

    private Vector3 _stepDirectionByAxisX;
    private Vector3 _targetPosition;
    private PlayerInput _input;

    private void Awake()
    {
        _stepDirectionByAxisX = new Vector3(_stepSizeByAxisX, 0, 0);
        _input = new PlayerInput();
        _input.Enable();
    }

    private void Start()
    {
        _targetPosition = _heroesGroup.position;
    }

    private void OnEnable()
    {
        _input.Player.MoveLeft.performed += ctx => OnMoveLeft();
        _input.Player.MoveRight.performed += ctx => OnMoveRight();
    }

    private void OnDisable()
    {
        _input.Player.MoveLeft.performed -= ctx => OnMoveLeft();
        _input.Player.MoveRight.performed -= ctx => OnMoveRight();
    }

    private void Update()
    {
        if (_heroesGroup.position.x != _targetPosition.x)
        {
            _targetPosition = new Vector3(_targetPosition.x, _targetPosition.y, _heroesGroup.position.z);
            _heroesGroup.position = Vector3.MoveTowards(_heroesGroup.position, _targetPosition, 
                _moveSpeed * Time.deltaTime);
        }
    }

    public void OnMoveLeft()
    {
        if (_targetPosition.x > _minWidth)
        {
            SetNextPosition(-_stepDirectionByAxisX);
        }
    }

    public void OnMoveRight()
    {
        if (_targetPosition.x < _maxWidth)
        {
            SetNextPosition(_stepDirectionByAxisX);
        }
    }

    private void SetNextPosition(Vector3 stepDirection)
    {
        _targetPosition += stepDirection;
    }
}
