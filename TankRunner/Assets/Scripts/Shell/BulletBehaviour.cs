using UnityEngine;
using com;

public class BulletBehaviour : MonoBehaviour
{
    public float speed;

    private void Start()
    {
        Destroy(gameObject, 5);
    }

    void Update()
    {
        transform.position += speed * transform.forward * GameTime.deltaTime;
    }
}
