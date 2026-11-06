FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["Journal/Journal.csproj", "Journal/"]
COPY ["Journal.Data/Journal.Data.csproj", "Journal.Data/"]
RUN dotnet restore "Journal/Journal.csproj"
COPY . .
WORKDIR "/src/Journal"
RUN dotnet publish "Journal.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Journal.dll"]