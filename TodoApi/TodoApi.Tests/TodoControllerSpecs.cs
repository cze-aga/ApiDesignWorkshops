using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Todo.Api.Controllers.Task.DeleteTask;
using Todo.Api.Controllers.Task.GetById;
using Todo.Api.Controllers.Task.RegisterNew;
using Todo.Api.Controllers.Task.UpdateTask;
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
            RegisterNewTaskResponse responseContent = await RegisterTask();

            var taskDto = await GetTaskDto(responseContent.TaskId);
            Assert.NotNull(taskDto);
        }

        private async Task<RegisterNewTaskResponse> RegisterTask()
        {
            var requestBody = JsonSerializer.Serialize(new RegisterNewTaskCommand("TestTask", "This is a test task description"));
            var responseContent = await GetPostResponse<RegisterNewTaskResponse>(ApiRoot, requestBody);
            return responseContent;
        }

        [Fact]
        public async Task TodoController_OnPostingInvalidRequest_ReturnsBadRequest()
        {
            var requestBody = JsonSerializer.Serialize(new RegisterNewTaskCommand(string.Empty, null));
            var responseContent = await client.PostAsync(ApiRoot, new StringContent(requestBody, Encoding.UTF8, ContentType));

            Assert.Equal(HttpStatusCode.BadRequest, responseContent.StatusCode);
        }

        [Fact]
        private async Task TodoControler_OnPuttingValidRequest_UpdatesEntity()
        {
            const string NewTaskName = "Hope it will work";

            //Arrange
            RegisterNewTaskResponse responseContent = await RegisterTask();

            //Act
            var putRequestBody = JsonSerializer.Serialize(new UpdateTaskCommand(responseContent.TaskId, NewTaskName, string.Empty));
            var response = await client.PutAsync(ApiRoot, new StringContent(putRequestBody,Encoding.UTF8, ContentType));

            //Assert
            response.EnsureSuccessStatusCode();
            var taskDto = await GetTaskDto(responseContent.TaskId);

            Assert.Equal(NewTaskName, taskDto.Name);
            Assert.Equal(string.Empty, taskDto.Description);
        }

        [Fact]
        public async Task TodoController_OnPuttingInvalidRequest_ReturnsBadRequest()
        {
            // Arrange
            // Act
            var putRequestBody = JsonSerializer.Serialize(new UpdateTaskCommand(Guid.Empty, string.Empty, null));
            var response = await client.PutAsync(ApiRoot, new StringContent(putRequestBody, Encoding.UTF8, ContentType));

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task TodoController_OnDeletingRequest_DeletesEntity()
        {
            //Arrange
            var responseContent = await RegisterTask();
            
            //Act
            var response = await client.DeleteAsync($"{ApiRoot}?taskId={responseContent.TaskId}");


            //Assert
            response.EnsureSuccessStatusCode();
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
