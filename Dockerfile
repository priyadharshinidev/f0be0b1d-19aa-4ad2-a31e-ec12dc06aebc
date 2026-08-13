# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS builder

WORKDIR /src

# Copy project files
COPY ["LIS.Cli/LIS.Cli.csproj", "LIS.Cli/"]
COPY ["LIS.Tests/LIS.Tests.csproj", "LIS.Tests/"]

# Restore dependencies
RUN dotnet restore "LIS.Cli/LIS.Cli.csproj"

# Copy source code
COPY LIS.Cli/ LIS.Cli/
COPY LIS.Tests/ LIS.Tests/

# Build
RUN dotnet build "LIS.Cli/LIS.Cli.csproj" -c Release -o /app/build

# Publish
RUN dotnet publish "LIS.Cli/LIS.Cli.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/runtime:8.0

WORKDIR /app

# Copy published application from builder
COPY --from=builder /app/publish .

# Set entry point
ENTRYPOINT ["dotnet", "LIS.Cli.dll"]

# Default command (can be overridden with docker run)
CMD [""]

# Labels
LABEL maintainer="LIS Implementation"
LABEL description="Longest Increasing Subsequence solver using O(n log n) algorithm"
LABEL version="1.0"
