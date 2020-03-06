using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterStatus : MonoBehaviour
{
    public int HP;
    public int MaxHP;
    public Image HPBar;
    public Animator animator;
    void Start()
    {
        HP = MaxHP;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void TakeDamage(int dmg)
    {
        HP -= dmg;
        if (HP < 0) HP = 0;
        float hpRatio = (float)HP / MaxHP;
        HPBar.rectTransform.localScale = new Vector3(hpRatio, 1, 1);
        if (HP == 0)
        {
            StartCoroutine(WaitForAnimation(animator));
        }
        else
        {
            animator.SetTrigger("Take Damage");
        }
    }

    IEnumerator WaitForAnimation(Animator animator)
    {
        animator.SetTrigger("Die");
        yield return new WaitForSeconds(
            animator.GetCurrentAnimatorStateInfo(0).length
        );
        Destroy(transform.gameObject);
    }
}
