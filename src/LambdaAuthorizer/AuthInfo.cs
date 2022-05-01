using System;

namespace LambdaAuthorizer;

public record ApiGatewayRequest {
    public string Type { get; init; }
    public string HttpMethod { get; init; }
    public Headers Headers { get; init; }
}

public record Headers {
    public string Username { get; init; }
    public string Password { get; init; }
}
