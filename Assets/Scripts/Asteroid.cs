using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 20f;
    [SerializeField]
    private GameObject _explosionPrefab;
    private SpawnManager _spawnManager;
    private AudioSource _audioSource;

    void Start() {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _audioSource = GetComponent<AudioSource>();
        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is null");
        }
        if (_audioSource == null)
        {
            Debug.LogError("Audio Source is null");
        } 
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _audioSource.Play();
            _spawnManager.StartSpawning();
            Destroy(this.gameObject, 0.25f);
        }
    }
}
