var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddSqlServer("db").WithDataVolume().AddDatabase("primary");

var cache = builder.AddRedis("cache");

var apiService = builder.AddProject<Projects.FjsGram_Server>("server")
    .WithReference(cache)
    .WithReference(db)
;

builder.Build().Run();
