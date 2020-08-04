using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Todo.Api.Controllers.Task.GetById;
using Todo.Api.Controllers.Task.RegisterNew;
using Todo.Tests.Fixture;
using Todo.Tests.TestCollections.Names;

using Xunit;

namespace Todo.Tests
{
    [Collection(CollectionNames.IntegrationTests)]
    public class TodoControllerSpecs : IDisposable
    {
        private const string ApiRoot = "api/todo";

        private const string ContentType = "application/json";

        private readonly IntegrationTestsFixture fixture;

        private readonly HttpClient client;

        public TodoControllerSpecs(IntegrationTestsFixture fixture)
        {
            this.fixture = fixture;
            client = fixture.CreateClient();
        }

        public void Dispose() => fixture.ClearDatabase();
    
        [Fact]
        public async Task TodoController_OnPostingValidRequest_SavesNewTask()
        {
            var requestBody = JsonSerializer.Serialize(new RegisterNewTaskCommand("TestTask", "This is a test task description"));
            var responseContent = await GetPostResponse<RegisterNewTaskResponse>(ApiRoot, requestBody);

            var taskDto = await GetTaskDto(responseContent.TaskId);
            Assert.NotNull(taskDto);
        }

        [Fact]
        public async Task TodoController_OnPostingInvalidRequest_ReturnsBadRequest()
        {
            var requestBody = JsonSerializer.Serialize(new RegisterNewTaskCommand(string.Empty, null));
            var responseContent = await client.PostAsync(ApiRoot, new StringContent(requestBody, Encoding.UTF8, ContentType));

            Assert.Equal(HttpStatusCode.BadRequest, responseContent.StatusCode);
        }

        private async Task<TResponse> GetPostResponse<TResponse>(string url, string request)
        {
            var response = await client.PostAsync(url, new StringContent(request, Encoding.UTF8, ContentType));
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TResponse>(responseString);
        }

        private async Task<GetTaskByIdResponse> GetTaskDto(Guid taskId)
        {
            var savedTaskResponse = await client.GetAsync($"{ApiRoot}?taskId={taskId}");
            savedTaskResponse.EnsureSuccessStatusCode();
            return JsonSerializer.Deserialize<GetTaskByIdResponse>(await savedTaskResponse.Content.ReadAsStringAsync());
        }
    }
}
