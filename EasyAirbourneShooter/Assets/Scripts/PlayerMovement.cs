using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Vector2 minBounds;
    Vector2 maxBounds;
    Animator myAnimator;
    Gun gun;

    [SerializeField] float defaultSpeed = 6f;
    [SerializeField] float screenPaddingH = 1f;
    [SerializeField] float screenPaddingTop = 1f;
    [SerializeField] float screenPaddingBottom = 1f;

    public static bool followMouse = false;

    private void Awake()
    {
        gun = GetComponent<Gun>();
    }
    private void Start()
    {
        myAnimator = GetComponent<Animator>();
        InitBounds();
    }

    private void Update()
    {
        MoveTransform();
    }

    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0,0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1,1));
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        MoveTransform();
    }

    private void MoveTransform()
    {
        if (followMouse)
        {
            var delta = defaultSpeed * Time.deltaTime;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            mousePosition.x = Mathf.Clamp(mousePosition.x, minBounds.x + screenPaddingH, maxBounds.x - screenPaddingH);
            mousePosition.y = Mathf.Clamp(mousePosition.y, minBounds.y + screenPaddingBottom, maxBounds.y - screenPaddingTop);
            transform.position = Vector2.MoveTowards(transform.position, mousePosition, delta);
            if (mousePosition.x == transform.position.x)
            {
                myAnimator.SetBool("SideMoveOn", false);
            }
            else
            {
                myAnimator.SetBool("SideMoveOn", true);
                transform.localScale = new Vector2(
                    Mathf.Sign(mousePosition.x - transform.position.x), 
                    1f);
                
            }
        }
        else
        {
            Vector2 delta = moveInput * defaultSpeed * Time.deltaTime;
            Vector2 newPosition = new Vector2();
            newPosition.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + screenPaddingH, maxBounds.x - screenPaddingH);
            newPosition.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + screenPaddingBottom, maxBounds.y - screenPaddingTop);
            if (Mathf.Abs(moveInput.x) < Mathf.Epsilon)
            {
                myAnimator.SetBool("SideMoveOn", false);
            }
            else
            {
                myAnimator.SetBool("SideMoveOn", true);
                transform.localScale = new Vector2(Mathf.Sign(moveInput.x) , 1f);
            }
            transform.position = newPosition;

        }
    }

    void OnFire(InputValue value)
    {
        if(gun != null)
        {
            gun.isFiring = value.isPressed;
        }
    }
}
