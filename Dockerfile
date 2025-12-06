FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY ["Primera_Evaluación.csproj", "./"]
RUN dotnet restore "Primera_Evaluación.csproj"

COPY . .
RUN dotnet publish "Primera_Evaluación.csproj" -c Release -o /app/publish /p:UseAppHost=false


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .


EXPOSE 8080


ENTRYPOINT ["dotnet", "Primera_Evaluación.dll"]