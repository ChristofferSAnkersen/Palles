FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Palles/API.csproj", "Palles/"]
COPY ["Library/Entities.csproj", "Library/"]
RUN dotnet restore "Palles/API.csproj"
COPY . .
WORKDIR "/src/Palles"
RUN dotnet build "API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "API.dll"]