﻿using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
    public CharacterMovementModel Movement;
    public CharacterInteractionModel Interaction;
    public CharacterMovementView MovementView;
    public CharacterInventoryModel Inventory;
    public CharacterHealthModel Health;
    public CharacterActionModel Action;
    public bool isDead;

    public AudioSource deathSound;
}
