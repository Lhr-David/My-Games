using UnityEngine;

public class WinBullet : MonoBehaviour
{
    float _distanceToGo;
    public float gravity = 8;
    public float speed = 9;
    public float distanceRatio = 40f;
    public float distanceOffset = 1f;
    float _distancePassed;
    float _speedY;

    public void Init(float fireValue)
    {
        _distanceToGo = fireValue * distanceRatio + distanceOffset;
        _distancePassed = 0;
        _speedY = 0;
        ChargeSystem.instance.SetCameraFollowWinBullet(this.transform);
    }

    private void Update()
    {
        Move();
    }

    void Move()
    {
        var ratio = _distancePassed / _distanceToGo;
        ChargeSystem.instance.SyncChargeBarByWinBullet(ratio);

        if (_distancePassed < _distanceToGo)
        {
            StraightMove();
            return;
        }

        GravityMove();
    }

    void StraightMove()
    {
        var deltaDist = speed * Time.deltaTime;
        transform.position += Vector3.forward * deltaDist;
        _distancePassed += deltaDist;
    }

    void GravityMove()
    {
        StraightMove();
        _speedY += gravity * Time.deltaTime;
        transform.position += Vector3.down * _speedY;
        if (_speedY > 2)
        {
            GameSystem.instance.WinTest();
            this.enabled = false;
        }
    }
}
