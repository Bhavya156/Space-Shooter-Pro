using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;

    [SerializeField]
    private int powerupID;
    [SerializeField]
    private AudioClip _clip;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y <= -5.9f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            AudioSource.PlayClipAtPoint(_clip, transform.position);
            if (player != null)
            {
                switch(powerupID)
                {
                    case 0:
                        player.TripleShotActivate();
                        break;
                    case 1:
                        player.SpeedPowerupActive();
                        break;
                    case 2:
                        player.ActivateShield();
                        break;
                    case 3:
                        player.Ammo();
                        break;
                    default:
                        Debug.Log("Default value");
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }
}
