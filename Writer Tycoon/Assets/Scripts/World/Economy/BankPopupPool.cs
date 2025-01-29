using UnityEngine;
using UnityEngine.Pool;

namespace GhostWriter.World.Economy
{
    public class BankPopupPool : MonoBehaviour
    {
        [SerializeField] private BankPopup bankPopupPrefab;
        private ObjectPool<BankPopup> pool;

        public ObjectPool<BankPopup> Pool => pool;

        private void Awake()
        {
            pool = new ObjectPool<BankPopup>(
                CreateBankPopup, 
                OnTakePopupFromPool, 
                OnReturnPopupToPool,
                OnDestroyPopup,
                true,
                10,
                100
            );
        }

        /// <summary>
        /// Create a Bank Popup within the Pool
        /// </summary>
        private BankPopup CreateBankPopup()
        {
            // Create a Bank Popup and set this as the parent
            BankPopup popup = Instantiate(bankPopupPrefab, transform.position, Quaternion.identity);
            popup.transform.SetParent(transform);
            popup.transform.localScale = Vector3.one;

            // Initialize the Bank Popup
            popup.Initialize();

            return popup;
        }

        /// <summary>
        /// Take a Bank Popup from the Pool
        /// </summary>
        private void OnTakePopupFromPool(BankPopup popup)
        {
            // Reset the Bank Popup
            popup.ResetPopup();

            // Set the GameObject as active
            popup.gameObject.SetActive(true);
        }

        /// <summary>
        /// Return a Bank Popup to the Pool
        /// </summary>
        private void OnReturnPopupToPool(BankPopup popup)
        {
            // Deactivate the Bank Popup
            popup.gameObject.SetActive(false);
        }

        /// <summary>
        /// Destroy a Bank Popup within the Pool
        /// </summary>
        private void OnDestroyPopup(BankPopup popup)
        {
            // Destroy the Bank Popup
            Destroy(popup.gameObject);
        }


    }
}
