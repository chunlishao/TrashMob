
namespace TrashMobJobs
{
    using System.Text.Json;
    using System.Threading.Tasks;
    using System.Net;
    using Microsoft.Azure.Functions.Worker;
    using Microsoft.Azure.Functions.Worker.Http;
    using Microsoft.Extensions.Logging;
    using TrashMob.Poco;
    using TrashMob.Shared.Managers.Interfaces;
    using System;

    public class DeleteUser
    {
        private readonly ILogger logger;
        private readonly IActiveDirectoryManager activeDirectoryManager;

        public DeleteUser(ILoggerFactory loggerFactory, IActiveDirectoryManager activeDirectoryManager)
        {
            logger = loggerFactory.CreateLogger<SignUpValidate>();
            this.activeDirectoryManager = activeDirectoryManager;
        }

        [Function("DeleteUser")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "DeleteUser/{id:guid}")] HttpRequestData req, Guid id)
        {
            logger.LogInformation("C# HTTP trigger function processed a request.");     

            try
            {
                var deleteResponse = await activeDirectoryManager.DeleteUserAsync(id);

                HttpResponseData response;
                switch (deleteResponse.action)
                {
                    case "Failed":
                        // Yes, really. It needs an Ok when failed...
                        response = req.CreateResponse(HttpStatusCode.InternalServerError);
                        var blockingResponse = deleteResponse as ActiveDirectoryBlockingResponse;
                        response.WriteString(JsonSerializer.Serialize(blockingResponse));
                        break;
                    default:
                        response = req.CreateResponse(HttpStatusCode.OK);
                        response.WriteString(JsonSerializer.Serialize(deleteResponse));
                        break;
                }

                return response;
            }
            catch (Exception ex)
            {
                // Yes, really. It needs an Ok when failed...
                var response = req.CreateResponse(HttpStatusCode.InternalServerError);
                var blockingResponse = new ActiveDirectoryBlockingResponse
                {
                    action = "Failed",
                    version = "1.0.0",
                    userMessage = $"User failed to delete."
                };

                response.WriteString(JsonSerializer.Serialize(blockingResponse));
                logger.LogError(ex, $"User {id} failed to delete.");
                return response;
            }
        }
    }
}
