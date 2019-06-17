using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RemoteSettingsProvider.Controllers
{
    [Route("api/setting")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        private IHttpContextAccessor HttpContextAccessor { get; }
        private ICommandHandler<ClientHasAccessCommand, ClientHasAccessCommandResult> ClientHasAccess { get; }
        private ICommandHandler<SettingGetByNameCommand, SettingGetByNameCommandResult> GetByName { get; }
        private ICommandHandler<SettingGetAllCollectionCommand, SettingGetAllCollectionCommandResult> GetAllCollection { get; }

        public SettingController(
            IHttpContextAccessor httpContextAccessor,
            ICommandHandler<ClientHasAccessCommand, ClientHasAccessCommandResult> clientHasAccess,
            ICommandHandler<SettingGetByNameCommand, SettingGetByNameCommandResult> getByName,
            ICommandHandler<SettingGetAllCollectionCommand, SettingGetAllCollectionCommandResult> getAllCollection
        )
        {
            HttpContextAccessor = httpContextAccessor;
            ClientHasAccess = clientHasAccess;
            GetByName = getByName;
            GetAllCollection = getAllCollection;
        }

        [HttpPost]
        [Route("by-name")]
        public async Task<ActionResult<SettingGetByNameCommandResult>> GetByNameAsync(SettingGetByNameCommand command)
        {
            // move to auth attribute
            if (await HasAccessAsync(command.ClientId) == false)
            {
                return NoContent();
            }

            return await GetByName.ExecuteAsync(command);
        }

        [HttpPost]
        [Route("all")]
        public async Task<ActionResult<SettingGetAllCollectionCommandResult>> GetAllCollectionAsync(SettingGetAllCollectionCommand command)
        {
            // move to auth attribute
            if (await HasAccessAsync(command.ClientId) == false)
            {
                return NoContent();
            }

            return await GetAllCollection.ExecuteAsync(command);
        }

        private async Task<bool> HasAccessAsync(string clientId)
        {
            var result = await ClientHasAccess.ExecuteAsync(command =>
            {
                command.ClientId = clientId;
                command.RemoteAddress = HttpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            }).ConfigureAwait(false);
            return result.HasAccess;
        }

    }
}