using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBack : MonoBehaviour {

    //! Variables
    [SerializeField] private float initHeight;
    [SerializeField] private Vector3 currSpeed;
    [SerializeField] private bool isJumping;

    //! Parameters
    [SerializeField] private float lift = 30;
    [SerializeField] private float shift = 10;
    [SerializeField] private float speed = 10;  
    [SerializeField] private float gravityMult = 2;

    //! Properties
    public bool IsJumping {
        get {
            return isJumping;
        }
    }


    //! MonoBehaviour
    void OnEnable() {
        initHeight = transform.position.y;
        currSpeed = Quaternion.Euler(-lift, Random.Range(-shift, shift), 0) * Vector3.forward * speed;
        isJumping = true;
    }

    void Update() {
        if(isJumping) {
            if(transform.position.y < initHeight) {
                transform.position = new Vector3(transform.position.x, initHeight, transform.position.z);
                isJumping = false;
            }
            else {
                transform.Translate(currSpeed * Time.deltaTime, Space.World);
                currSpeed += Physics.gravity * gravityMult * Time.deltaTime;
            }
        }
    }
}
