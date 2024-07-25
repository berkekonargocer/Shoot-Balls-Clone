using UnityEngine;
using UnityEngine.InputSystem;

public enum State
{
    IDLE,
    MOVING
}

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]
public class AutoForwardMovement : MonoBehaviour
{
    [field: SerializeField] public float HorizontalMovementSpeed { get; private set; } = 6.0f;
    [field: SerializeField] public float VerticalMovementSpeed { get; private set; } = 32.5f;

    PlayerInput _playerInput;
    Rigidbody _objectRigidbody;

    Vector2 _moveDirection;

    const float MAX_X_POSITION_LEFT = 2.6f;
    const float MAX_X_POSITION_RIGHT = 1.6f;
    const float VERTICAL_MOVEMENT_SPEED_MULTIPLIER = 2.0f;
    const float HORIZONTAL_MOVEMENT_SPEED_MULTIPLIER = 1.0f;

    public State CharState { get; private set; } = State.IDLE;


    void OnEnable() {
        GameManager.Instance.OnWinGame += StopMovement;
        GameManager.Instance.OnLoseGame += StopMovement;
        GameManager.Instance.OnStartGame += StartMovement;
    }

    void OnDisable() {
        GameManager.Instance.OnWinGame -= StopMovement;
        GameManager.Instance.OnLoseGame -= StopMovement;
        GameManager.Instance.OnStartGame -= StartMovement;
    }

    void Awake() {
        _objectRigidbody = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();
    }

    void Update() {
        switch (CharState)
        {
            case State.IDLE:
                break;
            case State.MOVING:
                ApplyMovement();
                break;
        }
    }


    public void IncrementVerticalMovementSpeed(float speedToAddUp) {
        VerticalMovementSpeed += speedToAddUp;
    }

    public void SetVerticalMovementSpeed(float speed) {
        VerticalMovementSpeed = speed;
    }


    Vector3 GetMovementDirection() {
        if (_playerInput.actions["LeftClick"].ReadValue<float>() == 0)
        {
            _moveDirection = Vector3.zero;
        }
        else
        {
            _moveDirection = _playerInput.actions["Move"].ReadValue<Vector2>();
        }

        Vector3 moveDirection = new Vector3(_moveDirection.x * HorizontalMovementSpeed * HORIZONTAL_MOVEMENT_SPEED_MULTIPLIER, 0, VerticalMovementSpeed * VERTICAL_MOVEMENT_SPEED_MULTIPLIER) * Time.deltaTime;

        return moveDirection;
    }

    void ApplyMovement() {
        Vector3 moveDirection = GetMovementDirection();
        Vector3 newPosition = transform.position + moveDirection;

        newPosition.x = Mathf.Clamp(newPosition.x, -MAX_X_POSITION_LEFT, MAX_X_POSITION_RIGHT);

        transform.position = newPosition;
    }

    void StartMovement() {
        CharState = State.MOVING;
    }

    void StopMovement(float score) {
        CharState = State.IDLE;
        _objectRigidbody.velocity = Vector3.zero;
    }
}