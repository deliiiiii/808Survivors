using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]

public class PlayerData:EntityData
{
    public override void InitializeData()
    {
        base.InitializeData();
    }
}
public class Player : Entity
{
    PlayerData playerData => entityData as PlayerData;
    public override void Initialize()
    {
        base.Initialize();
    }
    public override void OnHealthZero()
    {
        Debug.Log("Game over");
        UnityEditor.EditorApplication.isPaused = true;
    }
    protected override void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = new (horizontal, vertical, 0f);
        transform.position += entityData.moveSpeed * Time.deltaTime * movement;
    }
    [SerializeField]
    List<Weapon> weapons = new();

    protected override void Update()
    {
        base.Update();
        Attack();
    }
    void Attack()
    {
        foreach(Weapon weapon in weapons)
        {
            weapon.TryFire();
        }
    }
}
