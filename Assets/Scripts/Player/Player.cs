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
    public override void Initialize()
    {
        base.Initialize();
    }
    protected override void OnHealthZero()
    {
        Debug.Log("Game over");
        UnityEditor.EditorApplication.isPaused = true;
    }
    protected override void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal, vertical, 0f);
        transform.position += movement * Time.deltaTime * entityData.moveSpeed;
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
