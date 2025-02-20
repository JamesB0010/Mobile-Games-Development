using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class ShipItemUpgrade : ScriptableObject
{
    public abstract ShipItem GetUpgrade();

    public abstract EquipItemInteractorStrategy GenerateEquipItemInteractor(EquipItem itemEquipAction);

    public abstract UiUpdaterInteractorStrategy GenerateUiUpdatorInteractor(UIViewUpdater ui);

    public abstract UpgradesCounterInteractorStrategy GenerateUpgradeCounterInteractor(OwnedUpgradesCounter upgradesCounter);

    public abstract PlayerUpgradesStateInteractorStrategy GenerateUpgradesStateInteractor(
        PlayerUpgradesState playerUpgradesState);


    public abstract UpgradeAchievementInteractorStrategy generateUpgradeAcievementInteractor(
        FirstTimePurcaseAcievementIDCollection acievementIDCollection);

    [SerializeField] private float cost;
    public float Cost => this.cost;

    [SerializeField] private Sprite icon;
    public Sprite Icon => this.icon;

    [SerializeField] private bool isPurchaseable;
    public bool IsPurchaseable => this.isPurchaseable;

    public int quantityOwnedByDefault;
}
