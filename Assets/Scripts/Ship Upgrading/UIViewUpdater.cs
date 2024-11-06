using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIViewUpdater : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI itemNameField,
        itemFireRateField,
        damagePerShotField,
        costField,
        purchaseEquipButtonText,
        playerMoneyField;
    
    [SerializeField] private ScrollRect UpgradesScrollRect;

    [FormerlySerializedAs("playerWeaponsState")] [SerializeField] private PlayerUpgradesState playerUpgradesState;
    public PlayerUpgradesState PlayerUpgradesState => this.playerUpgradesState;

    [SerializeField] private FloatReference playerMoney;

    private static UIViewUpdater instance = null;

    public static UIViewUpdater GetInstance()
    {
        if (instance == null)
        {
            Debug.LogError("Ui view Updater does not exist please add it in the editor");
            throw new NullReferenceException();
        }

        return instance;
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            return;
        }
        
        Destroy(this.gameObject);
    }

    private void Start()
    {
        this.playerMoneyField.text = ((float)playerMoney.GetValue()).ToString();
    }

    public void ScrollRectTopAfterSeconds(float waitTime)
    {
        StartCoroutine(nameof(this.SetScrollVerticalAfterWait), waitTime);
    }
    private IEnumerator SetScrollVerticalAfterWait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        this.UpgradesScrollRect.verticalNormalizedPosition = 1;
    }

    public void SetItemStatsUi(Gun gun)
    {
        itemNameField.text = gun.name;

        this.itemFireRateField.text = gun.TimeBetweenBullets.ToString();

        this.damagePerShotField.text = gun.BulletDamage.ToString();
    }

    public void SetItemStatsUi(Armour armour)
    {
        itemNameField.text = armour.name;
        Debug.Log("Display armour stats");
    }

    public void SetItemStatsUi(Engine engine)
    {
        itemNameField.text = engine.name;
        Debug.Log("Display engine stats");
    }

    public void SetItemStatsUi(EnergySystem energySystem)
    {
        itemNameField.text = energySystem.name;
        Debug.Log("Display energy system stats");
    }

    public void CellSelected(UpgradeCell selectedCell)
    {
        this.costField.text = selectedCell.Upgrade.Cost.ToString();
        if (selectedCell.Upgrade.IsPurchaseable)
        {
            bool isEquipped = false;
            switch (selectedCell.ShipSection)
            {
                case ShipSections.lightWeapons: 
                    isEquipped = playerUpgradesState.LightGuns[selectedCell.WeaponIndex] == selectedCell.Upgrade;
                    break;
                case ShipSections.heavyWeapons:
                    isEquipped = playerUpgradesState.HeavyGuns[selectedCell.WeaponIndex] == selectedCell.Upgrade;
                    break;
                case ShipSections.armour:
                    isEquipped = playerUpgradesState.Armour == selectedCell.Upgrade;
                    break;
                case ShipSections.energy:
                    isEquipped = playerUpgradesState.EnergySystem == selectedCell.Upgrade;
                    break;
                case ShipSections.engine:
                    isEquipped = playerUpgradesState.Engine == selectedCell.Upgrade;
                    break;
                default:
                    break;
            }
            bool isOwned = OwnedUpgradesCounter.Instance.GetUpgradeCount(selectedCell.Upgrade) > 0 || selectedCell.Upgrade.OwnedByDefault || isEquipped;
            if (isOwned)
            {
                if (isEquipped)
                    this.purchaseEquipButtonText.text = "Equipped";
                else
                    this.purchaseEquipButtonText.text = "Equip";
            }
            else
                this.purchaseEquipButtonText.text = "Purchase";
        }
    }

    public void CellPurchased(SelectedCellHighlight highlight)
    {
        this.purchaseEquipButtonText.text = "Equipped";
        this.playerMoneyField.text = ((float)this.playerMoney.GetValue()).ToString();


        this.UpdateUiBasedOnItem(highlight.SelectedCell);

    }


    public void CellEquipped(SelectedCellHighlight highlight)
    {
        this.UpdateUiBasedOnItem(highlight.SelectedCell);
    }
    private void UpdateUiBasedOnItem(UpgradeCell cell)
    {
        ShipItem item = (ShipItem)cell.Upgrade.GetUpgrade();

        switch (item)
        {
            case LightGun lightGun:
                this.UpdateUiLightGun(lightGun);
                break;
            case HeavyGun heavyGun:
                this.UpdateUiHeavyGun(heavyGun);
                break;
            case Engine engine:
                this.UpdateUiEngine(engine);
                break;
            case EnergySystem energySystem:
                this.UpdateUiEnergySystem(energySystem);
                break;
            case Armour armour:
                this.UpdateUiArmour(armour);
                break;
            default:
                break;
        }


    }

    private void UpdateUiLightGun(LightGun gun)
    {
        itemNameField.text = gun.name;
        
        this.itemFireRateField.text = gun.TimeBetweenBullets.ToString();
        
        
        this.damagePerShotField.text = gun.BulletDamage.ToString();
    }

    private void UpdateUiHeavyGun(HeavyGun gun)
    {
        itemNameField.text = gun.name;
        
        this.itemFireRateField.text = gun.TimeBetweenBullets.ToString();
        
        this.damagePerShotField.text = gun.BulletDamage.ToString();
    }

    private void UpdateUiArmour(Armour armour)
    {
        itemNameField.text = armour.name;
    }

    private void UpdateUiEngine(Engine engine)
    {
        itemNameField.text = engine.name;
    }

    private void UpdateUiEnergySystem(EnergySystem energySystem)
    {
        itemNameField.text = energySystem.name;
    }
}
