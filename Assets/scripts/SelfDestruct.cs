using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] float blastDuration = 2f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, blastDuration);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
