FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app
EXPOSE 80 
EXPOSE 443

# Copy csproj and restore as distinct layers
COPY ./src/PostAPI/*.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY ./src/PostAPI ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "PostAPI.dll"]


# @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
# @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

# FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
# WORKDIR /app
# EXPOSE 80 443

# FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
# WORKDIR /src
# COPY ["PostAPI/PostAPI.csproj", "PostAPI/"]
# RUN dotnet restore "PostAPI/PostAPI.csproj"
# COPY . .
# WORKDIR "/src/PostAPI"
# RUN dotnet build "PostAPI.csproj" -c Release -o /app/build

# FROM build AS publish
# RUN dotnet publish "PostAPI.csproj" -c Release -o /app/publish

# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "PostAPI.dll"]