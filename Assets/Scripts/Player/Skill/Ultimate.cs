using System.Collections;
using UnityEngine;

public enum UltimateSkill
{
    BlackHole,
    Kamui,
    GravityHole
}

public class Ultimate : MonoBehaviour
{
    [SerializeField] UltimateSkill _ultimateSkill;

    [SerializeField] GameObject _blackHolePrefab;
    [SerializeField] GameObject _gravityHolePrefab;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            Transform blackHoleTransform = Instantiate(_blackHolePrefab, transform.position, Quaternion.identity).transform;
            blackHoleTransform.right = (Vector3)Managers.Input.MouseWorldPos - transform.position;
            Destroy(blackHoleTransform.gameObject, 1f);
        }

        if(Input.GetKeyDown(KeyCode.Y))
        {
            StartCoroutine(KamuiCoroutine());
        }

        if(Input.GetKeyDown(KeyCode.U))
        {
            Vector3 pos = (Vector3)Managers.Input.MouseWorldPos;
            Transform gravityHoleTransform = Instantiate(_gravityHolePrefab, pos, Quaternion.identity).transform;
            Destroy(gravityHoleTransform.gameObject, 1f);
        }
    }

    IEnumerator KamuiCoroutine()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Transform visual = player.transform.Find("Visual").Find("HideVisualSprite");
        SpriteRenderer spriteRenderer = visual.GetComponent<SpriteRenderer>();
        Collider2D collider = player.GetComponentInParent<Collider2D>();
        collider.enabled = false;
        spriteRenderer.color = new Color(1, 1, 1, 0.5f);
        yield return new WaitForSeconds(3f);
        collider.enabled = true;
        spriteRenderer.color = new Color(1, 1, 1, 1f);
    }
}