using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPowerup : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 25f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + rotateSpeed * Time.deltaTime, transform.rotation.eulerAngles.z);
    }
}
