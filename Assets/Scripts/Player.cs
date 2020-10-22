using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    #region SerializeFields
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _gravity = 1.0f;
    [SerializeField] private float _jumpHeight = 15.0f;
    [SerializeField] private int _coins = 0;
    [SerializeField] private int _lives = 3;
    [SerializeField] private float _deathZonePositionY = -20.0f;
    [SerializeField] private GameObject _deathZone;
    #endregion

    private float _yVelocity;
    private bool _canDoubleJump = false;
    private CharacterController _controller = null;
    private UIManager _uIManager = null;

    #region Scripts Handlers
    public CharacterController Controller
    {
        get
        {
            if (!_controller)
            {
                _controller = this.GetComponent<CharacterController>();
            }
            return _controller;
        }
    }

    public UIManager UIManager
    {
        get
        {
            if (!_uIManager)
            {
                _uIManager = GameObject.Find("Canvas")?.GetComponent<UIManager>();
            }
            return _uIManager;
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        UIManager.UpdateLifeDisplay(_lives);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        _deathZone.transform.position = new Vector3(this.transform.position.x, _deathZonePositionY, 0);
        Vector3 direction = new Vector3(horizontalInput, 0, 0);
        Vector3 velocity = _speed * direction;

        if (Controller.isGrounded == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
                _canDoubleJump = true;
            }
        }
        else
        {
            if (_canDoubleJump == true && Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity += _jumpHeight;
                _canDoubleJump = false;
            }

            _yVelocity = _yVelocity - _gravity;
        }

        velocity.y = _yVelocity;
        Controller.Move(velocity * Time.deltaTime);

        //if (this.gameObject.transform.position.y < -8.0f)
        //{
        //    this.gameObject.transform.GetChild(0).transform.parent = null;
        //    Destroy(this.gameObject);       
        //}
    }

    public void AddCoins()
    {
        _coins++;
        UIManager.UpdateCoinDisplay(_coins);
    }

    public void Damage()
    {
        _lives = _lives - 1;
        UIManager.UpdateLifeDisplay(_lives);

        if (_lives < 1)
        {
            SceneManager.LoadScene(0);
        }
    }
}
