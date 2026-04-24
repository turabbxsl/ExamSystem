FROM mcr.microsoft.com/dotnet/sdk:10.0
WORKDIR /src

# 1. İlk növbədə proyekt fayllarını kopyala (Dependency-ləri restore etmək üçün)
COPY ["ExamSystem.Web/ExamSystem.Web.csproj", "ExamSystem.Web/"]
COPY ["ExamSystem.Application/ExamSystem.Application.csproj", "ExamSystem.Application/"]

RUN dotnet restore "ExamSystem.Web/ExamSystem.Web.csproj"

COPY . .

WORKDIR /src/ExamSystem.Web
ENTRYPOINT ["dotnet", "watch", "run", "--no-hot-reload", "--urls", "http://0.0.0.0:8080"]