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

        private BuzzardGameData gameData;

        public void Start()
        {
            this.OnMoneyChanged?.Invoke("Credits: " + (float)this.money.GetValue());
            this.gameData = FindObjectOfType<BuzzardGameData>();
        }

        public void OnEnemyKilled()
        {
            this.money += this.moneyPerEnnemy;
            this.OnMoneyChanged?.Invoke("Credits: " + (float)this.money.GetValue());
        }
    }
}
