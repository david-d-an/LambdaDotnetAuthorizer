namespace LambdaAuthorizer;

public record PolicyDocPackage {
    public string PrincipalId { get; init; }
    public PolicyDocument PolicyDocument { get; init; }

    public PolicyDocPackage(string username, string resource, AuthType auth) {
        this.PrincipalId = username;
        this.PolicyDocument = new PolicyDocument(resource, auth);            
    }
}

public record PolicyDocument {
    public string Version { get; init; }
    public Statement[] Statement { get; init; }

    public PolicyDocument(string resource, AuthType auth) {
        this.Version = "2012-10-17";
        this.Statement = new Statement[] {
            new Statement(resource, auth)
        };
    }
}

public record Statement {
    public string Action { get; init; }
    public string[] Resource { get; init; }
    public string Effect { get; init; }

    public Statement(string resource, AuthType auth) {
        this.Action = "execute-api:Invoke";
        this.Resource = new string[] { resource };
        this.Effect = auth.ToString();
    }            
}
   
