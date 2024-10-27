using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerWallet : MonoBehaviour
    {
        [SerializeField]
        private FloatReference money;

        [SerializeField] private float moneyPerEnnemy;

        [SerializeField] private UnityEvent<string> OnMoneyChanged = new UnityEvent<string>();

        public void Start()
        {
            this.OnMoneyChanged?.Invoke("Credits: " + (float)this.money.GetValue());
            SceneManager.activeSceneChanged += this.OnSceneChange;
        }

        private void OnSceneChange(Scene scene, Scene sceneTo)
        {
            PlayerPrefs.SetFloat(PlayerPrefsKeys.PlayerMoneyKey, (float)this.money.GetValue());
        }

        private void OnDestroy()
        {
            SceneManager.activeSceneChanged -= this.OnSceneChange;
        }

        public void OnEnemyKilled()
        {
            this.money += this.moneyPerEnnemy;
            this.OnMoneyChanged?.Invoke("Credits: " + (float)this.money.GetValue());
        }

        public void OnApplicationQuit()
        {
            PlayerPrefs.SetFloat(PlayerPrefsKeys.PlayerMoneyKey, (float)this.money.GetValue());
        }

        public void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus)
            {
                PlayerPrefs.SetFloat(PlayerPrefsKeys.PlayerMoneyKey, (float)this.money.GetValue());
            }
        }
    }
}
