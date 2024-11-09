##FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
##USER $APP_UID
##WORKDIR /app
##EXPOSE 8080
##EXPOSE 8081
##
##FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
##ARG BUILD_CONFIGURATION=Release
##WORKDIR /src
##COPY ["MapachesLectoresBackend/MapachesLectoresBackend.csproj", "MapachesLectoresBackend/"]
##RUN dotnet restore "MapachesLectoresBackend/MapachesLectoresBackend.csproj"
##COPY . .
##WORKDIR "/src/MapachesLectoresBackend"
##RUN dotnet build "MapachesLectoresBackend.csproj" -c $BUILD_CONFIGURATION -o /app/build
##
##FROM build AS publish
##ARG BUILD_CONFIGURATION=Release
##RUN dotnet publish "MapachesLectoresBackend.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
##
##FROM base AS final
##WORKDIR /app
##COPY --from=publish /app/publish .
##ENTRYPOINT ["dotnet", "MapachesLectoresBackend.dll"]
#
## Dockerfile.dev
#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS dev
#WORKDIR /app
#COPY *.sln ./
#COPY ./MapachesLectoresBackend/*.csproj ./MapachesLectoresBackend/
#RUN dotnet restore
#COPY . .
#EXPOSE 4321
#ENV ASPNETCORE_ENVIRONMENT=Development
#CMD ["dotnet", "watch", "run", "--project", "MapachesLectoresBackend/MapachesLectoresBackend.csproj"]




# Imagen base
FROM mcr.microsoft.com/dotnet/sdk:8.0 
WORKDIR /app

# Copia archivos de proyecto y restaura dependencias
COPY *.csproj .

RUN dotnet restore 

# Copia el resto del código
COPY . .

# Exponer el puerto de la aplicación
EXPOSE 4321

# Configurar entorno de desarrollo
ENV ASPNETCORE_ENVIRONMENT=Development

# Comando de inicio de la aplicación
#CMD ["dotnet", "watch", "run", "--no-restore", "--project", "MapachesLectoresBackend/MapachesLectoresBackend.csproj"]
CMD ["dotnet", "run", "--project", "./MapachesLectoresBackend.csproj"]
