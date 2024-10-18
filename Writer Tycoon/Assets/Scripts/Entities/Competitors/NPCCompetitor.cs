using UnityEngine;

namespace WriterTycoon.Entities.Competitors
{
    public class NPCCompetitor : MonoBehaviour, ICompetitor
    {
        private Bank bank;

        [SerializeField] private string competitorName;
        [SerializeField] private bool learned;
        [SerializeField] private float learningQ;

        /// <summary>
        /// Initialize the NPC Competitor
        /// </summary>
        public void Initialize(string competitorName, float startingMoney, bool learned, float learningQ)
        {
            // Get Components
            bank = GetComponent<Bank>();

            // Link the bank and set the starting sum
            bank.LinkCompetitor(this);
            bank.SetBankSum(startingMoney);

            // Set variables
            this.competitorName = competitorName;
            this.learned = learned;
            this.learningQ = learningQ;
        }

        /// <summary>
        /// Calculate sales for the NPC Competitor
        /// </summary>
        public void CalculateSales(float amount) => bank.AddAmount(amount);
    }
}