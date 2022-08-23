using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;

public class Punch : MonoBehaviour
{
    private PlayerInput _playerInput;
    [SerializeField] ThirdPersonController controller;
    [SerializeField] private Animator _animator;
    private StarterAssetsInputs _input;
    [SerializeField] public float punchtime;
    [SerializeField] public float punchtime2;
    // Start is called before the first frame update
    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _input = GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_input.punch)
        {
            StartCoroutine(punching());
        }
    }
    

    IEnumerator punching()
    {
        _animator.SetBool("Punch", true);
        controller.enabled = false;
        _input.punch = false;
        if(_input.punch)
        {
            _input.punch = false;
            yield return new WaitForSeconds(punchtime2);
            _animator.SetBool("Punch", false);
            controller.enabled = true;
        }
        else
        {
            yield return new WaitForSeconds(punchtime);
            _animator.SetBool("Punch", false);
            yield return new WaitForSeconds(1.03f - punchtime);
            controller.enabled = true;
        }
        
    }
}
