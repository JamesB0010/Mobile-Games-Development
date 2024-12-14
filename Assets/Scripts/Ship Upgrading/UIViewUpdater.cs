using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIViewUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI costField, purchaseEquipButtonText;
    
    [SerializeField] private ScrollRect UpgradesScrollRect;

    public PlayerUpgradesState PlayerUpgradesState => this.playerUpgradesState;

    [SerializeField] private FloatReference playerMoney;
    
    [FormerlySerializedAs("playerWeaponsState")] [SerializeField] private PlayerUpgradesState playerUpgradesState;

    [Header("Current equipped stats setters")] [SerializeField]
    private Image EquippedSectionBackground;

    [SerializeField] private ArmourToPurchaseStatSection armourEquippedStatSection;

    [SerializeField] private EnergySystemToPurchaseStats energySystemEquippedStats;

    [SerializeField] private EngineToPurchaseStats engineEquippedStats;

    [SerializeField] private HeavyGunToPurchaseStats heavyGunEquippedStats;

    [SerializeField] private LightGunToPurchaseStats lightGunEquippedStats;
    

    [Header("Item To Purchase Stats Setters")] 
    [SerializeField] private Image toPurchaseSectionBackground;
    
    [SerializeField] private ArmourToPurchaseStatSection armourToPurchaseStatSection;

    [SerializeField] private EnergySystemToPurchaseStats energySystemToPurchaseStats;

    [SerializeField] private EngineToPurchaseStats engineToPurchaseStats;

    [SerializeField] private HeavyGunToPurchaseStats heavyGunToPurchaseStats;

    [SerializeField] private LightGunToPurchaseStats lightGunToPurchaseStats;

    
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

    public void ScrollRectTopAfterSeconds(float waitTime)
    {
        StartCoroutine(nameof(this.SetScrollVerticalAfterWait), waitTime);
    }
    private IEnumerator SetScrollVerticalAfterWait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        this.UpgradesScrollRect.verticalNormalizedPosition = 1;
    }

    public void SetItemStatsUi(LightGun gun)
    {
        Debug.Log("Update ui for light gun");
        this.DisableAllEquippedFields();
        this.EquippedSectionBackground.enabled = true;
        this.lightGunEquippedStats.gameObject.SetActive(true);

        if (!gun.AbleToShoot)
        {
            this.lightGunEquippedStats.WeaponCantShoot();
            return;
        }
        this.lightGunEquippedStats.SetFields(gun.ItemName, gun.TimeBetweenBullets.ToString(), gun.BulletDamage.ToString(), gun.EnergyExpensePerShot.ToString(), gun.Firepower.ToString());
    }

    private void DisableAllEquippedFields()
    {
        this.EquippedSectionBackground.enabled = false;
        this.armourEquippedStatSection.gameObject.SetActive(false);
        this.energySystemEquippedStats.gameObject.SetActive(false);
        this.engineEquippedStats.gameObject.SetActive(false);
        this.heavyGunEquippedStats.gameObject.SetActive(false);
        this.lightGunEquippedStats.gameObject.SetActive(false);
    }

    public void SetItemStatsUi(HeavyGun gun)
    {
        this.DisableAllEquippedFields();
        this.EquippedSectionBackground.enabled = true;
        this.heavyGunEquippedStats.gameObject.SetActive(true);
        Debug.Log("Update ui for heavy gun");
        if (!gun.AbleToShoot)
        {
            this.heavyGunEquippedStats.WeaponCantShoot();
            return;
        }
        
        this.heavyGunEquippedStats.SetFields(gun.ItemName, gun.TimeBetweenBullets.ToString(), gun.BulletDamage.ToString(), gun.MaxAmmoCount.ToString(), gun.Firepower.ToString());
    }

    public void SetItemStatsUi(Armour armour)
    {
        Debug.Log("Update ui for armour");
        
        this.DisableAllEquippedFields();
        this.EquippedSectionBackground.enabled = true;
        this.armourEquippedStatSection.gameObject.SetActive(true);
        
        this.armourEquippedStatSection.SetFields(armour.ItemName, armour.EnergyUsedPerHit.ToString(), armour.MinimumOperationalEnergyLevel.ToString(), (( 1 - armour.DamageDampeningMultiplier) * 100).ToString());
    }

    public void SetItemStatsUi(Engine engine)
    {
        Debug.Log("Update ui for engine");
        
        this.DisableAllEquippedFields();
        this.EquippedSectionBackground.enabled = true;
        this.engineEquippedStats.gameObject.SetActive(true); 
        
        this.engineEquippedStats.SetFields(engine.ItemName, engine.AccelerationSpeed.ToString(), engine.MaxVelocity.ToString(), engine.EnergyDrainRate.ToString());
    }

    public void SetItemStatsUi(EnergySystem energySystem)
    {
        Debug.Log("Update ui for energy system");
        
        this.DisableAllEquippedFields();
        this.EquippedSectionBackground.enabled = true;
        this.energySystemEquippedStats.gameObject.SetActive(true); 
        this.energySystemEquippedStats.SetFields(energySystem.ItemName, energySystem.MaxEnergy.ToString(), energySystem.RechargeRate.ToString());
    }

    public void CellSelected(UpgradeCell selectedCell)
    {
        this.costField.text = selectedCell.Upgrade.Cost.ToString();
        if (selectedCell.Upgrade.IsPurchaseable)
        {
            bool isEquipped = false;
            isEquipped = selectedCell.Upgrade.GenerateUpgradesStateInteractor(this.playerUpgradesState).IsEqualTo(selectedCell.Upgrade, selectedCell.WeaponIndex);

            bool ownedByDefault = selectedCell.Upgrade.quantityOwnedByDefault > OwnedUpgradesCounter.Instance.GetItemInCirculationCount(this.playerUpgradesState.DefaultLightGun);
            bool enoughOwned = OwnedUpgradesCounter.Instance.GetUpgradeCount(selectedCell.Upgrade) > 0;
            bool isOwned = ownedByDefault || enoughOwned|| isEquipped;
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
        
        
        selectedCell.Upgrade.GenerateUiUpdatorInteractor(this).UpdateItemToPurchaseStats((ShipItem)selectedCell.Upgrade.GetUpgrade());
    }

    public void CellPurchased(SelectedCellHighlight highlight)
    {
        this.purchaseEquipButtonText.text = "Equipped";
        this.UpdateUiBasedOnItem(highlight.SelectedCell);
    }


    public void CellEquipped(SelectedCellHighlight highlight)
    {
        this.UpdateUiBasedOnItem(highlight.SelectedCell);
    }
    private void UpdateUiBasedOnItem(UpgradeCell cell)
    {
        ShipItem item = (ShipItem)cell.Upgrade.GetUpgrade();

        cell.Upgrade.GenerateUiUpdatorInteractor(this).UpdateUi(item);;

    }

    public void UpdateUiArmourToPurchase(string armourName, string energyUsedPerHit, string minEnergyReq, string shieldsStrength)
    {
        this.DisableAllToPurchaseFields();
        this.toPurchaseSectionBackground.enabled = true;
        this.armourToPurchaseStatSection.gameObject.SetActive(true);
        this.armourToPurchaseStatSection.SetFields(armourName, energyUsedPerHit, minEnergyReq, shieldsStrength);
    }

    public void UpdateUiEnergySystemToPurchase(string energySysName, string maxEnergy, string rechargeRate)
    {
        this.DisableAllToPurchaseFields();
        this.toPurchaseSectionBackground.enabled = true;
        this.energySystemToPurchaseStats.gameObject.SetActive(true);
        this.energySystemToPurchaseStats.SetFields(energySysName, maxEnergy, rechargeRate);
    }

    public void UpdateUiEngineToPurchase(string engineName, string accelerationSpeed, string topSpeed, string energyDrainRate, string boostEnergyDrainRate)
    {
        this.DisableAllToPurchaseFields();
        this.toPurchaseSectionBackground.enabled = true;
        this.engineToPurchaseStats.gameObject.SetActive(true);
        this.engineToPurchaseStats.SetFields(engineName, accelerationSpeed, topSpeed, energyDrainRate, boostEnergyDrainRate);
    }
    
    public void UpdateUiEngineToPurchase(string engineName, string accelerationSpeed, string topSpeed, string energyDrainRate)
    {
        this.DisableAllToPurchaseFields();
        this.toPurchaseSectionBackground.enabled = true;
        this.engineToPurchaseStats.gameObject.SetActive(true);
        this.engineToPurchaseStats.SetFields(engineName, accelerationSpeed, topSpeed, energyDrainRate);
    }

    public void HeavyWeaponToPurchaseIsNoGun()
    {
        this.DisableAllToPurchaseFields();
        this.toPurchaseSectionBackground.enabled = true;
        this.heavyGunToPurchaseStats.gameObject.SetActive(true);
        this.heavyGunToPurchaseStats.WeaponCantShoot();
    }

    public void LightWeaponToPurchaseIsNoGun()
    {
        this.DisableAllToPurchaseFields();
        this.toPurchaseSectionBackground.enabled = true;
        this.lightGunToPurchaseStats.gameObject.SetActive(true);
        this.lightGunToPurchaseStats.WeaponCantShoot();
    }

    public void UpdateHeavyWeaponToPurchase(string weaponName, string fireRate, string bulletDamage, string ammoCount, string firepower)
    {
        this.DisableAllToPurchaseFields();
        this.toPurchaseSectionBackground.enabled = true;
        this.heavyGunToPurchaseStats.gameObject.SetActive(true);
        this.heavyGunToPurchaseStats.SetFields(weaponName, fireRate, bulletDamage, ammoCount, firepower);
    }

    public void UpdateLightGunWeaponToPurchase(string weaponName, string fireRate, string bulletDamage, string energyExpense, string firepower)
    {
        this.DisableAllToPurchaseFields();
        this.toPurchaseSectionBackground.enabled = true;
        this.lightGunToPurchaseStats.gameObject.SetActive(true);
        this.lightGunToPurchaseStats.SetFields(weaponName, fireRate, bulletDamage, energyExpense, firepower);
    }

    private void DisableAllToPurchaseFields()
    {
        this.toPurchaseSectionBackground.enabled = false;
        this.armourToPurchaseStatSection.gameObject.SetActive(false);
        this.energySystemToPurchaseStats.gameObject.SetActive(false);
        this.engineToPurchaseStats.gameObject.SetActive(false);
        this.heavyGunToPurchaseStats.gameObject.SetActive(false);
        this.lightGunToPurchaseStats.gameObject.SetActive(false);
    }
    
    public IEnumerator DisableAllToPurchaseFieldsAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        this.DisableAllToPurchaseFields();
    }

    public void StartDisableAllToPurchaseFieldsCoroutine(float timeToWait)
    {
        StartCoroutine(nameof(this.DisableAllToPurchaseFieldsAfter), timeToWait);
    }
    public void CancelDisableAllToPurchaseFieldsCoroutine()
    {
        StopCoroutine(nameof(this.DisableAllToPurchaseFieldsAfter));
    }
}
