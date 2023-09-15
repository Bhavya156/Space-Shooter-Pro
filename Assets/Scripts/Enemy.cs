using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;

    private Player _player;
    private Animator _animator;
    private AudioSource _audioSource;
    [SerializeField]
    private GameObject _laserPrefab;
    private float _fireRate = 3.0f;
    private float _canFire = -1f;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();
        if (_player == null)
        {
            Debug.LogError("Player is null");
        }
        if (_audioSource == null)
        {
            Debug.LogError("Audio Source is null");
        }

        _animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        shootLaser();
    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y <= -6f)
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 8f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            _animator.SetTrigger("OnEnemyDestroyed");
            _speed = 0;
            _audioSource.Play();
            Destroy(this.gameObject, 2.8f);
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.ScoreManager();
            }
            _animator.SetTrigger("OnEnemyDestroyed");
            _audioSource.Play();
            _speed = 0;
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.8f);
        }
    }
    void shootLaser()
    {
        if (Time.deltaTime > _canFire)
        {
            _fireRate = Random.Range(3f, 7f);
            _canFire += _fireRate;
            GameObject enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();
            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();
            }
        }
    }
}

