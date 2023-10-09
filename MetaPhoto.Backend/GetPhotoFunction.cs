namespace MetaPhoto.Backend;

public class GetPhotoFunction
{
    private readonly ILogger<GetPhotoFunction> _logger;
    private readonly PhotoService _photoService = new();

    public GetPhotoFunction(ILogger<GetPhotoFunction> log)
    {
        _logger = log;
    }

    [FunctionName("GetPhotoFunction")]
    [OpenApiOperation("Run", new[] {"Meta"})]
    [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
    [OpenApiParameter("id", In = ParameterLocation.Query, Required = true, Type = typeof(int),
        Description = "The **Id** parameter")]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "text/plain", typeof(string), Description = "The OK response")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]
        HttpRequest req)
    {
        try
        {
            var id = int.Parse(req.Query["id"]);
            var photo = await _photoService.GetPhotoAsync(id);
            var json = JsonConvert.SerializeObject(photo);
            return new OkObjectResult(json);
        }
        catch (Exception exception)
        {
            var errorMessage = $"GetPhotoFunction: Error:{exception}";
            _logger.LogCritical(errorMessage);
            return new OkObjectResult(errorMessage);
        }
    }
}