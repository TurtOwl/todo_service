FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY src/ ./src/
COPY *.sln .
RUN dotnet restore "src/Todo.Api/Todo.Api.csproj"
RUN dotnet restore "src/Todo.Worker/Todo.Worker.csproj"
RUN dotnet restore "src/Todo.TelegramBot/Todo.TelegramBot.csproj"

# API
FROM build AS api-build
WORKDIR "/src/src/Todo.Api"
RUN dotnet build "Todo.Api.csproj" -c Release -o /app/build
RUN dotnet publish "Todo.Api.csproj" -c Release -o /app/publish

FROM base AS api
WORKDIR /app
COPY --from=api-build /app/publish .
ENTRYPOINT ["dotnet", "Todo.Api.dll"]

# Worker
FROM build AS worker-build
WORKDIR "/src/src/Todo.Worker"
RUN dotnet build "Todo.Worker.csproj" -c Release -o /app/build
RUN dotnet publish "Todo.Worker.csproj" -c Release -o /app/publish

FROM base AS worker
WORKDIR /app
COPY --from=worker-build /app/publish .
ENTRYPOINT ["dotnet", "Todo.Worker.dll"]

# Bot
FROM build AS bot-build
WORKDIR "/src/src/Todo.TelegramBot"
RUN dotnet build "Todo.TelegramBot.csproj" -c Release -o /app/build
RUN dotnet publish "Todo.TelegramBot.csproj" -c Release -o /app/publish

FROM base AS bot
WORKDIR /app
COPY --from=bot-build /app/publish .
ENTRYPOINT ["dotnet", "Todo.TelegramBot.dll"]