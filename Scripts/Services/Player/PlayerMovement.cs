// Main Dependencies
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

// Game Dependencies
using Atbt.Controller;
using Atbt.Item;

namespace Atbt.Player {
public class PlayerMovement : MonoBehaviour {

#region -------------------- Serialized Variables --------------------
    [SerializeField] private SpriteRenderer Renderer;
    [SerializeField] private Rigidbody2D Rigidbody;
    
    [SerializeField] private Transform PlayerTransform;
    [SerializeField] private Transform CameraTransform;
    
    [SerializeField] private UnityEngine.Sprite CenterSprite;
    [SerializeField] private UnityEngine.Sprite SideSprite;
    [SerializeField] private UnityEngine.Sprite BackSprite;
#endregion
#region -------------------- Public Variables --------------------
    
#endregion
#region -------------------- Private Variables --------------------
    private Vector2 moveInput;
    
    private float moveSpeed = 5f;
#endregion
#region -------------------- Initial Functions --------------------
    void FixedUpdate()
    {
        MovePlayer();
    }

    void LateUpdate()
    {
        PlayerTransform.localRotation = Quaternion.Euler(0,0,0);
        CameraTransform.localRotation = Quaternion.Euler(0,0,0);
    }
#endregion
#region -------------------- Coroutines --------------------
    
#endregion
#region -------------------- Public Methods --------------------
    
#endregion
#region -------------------- Private Methods --------------------
    private void OnEnable()
    {
        InputController.Inst.MoveInputChanged += OnMoveInputChanged;
    }

    private void OnDisable()
    {
        if (InputController.Inst == null) return;

        InputController.Inst.MoveInputChanged -= OnMoveInputChanged;
    }
    
    private void OnTriggerEnter2D(Collider2D trigger)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Player has triggered an action");

        if (trigger.TryGetComponent(out Interactable interactable))
        {
            PlayerController.Inst.SetInteractable(interactable);
        }
    }

    private void OnTriggerExit2D(Collider2D trigger)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Player has left a trigger");

        if (trigger.TryGetComponent(out Interactable interactable))
        {
            PlayerController.Inst.SetInteractable(null);
        }
    }

    private void OnMoveInputChanged(Vector2 input)
    {
        moveInput = input;
    }

    private void MovePlayer()
    {
        SetSpriteXDirection();
        
        Vector2 movement = moveInput.normalized;
        Rigidbody.MovePosition(Rigidbody.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void SetSpriteXDirection()
    {
        if (moveInput.x > 0)
        {
            Renderer.sprite = SideSprite;
            Renderer.flipX = true;
        }
        
        else if (moveInput.x < 0)
        {
            Renderer.sprite = SideSprite;
            Renderer.flipX = false;
        }

        SetSpriteYDirection();
    }
    
    private void SetSpriteYDirection()
    {
        if (moveInput.y > 0)
        {
            Renderer.sprite = BackSprite;
        }
        
        else if (moveInput.y < 0)
        {
            Renderer.sprite = CenterSprite;
        }
    }
#endregion
}}
