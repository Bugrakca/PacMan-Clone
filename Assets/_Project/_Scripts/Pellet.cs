using System;
using System.Collections;
using System.Collections.Generic;
using _Project._Scripts;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    public int points = 10;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            Eat();
        }
    }

    protected virtual void Eat()
    {
        FindObjectOfType<GameManager>().PelletEaten(this);
    }
}
