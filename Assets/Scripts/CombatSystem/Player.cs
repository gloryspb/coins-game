using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float damage;
    public float healthPoints; // меняем инт на флоат
    public float maxHealthPoints = 10f;
    private Animator _animator;
    private Rigidbody2D _rb;
    private SpriteRenderer spriteRenderer;
    public static bool isDead;
    private Collider2D _collider;
    private bool _isCombatMode = false;
    private float _timer = 0;
    private List<GameObject> enemies; // = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
    }

    private void Start()
    {
        healthPoints = maxHealthPoints;
        isDead = false;
    }

    private void Update()
    {
        if (!_isCombatMode)
        {
            RegenHealthPoints();
        }

        UpdateCombatMode();
    }

    private void RegenHealthPoints()
    {
        if (_isCombatMode) _timer = 0f;
        _timer += Time.deltaTime;
        if (_timer >= 1f)
        {
            if (healthPoints < maxHealthPoints)
            {
                healthPoints += 1f;
            }
            _timer = 0f;
        }
    }

    private void UpdateCombatMode()
    {
        GameObject closestEnemy = FindClosestEnemy(11f);
        Debug.Log(_isCombatMode);
        if (closestEnemy != null)
        {
            if (closestEnemy.GetComponent<EnemyController>().isCombatMode == true)
            {
                _isCombatMode = true;
            }
            else
            {
                Invoke("ExitCombatMode", 5f);
            }
        }
        else
        {
            Invoke("ExitCombatMode", 5f);
        }

        // Debug.Log(closestEnemy.GetComponent<EnemyController>().isCombatMode);
    }
    public void TakeDamage(float damageTaken)
    {
        if (PlayerController.isAttack)
        {
            return;
        }
        healthPoints -= damageTaken;
        if (healthPoints <= 0f)
        {
            Death();
        }

        spriteRenderer.color = Color.red;
        Invoke("SetColorWhite", 0.25f);
        
        // _isCombatMode = true;
        // Invoke("ExitCombatMode", 5f);
    }

    public void Attack()
    {
        // _isCombatMode = true;
        // Invoke("ExitCombatMode", 5f);
    }

    private void ExitCombatMode()
    {
        _isCombatMode = false;
    }
    
    private void SetColorWhite()
    {
        spriteRenderer.color = Color.white;
    }
    
    public void Death()
    {
        _animator.SetTrigger("DeathTrigger");
        isDead = true;
        _rb.isKinematic = false;
        _collider.enabled = false;
        StartCoroutine(InvokeMethodWithDelay());
    }

    IEnumerator InvokeMethodWithDelay()
    {
        yield return new WaitForSeconds(1f);
        DeathHandler();
    }

    private void DeathHandler()
    {
        UIEventHandler.Instance.DeathScreen();
        Time.timeScale = 0f;
    }
    
    private GameObject FindClosestEnemy(float maxDistance)
    {
        GameObject closest = null;
        List<GameObject> enemyes = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        
        float distance = Mathf.Infinity;
        float curDistance;
        if (enemyes.Count > 0)
        {
            foreach (GameObject go in enemyes)
            {
                Vector3 diff = go.transform.position - transform.position;
                curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = go;
                    distance = curDistance;
                }
            }
            if (distance <= maxDistance) return closest;
            else return null;
        }
        else return null;
    }
}
