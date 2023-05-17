using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.Factory
{
public class FollowerFactory : Factory
{
    //used to create a Prefab
    [SerializeField] private Follower productPrefab;

    public override IProduct GetProduct(Vector3 position)
    {
        //create a Prefab instance and get the product component
        GameObject instance = Instantiate(productPrefab.gameObject, position, Quaternion.identity);
        Follower newProduct = instance.GetComponent<Follower>();

        //each product contains its own logic
        newProduct.Initialize();

        return newProduct;
    }
}
}