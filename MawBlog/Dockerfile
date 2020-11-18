#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
#FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.8-focal AS base
WORKDIR /app
#EXPOSE 80
#EXPOSE 443

#FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
#COPY ["MawBlog/MawBlog.csproj", "MawBlog/"]
COPY ["MawBlog.csproj", ""]
RUN dotnet restore "./MawBlog.csproj"
COPY . .
#WORKDIR "/src/MawBlog"
WORKDIR "/src/."
RUN dotnet build "MawBlog.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MawBlog.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet MawBlog.dll
#ENTRYPOINT ["dotnet", "MawBlog.dll"]