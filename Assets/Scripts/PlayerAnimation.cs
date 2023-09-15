using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator _anim;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        if (_anim == null) {
            Debug.LogError("Animator not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
            _anim.SetBool("Left", true);
            _anim.SetBool("Right", false);
        }
        if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {
            _anim.SetBool("Right", true);
            _anim.SetBool("Left", false);
        }
        if(Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A)) {
            _anim.SetBool("Left", false);
            _anim.SetBool("Right", false);
        }
        if(Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D)) {
            _anim.SetBool("Right", false);
            _anim.SetBool("Left", false);
        }
    }
}
