#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ConsoleAppLora/ConsoleAppLora.csproj", "ConsoleAppLora/"]
COPY ["ALoRa.LibraryV3-main/ALoRa.Library.csproj", "ALoRa.LibraryV3-main/"]
COPY ["CayenneParser/CayenneParser.csproj", "CayenneParser/"]
COPY ["Elsys.Decoder.Test/Elsys.Decoder.Test.csproj", "Elsys.Decoder.Test/"]
COPY ["Elsys.Decoder/Elsys.Decoder.csproj", "Elsys.Decoder/"]
RUN dotnet restore "ConsoleAppLora/ConsoleAppLora.csproj"
COPY . .
WORKDIR "/src/ConsoleAppLora"
RUN dotnet build "ConsoleAppLora.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ConsoleAppLora.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConsoleAppLora.dll"]