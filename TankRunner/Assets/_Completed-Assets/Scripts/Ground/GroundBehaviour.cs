using UnityEngine;

public class GroundBehaviour : MonoBehaviour
{
    public int x;
    public int z;
    public int gen;

    public void SyncPos()
    {
        transform.position = new Vector3(x, -0.5f, z);
    }

    public void SetPos(int pX, int pZ)
    {
        x = pX;
        z = pZ;
    }
}
