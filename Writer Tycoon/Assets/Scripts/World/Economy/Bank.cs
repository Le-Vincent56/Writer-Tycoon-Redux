using UnityEngine;
using WriterTycoon.Entities;
using WriterTycoon.Entities.Tracker;
using WriterTycoon.Patterns.EventBus;
using WriterTycoon.Patterns.ServiceLocator;

public class Bank : MonoBehaviour, ICompetitor
{
    private CompetitorRecord competitorRecord;

    [SerializeField] private bool isPlayer;
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

    private void Start()
    {
        // Get the competitor record to use as a service
        competitorRecord = ServiceLocator.ForSceneOf(this).Get<CompetitorRecord>();
        competitorRecord.RecordCompetitor(this, isPlayer);
    }

    /// <summary>
    /// Increase the amount of money within the bank
    /// </summary>
    public void IncreaseMoney(float amount)
    {
        // Increase the player money
        bank += amount;

        // Update the player income
        EventBus<DisplayBank>.Raise(new DisplayBank()
        {
            Competitor = this,
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
            Competitor = this,
            BankAmount = bank,
            Revenue = rent,
        });
    }
}
