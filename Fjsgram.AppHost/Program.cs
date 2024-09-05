var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var apiService = builder.AddProject<Projects.FjsGram_Server>("server")
    .WithReference(cache)
;

builder.Build().Run();
