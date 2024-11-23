using ACME.BankingPlatform.API.Accounts.Application.Commands.Services;
using ACME.BankingPlatform.API.Accounts.Application.Commands.Validators;
using ACME.BankingPlatform.API.Accounts.Application.Queries.Services;
using ACME.BankingPlatform.API.Accounts.Domain.Repositories;
using ACME.BankingPlatform.API.Accounts.Infrastructure.Persistence.EFC.Repositories;
using ACME.BankingPlatform.API.Accounts.Interfaces.ACL.Services;
using ACME.BankingPlatform.API.Clients.Application.Commands;
using ACME.BankingPlatform.API.Clients.Application.Commands.Services;
using ACME.BankingPlatform.API.Clients.Application.Commands.Validators;
using ACME.BankingPlatform.API.Clients.Application.Queries.Services;
using ACME.BankingPlatform.API.Clients.Domain.Repositories;
using ACME.BankingPlatform.API.Clients.Infrastructure.Persistence.EFC.Repositories;
using ACME.BankingPlatform.API.Shared.Domain.Repositories;
using ACME.BankingPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using ACME.BankingPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using ACME.BankingPlatform.API.Shared.Interfaces.ASP.Configuration;
using ACME.BankingPlatform.API.Transactions.Application.Commands.Services;
using ACME.BankingPlatform.API.Transactions.Application.Commands.Validators;
using ACME.BankingPlatform.API.Transactions.Application.OutboundServices.ACL;
using ACME.BankingPlatform.API.Transactions.Domain.Repositories;
using ACME.BankingPlatform.API.Transactions.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
//using MySql.Data.MySqlClient;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

// Add Database Connection
var connectionString = Environment.GetEnvironmentVariable("BANKING_POSTGRESQL_CONNECTION") ?? "";

// Configure Database Context and Logging Levels

builder.Services.AddDbContext<AppDbContext>(
    options =>
    {
        if (builder.Environment.IsDevelopment())
            options.UseNpgsql(connectionString)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        else if (builder.Environment.IsProduction())
            options.UseNpgsql(connectionString)
                .LogTo(Console.WriteLine, LogLevel.Error)
                .EnableDetailedErrors();
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// MediatR
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(Program).Assembly));

// Configure Lowercase URLs
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Shared Bounded Context Injection Configuration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Clients Bounded Context Injection Configuration
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<RegisterClientValidator>();
builder.Services.AddScoped<ClientCommandService>();
builder.Services.AddScoped<ClientQueryService>();

// Accounts Bounded Context Injection Configuration
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<OpenAccountValidator>();
builder.Services.AddScoped<AccountsContextFacade>();
builder.Services.AddScoped<AccountCommandService>();
builder.Services.AddScoped<AccountQueryService>();

// Transactions Bounded Context Injection Configuration
builder.Services.AddScoped(typeof(ITransactionRepository<>), typeof(TransactionRepository<>));
builder.Services.AddScoped<StartDepositValidator>();
builder.Services.AddScoped<StartWithdrawalValidator>();
builder.Services.AddScoped<StartTransferValidator>();
builder.Services.AddScoped<ExternalAccountsContextService>();
builder.Services.AddScoped<TransactionCommandService>();

builder.Host.UseNServiceBus(context =>
{
    var endpointName = "ACME.BankingPlatform.Messages";
    var endpointConfiguration = new EndpointConfiguration(endpointName);
    endpointConfiguration.EnableInstallers();
    endpointConfiguration.AuditProcessedMessagesTo("audit");
    endpointConfiguration.UseSerialization<SystemJsonSerializer>();
    var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
    transport.ConnectionString(Environment.GetEnvironmentVariable("BANKING_RABBITMQ_CONNECTION") ?? "");
    transport.UseConventionalRoutingTopology(QueueType.Quorum);
    var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
    persistence.SqlDialect<SqlDialect.PostgreSql>();
    persistence.TablePrefix("nsb_");
    persistence.ConnectionBuilder(
        connectionBuilder: () => new NpgsqlConnection(connectionString)
    );
    endpointConfiguration.EnableOutbox();
    var routing = transport.Routing();
    routing.RouteToEndpoint(
        assembly: typeof(RegisterClient).Assembly,
        destination: endpointName
    );
    return endpointConfiguration;
});
    
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
