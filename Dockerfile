# Build and publish the CLI in an SDK image, then run it in a smaller runtime image.
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS builder

WORKDIR /src

# Copy project files first so dependency restoration can be cached.
COPY ["LIS.Cli/LIS.Cli.csproj", "LIS.Cli/"]
COPY ["LIS.Tests/LIS.Tests.csproj", "LIS.Tests/"]

# Restore the application dependencies.
RUN dotnet restore "LIS.Cli/LIS.Cli.csproj"

# Copy source code after restoration so code changes do not invalidate the restore layer.
COPY LIS.Cli/ LIS.Cli/
COPY LIS.Tests/ LIS.Tests/

# Compile the application in Release mode.
RUN dotnet build "LIS.Cli/LIS.Cli.csproj" -c Release -o /app/build

# Produce the files required by the runtime image.
RUN dotnet publish "LIS.Cli/LIS.Cli.csproj" -c Release -o /app/publish

# Run the published application without the SDK tooling.
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
LABEL description="Longest Increasing Sequence solver using an O(n) contiguous-run algorithm"
LABEL version="1.0"
