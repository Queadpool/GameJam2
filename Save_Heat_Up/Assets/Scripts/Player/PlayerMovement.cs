using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController _controller = null;
    [SerializeField] private Animator _animController = null;
    [SerializeField] private AudioSource _audio = null;
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _gravity = -9.81f;
    private Vector3 _velocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        _controller.Move(move * _speed * Time.deltaTime);

        _velocity.y = _gravity;

        _controller.Move(_velocity * Time.deltaTime);


        if (Input.GetMouseButton(0))
        {
            _animController.SetBool("Hitting", true);
            _audio.Play();
        }
    }

    private void StopHit()
    {
        _animController.SetBool("Hitting", false);
    }
}
