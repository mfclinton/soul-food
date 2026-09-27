using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    [Header("Combo SFX")]
    [SerializeField] private AudioClip[] comboSFX;
    [SerializeField] private AudioSourcePool comboSFXPool;
    
    [Header("Cast SFX")]
    [SerializeField] private AudioClip[] castSFX;
    [SerializeField] private AudioSourcePool castSFXPool;
    
    [Header("Attack SFX")]
    [SerializeField] private AudioClip[] attackSFX;
    [SerializeField] private AudioSourcePool attackSFXPool;

    private void Awake()
    {
        PlayerController playerController = FindObjectOfType<PlayerController>();
        InitializePlayerControllerSFX(playerController);
    }
    
    private GameObject CreateSFXPoolGO(string poolName)
    {
        GameObject poolGO = new GameObject(poolName);
        poolGO.transform.SetParent(transform);
        return poolGO;
    }

    #region Player Controller SFX

    void InitializePlayerControllerSFX(PlayerController playerController)
    {
        GameObject comboSFXPoolGO = CreateSFXPoolGO("ComboSFXPool");
        comboSFXPool.Initialize(comboSFXPoolGO);

        GameObject castSFXPoolGO = CreateSFXPoolGO("CookSFXPool");
        castSFXPool.Initialize(castSFXPoolGO);
        
        GameObject attackSFXPoolGO = CreateSFXPoolGO("AttackSFXPool");
        attackSFXPool.Initialize(attackSFXPoolGO);
        
        playerController.OnComboAction += PlayComboSFX;
        playerController.OnCastAction += PlayCastSFX;
        playerController.OnAttackAction += PlayAttackSFX;
    }
    
    void PlayComboSFX(ComboAction action)
    {
        int index = Random.Range(0, comboSFX.Length);
        AudioClip clip = comboSFX[index];
        comboSFXPool.PlaySound(clip);
    }
    
    void PlayCastSFX()
    {
        int index = Random.Range(0, castSFX.Length);
        AudioClip clip = castSFX[index];
        castSFXPool.PlaySound(clip);
    }
    
    void PlayAttackSFX()
    {
        int index = Random.Range(0, attackSFX.Length);
        AudioClip clip = attackSFX[index];
        attackSFXPool.PlaySound(clip);
    }

    #endregion
}
