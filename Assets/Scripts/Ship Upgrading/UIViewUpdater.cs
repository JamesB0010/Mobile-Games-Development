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

    public PlayerUpgradesState PlayerUpgradesState => this.playerUpgradesState;

    [SerializeField] private FloatReference playerMoney;
    
    [FormerlySerializedAs("playerWeaponsState")] [SerializeField] private PlayerUpgradesState playerUpgradesState;

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
            isEquipped = selectedCell.Upgrade.GenerateUpgradesStateInteractor(this.playerUpgradesState).IsEqualTo(selectedCell.Upgrade, selectedCell.WeaponIndex);
            
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
        
        
        selectedCell.Upgrade.GenerateUiUpdatorInteractor(this).UpdateItemToPurchaseStats((ShipItem)selectedCell.Upgrade.GetUpgrade());
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

        cell.Upgrade.GenerateUiUpdatorInteractor(this).UpdateUi(item);;

    }

    public void UpdateUiLightGun(LightGun gun)
    {
        itemNameField.text = gun.name;
        
        this.itemFireRateField.text = gun.TimeBetweenBullets.ToString();
        
        
        this.damagePerShotField.text = gun.BulletDamage.ToString();
    }

    public void UpdateUiHeavyGun(HeavyGun gun)
    {
        itemNameField.text = gun.name;
        
        this.itemFireRateField.text = gun.TimeBetweenBullets.ToString();
        
        this.damagePerShotField.text = gun.BulletDamage.ToString();
    }

    public void UpdateUiArmour(Armour armour)
    {
        itemNameField.text = armour.name;
    }

    public void UpdateUiEngine(Engine engine)
    {
        itemNameField.text = engine.name;
    }

    public void UpdateUiEnergySystem(EnergySystem energySystem)
    {
        itemNameField.text = energySystem.name;
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

    public void UpdateHeavyWeaponToPurchase(string weaponName, string fireRate, string bulletDamage, string ammoCount)
    {
        this.DisableAllToPurchaseFields();
        this.toPurchaseSectionBackground.enabled = true;
        this.heavyGunToPurchaseStats.gameObject.SetActive(true);
        this.heavyGunToPurchaseStats.SetFields(weaponName, fireRate, bulletDamage, ammoCount);
    }

    public void UpdateLightGunWeaponToPurchase(string weaponName, string fireRate, string bulletDamage, string energyExpense)
    {
        this.DisableAllToPurchaseFields();
        this.toPurchaseSectionBackground.enabled = true;
        this.lightGunToPurchaseStats.gameObject.SetActive(true);
        this.lightGunToPurchaseStats.SetFields(weaponName, fireRate, bulletDamage, energyExpense);
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
