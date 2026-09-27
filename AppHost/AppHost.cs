using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder
    .AddPostgres("postgres")
    .WithHostPort(5432)
    .WithDataVolume()
    .WithPgAdmin(p => p.WithHostPort(5050));
var database = postgres.AddDatabase("AspnetcoreIdentityDemo");

_ = builder
    .AddProject<Api>("api")
    .WithReference(database);

builder.Build().Run();
