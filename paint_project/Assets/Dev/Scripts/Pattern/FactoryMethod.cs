using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.Factory
{
    // base class for factories
    public abstract class Factory : MonoBehaviour
    {
        public abstract IProduct GetProduct(Vector2 position);
        [SerializeField] public bool canSpawn = true;
        
        // shared method with all factories
        public string GetLog(IProduct product)
        {
            string logMessage = "Factory: created product " + product.ProductName;
            return logMessage;
        }
    }
}