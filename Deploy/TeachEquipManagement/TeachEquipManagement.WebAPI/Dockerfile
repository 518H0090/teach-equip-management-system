#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TeachEquipManagement.WebAPI/TeachEquipManagement.WebAPI.csproj", "TeachEquipManagement.WebAPI/"]
COPY ["TeachEquipManagement.BLL/TeachEquipManagement.BLL.csproj", "TeachEquipManagement.BLL/"]
COPY ["TeachEquipManagement.DAL/TeachEquipManagement.DAL.csproj", "TeachEquipManagement.DAL/"]
COPY ["TeachEquipManagement.Utilities/TeachEquipManagement.Utilities.csproj", "TeachEquipManagement.Utilities/"]
RUN dotnet restore "./TeachEquipManagement.WebAPI/TeachEquipManagement.WebAPI.csproj"
COPY . .
WORKDIR "/src/TeachEquipManagement.WebAPI"
RUN dotnet build "./TeachEquipManagement.WebAPI.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TeachEquipManagement.WebAPI.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TeachEquipManagement.WebAPI.dll"]