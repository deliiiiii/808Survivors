using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
//[CreateAssetMenu(fileName = "EntityData", menuName = "ScriptableObjects/EntityData")]
[Serializable]
public class EntityData : ScriptableObject
{
    public float maxHealth;
    public float moveSpeed;
    public virtual void InitializeData()
    {
        if (maxHealth <= 0)
        {
            Debug.LogError(GetType().Name + " init HP fail");
        }
    }
}


public abstract class Entity : MonoBehaviour
{
    [SerializeReference]
    protected EntityData entityData;
    
    #region HP
    public BuffFloat maxHealthBuff;
    public BuffFloat curHealthRecoverBuff;//Ã¿Ãë»Ø¸´ÉúÃü
    public float MaxHealth => maxHealthBuff.Value;
    [SerializeField]
    float curHealth;
    public float CurHealth
    {
        get
        {
            return curHealth;
        }
        set
        {
            if (value > MaxHealth)
            {
                curHealth = MaxHealth;
            }
            else if (value <= 0)
            {
                curHealth = 0f;
                OnHealthZero();
            }
            else
                curHealth = value;
        }
    }
    public Slider healthBarPrefab;
    Slider healthBar;
    public void RecoverHP()
    {
        CurHealth += curHealthRecoverBuff.Value * Time.deltaTime;
    }
    public virtual void SetHealthBar()
    {
        healthBar.value = CurHealth / MaxHealth;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 canvasPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            healthBar.transform.parent.GetComponent<RectTransform>(),
            screenPos,
            Camera.main,
            out canvasPos);
        healthBar.transform.localPosition = canvasPos + new Vector2(0, -35);
    }
    void RefreshHealthBar(bool newV)
    {
        healthBar.gameObject.SetActive(newV);
    }
    public virtual void OnHealthZero()
    {
        GetComponent<ObjectPoolObject>().MyDestroy(gameObject);
    }
    public void TakeDamage(float damage)
    {
        CurHealth -= damage;
    }
    
    #endregion
    #region Initialize
    public virtual void Initialize()
    {
        entityData.InitializeData();
        curHealth = entityData.maxHealth;
        maxHealthBuff = new(entityData.maxHealth);
        curHealthRecoverBuff = new(0f);

        if(healthBar == null)
        {
            healthBar = Instantiate(healthBarPrefab, UIManager.Instance.transHpBar);
        }
        healthBar.gameObject.SetActive(true);
        GameSetting.Instance.OnShowHealthBarAction += RefreshHealthBar;
    }
    public virtual void OnEnable()
    {
        Initialize();
    }
    public virtual void OnDisable()
    {
        GameSetting.Instance.OnShowHealthBarAction -= RefreshHealthBar;
        healthBar.gameObject.SetActive(false);
    }
    #endregion
    #region Move
    protected virtual void Move(){}
    #endregion
    protected virtual void Update()
    {
        RecoverHP();
        SetHealthBar();
        Move();
    }
}
