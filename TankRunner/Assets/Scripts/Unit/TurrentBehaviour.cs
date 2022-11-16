using UnityEngine;
using System.Collections;
using DG.Tweening;

public class TurrentBehaviour : MonoBehaviour
{

    public Place myWay;
    public Transform attackLeftTrans;
    public Transform attackRightTrans;
    public Transform attackMidTrans;

    public GameObject bulletPrefab;
    public Transform attackStartPos;
    public ParticleSystem psFire;

    public GameObject attackAimLeft;
    public GameObject attackAimMid;
    public GameObject attackAimRight;
    public GameObject psBulletDie;
    public GameObject cannon;

    public string hitSound;
    public string fireSound;
    //public float attackDuration = 0.5f;
    public float hitRange = 1.5f;

    public void Setup(Place way)
    {
        switch (way)
        {
            case Place.Road_Center:
            case Place.Road_RightCenter:
            case Place.Road_LeftCenter:
                myWay = Place.Road_Center;
                break;

            case Place.Road_Right:
            case Place.Side_Right:
                myWay = Place.Road_Right;
                break;

            case Place.Road_Left:
            case Place.Side_Left:
                myWay = Place.Road_Left;
                break;
        }

        switch (myWay)
        {
            case Place.Road_Left:
                cannon.transform.rotation = attackLeftTrans.rotation;
                cannon.transform.position = attackLeftTrans.position;
                attackAimLeft.SetActive(true);
                attackAimMid.SetActive(false);
                attackAimRight.SetActive(false);
                break;

            case Place.Road_Center:
                cannon.transform.rotation = attackMidTrans.rotation;
                cannon.transform.position = attackMidTrans.position;
                attackAimLeft.SetActive(false);
                attackAimMid.SetActive(true);
                attackAimRight.SetActive(false);
                break;

            case Place.Road_Right:
                cannon.transform.rotation = attackRightTrans.rotation;
                cannon.transform.position = attackRightTrans.position;
                attackAimLeft.SetActive(false);
                attackAimMid.SetActive(false);
                attackAimRight.SetActive(true);
                break;
        }
    }

    public void Fire()
    {
        //bulletPrefab
        com.SoundSystem.instance.Play(fireSound);
        psFire.Play();
        var bullet = Instantiate(bulletPrefab, attackStartPos.position, attackStartPos.rotation, null);
        var endPos = attackAimMid.transform.position;
        var attackDuration = 0.55f;
        switch (myWay)
        {
            case Place.Road_Left:
                endPos = attackAimLeft.transform.position;
                attackDuration = 0.3f;
                break;

            case Place.Road_Center:
                endPos = attackAimMid.transform.position;
                attackDuration = 0.425f;
                break;

            case Place.Road_Right:
                endPos = attackAimRight.transform.position;
                attackDuration = 0.55f;
                break;
        }
        bullet.SetActive(true);
        bullet.transform.DOMoveX(endPos.x, attackDuration);
        bullet.transform.DOMoveY(endPos.y, attackDuration).SetEase(Ease.InQuad).OnComplete(
            () =>
            {
                var vfx = Instantiate(psBulletDie, bullet.transform.position, Quaternion.identity, null);
                Destroy(vfx, 2.0f);
                Destroy(bullet, 0.1f);
                com.SoundSystem.instance.Play(hitSound);
                var dist = bullet.transform.position - GameSystem.instance.tankMovement.transform.position;
                dist.y = 0;
                dist.z = 0;
                if (dist.magnitude < hitRange)
                {
                    GameSystem.instance.DamagePlayer(1);
                }
            });
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Fire();
        }
    }
}
