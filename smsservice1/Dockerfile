FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["smsservice1/smsservice1.csproj", "smsservice1/"]
RUN dotnet restore "smsservice1/smsservice1.csproj"
COPY . .
WORKDIR "/src/smsservice1"
RUN dotnet build "smsservice1.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "smsservice1.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "smsservice1.dll"]