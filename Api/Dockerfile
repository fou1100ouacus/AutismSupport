# 1. بيئة التشغيل المستهدفة بدوت نت 10
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app

# 2. بيئة البناء والـ SDK لدوت نت 10
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG configuration=Release
WORKDIR /src

# 3. نسخ ملفات الـ .csproj لكل الطبقات لعمل Restore صحيح ومنع مشاكل الـ Multi-layers
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

# 5. عمل نشر (Publish) مع إجبار استخدام فراموورك net10.0 لمنع البحث عن إصدارات قديمة
FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Api.csproj" -c $configuration -f net10.0 -o /app/publish /p:UseAppHost=false

# 6. المرحلة النهائية لتشغيل التطبيق
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]