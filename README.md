# Azure DevOps CI/CD — .NET 8 Web API

![Azure DevOps](https://img.shields.io/badge/Azure%20DevOps-CI%2FCD-blue)
![.NET](https://img.shields.io/badge/.NET-8.0-purple)
![Docker](https://img.shields.io/badge/Docker-Containerized-blue)
![Azure](https://img.shields.io/badge/Azure-App%20Service-orange)

## 🏗️ Architecture

A production-grade .NET 8 Web API deployed to Azure App Service via a multi-stage Azure DevOps CI/CD pipeline.

GitHub Push → Azure DevOps Pipeline → Build & Test → Docker Push to ACR → Deploy to App Service

## 🛠️ Tech Stack

| Component | Technology |
|-----------|-----------|
| API Framework | .NET 8 Web API |
| Containerization | Docker (multi-stage build) |
| Container Registry | Azure Container Registry (ACR) |
| Hosting | Azure App Service (Linux) |
| CI/CD | Azure DevOps Pipelines (YAML) |
| Secret Management | Azure Key Vault |
| Monitoring | Azure Application Insights |
| Logging | Serilog |
| Testing | xUnit + FluentAssertions |
| Build Agent | Self-hosted Windows Agent |

## 🚀 CI/CD Pipeline

### Stage 1: Build and Test
- Fetches secrets from Azure Key Vault
- Runs unit tests with xUnit
- Builds Docker image using multi-stage build
- Pushes image to ACR with unique build ID tag

### Stage 2: Deploy
- Pulls secrets from Key Vault
- Deploys container to Azure App Service
- Configures App Insights connection string

## 📁 Project Structure

├── AzureDevOpsDemo.API/
│   ├── Controllers/        # API endpoints
│   ├── Middleware/         # Global exception handler
│   ├── Models/             # Request/Response models
│   ├── Services/           # Business logic
│   ├── Program.cs          # App entry point
│   └── appsettings.json    # Configuration
├── AzureDevOpsDemo.Tests/  # Unit tests
├── .azure-pipelines/
│   └── azure-pipelines.yml # CI/CD pipeline
└── Dockerfile              # Multi-stage build


## 🔗 Live Demo

- **Status:** Deployed successfully to Azure App Service
- **Health Check:** /health — returns Healthy
- **Products API:** /api/v1/products — returns product list
- **Note:** Azure resources cleaned up after project completion to avoid costs


## 🔒 Security

- Secrets stored in Azure Key Vault (never in code)
- Docker container runs as non-root user
- Service Principal with minimum required permissions
- No credentials in pipeline YAML

## 📊 API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/v1/products | Get all products |
| GET | /api/v1/products/{id} | Get product by ID |
| POST | /api/v1/products | Create new product |
| GET | /health | Health check |

## 🏛️ Azure Resources

| Resource | Name |
|----------|------|
| Resource Group | rg-dotnet-cicd-dev |
| Container Registry | acrdevopsdemo2025 |
| App Service | app-dotnet-cicd-dev |
| Key Vault | kv-dotnet-cicd-dev |
| App Insights | appi-dotnet-cicd-dev |

## 👨‍💻 Author

**Surya** — Azure DevOps and Data Engineer
- Portfolio: https://stvzg6u3gi2zmok.z30.web.core.windows.net/
- GitHub: https://github.com/Surya-rv05
