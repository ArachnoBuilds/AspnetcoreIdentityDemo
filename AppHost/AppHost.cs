using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder
    .AddPostgres("postgres")
    .WithDataVolume()
    .WithPgAdmin(p => p.WithHostPort(5050));
postgres.AddDatabase("AspnetcoreIdentityDemo");

_ = builder
    .AddProject<Api>("api")
    .WithReference(postgres);

builder.Build().Run();
