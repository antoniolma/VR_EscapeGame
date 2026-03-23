using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        
        GameObject go = other.gameObject;
        
        if (go.CompareTag("RevolverCylinder"))
        {
            Pistol revolver = go.GetComponentInParent<Pistol>();
            if (revolver.numBullets < 6)
            {
                Destroy(gameObject);
                revolver.reloadOneBullet = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
