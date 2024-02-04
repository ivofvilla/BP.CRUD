using Microsoft.AspNetCore.Mvc;
using MediatR;
using BP.CRUD.Domain.Commands.Client.Create;
using BP.CRUD.Domain.Commands.Client.Update;
using BP.CRUD.Domain.Commands.Client.Delete;
using BP.CRUD.Domain.Commands.Client.DeleteLogic;
using BP.CRUD.Domain.Queries.Client.Gets;
using BP.CRUD.Domain.Queries.Client.Get;
using BP.CRUD.Domain.Models;
using Newtonsoft.Json;

namespace BP.CRUD.Controllers
{
    public class ClientController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("api/Client/Create")]
        public async Task<IActionResult> CreateClientAsync([FromBody] CreateClientCommand client, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(client, cancellationToken);

            if (result != null)
            {
                return Created("", "Cliente cadastrado!");
            }

            return BadRequest("Erro ao cadastrar o cliente!");
        }

        [HttpGet]
        [Route("api/Client/Gets")]
        public async Task<ActionResult<GetClientsResult>?> GetClientsAsync(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetClientsQuery(), cancellationToken);
            var jsonSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            return Ok(JsonConvert.SerializeObject(result, jsonSettings));
        }
        
        [HttpGet]
        [Route("api/Client/Get")]
        public async Task<ActionResult<Client>> GetClientAsync([FromQuery] GetClientQuery query, CancellationToken cancellationToken)
        {
            var clients = await _mediator.Send(query, cancellationToken);
        
            if (clients == null)
            {
                return NotFound();
            }

            var jsonSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            return Ok(JsonConvert.SerializeObject(clients, jsonSettings));
        }
        
        [HttpPut]
        [Route("api/Client/Update/{id}")]
        public async Task<IActionResult> UpdateClientAsync(Guid id, [FromBody] UpdateClientCommand command, CancellationToken cancellationToken)
        {
            command.SetId(id);
        
            if (await _mediator.Send(command, cancellationToken))
            {
                return Ok("Cliente atualizado com sucesso!");
            }
        
            return BadRequest("Ocorreu um erro!");
        }
        
        [HttpDelete]
        [Route("api/Client/Delete/{id}")]
        public async Task<IActionResult> DeleteClientAsync(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteClientCommand { Id = id };
            await _mediator.Send(command, cancellationToken);
            return Ok("Cliente apagado com sucesso!");
        }
        
        [HttpDelete]
        [Route("api/Client/DeleteLogic/{id}")]
        public async Task<IActionResult> DeleteClientLogicAsync(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteClientLogicCommand { Id = id };
            await _mediator.Send(command, cancellationToken);
            return Ok("Cliente apagado com sucesso!");
        }
    }
}
