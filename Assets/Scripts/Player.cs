using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isPlayerOne = false;
    public bool isPlayerTwo = false;
    [SerializeField]
    private float _speed = 3.5f;
    private float _speedmulitplier = 2;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.15f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;

    private SpawnManager _spawnmanager;
    private bool _isTripleShotActive = false;

    [SerializeField]
    private GameObject _tripleShotPrefab;
    private bool _isSpeedPowerupActive = false;
    private bool _isShieldActive = false;
    [SerializeField]
    private GameObject _shieldVisualizer;
    [SerializeField]
    private GameObject _rightEnginehurt;
    [SerializeField]
    private GameObject _leftEnginehurt;

    [SerializeField]
    private AudioClip _laserAudioClip;
    private AudioSource _audioSource;

    private UImanager _uiManager;
    private GameManager _gameManager;
    [SerializeField]
    private int _laserCount = 25;
    [SerializeField]
    public bool isThereAmmo;
    // Start is called before the first frame update
    void Start()
    {
        _spawnmanager = GameObject.FindWithTag("Spawn Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.FindWithTag("Canvas").GetComponent<UImanager>();
        _audioSource = GetComponent<AudioSource>();
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if (_spawnmanager == null)
        {
            Debug.LogError("The Spawn Manager is not found");
        }

        if (_uiManager == null)
        {
            Debug.LogError("The UIManager is null");
        }
        if (_audioSource == null)
        {
            Debug.LogError("the audio Source is null");
        }
        else
        {
            _audioSource.clip = _laserAudioClip;
        }
        if (_gameManager._isCoopMode == false)
        {
            //take player to position = new postion(0,0,0)
            transform.position = new Vector3(0, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameManager._isCoopMode == false)
        {
            CalculateMovement();
            if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire && _laserCount > 0)
            {
                if (_laserCount >= 0)
                {
                    isThereAmmo = true;
                    ShootLaser();
                    _laserCount -= 1;
                    _uiManager.AmmoCount(_laserCount);
                }
                else
                {
                    isThereAmmo = false;
                }
            }
        }
        else
        {
            if (isPlayerOne == true)
            {
                PlayerOne();
                if (Input.GetKeyDown(KeyCode.Space) && (Time.time > _canFire) && (isPlayerOne == true))
                {
                    ShootLaser();
                }
            }
            else
            {
                PlayerTwo();
                if (Input.GetKeyDown(KeyCode.RightShift) && (Time.time > _canFire) && (isPlayerTwo == true))
                {
                    ShootLaser();
                }
            }
        }

    }

    void PlayerOne()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
        }

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }
    void PlayerTwo()
    {
        if (Input.GetKey(KeyCode.Keypad8))
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Keypad4))
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Keypad5))
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Keypad6))
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
        }

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    void CalculateMovement()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    void ShootLaser()
    {
        Vector3 laserSpawn = transform.position;
        laserSpawn.y += 1.05f;

        _canFire = Time.time + _fireRate;
        if (_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, laserSpawn, Quaternion.identity);
        }
        _audioSource.Play();
    }

    public void Damage()
    {
        if (_isShieldActive == true)
        {
            _isShieldActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }
        else
        {
            _lives--;

            if (_lives == 2)
            {
                _rightEnginehurt.SetActive(true);
            }
            else if (_lives == 1)
            {
                _leftEnginehurt.SetActive(true);
            }

            _uiManager.UpdateLives(_lives);

            if (_lives < 1)
            {
                _spawnmanager.OnPlayerDead();
                Destroy(this.gameObject);
            }
        }}

    public void TripleShotActivate()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    IEnumerator TripleShotPowerDownRoutine()
    {
        while (_isTripleShotActive == true)
        {
            yield return new WaitForSeconds(5f);
            _isTripleShotActive = false;
        }
    }

    public void SpeedPowerupActive()
    {
        _isSpeedPowerupActive = true;
        _speed *= _speedmulitplier;
        StartCoroutine(SpeedPowerDownRoutine());
    }
    IEnumerator SpeedPowerDownRoutine()
    {
        while (_isSpeedPowerupActive == true)
        {
            yield return new WaitForSeconds(5f);
            _isSpeedPowerupActive = false;
            _speed /= _speedmulitplier;
        }
    }

    public void ActivateShield()
    {
        _isShieldActive = true;
        StartCoroutine(ShieldActiveRoutine());
    }

    IEnumerator ShieldActiveRoutine() {
        while (_isShieldActive == true) {
            _shieldVisualizer.SetActive(true);
            yield return new WaitForSeconds(5f);
            _isShieldActive = false;
            _shieldVisualizer.SetActive(false);
        }
    }

    public void ScoreManager()
    {
        if (_gameManager._isCoopMode == false)
        {
            _uiManager.AddScore();
            _uiManager.BestScore();
        }
        else
        {
            _uiManager.AddScore();
        }
    }
    public void Ammo() {
        _laserCount += 25;
    }

}
