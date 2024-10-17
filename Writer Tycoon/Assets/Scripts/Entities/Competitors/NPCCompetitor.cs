using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WriterTycoon.Entities.Competitors
{
    public class NPCCompetitor : MonoBehaviour, ICompetitor
    {
        private Bank bank;
        [SerializeField] private bool learned;

        /// <summary>
        /// Initialize the NPC Competitor
        /// </summary>
        public void Initialize(bool learned)
        {
            // Get Components
            bank = GetComponent<Bank>();

            // Link the bank
            bank.LinkCompetitor(this);

            // Set variables
            this.learned = learned;
        }

        /// <summary>
        /// Calculate sales for the NPC Competitor
        /// </summary>
        public void CalculateSales(float amount) => bank.AddAmount(amount);
    }
}