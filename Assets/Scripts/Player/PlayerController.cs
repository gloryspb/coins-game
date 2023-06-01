using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    private Animator _animator;
    private Vector2 _direction;
    private Rigidbody2D _rigidbody;
    public PlayerControlTypeHolder.ControlTypeEnum currentControlType;
    public ItemStorage itemStorage;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (InventoryRenderer.inventoryIsOpen)
            {
                InventoryRenderer.Instance.CloseInventory();
            }
            else
            {
                InventoryRenderer.Instance.OpenInventory(itemStorage, false);
            }
        }
    }

    private void FixedUpdate()
    {
        float _speedModifier = 1f;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _speedModifier = 1.5f;
        }
        Move(_speedModifier);
    }

    private void Move(float _speedModifier)
    {
        currentControlType = PlayerControlTypeHolder.ControlType;
        _direction = Vector2.zero;

        if (currentControlType == PlayerControlTypeHolder.ControlTypeEnum.Mouse || currentControlType == PlayerControlTypeHolder.ControlTypeEnum.Both)
        {
            if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
            {
                Vector2 _centerScreen = new Vector2(Screen.width / 2f, Screen.height / 2f);
                Vector2 _mousePosition = Input.mousePosition;

                float _distance = Vector2.Distance(_centerScreen, _mousePosition);
                float _distancePercent = _distance / (Screen.width / 2f);

                // доработать

                _speedModifier = _distancePercent < 0.5f ? 1f : 1.5f;
                Vector2 _targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _direction = _targetPosition - _rigidbody.position;
                _direction.Normalize();
            }
        }
        if (currentControlType == PlayerControlTypeHolder.ControlTypeEnum.WASD || currentControlType == PlayerControlTypeHolder.ControlTypeEnum.Both)
        {
            float _horizontalInput = Input.GetAxisRaw("Horizontal");
            float _verticalInput = Input.GetAxisRaw("Vertical");
            _direction += new Vector2(_horizontalInput, _verticalInput);
            _direction.Normalize();
        }

        _rigidbody.MovePosition(_rigidbody.position + _direction * Time.deltaTime * _moveSpeed * _speedModifier);

        _animator.SetFloat("Speed", _direction.magnitude);
        if (_direction != new Vector2(0, 0))
        {
            _animator.SetFloat("Vertical", _direction.y);
            _animator.SetFloat("Horizontal", _direction.x);
            _animator.speed = _speedModifier != 1 ? 1.4f : 1f;
            Vector2 Scaler = transform.localScale;
            if (_direction.x < 0 && Scaler.x > 0)
            {
                Scaler.x *= -1;
            }
            else if (_direction.x > 0 && Scaler.x < 0)
            {
                Scaler.x *= -1;
            }
            transform.localScale = Scaler;
        }
    }
}
