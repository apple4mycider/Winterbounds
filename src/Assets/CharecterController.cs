using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this code is litteral dogshit
public class CharecterController : MonoBehaviour
{
    Transform PlayerTransform;
    public Transform Test;
    Animator animator;
    public float speed;
    bool YMovementLock = false;
    bool XMovementLock = false;

    public bool movement = true;
    // Start is called before the first frame update
    void Start()
    {
        PlayerTransform = GetComponent<Transform>();
        animator = gameObject.GetComponent<Animator>();
    }
    void KeyMovement()
    {
        if (Input.GetKey(KeyCode.W) && !YMovementLock)
        {
            PlayerTransform.position = transform.position + Vector3.up * speed * Time.deltaTime;
            animator.SetBool("Up", true);
            animator.SetBool("Down", false);
            animator.SetBool("Left", false);
            animator.SetBool("Right", false);
        }
        if (Input.GetKey(KeyCode.S) && !YMovementLock)
        {
            PlayerTransform.position = transform.position + Vector3.down * speed * Time.deltaTime;
            animator.SetBool("Up", false);
            animator.SetBool("Down", true);
            animator.SetBool("Left", false);
            animator.SetBool("Right", false);
        }
        if (Input.GetKey(KeyCode.A) && !XMovementLock)
        {
            PlayerTransform.position = transform.position + Vector3.left * speed * Time.deltaTime;
            animator.SetBool("Up", false);
            animator.SetBool("Down", false);
            animator.SetBool("Left", true);
            animator.SetBool("Right", false);
        }
        if (Input.GetKey(KeyCode.D) && !XMovementLock)
        {
            PlayerTransform.position = PlayerTransform.position + Vector3.right * speed * Time.deltaTime;
            animator.SetBool("Up", false);
            animator.SetBool("Down", false);
            animator.SetBool("Left", false);
            animator.SetBool("Right", true);
        }
        if (!Input.GetKey(KeyCode.D))
            animator.SetBool("Right", false);
        if (!Input.GetKey(KeyCode.A))
            animator.SetBool("Left", false);
        if (!Input.GetKey(KeyCode.S))
            animator.SetBool("Down", false);
        if (!Input.GetKey(KeyCode.W))
            animator.SetBool("Up", false);
    }
    
    bool idk = true;
    float RelativeX;
    float RelativeY;
    bool endOfCutscene = false;  
    bool cinematicMove(Transform point, float speed)
    {
        if (!endOfCutscene)
        {
            movement = false;
            if (idk)
            {
                RelativeX = point.position.x - PlayerTransform.position.x;
                RelativeY = point.position.y - PlayerTransform.position.y;
                idk = false;
            }
            if (Mathf.Abs(RelativeX) > Mathf.Abs(RelativeY))
                if (RelativeX > 0)
                    animator.SetBool("Right", true);
                else
                    animator.SetBool("Left", true);
            else
                if (RelativeY > 0)
                    animator.SetBool("Up", true);
                else
                    animator.SetBool("Down", true);
            
            PlayerTransform.position = PlayerTransform.position + Vector3.right * RelativeX * Time.deltaTime * speed;
            PlayerTransform.position = PlayerTransform.position + Vector3.up * RelativeY * Time.deltaTime * speed;
            if (new Vector3(Mathf.Round(PlayerTransform.position.x), Mathf.Round(PlayerTransform.position.y), 0) == new Vector3(Mathf.Round(point.position.x), Mathf.Round(point.position.y), 0))
            {
                movement = true;
                endOfCutscene = true;
                return true;
                animator.SetBool("Right", false);
                animator.SetBool("Left", false);
                animator.SetBool("Down", false);
                animator.SetBool("Up", false);
            }
            else    
                return false;
        }
        return true;
    }

    void Update()
    {
        if (movement)
            KeyMovement();
        cinematicMove(Test, 1);
        
    }
}
