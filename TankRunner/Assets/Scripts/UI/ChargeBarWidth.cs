using UnityEngine;
using System.Collections;

public class ChargeBarWidth : MonoBehaviour
{
    public Transform child;
    public float ratio = 0.3f;
    public RectTransform barChild;

    void Start()
    {
        StartCoroutine(Align());
    }

    IEnumerator Align()
    {
        yield return new WaitForSeconds(0.25f);
        var rect = GetComponent<RectTransform>();
        var size = rect.sizeDelta;
        //Debug.Log(size);
        size.x = rect.rect.height * ratio;
        rect.sizeDelta = size;
        yield return new WaitForSeconds(0.25f);
        child.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        //Debug.Log(child.GetComponent<RectTransform>().rect.height);
        var barChildSize = barChild.sizeDelta;
        barChildSize.y = child.GetComponent<RectTransform>().rect.height;
        barChild.sizeDelta = barChildSize;
        ChargeSystem.instance.Init(barChildSize.y - 3);
    }
}
