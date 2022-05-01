using System;

namespace LambdaAuthorizer;

public record ApiGatewayRequest {
    public string Type { get; init; }
    public string HttpMethod { get; init; }
    public Headers Headers { get; init; }
}

public record Headers {
    public String Username { get; init; }
    public String Password { get; init; }
}
