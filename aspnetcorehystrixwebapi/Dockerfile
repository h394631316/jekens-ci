FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["aspnetcorehystrixwebapi/aspnetcorehystrixwebapi.csproj", "aspnetcorehystrixwebapi/"]
RUN dotnet restore "aspnetcorehystrixwebapi/aspnetcorehystrixwebapi.csproj"
COPY . .
WORKDIR "/src/aspnetcorehystrixwebapi"
RUN dotnet build "aspnetcorehystrixwebapi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "aspnetcorehystrixwebapi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "aspnetcorehystrixwebapi.dll"]