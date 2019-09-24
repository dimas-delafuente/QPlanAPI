FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["QPlanAPI/QPlanAPI.csproj", "QPlanAPI/"]
COPY ["QPlanAPI.Core/QPlanAPI.Core.csproj", "QPlanAPI.Core/"]
COPY ["QPlanAPI.Domain/QPlanAPI.Domain.csproj", "QPlanAPI.Domain/"]
COPY ["QPlanAPI.Infrastructure/QPlanAPI.Infrastructure.csproj", "QPlanAPI.Infrastructure/"]
COPY ["QPlanAPI.DataAccess/QPlanAPI.DataAccess.csproj", "QPlanAPI.DataAccess/"]
RUN dotnet restore "QPlanAPI/QPlanAPI.csproj"
COPY . .
WORKDIR "/src/QPlanAPI"
RUN dotnet build "QPlanAPI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "QPlanAPI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "QPlanAPI.dll"]