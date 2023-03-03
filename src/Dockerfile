FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Scribble.Authors.Contracts.Proto/Scribble.Authors.Contracts.Proto.csproj", "Scribble.Authors.Contracts.Proto/"]
COPY ["Scribble.Authors.Web/Scribble.Authors.Web.csproj", "Scribble.Authors.Web/"]
COPY ["Scribble.Authors.Infrastructure/Scribble.Authors.Infrastructure.csproj", "Scribble.Authors.Infrastructure/"]
COPY ["Scribble.Authors.Models/Scribble.Authors.Models.csproj", "Scribble.Authors.Models/"]
RUN dotnet restore "Scribble.Authors.Web/Scribble.Authors.Web.csproj"
COPY . .
WORKDIR "/src/Scribble.Authors.Web"
RUN dotnet build "Scribble.Authors.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Scribble.Authors.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Scribble.Authors.Web.dll"]
