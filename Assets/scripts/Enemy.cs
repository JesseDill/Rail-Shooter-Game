using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //[SerializeField] ParticleSystem deathFX;
    [SerializeField] GameObject deathFX;
    [SerializeField] Transform parent;
    [SerializeField] int scorePerHit = 12;
    [SerializeField] int hits = 30;




    ScoreBoard scoreBoard;



    // Start is called before the first frame update
    void Start()
    {
        AddBoxCollider();
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void AddBoxCollider()
    {
        Collider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        hits = hits - 1;
        if(hits <= 0)
        {
            KillEnemy();
        }
    }

    private void KillEnemy()
    {   
        scoreBoard.ScoreHit(scorePerHit);
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parent;

        //deathFX.transform.position = this.transform.position;
        //deathFX.Play();

        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
