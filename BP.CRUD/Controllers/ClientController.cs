using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using BP.CRUD.Domain.Commands.Client.Create;
using BP.CRUD.Domain.Commands.Client.Update;
using BP.CRUD.Domain.Commands.Client.Delete;
using BP.CRUD.Domain.Commands.Client.DeleteLogic;
using BP.CRUD.Domain.Queries.Client.Gets;
using BP.CRUD.Domain.Queries.Client.Get;
using BP.CRUD.Domain.Models;

namespace BP.CRUD.Controllers
{
    public class ClientController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ClientAsync([FromBody] CreateClientCommand client, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(client, cancellationToken);
            if (result != null)
            {
                return Created("", "Cliente cadastrado!");
            }

            return BadRequest("Cliente não encontrado!");
        }

        [HttpGet]
        public async Task<ActionResult<GetClientsResult>?> GetClients(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetClientsQuery(), cancellationToken);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<Client>> GetClient(GetClientQuery query, CancellationToken cancellationToken)
        {
            var client = await _mediator.Send(query, cancellationToken);

            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Client(Guid id, [FromBody] UpdateClientCommand command, CancellationToken cancellationToken)
        {
            command.SetId(id);

            if (await _mediator.Send(command, cancellationToken))
            {
                return Ok("Cliente atualizado com sucesso!");
            }

            return BadRequest("Ocorreu um erro!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Client(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteClientCommand { Id = id };
            await _mediator.Send(command, cancellationToken);
            return Ok("Cliente apagado com sucesso!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ClientLogic(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteClientLogicCommand { Id = id };
            await _mediator.Send(command, cancellationToken);
            return Ok("Cliente apagado com sucesso!");
        }
    }
}
