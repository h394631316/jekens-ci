FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src

COPY . .

RUN dotnet restore "WebApplication3.csproj"

WORKDIR "/src/"

RUN dotnet build "WebApplication3.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "WebApplication3.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebApplication3.dll"]