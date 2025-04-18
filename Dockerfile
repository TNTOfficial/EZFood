# Use .NET 9 SDK for build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /app

# Copy csproj and restore as distinct layers
#COPY EZFood.Server/EZFood.Server.csproj ./
#RUN dotnet restore

# Copy the rest of the project files
COPY . .

# Publish the app for release

RUN apt update -y
RUN apt install wget -y
RUN wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb

RUN dpkg -i packages-microsoft-prod.deb
RUN apt update -y
RUN apt install -y dotnet-sdk-9.0


#RUN dotnet new tool-manifest && dotnet tool install dotnet-ef
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"
RUN dotnet ef database update --project EZFood.Server/EZFood.Server.csproj
RUN dotnet publish -c Release -o out

# Use .NET 9 runtime for production
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime


RUN apt update -y
RUN apt install wget -y
RUN wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb

RUN dpkg -i packages-microsoft-prod.deb
RUN apt update -y
RUN apt install -y dotnet-sdk-9.0

RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

WORKDIR /app
RUN mkdir -p /app/uploads
# Copy the published output from build image
COPY --from=build /app/out ./
#RUN dotnet ef database update --project EZFood.Server/EZFood.Server.csproj
# Ensure EF tools are available (if needed — usually bundled with your app)
# Run migrations and then start the app
ENTRYPOINT ["sh", "-c", "dotnet EZFood.Server.dll"]
#ENTRYPOINT ["sh", "-c", "dotnet EZFood.Server.dll"]
