# FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
# WORKDIR /app
# EXPOSE 7089

# ENV ASPNETCORE_URLS=http://+:7089

# USER app
# FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# ARG configuration=Release
# WORKDIR /src
# COPY ["Api/Api.csproj", "Api/"]
# RUN dotnet restore "Api/Api.csproj"
# COPY . .
# WORKDIR "/src/Api"
# RUN dotnet build "Api.csproj" -c $configuration -o /app/build

# FROM build AS publish
# ARG configuration=Release
# RUN dotnet publish "Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "Api.dll"]d

  
# 1. بيئة التشغيل المستهدفة بدوت نت 10
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
# نترك تحديد الـ Port ديناميكياً لـ Railway بناءً على التعديل الذي قمنا به في Program.cs

# 2. بيئة البناء والـ SDK لدوت نت 10
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG configuration=Release
WORKDIR /src

# 3. نسخ ملفات الـ .csproj لكل الطبقات لعمل Restore صحيح
COPY ["Api/Api.csproj", "Api/"]
COPY ["Core/Core.csproj", "Core/"]
COPY ["Data/Data.csproj", "Data/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["Service/Service.csproj", "Service/"]

# عمل الـ Restore من الملف الرئيسي للـ API
RUN dotnet restore "Api/Api.csproj"

# 4. نسخ باقي كود المشروع بالكامل وبنائه
COPY . .
WORKDIR "/src/Api"
RUN dotnet build "Api.csproj" -c $configuration -o /app/build

# 5. عمل نشر (Publish) للملفات التنفيذية
FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

# 6. المرحلة النهائية لتشغيل التطبيق
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]