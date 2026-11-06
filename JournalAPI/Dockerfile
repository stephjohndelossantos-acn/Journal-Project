FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["JournalAPI/JournalAPI.csproj", "JournalAPI/"]
COPY ["Journal.Data/Journal.Data.csproj", "Journal.Data/"]
RUN dotnet restore "JournalAPI/JournalAPI.csproj"
COPY . .
WORKDIR "/src/JournalAPI"
RUN dotnet publish "JournalAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "JournalAPI.dll"]