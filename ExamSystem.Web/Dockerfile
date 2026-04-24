
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["ExamSystem.Web/ExamSystem.Web.csproj", "ExamSystem.Web/"]
COPY ["ExamSystem.Application/ExamSystem.Application.csproj", "ExamSystem.Application/"]

RUN dotnet restore "ExamSystem.Web/ExamSystem.Web.csproj"

COPY . .
WORKDIR /src/ExamSystem.Web

RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "ExamSystem.Web.dll"]