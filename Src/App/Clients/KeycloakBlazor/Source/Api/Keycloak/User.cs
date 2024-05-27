namespace KeycloakBlazor.Source.Api.Keycloak;

public sealed record User(
    Guid Id,
    long CreatedTimestamp,
    string Username,
    bool Enabled,
    bool Totp,
    bool EmailVerified,
    string FirstName,
    string LastName,
    List<object> DisableableCredentialTypes,
    List<object> RequiredActions,
    int NotBefore,
    Access Access
    );

public sealed record Access(
    bool ManageGroupMembership,
    bool View,
    bool MapRoles,
    bool Impersonate,
    bool Manage
    );