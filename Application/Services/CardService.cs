using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Infrastructure.Repositories;


namespace Application.Services
{
    public class CardService
    {
        private readonly ICardRepository _cardRepository;
        private readonly AllowedActionsRepository _allowedActionsRepository;

        public CardService(ICardRepository cardRepository, AllowedActionsRepository allowedActionsRepository)
        {
            _cardRepository = cardRepository;
            _allowedActionsRepository = allowedActionsRepository;
        }

        public async Task<CardDetails?> GetCardDetails(string userId, string cardNumber)
        {
            return await _cardRepository.GetCardDetails(userId, cardNumber);
        }

        public AllowedActionsResponse GetAllowedActions(CardDetails cardDetails)
        {
            var actions = _allowedActionsRepository.GetAllowedActions(cardDetails.CardType, cardDetails.CardStatus);

            if ((cardDetails.CardStatus == CardStatus.Ordered || cardDetails.CardStatus == CardStatus.Inactive || cardDetails.CardStatus == CardStatus.Active) && !cardDetails.IsPinSet)
            {
                actions.Remove("ACTION6");               
            }

            if ((cardDetails.CardStatus == CardStatus.Ordered || cardDetails.CardStatus == CardStatus.Inactive || cardDetails.CardStatus == CardStatus.Active) && cardDetails.IsPinSet)
            {                
                actions.Remove("ACTION7");
            }

            if (cardDetails.CardStatus == CardStatus.Blocked  && !cardDetails.IsPinSet)
            {
                actions.Remove("ACTION6");
                actions.Remove("ACTION7");
            }

            return new AllowedActionsResponse(actions);
        }
    }
}
