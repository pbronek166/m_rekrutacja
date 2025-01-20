namespace UserCardActionsService.Controllers
{

    using Microsoft.AspNetCore.Mvc;
    using Application.Services;
    using global::UserCardActionsService.Model;

    namespace UserCardActionsService.Web.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class UserCardActionsController : ControllerBase
        {
            private readonly CardService _cardService;

            public UserCardActionsController(CardService cardService)
            {
                _cardService = cardService;
            }

            [HttpGet]
            public async Task<IActionResult> GetAllowedActions(AllowActionIn? allowActionIn)
            {
                if (allowActionIn == null)
                {
                    return NotFound();
                }
                if (string.IsNullOrWhiteSpace(allowActionIn.userId) || string.IsNullOrWhiteSpace(allowActionIn.cardNumber))
                {
                    return NotFound();
                }
                var cardDetails = await _cardService.GetCardDetails(allowActionIn.userId, allowActionIn.cardNumber);
                if (cardDetails == null)
                {
                    return NotFound();
                }

                var allowedActions = _cardService.GetAllowedActions(cardDetails);
                return Ok(allowedActions);
            }
        }
    }

}
