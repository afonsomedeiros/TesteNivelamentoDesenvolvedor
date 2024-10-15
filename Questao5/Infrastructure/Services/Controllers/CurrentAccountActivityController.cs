using Microsoft.AspNetCore.Mvc;
using Questao5.Domain.Interfaces;
using Questao5.Application.Commands.Response;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Handlers;
using Questao5.Application.Queries.Requests;

namespace Questao5.Infrastructure.Services.Controllers;

[ApiController]
[Route("[controller]")]
public class CurrentAccountActivityController : ControllerBase
{
    private readonly CreateCurrentAccountActivityHandler _createCurrentAccountActivityHandler;
    private readonly CurrentAccountBalanceHandler _currentAccountBalanceHandler;

    public CurrentAccountActivityController(CreateCurrentAccountActivityHandler createCurrentAccountActivityHandler, CurrentAccountBalanceHandler currentAccountBalanceHandler)
    {
        _createCurrentAccountActivityHandler = createCurrentAccountActivityHandler;
        _currentAccountBalanceHandler = currentAccountBalanceHandler;
    }

    [HttpPost]
    [Route("/create")]
    public CreateCurrentAccountActivityResponse CreateActivity([FromBody] CreateCurrentAccountActivityCommand request){
        var response = _createCurrentAccountActivityHandler.Handle(request);
        Response.StatusCode = string.IsNullOrEmpty(response.Message) ? 200 : 400;
        return response;
    }

    [HttpGet]
    [Route("/balance")]
    public CurrentAccountBalanceResponse Balace([FromQuery] int AccountNumber)
    {
        var response = _currentAccountBalanceHandler.Handle(AccountNumber);
        Response.StatusCode = string.IsNullOrEmpty(response.Message) ? 200 : 400;
        return response;
    }

}
