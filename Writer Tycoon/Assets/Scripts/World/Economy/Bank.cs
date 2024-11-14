using UnityEngine;
using GhostWriter.Entities;
using GhostWriter.Patterns.EventBus;

public class Bank : MonoBehaviour
{
    [SerializeField] private ICompetitor competitor;
    [SerializeField] private float bank;
    [SerializeField] private float rent;

    private EventBinding<PassMonth> passMonthEvent;

    private void OnEnable()
    {
        passMonthEvent = new EventBinding<PassMonth>(PayRent);
        EventBus<PassMonth>.Register(passMonthEvent);
    }

    private void OnDisable()
    {
        EventBus<PassMonth>.Deregister(passMonthEvent);
    }

    /// <summary>
    /// Link the Bank's competitor
    /// </summary>
    public void LinkCompetitor(ICompetitor competitor) => this.competitor = competitor;

    /// <summary>
    /// Set the total bank sum
    /// </summary>
    public void SetBankSum(float bankSum) => bank = bankSum;

    /// <summary>
    /// Increase the amount of money within the bank
    /// </summary>
    public void AddAmount(float amount)
    {
        // Increase the player money
        bank += amount;

        // Update the player income
        EventBus<DisplayBank>.Raise(new DisplayBank()
        {
            Competitor = competitor,
            BankAmount = bank,
            Revenue = amount
        });
    }

    /// <summary>
    /// Pay rent by subtracting the rent from the bank
    /// </summary>
    private void PayRent()
    {
        // Subtract the rent from the bank
        bank -= rent;

        // Update the display
        EventBus<DisplayBank>.Raise(new DisplayBank()
        {
            Competitor = competitor,
            BankAmount = bank,
            Revenue = rent,
        });
    }
}
