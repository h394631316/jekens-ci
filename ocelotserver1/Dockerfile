FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["ocelotserver1/ocelotserver1.csproj", "ocelotserver1/"]
RUN dotnet restore "ocelotserver1/ocelotserver1.csproj"
COPY . .
WORKDIR "/src/ocelotserver1"
RUN dotnet build "ocelotserver1.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ocelotserver1.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ocelotserver1.dll"]