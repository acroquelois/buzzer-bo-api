FROM microsoft/dotnet:2.2-aspnetcore-runtime

RUN apt-get update

WORKDIR /app

EXPOSE 80


FROM microsoft/dotnet:2.2-sdk AS build

WORKDIR /src

COPY . .

RUN dotnet restore "buzzerApi.csproj"

RUN dotnet build "buzzerApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "buzzerApi.csproj" -c Release -o /app

WORKDIR /app

ENTRYPOINT ["dotnet", "buzzerApi.dll"]