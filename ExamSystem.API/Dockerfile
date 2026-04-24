FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet publish "ExamSystem.API/ExamSystem.API.csproj" -c Debug -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

# Bütün lazımi paketləri bura əlavə etdik
RUN apt-get update && apt-get install -y procps curl unzip

# vsdbg-ni yüklə və xəta varsa, build-i dayandır
RUN curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v latest -l /remote_debugger

COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "ExamSystem.API.dll"]