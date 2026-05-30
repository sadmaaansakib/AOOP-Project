using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Include the UnityEngine.UI namespace

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Healt playerHealth; // Corrected from 'Healt' to 'Health'
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;

    private void Start()
    {
        // Set the total health bar's fill amount based on the player's current health
        totalHealthBar.fillAmount = playerHealth.currentHealth / 10f;
    }

    private void Update()
    {
        // Update the current health bar's fill amount as the player's health changes
        currentHealthBar.fillAmount = playerHealth.currentHealth / 10f;
    }
}
