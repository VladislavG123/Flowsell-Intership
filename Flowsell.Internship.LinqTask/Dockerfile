FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Flowsell.Internship.LinqTask/Flowsell.Internship.LinqTask.csproj", "Flowsell.Internship.LinqTask/"]
RUN dotnet restore "Flowsell.Internship.LinqTask/Flowsell.Internship.LinqTask.csproj"
COPY . .
WORKDIR "/src/Flowsell.Internship.LinqTask"
RUN dotnet build "Flowsell.Internship.LinqTask.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Flowsell.Internship.LinqTask.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Flowsell.Internship.LinqTask.dll"]
