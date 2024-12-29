using Models;
using Workshop;
using System.Web.Http;
using Microsoft.Ajax.Utilities;
using System;


namespace WebAPI.Controllers
{
    /// <summary>
    /// Controlador the Account
    /// </summary>
    [Route("api/Account/{action}")]
    //[Authorize]
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TransactionController : ApiController
    {
        public TransactionService _transactionService;

        public TransactionController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost]
        public IHttpActionResult Transfer([FromBody] TransferRequest request)
        {
            try
            {
                // Validar la solicitud
                if (request == null)
                {
                    return Content(System.Net.HttpStatusCode.BadRequest, new TransferResponse
                    {
                        Success = false,
                        Message = "La solicitud no puede ser nula."
                    });
                }

                if (request.SenderId == request.RecipientId)
                {
                    return Content(System.Net.HttpStatusCode.BadRequest, new TransferResponse
                    {
                        Success = false,
                        Message = "El remitente y el destinatario no pueden ser la misma cuenta."
                    });
                }

                // Simular cuentas (en un entorno real, se obtendrían de una base de datos)
                var sender = new Account { Id = request.SenderId, Balance = 1000 }; // Mock sender
                var recipient = new Account { Id = request.RecipientId, Balance = 500 }; // Mock recipient

                // Realizar la transferencia
                var result = _transactionService.Transfer(sender, recipient, request.Amount, request.Description);

                if (result)
                {
                    // Devolver éxito con estado 200
                    return Ok(new TransferResponse
                    {
                        Success = true,
                        Message = "Transferencia exitosa.",
                        SenderBalance = sender.Balance,
                        RecipientBalance = recipient.Balance
                    });
                }

                // Saldo insuficiente (400 Bad Request)
                return Content(System.Net.HttpStatusCode.BadRequest, new TransferResponse
                {
                    Success = false,
                    Message = "Saldo insuficiente.",
                    SenderBalance = sender.Balance
                });
            }
            catch (Exception ex)
            {
                // Devolver error interno con estado 500
                return Content(System.Net.HttpStatusCode.InternalServerError, new TransferResponse
                {
                    Success = false,
                    Message = "Ocurrió un error interno al procesar la transferencia.",
                    ErrorMessage = ex.Message
                });
            }
        }
    }
}

