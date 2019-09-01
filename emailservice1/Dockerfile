FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["emailservice1/emailservice1.csproj", "emailservice1/"]
RUN dotnet restore "emailservice1/emailservice1.csproj"
COPY . .
WORKDIR "/src/emailservice1"
RUN dotnet build "emailservice1.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "emailservice1.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "emailservice1.dll"]