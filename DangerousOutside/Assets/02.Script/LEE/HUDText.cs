using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDText : MonoBehaviour
{
    public float moveSpeed;
    public float alphaSpeed;
    public TMP_Text damageText;
    Color alpha;

    private void Start()
    {
        damageText.text = GameManager.instance.weapon.damage.ToString();
        alpha = damageText.color;
    }

    private void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime));
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed);
        damageText.color = alpha;
    }

}
