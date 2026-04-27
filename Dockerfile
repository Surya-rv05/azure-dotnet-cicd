# ── Stage 1: Build ────────────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files first (layer caching optimization)
COPY AzureDevOpsDemo.sln .
COPY AzureDevOpsDemo.API/AzureDevOpsDemo.API.csproj AzureDevOpsDemo.API/
COPY AzureDevOpsDemo.Tests/AzureDevOpsDemo.Tests.csproj AzureDevOpsDemo.Tests/

# Restore dependencies (cached if .csproj files unchanged)
RUN dotnet restore

# Copy everything else
COPY . .

# Run tests
RUN dotnet test AzureDevOpsDemo.Tests/AzureDevOpsDemo.Tests.csproj --no-restore --verbosity minimal

# Publish the API
RUN dotnet publish AzureDevOpsDemo.API/AzureDevOpsDemo.API.csproj \
    --no-restore \
    -c Release \
    -o /app/publish

# ── Stage 2: Runtime ──────────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Create non-root user (security best practice)
RUN addgroup --system appgroup && adduser --system --ingroup appgroup appuser

# Copy published output from build stage
COPY --from=build /app/publish .

# Set ownership
RUN chown -R appuser:appgroup /app

# Switch to non-root user
USER appuser

# Expose port
EXPOSE 8080

# Set environment variables
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "AzureDevOpsDemo.API.dll"]