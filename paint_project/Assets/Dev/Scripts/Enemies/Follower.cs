using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.Factory

{
public class Follower : MonoBehaviour, IProduct
{
    [SerializeField] private string productName = "Follower";
    public string ProductName { get => productName; set => productName = value ;}

    private ParticleSystem particleSystem;

    public void Initialize()
    {
        gameObject.name = productName;
        particleSystem = GetComponentInChildren<ParticleSystem>();
        particleSystem?.Stop();
        particleSystem?.Play();
    }
}
}