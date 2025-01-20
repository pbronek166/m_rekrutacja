using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public class AllowedActionsRepository
    {
        private static readonly Dictionary<CardType, List<string>> AllowedActionsByCardType = new()
        {
            { CardType.Prepaid, new List<string> { "ACTION1", "ACTION2", "ACTION3", "ACTION4", "ACTION6", "ACTION7", "ACTION8", "ACTION9", "ACTION10", "ACTION11", "ACTION12", "ACTION13" } },
            { CardType.Credit, new List<string> { "ACTION1", "ACTION2", "ACTION3", "ACTION4", "ACTION5", "ACTION6", "ACTION7", "ACTION8", "ACTION9" , "ACTION10", "ACTION11", "ACTION12", "ACTION13" } },
             { CardType.Debit, new List<string> { "ACTION1", "ACTION2", "ACTION3", "ACTION4", "ACTION6", "ACTION7", "ACTION8", "ACTION9" , "ACTION10", "ACTION11", "ACTION12", "ACTION13" } },

        };

        private static readonly Dictionary<CardStatus, List<string>> AllowedActionsByCardStatus = new()
        {
            { CardStatus.Ordered, new List<string> {  "ACTION3", "ACTION4", "ACTION5", "ACTION6", "ACTION7", "ACTION8", "ACTION9", "ACTION10",  "ACTION12", "ACTION13" } },
            { CardStatus.Inactive, new List<string> {  "ACTION2", "ACTION3", "ACTION4", "ACTION5", "ACTION6", "ACTION7", "ACTION8", "ACTION9", "ACTION10", "ACTION11", "ACTION12", "ACTION13" } },
            { CardStatus.Active, new List<string> { "ACTION1", "ACTION3", "ACTION4", "ACTION5", "ACTION6", "ACTION7", "ACTION8", "ACTION9", "ACTION10", "ACTION11", "ACTION12", "ACTION13" } },
            { CardStatus.Restricted, new List<string> { "ACTION3", "ACTION4", "ACTION5", "ACTION6", "ACTION9"} },
            { CardStatus.Blocked, new List<string> {  "ACTION3", "ACTION4", "ACTION5", "ACTION6", "ACTION7", "ACTION8", "ACTION9" } },
            { CardStatus.Expired, new List<string> { "ACTION3", "ACTION4", "ACTION5", "ACTION9" } },
            { CardStatus.Closed, new List<string> { "ACTION3", "ACTION4", "ACTION5",  "ACTION9" } }
          
        };

        public List<string> GetAllowedActionsByCardType(CardType cardType)
        {
            if (AllowedActionsByCardType.TryGetValue(cardType, out var actions))
            {
                return new List<string>(actions);
            }

            return new List<string>();
        }

        public List<string> GetAllowedActionsByCardStatus(CardStatus cardStatus)
        {
            if (AllowedActionsByCardStatus.TryGetValue(cardStatus, out var actions))
            {
                return new List<string>(actions);
            }

            return new List<string>();
        }

        public List<string> GetAllowedActions(CardType cardType, CardStatus cardStatus)
        {
            var actionsByType = GetAllowedActionsByCardType(cardType);
            var actionsByStatus = GetAllowedActionsByCardStatus(cardStatus);

            return actionsByType.Intersect(actionsByStatus).ToList();
        }
    }
}



